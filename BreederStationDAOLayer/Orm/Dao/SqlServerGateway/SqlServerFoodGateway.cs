using BreederStationDataLayer.Database;
using BreederStationDataLayer.Orm.Dto;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using BreederStationDataLayer.Orm.SelectCriteria;

namespace BreederStationDataLayer.Orm.Dao
{
    public class SqlServerFoodGateway : FoodGateway
    {
        public static string TABLE_NAME = "Food";

        private static string SQL_INSERT = @"INSERT INTO FOOD (PROTEINS, FAT, CARBOHYDRATES, NAME, PRICE, COMPANY_ID, FOOD_RUNNING_OUT) 
                                             VALUES (@p_proteins, @p_fat, @p_carbohydrates, @p_name, @p_price, @p_company_id, @p_food_running_out)";

        private static string SQL_SELECT = @"SELECT f.ID, f.PROTEINS, f.FAT, f.CARBOHYDRATES, f.NAME, f.PRICE, f.FOOD_RUNNING_OUT, c.id, c.TRADEMARK, c.PHONE, c.EMAIL
                                             FROM FOOD f
                                             JOIN COMPANY c ON f.company_id = c.id";

        private static string SQL_SELECT_ID = @"SELECT f.ID, f.PROTEINS, f.FAT, f.CARBOHYDRATES, f.NAME, f.PRICE, f.FOOD_RUNNING_OUT, c.id, c.TRADEMARK, c.PHONE, c.EMAIL
                                                FROM FOOD f
                                                JOIN COMPANY c ON f.company_id = c.id
                                                WHERE f.id=@p_id";

        private static string SQL_UPDATE = @"UPDATE FOOD SET PROTEINS=@p_proteins, FAT=@p_fat, CARBOHYDRATES=@p_carbohydrates, NAME=@p_name, PRICE=@p_price, 
                                             company_id=@p_company_id, FOOD_RUNNING_OUT=@p_food_running_out
                                             WHERE ID=@p_id";

        private static string SQL_DELETE_ID = "DELETE FROM FOOD WHERE ID=@p_id";

        public SqlServerFoodGateway(IDatabaseService db) : base(db)
        {

        }

        protected override  void PrepareInsertUpdateCommand(DbCommand command, Food food)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.Parameters.Add("@p_proteins", SqlDbType.Int).Value = food.Proteins;
            sqlCommand.Parameters.Add("@p_fat", SqlDbType.Int).Value = food.Fat;
            sqlCommand.Parameters.Add("@p_carbohydrates", SqlDbType.Int).Value = food.Carbohydrates;
            sqlCommand.Parameters.Add("@p_name", SqlDbType.VarChar).Value = food.Name;
            sqlCommand.Parameters.Add("@p_price", SqlDbType.Decimal).Value = food.Price;
            sqlCommand.Parameters.Add("@p_company_id", SqlDbType.Int).Value = food.Company.Id;
            sqlCommand.Parameters.Add("@p_food_running_out", SqlDbType.Bit).Value = food.FoodRunningOut;
        }

        protected override void PrepareIdCommand(DbCommand command, int foodId)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.Parameters.Add("@p_id", SqlDbType.Int).Value = foodId;
        }

        protected override IList<Food> Read(DbDataReader reader)
        {
            IList<Food> foods = new List<Food>();
            SqlDataReader sqlReader = (SqlDataReader)reader;

            while (sqlReader.Read())
            {
                int i = -1;
                Food food = new Food();
                food.Id = sqlReader.GetInt32(++i);
                food.Proteins = sqlReader.GetInt32(++i);
                food.Fat = sqlReader.GetInt32(++i);
                food.Carbohydrates = sqlReader.GetInt32(++i);
                food.Name = sqlReader.GetString(++i);
                food.Price = (double)sqlReader.GetDecimal(++i);
                food.FoodRunningOut = sqlReader.GetBoolean(++i);

                food.Company = new Company();

                food.Company.Id = sqlReader.GetInt32(++i);
                food.Company.Trademark = sqlReader.GetString(++i);
                food.Company.Phone = sqlReader.GetString(++i);
                food.Company.Email = sqlReader.GetString(++i);
                foods.Add(food);
            }
            return foods;
        }

        protected override void ApplyCriteria(DbCommand command, FoodCriteria criteria)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            if (criteria.MinimunProteins != null)
            {
                sqlCommand.CommandText += " WHERE PROTEINS>=@c_proteins";
                sqlCommand.Parameters.Add("@c_proteins", SqlDbType.Int).Value = criteria.MinimunProteins;
            }

            if (criteria.MinimumFat != null)
            {
                sqlCommand.CommandText += " AND FAT>=@c_fat";
                sqlCommand.Parameters.Add("c_fat", SqlDbType.Int).Value = criteria.MinimumFat;
            }

            if (criteria.MinimumCarbohydrates != null)
            {
                sqlCommand.CommandText += " AND CARBOHYDRATES>=@c_carbohydrates";
                sqlCommand.Parameters.Add("c_carbohydrates", SqlDbType.Int).Value = criteria.MinimumCarbohydrates;
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
