using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreederStationDAOLayer.Database
{
    public class CommandCreator
    {
        public static DbCommand createCommand(DbConnection connection, String strCommand)
        {
            if(connection.GetType() == typeof(OracleConnection))
            {
                return new OracleCommand(strCommand, (OracleConnection)connection);
            }
            return null;
        }
    }
}
