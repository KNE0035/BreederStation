using BreederStationDataLayer.Database;
using BreederStationDataLayer.Orm.Dao;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;

namespace BreederStationDataLayer.Orm.Dto
{
    public class SqlServerEventGateway : EventGateway
    {
        public static string TABLE_NAME = "Event";

        private static string SQL_INSERT_EVENT = "INSERT INTO EVENT (DESCRIPTION, START_DATE, END_DATE, BREEDER_ID) VALUES(@p_description, @p_start_date, @p_end_date, @p_breeder_id)";

        private static string SQL_INSERT_ANIMAL_EVENT = "INSERT INTO ANIMAL_EVENT (EVENT_ID, ANIMAL_ID) VALUES (@p_event_id, @p_animal_id)";

        private static string SQL_SELECT = @"SELECT  e.ID, e.DESCRIPTION, e.START_DATE, e.END_DATE, b.person_id, p.login, b.ANIMAL_GROUP_ID, ae.ANIMAL_ID, a.name
                                             FROM EVENT e 
                                             LEFT JOIN ANIMAL_EVENT ae ON e.id = ae.EVENT_ID
                                             LEFT JOIN ANIMAL a ON a.id = ae.ANIMAL_ID
                                             JOIN BREEDER b ON b.person_id = e.BREEDER_ID
                                             JOIN PERSON p ON b.person_id = p.id
                                             ORDER BY e.ID";

        private static string SQL_SELECT_ID = @"SELECT  e.ID, e.DESCRIPTION, e.START_DATE, e.END_DATE, b.person_id, p.login, b.ANIMAL_GROUP_ID, ae.ANIMAL_ID, a.name
                                                   FROM EVENT e 
                                                   LEFT JOIN ANIMAL_EVENT ae ON e.id = ae.EVENT_ID
                                                   LEFT JOIN ANIMAL a ON a.id = ae.ANIMAL_ID
                                                   JOIN BREEDER b ON b.person_id = e.BREEDER_ID
                                                   JOIN PERSON p ON b.person_id = p.id
                                                   WHERE e.ID=:p_id";

        private static string SQL_UPDATE = @"UPDATE EVENT SET DESCRIPTION=@p_description, START_DATE=@p_start_date, END_DATE=@p_end_date, BREEDER_ID=@p_breeder_id
                                             WHERE ID=@p_id";

        private static string SQL_DELETE = "DELETE FROM EVENT WHERE ID=@p_id";

        public SqlServerEventGateway(IDatabaseService db) : base(db)
        {
        }

        protected override void PrepareEventInsertUpdateCommand(DbCommand command, Event animalEvent)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.Parameters.Add("@p_description", SqlDbType.VarChar).Value = animalEvent.Description;
            sqlCommand.Parameters.Add("@p_breeder_id", SqlDbType.Int).Value = animalEvent.Breeder.Id;
            sqlCommand.Parameters.Add("@p_start_date", SqlDbType.Date).Value = animalEvent.StartDate;
            sqlCommand.Parameters.Add("@p_end_date", SqlDbType.Date).Value = animalEvent.EndDate == null ? DBNull.Value : (object)animalEvent.EndDate;
        }

        protected override void PrepareEventIdCommand(DbCommand command, int eventId)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.Parameters.Add("@p_id", SqlDbType.Int).Value = eventId;
        }

        protected override void PrepareAnimalEventCommand(DbCommand command, int eventId, int animalId)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.Parameters.Add("@p_event_id", SqlDbType.Int).Value = eventId;
            sqlCommand.Parameters.Add("@p_animal_id", SqlDbType.Int).Value = animalId;
        }

        protected override IList<Event> Read(DbDataReader reader)
        {
            SqlDataReader sqlReader = (SqlDataReader)reader;
            bool goNext = false;

            const int ANIMAL_ID_COLLUMN_POSSITION = 7;
            const int EVENT_ID_COLLUMN_POSSITION = 0;
            IList<Event> animalEvents = new List<Event>();

            while (goNext || sqlReader.Read())
            {
                int i = -1;
                Event animalEvent = new Event();
                animalEvent.Id = sqlReader.GetInt32(++i);
                animalEvent.Description = sqlReader.GetString(++i);
                animalEvent.StartDate = sqlReader.GetDateTime(++i);

                if (!sqlReader.IsDBNull(++i))
                {
                    animalEvent.EndDate = sqlReader.GetDateTime(i);
                }

                animalEvent.Breeder = new Breeder();
                animalEvent.Breeder.Id = sqlReader.GetInt32(++i);
                animalEvent.Breeder.Login = sqlReader.GetString(++i);
                animalEvent.Breeder.AnimalGroup = new AnimalGroup();
                animalEvent.Breeder.AnimalGroup.Id = sqlReader.GetInt32(++i);

                //adding all animal ids for every animalEvent
                int lastEventId = animalEvent.Id;

                animalEvent.animals = new List<Animal>();

                if (!sqlReader.IsDBNull(ANIMAL_ID_COLLUMN_POSSITION))
                {
                    do
                    {
                        Animal animal = new Animal();
                        animal.Id = sqlReader.GetInt32(ANIMAL_ID_COLLUMN_POSSITION);
                        animal.Name = sqlReader.GetString(ANIMAL_ID_COLLUMN_POSSITION + 1);
                        animalEvent.animals.Add(animal);

                        goNext = sqlReader.Read();
                    } while (goNext && lastEventId == sqlReader.GetInt32(EVENT_ID_COLLUMN_POSSITION));
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
