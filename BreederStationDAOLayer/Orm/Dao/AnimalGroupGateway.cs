using BreederStationDataLayer.Database;
using BreederStationDataLayer.Orm.Dto;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreederStationDataLayer.Orm.Dao
{
    public abstract class AnimalGroupGateway
    {
        private IDatabaseService db;

        public AnimalGroupGateway(IDatabaseService databaseService)
        {
            this.db = databaseService;
        }

        public int Delete(int animalGroupId)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetDeleteSql());
            PrepareGroupIdCommand(command, animalGroupId);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public int Update(AnimalGroup animalGroup)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetUpdateSql());
            PrepareInsertUpdateCommand(command, animalGroup);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public IList<AnimalGroup> Select(bool allInfo)
        {
            db.Connect();
            DbCommand command;
            if (allInfo)
            {
                command = db.CreateCommand(GetSelectAllInfoSql());
            }
            else
            {
                command = db.CreateCommand(GetSelectSql());
            }

            DbDataReader reader = db.Select(command);

            IList<AnimalGroup> animalGroups = Read(reader, allInfo);
            reader.Close();
            db.Close();
            return animalGroups;
        }

        public AnimalGroup Select(int animalGroupId)
        {
            db.Connect();

            DbCommand command = db.CreateCommand(GetSelectAllInfoByIdSql());
            PrepareGroupIdCommand(command, animalGroupId);
            DbDataReader reader = db.Select(command);

            IList<AnimalGroup> animalGroups = Read(reader, true);

            AnimalGroup animalGroup = new AnimalGroup();
            if (animalGroups.Count != 0)
            {
                animalGroup = animalGroups[0];
            }

            reader.Close();
            db.Close();

            return animalGroup;
        }

        public int Insert(AnimalGroup animalGroup)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetInsertSql());
            PrepareInsertUpdateCommand(command, animalGroup);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        protected abstract void PrepareInsertUpdateCommand(DbCommand command, AnimalGroup animalGroup);
        protected abstract void PrepareGroupIdCommand(DbCommand command, int animalGroupId);
        protected abstract string GetInsertSql();

        protected abstract string GetSelectAllInfoSql();
        protected abstract string GetSelectAllInfoByIdSql();
        protected abstract string GetDeleteSql();
        protected abstract string GetUpdateSql();
        protected abstract string GetSelectSql();

        protected abstract IList<AnimalGroup> Read(DbDataReader reader, bool allInfo);
    }
}
