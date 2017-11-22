using BreederStationDataLayer.Database;
using BreederStationDataLayer.Orm.Dto;
using System.Collections.Generic;
using System.Data.Common;

namespace BreederStationDataLayer.Orm.Dao
{
    public abstract class EventGateway
    {
        private IDatabaseService db;

        public EventGateway(IDatabaseService databaseService)
        {
            this.db = databaseService;
        }

        public int Delete(int animalEventID)
        {
            db.Connect();

            DbCommand command = db.CreateCommand(GetDeleteSql());
            PrepareEventIdCommand(command, animalEventID);
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }

        public int Update(Event animalEvent)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetUpdateSql());
            PrepareEventInsertUpdateCommand(command, animalEvent);
            PrepareEventIdCommand(command, animalEvent.Id);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }


        public Event Select(int animalEventID)
        {
            db.Connect();

            DbCommand command = db.CreateCommand(GetSelectIdSql());
            PrepareEventIdCommand(command, animalEventID);
            DbDataReader reader = db.Select(command);

            IList<Event> animalEvents = Read(reader);
            Event animalEvent = new Event();
            if (animalEvents.Count != 0)
            {
                animalEvent = animalEvents[0];
            }

            reader.Close();
            db.Close();

            return animalEvent;
        }

        public IList<Event> Select(bool closeConnection = true)
        {
            db.Connect();

            DbCommand command = db.CreateCommand(GetSelectSql());
            DbDataReader reader = db.Select(command);

            IList<Event> animalEvents = Read(reader);
            reader.Close();

            if (closeConnection)
            {
                db.Close();
            }
            
            return animalEvents;
        }

        public int Insert(Event animalEvent)
        {
            db.Connect();
            db.BeginTransaction();
            DbCommand insertEventcommand = db.CreateCommand(GetInsertEventSql());
            PrepareEventInsertUpdateCommand(insertEventcommand, animalEvent);

            int ret = db.ExecuteNonQuery(insertEventcommand);

            IList<Event> events = Select(false);

            int eventId = events[events.Count - 1].Id;

            foreach(int animalId in animalEvent.AnimalIds)
            {
                DbCommand intserAnimalEventcommand = db.CreateCommand(GetInsertAnimalEventSql());
                PrepareAnimalEventCommand(intserAnimalEventcommand, eventId, animalId);
                ret += db.ExecuteNonQuery(intserAnimalEventcommand);
            }
            db.EndTransaction();
            return ret;
        }

        protected abstract void PrepareEventInsertUpdateCommand(DbCommand command, Event animalEvent);
        protected abstract void PrepareEventIdCommand(DbCommand command, int eventId);
        protected abstract void PrepareAnimalEventCommand(DbCommand command, int eventId, int animalId);

        protected abstract string GetInsertEventSql();
        protected abstract string GetInsertAnimalEventSql();
        protected abstract string GetDeleteSql();
        protected abstract string GetUpdateSql();
        protected abstract string GetSelectSql();
        protected abstract string GetSelectIdSql();
        protected abstract IList<Event> Read(DbDataReader reader);
    }
}
