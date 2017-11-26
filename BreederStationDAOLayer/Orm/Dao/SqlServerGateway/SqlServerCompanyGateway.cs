using BreederStationDataLayer.Orm.Dto;
using System.Collections.Generic;
using System.Data.Common;
using BreederStationDataLayer.Database;
using System.Data.SqlClient;
using System.Data;
using BreederStationDataLayer.Orm.SelectCriteria;

namespace BreederStationDataLayer.Orm.Dao
{
    public class SqlServerCompanyGateway : CompanyGateway
    {
        public static string TABLE_NAME = "Company";

        private static string SQL_INSERT = "INSERT INTO COMPANY (TRADEMARK, PHONE, EMAIL, ADDRESS_ID) VALUES (@p_trademark, @p_phone, @p_email, @p_address_id)";

        private static string SQL_SELECT = @"SELECT c.id, c.TRADEMARK, c.PHONE, c.EMAIL, a.ID, a.STREET, a.CITY, a.ZIPCODE FROM COMPANY c
                                             JOIN ADDRESS a ON c.ADDRESS_ID = a.id";

        private static string SQL_SELECT_TRADEMARK = @"SELECT c.id, c.TRADEMARK, c.PHONE, c.EMAIL, a.ID, a.STREET, a.CITY, a.ZIPCODE FROM COMPANY c
                                                       JOIN ADDRESS a ON c.ADDRESS_ID = a.id
                                                       WHERE c.TRADEMARK=@p_trademark";

        private static string SQL_UPDATE = "UPDATE COMPANY SET PHONE=@p_phone, EMAIL=@p_email, ADDRESS_ID=@p_address_id  WHERE TRADEMARK=@p_trademark";

        private static string SQL_DELETE_TRADEMARK = "DELETE FROM COMPANY WHERE TRADEMARK=@p_trademark";

        public SqlServerCompanyGateway(IDatabaseService db) : base(db)
        {

        }

        protected override void PrepareInsertUpdateCommand(DbCommand command, Company company)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.Parameters.Add("@p_trademark", SqlDbType.VarChar).Value = company.Trademark;
            sqlCommand.Parameters.Add("@p_phone", SqlDbType.VarChar).Value = company.Phone;
            sqlCommand.Parameters.Add("@p_email", SqlDbType.VarChar).Value = company.Email;
            sqlCommand.Parameters.Add("@p_address_id", SqlDbType.Int).Value = company.Address.Id;
        }

        protected override void PrepareTrademarkCommand(DbCommand command, string trademark)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.Parameters.Add("@p_trademark", SqlDbType.VarChar).Value = trademark;
        }

        protected override IList<Company> Read(DbDataReader reader)
        {
            IList<Company> companies = new List<Company>();
            SqlDataReader sqlReader = (SqlDataReader)reader;

            while (sqlReader.Read())
            {
                int i = -1;
                Company company = new Company();
                company.Id = sqlReader.GetInt32(++i);
                company.Trademark = sqlReader.GetString(++i);
                company.Phone = sqlReader.GetString(++i);
                company.Email = sqlReader.GetString(++i);

                company.Address = new Address();
                company.Address.Id = sqlReader.GetInt32(++i);

                company.Address.Street = sqlReader.GetString(++i);
                company.Address.City = sqlReader.GetString(++i);
                company.Address.Zipcode = sqlReader.GetString(++i);

                companies.Add(company);
            }
            return companies;
        }

        protected override void applyCriteria(CompanyCriteria criteria, DbCommand command)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            if (criteria.City != null)
            {
                sqlCommand.CommandText += " WHERE LOWER(a.CITY)=LOWER(@c_city)";
                sqlCommand.Parameters.Add("@c_city", SqlDbType.VarChar).Value = criteria.City;
            }

            if (criteria.Trademark != null)
            {
                sqlCommand.CommandText += " AND Lower(c.TRADEMARK) like '%' || LOWER(@c_trademark) || '%'";
                sqlCommand.Parameters.Add("@c_trademark", SqlDbType.VarChar).Value = criteria.Trademark;
            }
        }

        protected override string GetInsertSql()
        {
            return SQL_INSERT;
        }

        protected override string GetDeleteSql()
        {
            return SQL_DELETE_TRADEMARK;
        }

        protected override string GetUpdateSql()
        {
            return SQL_UPDATE;
        }

        protected override string GetSelectSql()
        {
            return SQL_SELECT;
        }

        protected override string GetSelectTrademarkSql()
        {
            return SQL_SELECT_TRADEMARK;
        }
    }
}
