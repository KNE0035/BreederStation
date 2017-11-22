using BreederStationDataLayer.Database;
using BreederStationDataLayer.Orm.Dto;
using DAIS_KNE0035.Orm.SelectCriteria;
using System.Collections.Generic;
using System.Data.Common;
namespace BreederStationDataLayer.Orm.Dao
{
    public abstract class CompanyGateway
    {
        private IDatabaseService db;

        public CompanyGateway(IDatabaseService databaseService)
        {
            this.db = databaseService;
        }

        public int Delete(string trademark)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetDeleteSql());
            PrepareTrademarkCommand(command, trademark);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public int Update(Company company)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetUpdateSql());
            PrepareInsertUpdateCommand(command, company);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }
        public IList<Company> Select(CompanyCriteria criteria)
        {
            db.Connect();

            DbCommand command = db.CreateCommand(GetSelectSql());
            applyCriteria(criteria, command);
            DbDataReader reader = db.Select(command);

            IList<Company> companies = Read(reader);
            reader.Close();
            db.Close();
            return companies;
        }

        public Company Select(string trademark)
        {
            db.Connect();

            DbCommand command = db.CreateCommand(GetSelectTrademarkSql());
            PrepareTrademarkCommand(command, trademark);
            DbDataReader reader = db.Select(command);

            IList<Company> companies = Read(reader);

            Company company = new Company();
            if (companies.Count != 0)
            {
                company = companies[0];
            }

            reader.Close();
            db.Close();

            return company;
        }
        public int Insert(Company company)
        {
   
            db.Connect();
            DbCommand command = db.CreateCommand(GetInsertSql());
            PrepareInsertUpdateCommand(command, company);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        protected abstract void PrepareInsertUpdateCommand(DbCommand command, Company company);
        protected abstract void PrepareTrademarkCommand(DbCommand command, string trademark);

        protected abstract string GetInsertSql();
        protected abstract string GetDeleteSql();
        protected abstract string GetUpdateSql();
        protected abstract string GetSelectSql();
        protected abstract string GetSelectTrademarkSql();

        protected abstract IList<Company> Read(DbDataReader reader);

        protected abstract void applyCriteria(CompanyCriteria criteria, DbCommand command);
    }
}
