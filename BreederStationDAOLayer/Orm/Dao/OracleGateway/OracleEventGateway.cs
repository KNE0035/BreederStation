using BreederStationDataLayer.Database;
using BreederStationDataLayer.Orm.Dao;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;

namespace BreederStationDataLayer.Orm.Dto
{
    public class OracleEventGateway : EventGateway
    {
        public static string TABLE_NAME = "Event";

        private static string SQL_INSERT_EVENT = "INSERT INTO EVENT (DESCRIPTION, START_DATE, END_DATE, BREEDER_ID) VALUES(:p_description, :p_start_date, :p_end_date, :p_breeder_id)";

        private static string SQL_INSERT_ANIMAL_EVENT = "INSERT INTO ANIMAL_EVENT (EVENT_ID, ANIMAL_ID) VALUES (:p_event_id, :p_animal_id)";

        private static string SQL_SELECT = @"SELECT  e.ID, e.DESCRIPTION, e.START_DATE, e.END_DATE, b.person_id, b.ANIMAL_GROUP_ID, ae.ANIMAL_ID 
                                             FROM EVENT e 
                                             LEFT JOIN ANIMAL_EVENT ae ON e.id = ae.EVENT_ID
                                             JOIN BREEDER b ON b.person_id = e.BREEDER_ID
                                             ORDER BY e.ID";

        private static string SQL_SELECT_ID = @"SELECT  e.ID, e.DESCRIPTION, e.START_DATE, e.END_DATE, b.person_id, b.ANIMAL_GROUP_ID, ae.ANIMAL_ID 
                                                   FROM EVENT e 
                                                   LEFT JOIN ANIMAL_EVENT ae ON e.id = ae.EVENT_ID
                                                   JOIN BREEDER b ON b.person_id = e.BREEDER_ID
                                                   WHERE e.ID=:p_id";

        private static string SQL_UPDATE = @"UPDATE EVENT SET DESCRIPTION=:p_description, START_DATE=:p_start_date, END_DATE=:p_end_date, BREEDER_ID=:p_breeder_id
                                             WHERE ID=:p_id";

        private static string SQL_DELETE = "DELETE FROM EVENT WHERE ID=:p_id";

        public OracleEventGateway(IDatabaseService db) : base(db)
        {
        }

        protected override void PrepareEventInsertUpdateCommand(DbCommand command, Event animalEvent)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_description", OracleDbType.Varchar2).Value = animalEvent.Description;
            oracleCommand.Parameters.Add("p_breeder_id", OracleDbType.Int32).Value = animalEvent.Breeder.Id;
            oracleCommand.Parameters.Add("p_start_date", OracleDbType.Date).Value = animalEvent.StartDate;
            oracleCommand.Parameters.Add("p_end_date", OracleDbType.Date).Value = animalEvent.EndDate == null ? DBNull.Value : (object)animalEvent.EndDate;
        }

        protected override void PrepareEventIdCommand(DbCommand command, int eventId)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_id", OracleDbType.Int32).Value = eventId;
        }

        protected override void PrepareAnimalEventCommand(DbCommand command, int eventId, int animalId)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_event_id", OracleDbType.Int32).Value = eventId;
            oracleCommand.Parameters.Add("p_animal_id", OracleDbType.Int32).Value = animalId;
        }

        protected override IList<Event> Read(DbDataReader reader)
        {
            OracleDataReader oracleReader = (OracleDataReader)reader;

            bool goNext = false;

            const int ANIMAL_ID_COLLUMN_POSSITION = 6;
            const int EVENT_ID_COLLUMN_POSSITION = 0;
            IList<Event> animalEvents = new List<Event>();

            while (goNext || oracleReader.Read())
            {
                int i = -1;
                Event animalEvent = new Event();
                animalEvent.Id = oracleReader.GetInt32(++i);
                animalEvent.Description = oracleReader.GetString(++i);
                animalEvent.StartDate = oracleReader.GetDateTime(++i);

                if (!oracleReader.IsDBNull(++i))
                {
                    animalEvent.EndDate = oracleReader.GetDateTime(i);
                }

                animalEvent.Breeder = new Breeder();
                animalEvent.Breeder.Id = oracleReader.GetInt32(++i);
                animalEvent.Breeder.AnimalGroup = new AnimalGroup();
                animalEvent.Breeder.AnimalGroup.Id = oracleReader.GetInt32(++i);

                //adding all animal ids for every animalEvent
                int lastEventId = animalEvent.Id;

                animalEvent.AnimalIds = new List<int>();

                if (!oracleReader.IsDBNull(ANIMAL_ID_COLLUMN_POSSITION))
                {
                    do
                    {
                        int animalId = oracleReader.GetInt32(ANIMAL_ID_COLLUMN_POSSITION);
                        animalEvent.AnimalIds.Add(animalId);
                        goNext = oracleReader.Read();
                    } while (goNext && lastEventId == oracleReader.GetInt32(EVENT_ID_COLLUMN_POSSITION));
                } else
                {
                    goNext = false;
                }
                    

                animalEvents.Add(animalEvent);
            }
            return animalEvents;
        }

        protected override string GetInsertEventSql()
        {
            return SQL_INSERT_EVENT;
        }

        protected override string GetInsertAnimalEventSql()
        {
            return SQL_INSERT_ANIMAL_EVENT;
        }

        protected override string GetDeleteSql()
        {
            return SQL_DELETE;
        }

        protected override string GetUpdateSql()
        {
            return SQL_UPDATE;
        }

        protected override string GetSelectSql()
        {
            return SQL_SELECT;
        }

        protected override string GetSelectIdSql()
        {
            return SQL_SELECT_ID;
        }
    }
}
