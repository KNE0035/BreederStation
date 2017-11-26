using BreederStationDataLayer.Database;
using BreederStationDataLayer.Orm.Dto;
using BreederStationDataLayer.Orm.SelectCriteria;
using System.Collections.Generic;
using System.Data.Common;

namespace BreederStationDataLayer.Orm.Dao
{
    public abstract class AnimalGateway
    {
        private IDatabaseService db;

        public AnimalGateway(IDatabaseService databaseService)
        {
            this.db = databaseService;
        }

        public int Delete(int addressId)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetDeleteSql());
            PrepareDeleteCommand(command, addressId);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public int Update(Animal animal)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetUpdateSql());
            PrepareInsertUpdateCommand(command, animal);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }
        public IList<Animal> Select(AnimalCriteria criteria)
        {
            db.Connect();

            DbCommand command = db.CreateCommand(GetSelectSql());
            applyCriteria(criteria, command);
            DbDataReader reader = db.Select(command);

            IList<Animal> animals = Read(reader);
            reader.Close();
            db.Close();
            return animals;
        }

        public Animal Select(int animalId)
        {
            db.Connect();

            DbCommand command = db.CreateCommand(GetSelectIdSql());
            PrepareSelectIdCommand(command, animalId);
            DbDataReader reader = db.Select(command);

            IList<Animal> animals = Read(reader);
            Animal animal = new Animal();
            if (animals.Count != 0)
            {
                animal = animals[0];
            }

            reader.Close();
            db.Close();

            return animal;
        }

        public int Insert(Animal animal)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetInsertSql());
            PrepareInsertUpdateCommand(command, animal);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        protected abstract void PrepareInsertUpdateCommand(DbCommand command, Animal animal);
        protected abstract void PrepareSelectIdCommand(DbCommand command, int animalId);
        protected abstract void PrepareDeleteCommand(DbCommand command, int animalId);


        protected abstract string GetInsertSql();
        protected abstract string GetDeleteSql();
        protected abstract string GetUpdateSql();
        protected abstract string GetSelectSql();
        protected abstract string GetSelectIdSql();
        protected abstract IList<Animal> Read(DbDataReader reader);
        protected abstract void applyCriteria(AnimalCriteria criteria, DbCommand command);
    }
}
