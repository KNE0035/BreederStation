using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Diagnostics;
using BreederStationDataLayer.Orm.Dto;
using BreederStationDataLayer.Database;
using System.Data.Common;

namespace BreederStationDataLayer.Orm.Dao
{
    public class OracleAddressGateway : AddressGateway
    {

        public static string TABLE_NAME = "Address";

        private static string SQL_INSERT = "INSERT INTO ADDRESS (STREET, CITY, ZIPCODE) VALUES (:p_street, :p_city, :p_zipcode)";

        private static string SQL_SELECT = "SELECT ID, STREET, CITY, ZIPCODE FROM ADDRESS ORDER BY ID";

        private static string SQL_UPDATE = "UPDATE ADDRESS SET STREET=:p_street, CITY=:p_city, ZIPCODE=:p_zipcode  WHERE id=:p_id";

        private static string SQL_DELETE_ID = "DELETE FROM ADDRESS WHERE id=:p_id";

        public OracleAddressGateway(IDatabaseService databaseService) : base(databaseService)
        {
        }

        protected override IList<Address> Read(DbDataReader reader)
        {
            OracleDataReader oracleDataReader = (OracleDataReader)reader;
            IList<Address> addresses = new List<Address>();

            while (oracleDataReader.Read())
            {
                int i = -1;
                Address address = new Address();
                address.Id = oracleDataReader.GetInt32(++i);

                address.Street = oracleDataReader.GetString(++i);
                address.City = oracleDataReader.GetString(++i);
                address.Zipcode = oracleDataReader.GetString(++i);

                addresses.Add(address);
            }
            return addresses;
        }

        protected override string GetInsertSql()
        {
            return SQL_INSERT;
        }

        protected override string GetDeleteSql()
        {
            return SQL_DELETE_ID;
        }

        protected override string GetUpdateSql()
        {
            return SQL_UPDATE;
        }

        protected override string GetSelectSql()
        {
            return SQL_SELECT;
        }

        protected override void PrepareCommand(DbCommand command, Address address)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_street", OracleDbType.Varchar2).Value = address.Street;
            oracleCommand.Parameters.Add("p_city", OracleDbType.Varchar2).Value = address.City;
            oracleCommand.Parameters.Add("p_zipcode", OracleDbType.Varchar2).Value = address.Zipcode;
            oracleCommand.Parameters.Add("p_id", OracleDbType.Int32).Value = address.Id;

            foreach (OracleParameter p in command.Parameters)
            {
                Debug.WriteLine(string.Format("{0} = {1}", p.ParameterName, p.Value));
            }
        }
    }
}
