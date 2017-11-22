using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Diagnostics;
using BreederStationDataLayer.Orm.Dto;
using BreederStationDataLayer.Database;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace BreederStationDataLayer.Orm.Dao
{
    public class SqlServerAddressGateway : AddressGateway
    {

        public static string TABLE_NAME = "Address";

        private static string SQL_INSERT = "INSERT INTO ADDRESS (STREET, CITY, ZIPCODE) VALUES (@p_street, @p_city, @p_zipcode)";

        private static string SQL_SELECT = "SELECT ID, STREET, CITY, ZIPCODE FROM ADDRESS ORDER BY ID";

        private static string SQL_UPDATE = "UPDATE ADDRESS SET STREET=@p_street, CITY=@p_city, ZIPCODE=@p_zipcode  WHERE id=@p_id";

        private static string SQL_DELETE_ID = "DELETE FROM ADDRESS WHERE id=@p_id";

        public SqlServerAddressGateway(IDatabaseService databaseService) : base(databaseService)
        {
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
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.Parameters.Add("@p_street", SqlDbType.VarChar).Value = address.Street;
            sqlCommand.Parameters.Add("@p_city", SqlDbType.VarChar).Value = address.City;
            sqlCommand.Parameters.Add("@p_zipcode", SqlDbType.VarChar).Value = address.Zipcode;
            sqlCommand.Parameters.Add("@p_id", SqlDbType.Int).Value = address.Id;
        }

        protected override IList<Address> Read(DbDataReader reader)
        {
            IList<Address> addresses = new List<Address>();
            SqlDataReader sqlReader = (SqlDataReader)reader;

            while (sqlReader.Read())
            {
                int i = -1;
                Address address = new Address();
                address.Id = sqlReader.GetInt32(++i);

                address.Street = sqlReader.GetString(++i);
                address.City = sqlReader.GetString(++i);
                address.Zipcode = sqlReader.GetString(++i);

                addresses.Add(address);
            }
            return addresses;
        }

        protected override void PrepareIdCommand(DbCommand command, int addressId)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.Parameters.Add("@p_id", SqlDbType.Int).Value = addressId;
        }
    }
}
