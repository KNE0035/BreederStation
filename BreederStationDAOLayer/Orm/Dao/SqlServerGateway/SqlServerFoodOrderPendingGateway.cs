using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using BreederStationDataLayer.Orm.Dto;
using System.Data.Common;
using System;
using BreederStationDataLayer.Database;
using System.Data.SqlClient;
using System.Data;

namespace BreederStationDataLayer.Orm.Dao
{
    public class SqlServerFoodOrderPendingGateway : FoodOrderPendingGateway
    {
        public static string TABLE_NAME = "Food_order_pending";

        private static string SQL_INSERT = "INSERT INTO FOOD_ORDER_PENDING(START_DATE, RESOLVED_DATE, FOOD_ID, PRIORITY) VALUES(@p_start_date, @p_resolved_date, @p_food_id, @p_priority)";

        private static string SQL_SELECT = @"SELECT fop.ID, fop.START_DATE, fop.RESOLVED_DATE, fop.PRIORITY,
                                                    f.ID, f.name, f.PRICE, f.PROTEINS, f.CARBOHYDRATES, f.FAT, f.FOOD_RUNNING_OUT, f.COMPANY_ID
                                                FROM Food_order_pending fop
                                                JOIN FOOD f ON fop.food_id = f.id
                                                WHERE fop.RESOLVED_DATE IS ";

        private static string SQL_UPDATE = "UPDATE Food_order_pending SET PRIORITY=@p_priority, RESOLVED_DATE=@p_resolved_date WHERE id=@p_id";

        public SqlServerFoodOrderPendingGateway(IDatabaseService db) : base(db)
        {

        }

        protected override void applyResolvedCriterium(DbCommand command, bool isResolved)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.CommandText += isResolved ? "NOT NULL" : "NULL";
        }

        protected override void PrepareInsertUpdateCommand(DbCommand command, FoodOrderPending foodOrderPending)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.Parameters.Add("@p_priority", SqlDbType.VarChar).Value = foodOrderPending.Priority.ToString();
            sqlCommand.Parameters.Add("@p_start_date", SqlDbType.Date).Value = foodOrderPending.StartDate;
            sqlCommand.Parameters.Add("@p_resolved_date", SqlDbType.Date).Value = foodOrderPending.ResolvedDate;
            sqlCommand.Parameters.Add("@p_id", SqlDbType.Int).Value = foodOrderPending.Id;
            sqlCommand.Parameters.Add("@p_food_id", SqlDbType.Int).Value = foodOrderPending.Food.Id;
        }

        protected override IList<FoodOrderPending> Read(DbDataReader reader)
        {
            IList<FoodOrderPending> foodOrderPendings = new List<FoodOrderPending>();
            SqlDataReader sqlReader = (SqlDataReader)reader;

            while (sqlReader.Read())
            {
                int i = -1;
                FoodOrderPending foodOrderPending = new FoodOrderPending();
                foodOrderPending.Id = sqlReader.GetInt32(++i);
                foodOrderPending.StartDate = sqlReader.GetDateTime(++i);

                if (!sqlReader.IsDBNull(++i))
                {
                    foodOrderPending.ResolvedDate = sqlReader.GetDateTime(i);
                }
                foodOrderPending.Priority = FoodOrderPriorityEnumUtils.getPriority(sqlReader.GetString(++i));

                foodOrderPending.Food = new Food();


                foodOrderPending.Food = new Food();
                foodOrderPending.Food.Id = sqlReader.GetInt32(++i);
                foodOrderPending.Food.Name = sqlReader.GetString(++i);
                foodOrderPending.Food.Price = (double)sqlReader.GetDecimal(++i);
                foodOrderPending.Food.Proteins = sqlReader.GetInt32(++i);
                foodOrderPending.Food.Carbohydrates = sqlReader.GetInt32(++i);
                foodOrderPending.Food.Fat = sqlReader.GetInt32(++i);
                foodOrderPending.Food.FoodRunningOut = sqlReader.GetBoolean(++i);

                foodOrderPending.Food.Company = new Company();
                foodOrderPending.Food.Company.Id = sqlReader.GetInt32(++i);

                foodOrderPendings.Add(foodOrderPending);
            }
            return foodOrderPendings;
        }

        protected override string GetInsertSql()
        {
            return SQL_INSERT;
        }

        protected override string GetSelectSql()
        {
            return SQL_SELECT;
        }

        protected override string GetUpdateSql()
        {
            return SQL_UPDATE;
        }

    }
}
