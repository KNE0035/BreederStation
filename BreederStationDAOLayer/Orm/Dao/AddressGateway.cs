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
    public abstract class AddressGateway
    {
        private IDatabaseService db;

        public AddressGateway(IDatabaseService databaseService)
        {
            this.db = databaseService;
        }

        public int Delete(int addressId)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetDeleteSql());
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public int Update(Address address)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetUpdateSql());
            PrepareCommand(command, address);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }
        public IList<Address> Select()
        {
            db.Connect();

            DbCommand command = db.CreateCommand(GetSelectSql());
            DbDataReader reader = db.Select(command);

            IList<Address> addresses = Read(reader);
            reader.Close();
            db.Close();
            return addresses;
        }
        public int Insert(Address address)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetInsertSql());
            PrepareCommand(command, address);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        protected abstract void PrepareCommand(DbCommand command, Address address);

        protected abstract string GetInsertSql();
        protected abstract string GetDeleteSql();
        protected abstract string GetUpdateSql();
        protected abstract string GetSelectSql();
        protected abstract IList<Address> Read(DbDataReader reader);
    }
}
