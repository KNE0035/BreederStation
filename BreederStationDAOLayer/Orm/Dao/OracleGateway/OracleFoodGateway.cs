using BreederStationDataLayer.Database;
using BreederStationDataLayer.Orm.Dto;
using DAIS_KNE0035.Orm.SelectCriteria;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System;

namespace BreederStationDataLayer.Orm.Dao
{
    public class OracleFoodGateway : FoodGateway
    {
        public static string TABLE_NAME = "Food";

        private static string SQL_INSERT = @"INSERT INTO FOOD (PROTEINS, FAT, CARBOHYDRATES, NAME, PRICE, COMPANY_ID, FOOD_RUNNING_OUT) 
                                             VALUES (:p_proteins, :p_fat, :p_carbohydrates, :p_name, :p_price, :p_company_id, :p_food_running_out)";

        private static string SQL_SELECT = @"SELECT f.ID, f.PROTEINS, f.FAT, f.CARBOHYDRATES, f.NAME, f.PRICE, f.FOOD_RUNNING_OUT, c.id, c.TRADEMARK, c.PHONE, c.EMAIL
                                             FROM FOOD f
                                             JOIN COMPANY c ON f.company_id = c.id";

        private static string SQL_SELECT_ID = @"SELECT f.ID, f.PROTEINS, f.FAT, f.CARBOHYDRATES, f.NAME, f.PRICE, f.FOOD_RUNNING_OUT, c.id, c.TRADEMARK, c.PHONE, c.EMAIL
                                                FROM FOOD f
                                                JOIN COMPANY c ON f.company_id = c.id
                                                WHERE f.id=:p_id";

        private static string SQL_UPDATE = @"UPDATE FOOD SET PROTEINS=:p_proteins, FAT=:p_fat, CARBOHYDRATES=:p_carbohydrates, NAME=:p_name, PRICE=:p_price, 
                                             company_id=:p_company_id, FOOD_RUNNING_OUT=:p_food_running_out
                                             WHERE ID=:p_id";

        private static string SQL_DELETE_ID = "DELETE FROM FOOD WHERE ID=:p_id";

        public OracleFoodGateway(IDatabaseService db) : base(db)
        {

        }

        protected override  void PrepareInsertUpdateCommand(DbCommand command, Food food)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_proteins", OracleDbType.Int32).Value = food.Proteins;
            oracleCommand.Parameters.Add("p_fat", OracleDbType.Int32).Value = food.Fat;
            oracleCommand.Parameters.Add("p_carbohydrates", OracleDbType.Int32).Value = food.Carbohydrates;
            oracleCommand.Parameters.Add("p_name", OracleDbType.Varchar2).Value = food.Name;
            oracleCommand.Parameters.Add("p_price", OracleDbType.Double).Value = food.Price;
            oracleCommand.Parameters.Add("p_company_id", OracleDbType.Int32).Value = food.Company.Id;
            oracleCommand.Parameters.Add("p_food_running_out", OracleDbType.Char).Value = food.FoodRunningOut ? 1 : 0;

            foreach (OracleParameter p in command.Parameters)
            {
                Debug.WriteLine(string.Format("{0} = {1}", p.ParameterName, p.Value));
            }
        }

        protected override void PrepareIdCommand(DbCommand command, int foodId)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_id", OracleDbType.Int32).Value = foodId;
        }

        protected override IList<Food> Read(DbDataReader reader)
        {
            IList<Food> foods = new List<Food>();
            OracleDataReader oracleReader = (OracleDataReader)reader;

            while (oracleReader.Read())
            {
                int i = -1;
                Food food = new Food();
                food.Id = oracleReader.GetInt32(++i);
                food.Proteins = oracleReader.GetInt32(++i);
                food.Fat = oracleReader.GetInt32(++i);
                food.Carbohydrates = oracleReader.GetInt32(++i);
                food.Name = oracleReader.GetString(++i);
                food.Price = oracleReader.GetDouble(++i);
                food.FoodRunningOut = oracleReader.GetString(++i) == "1" ? true : false;

                food.Company = new Company();

                food.Company.Id = oracleReader.GetInt32(++i);
                food.Company.Trademark = oracleReader.GetString(++i);
                food.Company.Phone = oracleReader.GetString(++i);
                food.Company.Email = oracleReader.GetString(++i);
                foods.Add(food);
            }
            return foods;
        }

        protected override void ApplyCriteria(DbCommand command, FoodCriteria criteria)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            if (criteria.MinimunProteins != null)
            {
                oracleCommand.CommandText += " WHERE PROTEINS>=:c_proteins";
                oracleCommand.Parameters.Add("c_proteins", OracleDbType.Int32).Value = criteria.MinimunProteins;
            }

            if (criteria.MinimumFat != null)
            {
                oracleCommand.CommandText += " AND FAT>=:c_fat";
                oracleCommand.Parameters.Add("c_fat", OracleDbType.Int32).Value = criteria.MinimumFat;
            }

            if (criteria.MinimumCarbohydrates != null)
            {
                oracleCommand.CommandText += " AND CARBOHYDRATES>=:c_carbohydrates";
                oracleCommand.Parameters.Add("c_carbohydrates", OracleDbType.Int32).Value = criteria.MinimumCarbohydrates;
            }
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

        protected override string GetSelectIdSql()
        {
            return SQL_SELECT_ID;
        }
    }
}
