using BreederStationDataLayer.Database;
using BreederStationDataLayer.Orm.Dto;
using System.Collections.Generic;
using System.Data.Common;


namespace BreederStationDataLayer.Orm.Dao
{
    public abstract class RoleGateway
    {
        private IDatabaseService db;

        public RoleGateway(IDatabaseService databaseService)
        {
            this.db = databaseService;
        }

        public int Delete(int roleId)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetDeleteSql());
            PrepareIdCommand(command, roleId);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public int Update(Role role)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetUpdateSql());
            PrepareInsertUpdateCommand(command, role);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public IList<Role> Select()
        {
            db.Connect();

            DbCommand command = db.CreateCommand(GetSelectSql());
            DbDataReader reader = db.Select(command);

            IList<Role> roles = Read(reader);
            reader.Close();
            db.Close();
            return roles;
        }
        public int Insert(Role role)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetInsertSql());
            PrepareInsertUpdateCommand(command, role);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        protected abstract void PrepareInsertUpdateCommand(DbCommand command, Role role);
        protected abstract void PrepareIdCommand(DbCommand command, int roleId);

        protected abstract string GetInsertSql();
        protected abstract string GetDeleteSql();
        protected abstract string GetUpdateSql();
        protected abstract string GetSelectSql();

        protected abstract IList<Role> Read(DbDataReader reader);
    }
}
