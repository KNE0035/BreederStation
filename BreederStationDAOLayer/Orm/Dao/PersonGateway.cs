using BreederStationDataLayer.Orm.Dto;
using BreederStationDataLayer.Orm.SelectCriteria;
using System.Collections.Generic;
using System.Data.Common;
using BreederStationDataLayer.Database;
using System;

namespace BreederStationDataLayer.Orm.Dao
{
    public abstract class PersonGateway
    {
        private IDatabaseService db;

        public PersonGateway(IDatabaseService databaseService)
        {
            this.db = databaseService;
        }

        public int Delete(string login)
        {
            db.Connect();

            DbCommand command = db.CreateCommand(GetDeletePersonSql());

            PrepareDeletePersonCommand(command, login);
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }

        public int Update(Person person)
        {
            db.Connect();
            DbCommand updatePersonCommand  = db.CreateCommand(GetUpdatePersonSql());
            DbCommand updateBreederCommand = db.CreateCommand(GetUpdateBreederSql());
            DbCommand updateCleanerCommand = db.CreateCommand(GetUpdateCleanerSql());
            PrepareUpdateInsertPersonCommand(updatePersonCommand, person);
            
            db.BeginTransaction();

            int ret = db.ExecuteNonQuery(updatePersonCommand);

            if (person.GetType() == typeof(Breeder))
            {
                PrepareUpdateInsertBreederCommand(updateBreederCommand, person);
                ret += db.ExecuteNonQuery(updateBreederCommand);
            }
            else if (person.GetType() == typeof(Cleaner))
            {
                PrepareUpdateInsertCleanerCommand(updateCleanerCommand, person);
                ret += db.ExecuteNonQuery(updateCleanerCommand);
            }

            db.EndTransaction();
            db.Close();
            return ret;
        }

        public IList<Person> Select(PersonCriteria criteria, bool closeAtEnd = true)
        {
            db.Connect();

            DbCommand command = db.CreateCommand(GetSelectSql());
            ApplyCriteriaAndOrder(criteria, command);

            DbDataReader reader = db.Select(command);
            IList<Person> persons = Read(reader);
            reader.Close();

            if (closeAtEnd) {
                db.Close();
            }
            return persons;
        }

        public Person Select(string login, bool closeAtEnd = true)
        {
            db.Connect();

            DbCommand command = db.CreateCommand(GetSelectLoginSql());
            PrepareSelectLoginCommand(command, login);
            DbDataReader reader = db.Select(command);

            IList<Person> persons = Read(reader);
            Person person = null;
            if (persons.Count != 0)
            {
                person = persons[0];
            }

            reader.Close();

            if (closeAtEnd)
            {
                db.Close();
            }
            return person;
        }

        public int Insert(Person person)
        {
            db.Connect();
            DbCommand personInsertcommand = db.CreateCommand(GetInsertPersonSql());
            db.BeginTransaction();
            PrepareUpdateInsertPersonCommand(personInsertcommand, person);
            int ret = db.ExecuteNonQuery(personInsertcommand);

            Person createdPerson = Select(person.Login, false);
            person.Id = createdPerson.Id;

            if (person.GetType() == typeof(Breeder))
            {
                DbCommand breederInsertcommand = db.CreateCommand(GetInsertBreederSql());
                PrepareUpdateInsertBreederCommand(breederInsertcommand, person);
                ret = db.ExecuteNonQuery(breederInsertcommand);

            } else if(person.GetType() == typeof(Cleaner))
            {
                DbCommand cleanerInsertcommand = db.CreateCommand(GetInsertCleanerSql());
                PrepareUpdateInsertCleanerCommand(cleanerInsertcommand, person);
                ret = db.ExecuteNonQuery(cleanerInsertcommand);
            }

            db.EndTransaction();
            return ret;
        }

        public int InsertResponsibilityForCage(int idCleaner, int idCage)
        {
            db.Connect();

            DbCommand command = db.CreateCommand(GetInsertCageResponsibilitySql());

            PrepareResponsibilityForCageCommand(command, idCleaner, idCage);

            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }

        public int DeleteResponsibilityForCage(int idCleaner, int idCage)
        {
            db.Connect();

            DbCommand command = db.CreateCommand(GetDeleteCageResponsibilitySql());

            PrepareResponsibilityForCageCommand(command, idCleaner, idCage);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        protected abstract void PrepareUpdateInsertPersonCommand(DbCommand command, Person person);
        protected abstract void PrepareUpdateInsertBreederCommand(DbCommand command, Person person);
        protected abstract void PrepareUpdateInsertCleanerCommand(DbCommand command, Person person);
        protected abstract void PrepareResponsibilityForCageCommand(DbCommand command, int idCleaner, int idCage);
        protected abstract void PrepareDeletePersonCommand(DbCommand command, string login);
        protected abstract void PrepareSelectLoginCommand(DbCommand command, string login);


        protected abstract String GetUpdatePersonSql();
        protected abstract String GetUpdateBreederSql();
        protected abstract String GetUpdateCleanerSql();
        protected abstract String GetDeletePersonSql();
        protected abstract String GetInsertCageResponsibilitySql();
        protected abstract String GetDeleteCageResponsibilitySql();
        protected abstract String GetSelectLoginSql();
        protected abstract String GetSelectSql();
        protected abstract string GetInsertPersonSql();
        protected abstract string GetInsertBreederSql();
        protected abstract string GetInsertCleanerSql();

        protected abstract IList<Person> Read(DbDataReader reader);

        protected abstract void ApplyCriteriaAndOrder(PersonCriteria criteria, DbCommand command);
    }
}
