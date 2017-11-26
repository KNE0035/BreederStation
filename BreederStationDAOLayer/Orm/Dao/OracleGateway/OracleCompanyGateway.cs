using BreederStationDataLayer.Orm.Dto;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using BreederStationDataLayer.Database;
using BreederStationDataLayer.Orm.SelectCriteria;

namespace BreederStationDataLayer.Orm.Dao
{
    public class OracleCompanyGateway : CompanyGateway
    {
        public static string TABLE_NAME = "Company";

        private static string SQL_INSERT = "INSERT INTO COMPANY (TRADEMARK, PHONE, EMAIL, ADDRESS_ID) VALUES (:p_trademark, :p_phone, :p_email, :p_address_id)";

        private static string SQL_SELECT = @"SELECT c.id, c.TRADEMARK, c.PHONE, c.EMAIL, a.ID, a.STREET, a.CITY, a.ZIPCODE FROM COMPANY c
                                             JOIN ADDRESS a ON c.ADDRESS_ID = a.id";

        private static string SQL_SELECT_TRADEMARK = @"SELECT c.id, c.TRADEMARK, c.PHONE, c.EMAIL, a.ID, a.STREET, a.CITY, a.ZIPCODE FROM COMPANY c
                                                       JOIN ADDRESS a ON c.ADDRESS_ID = a.id
                                                       WHERE c.TRADEMARK=:p_trademark";

        private static string SQL_UPDATE = "UPDATE COMPANY SET PHONE=:p_phone, EMAIL=:p_email, ADDRESS_ID=:p_address_id  WHERE TRADEMARK=:p_trademark";

        private static string SQL_DELETE_TRADEMARK = "DELETE FROM COMPANY WHERE TRADEMARK=:p_trademark";

        public OracleCompanyGateway(IDatabaseService db) : base(db)
        {

        }

        protected override void PrepareInsertUpdateCommand(DbCommand command, Company company)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_trademark", OracleDbType.Varchar2).Value = company.Trademark;
            oracleCommand.Parameters.Add("p_phone", OracleDbType.Varchar2).Value = company.Phone;
            oracleCommand.Parameters.Add("p_email", OracleDbType.Varchar2).Value = company.Email;
            oracleCommand.Parameters.Add("p_address_id", OracleDbType.Int32).Value = company.Address.Id;
        }

        protected override void PrepareTrademarkCommand(DbCommand command, string trademark)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_trademark", OracleDbType.Varchar2).Value = trademark;
        }

        protected override IList<Company> Read(DbDataReader reader)
        {
            IList<Company> companies = new List<Company>();
            OracleDataReader oracleReader = (OracleDataReader)reader;

            while (oracleReader.Read())
            {
                int i = -1;
                Company company = new Company();
                company.Id = oracleReader.GetInt32(++i);
                company.Trademark = oracleReader.GetString(++i);
                company.Phone = oracleReader.GetString(++i);
                company.Email = oracleReader.GetString(++i);

                company.Address = new Address();
                company.Address.Id = oracleReader.GetInt32(++i);

                company.Address.Street = oracleReader.GetString(++i);
                company.Address.City = oracleReader.GetString(++i);
                company.Address.Zipcode = oracleReader.GetString(++i);


                companies.Add(company);
            }
            return companies;
        }

        protected override void applyCriteria(CompanyCriteria criteria, DbCommand command)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            if (criteria.City != null)
            {
                oracleCommand.CommandText += " WHERE LOWER(a.CITY)=LOWER(:c_city)";
                oracleCommand.Parameters.Add("c_city", OracleDbType.Varchar2).Value = criteria.City;
            }

            if (criteria.Trademark != null)
            {
                oracleCommand.CommandText += " AND Lower(c.TRADEMARK) like '%' || LOWER(:c_trademark) || '%'";
                oracleCommand.Parameters.Add("c_trademark", OracleDbType.Varchar2).Value = criteria.Trademark;
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
