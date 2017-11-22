using BreederStationDataLayer.Database;
using BreederStationDataLayer.Orm.Dto;
using DAIS_KNE0035.Orm.SelectCriteria;
using System.Collections.Generic;
using System.Data.Common;

namespace BreederStationDataLayer.Orm.Dao
{
    public abstract class FoodGateway
    {
        private IDatabaseService db;

        public FoodGateway(IDatabaseService databaseService)
        {
            this.db = databaseService;
        }

        public int Delete(int food_id)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetDeleteSql());
            PrepareIdCommand(command, food_id);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public int Update(Food food)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetUpdateSql());
            PrepareInsertUpdateCommand(command, food);
            PrepareIdCommand(command, food.Id);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }
        public IList<Food> Select(FoodCriteria criteria)
        {
            db.Connect();

            DbCommand command = db.CreateCommand(GetSelectSql());
            ApplyCriteria(command, criteria);
            DbDataReader reader = db.Select(command);

            IList<Food> foods = Read(reader);
            reader.Close();
            db.Close();
            return foods;
        }

        public Food Select(int foodID)
        {
            db.Connect();

            DbCommand command = db.CreateCommand(GetSelectIdSql());
            PrepareIdCommand(command, foodID);
            DbDataReader reader = db.Select(command);

            IList<Food> foods = Read(reader);

            Food food = new Food();
            if (foods.Count != 0)
            {
                food = foods[0];
            }

            reader.Close();
            db.Close();

            return food;
        }

        public int Insert(Food food)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetInsertSql());
            PrepareInsertUpdateCommand(command, food);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }
        protected abstract void PrepareInsertUpdateCommand(DbCommand command, Food food);

        protected abstract void PrepareIdCommand(DbCommand command, int foodId);

        protected abstract void ApplyCriteria(DbCommand command, FoodCriteria criteria);

        protected abstract string GetInsertSql();
        protected abstract string GetDeleteSql();
        protected abstract string GetUpdateSql();
        protected abstract string GetSelectSql();
        protected abstract string GetSelectIdSql();

        protected abstract IList<Food> Read(DbDataReader reader);
    }
}
