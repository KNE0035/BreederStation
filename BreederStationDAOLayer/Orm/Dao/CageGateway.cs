using BreederStationDataLayer.Database;
using BreederStationDataLayer.Orm.Dto;
using System.Collections.Generic;
using System.Data.Common;

namespace BreederStationDataLayer.Orm.Dao
{
    public abstract class CageGateway
    {
        private IDatabaseService db;

        public CageGateway(IDatabaseService databaseService)
        {
            this.db = databaseService;
        }

        public int Delete(int cageId)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetDeleteSql());
            PrepareDeleteCommand(command, cageId);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public int Update(Cage cage)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetUpdateSql());
            PrepareCommand(command, cage);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }
        public IList<Cage> Select()
        {
            db.Connect();

            DbCommand command = db.CreateCommand(GetSelectSql());
            DbDataReader reader = db.Select(command);

            IList<Cage> cages = Read(reader);
            reader.Close();
            db.Close();
            return cages;
        }
        public int Insert(Cage cage)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetInsertSql());
            PrepareCommand(command, cage);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        protected abstract void PrepareCommand(DbCommand command, Cage cage);
        protected abstract void PrepareDeleteCommand(DbCommand command, int cageId);

        protected abstract string GetInsertSql();
        protected abstract string GetDeleteSql();
        protected abstract string GetUpdateSql();
        protected abstract string GetSelectSql();
        protected abstract IList<Cage> Read(DbDataReader reader);
    }
}
