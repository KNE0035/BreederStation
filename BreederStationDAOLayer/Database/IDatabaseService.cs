using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreederStationDataLayer.Database
{
    public interface IDatabaseService
    {
        bool Connect();
        void Close();
        void BeginTransaction();
        void EndTransaction();
        void Rollback();
        int ExecuteNonQuery(DbCommand command);
        DbCommand CreateCommand(string strCommand);
        DbDataReader Select(DbCommand command);


    }
}
