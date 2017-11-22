using BreederStationDataLayer.Database;
using BreederStationDataLayer.Orm.Dto;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreederStationDataLayer.Orm.Dao
{
    public abstract class FoodOrderPendingGateway
    {
        private IDatabaseService db;

        public FoodOrderPendingGateway(IDatabaseService databaseService)
        {
            this.db = databaseService;
        }

        public int Update(FoodOrderPending foodOrderPending)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetUpdateSql());
            PrepareInsertUpdateCommand(command, foodOrderPending);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }
        public IList<FoodOrderPending> Select(bool isResolved)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetSelectSql());
            applyResolvedCriterium(command, isResolved);
            DbDataReader reader = db.Select(command);

            IList<FoodOrderPending> foodOrderPendings = Read(reader);
            reader.Close();
            db.Close();
            return foodOrderPendings;
        }
        public int Insert(FoodOrderPending foodOrderPending)
        {
            db.Connect();
            DbCommand command = db.CreateCommand(GetInsertSql());
            PrepareInsertUpdateCommand(command, foodOrderPending);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        protected abstract void PrepareInsertUpdateCommand(DbCommand command, FoodOrderPending foodOrderPending);

        protected abstract void applyResolvedCriterium(DbCommand command, bool isResolved);

        protected abstract string GetInsertSql();
        protected abstract string GetUpdateSql();
        protected abstract string GetSelectSql();
        protected abstract IList<FoodOrderPending> Read(DbDataReader reader);
    }
}
