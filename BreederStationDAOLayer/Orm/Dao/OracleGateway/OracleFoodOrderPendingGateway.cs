using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using BreederStationDataLayer.Orm.Dto;
using System.Data.Common;
using System;
using BreederStationDataLayer.Database;

namespace BreederStationDataLayer.Orm.Dao
{
    public class OracleFoodOrderPendingGateway : FoodOrderPendingGateway
    {
        public static string TABLE_NAME = "Food_order_pending";

        private static string SQL_INSERT = "INSERT INTO FOOD_ORDER_PENDING(START_DATE, RESOLVED_DATE, FOOD_ID, PRIORITY) VALUES(:p_start_date, :p_resolved_date, :p_food_id, :p_priority)";

        private static string SQL_SELECT = @"SELECT fop.ID, fop.START_DATE, fop.RESOLVED_DATE, fop.PRIORITY,
                                                    f.ID, f.name, f.PRICE, f.PROTEINS, f.CARBOHYDRATES, f.FAT, f.FOOD_RUNNING_OUT, f.COMPANY_ID
                                                FROM Food_order_pending fop
                                                JOIN FOOD f ON fop.food_id = f.id
                                                WHERE fop.RESOLVED_DATE IS ";

        private static string SQL_UPDATE = "UPDATE Food_order_pending SET PRIORITY=:p_priority, RESOLVED_DATE=:p_resolved_date WHERE id=:p_id";

        public OracleFoodOrderPendingGateway(IDatabaseService db) : base(db)
        {

        }

        protected override void applyResolvedCriterium(DbCommand command, bool isResolved)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            command.CommandText += isResolved ? "NOT NULL" : "NULL";
        }

        protected override void PrepareInsertUpdateCommand(DbCommand command, FoodOrderPending foodOrderPending)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_priority", OracleDbType.Varchar2).Value = foodOrderPending.Priority.ToString();
            oracleCommand.Parameters.Add("p_start_date", OracleDbType.Date).Value = foodOrderPending.StartDate;
            oracleCommand.Parameters.Add("p_resolved_date", OracleDbType.Date).Value = foodOrderPending.ResolvedDate;
            oracleCommand.Parameters.Add("p_id", OracleDbType.Int32).Value = foodOrderPending.Id;
            oracleCommand.Parameters.Add("p_food_id", OracleDbType.Int32).Value = foodOrderPending.Food.Id;
        }

        protected override IList<FoodOrderPending> Read(DbDataReader reader)
        {
            IList<FoodOrderPending> foodOrderPendings = new List<FoodOrderPending>();
            OracleDataReader oracleReader = (OracleDataReader)reader;

            while (oracleReader.Read())
            {
                int i = -1;
                FoodOrderPending foodOrderPending = new FoodOrderPending();
                foodOrderPending.Id = oracleReader.GetInt32(++i);
                foodOrderPending.StartDate = oracleReader.GetDateTime(++i);

                if (!oracleReader.IsDBNull(++i))
                {
                    foodOrderPending.ResolvedDate = oracleReader.GetDateTime(i);
                }
                foodOrderPending.Priority = FoodOrderPriorityEnumUtils.getPriority(oracleReader.GetString(++i));

                foodOrderPending.Food = new Food();


                foodOrderPending.Food = new Food();
                foodOrderPending.Food.Id = oracleReader.GetInt32(++i);
                foodOrderPending.Food.Name = oracleReader.GetString(++i);
                foodOrderPending.Food.Price = oracleReader.GetDouble(++i);
                foodOrderPending.Food.Proteins = oracleReader.GetInt32(++i);
                foodOrderPending.Food.Carbohydrates = oracleReader.GetInt32(++i);
                foodOrderPending.Food.Fat = oracleReader.GetInt32(++i);
                foodOrderPending.Food.FoodRunningOut = oracleReader.GetString(++i) == "1" ? true : false;

                foodOrderPending.Food.Company = new Company();
                foodOrderPending.Food.Company.Id = oracleReader.GetInt32(++i);

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
