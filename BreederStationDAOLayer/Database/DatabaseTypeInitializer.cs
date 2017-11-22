using BreederStationDataLayer;
using BreederStationDataLayer.Database;
using BreederStationDataLayer.Orm.Dao;
using BreederStationDataLayer.Orm.Dto;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;

namespace BreederStationDAOLayer.Database
{
    public class DatabaseTypeInitializer
    {
        public static void InitializeDatabaseType(DatabaseTypeEnum databaseType)
        {
            RepositoryRegister register = RepositoryRegister.getInstance();
            if (databaseType == DatabaseTypeEnum.ORACLE_DATABASE)
            {
                DatabaseService.init(new OracleConnection());
                register.Register(typeof(PersonGateway), new OraclePersonGateway(DatabaseService.getInstance()));
                register.Register(typeof(AddressGateway), new OracleAddressGateway(DatabaseService.getInstance()));
                register.Register(typeof(CageGateway), new OracleCageGateway(DatabaseService.getInstance()));
                register.Register(typeof(AnimalGateway), new OracleAnimalGateway(DatabaseService.getInstance()));
                register.Register(typeof(AnimalGroupGateway), new OracleAnimalGroupGateway(DatabaseService.getInstance()));
                register.Register(typeof(CompanyGateway), new OracleCompanyGateway(DatabaseService.getInstance()));
                register.Register(typeof(EventGateway), new OracleEventGateway(DatabaseService.getInstance()));
                register.Register(typeof(FoodOrderPendingGateway), new OracleFoodOrderPendingGateway(DatabaseService.getInstance()));
                register.Register(typeof(FoodGateway), new OracleFoodGateway(DatabaseService.getInstance()));
                register.Register(typeof(RoleGateway), new OracleRoleGateway(DatabaseService.getInstance()));

            } else if(databaseType == DatabaseTypeEnum.SQL_SERVER_DATABASE)
            {
                DatabaseService.init(new SqlConnection());
                register.Register(typeof(PersonGateway), new SqlServerPersonGateway(DatabaseService.getInstance()));
                register.Register(typeof(AddressGateway), new SqlServerAddressGateway(DatabaseService.getInstance()));
                register.Register(typeof(CageGateway), new SqlServerCageGateway(DatabaseService.getInstance()));
                register.Register(typeof(AnimalGateway), new SqlServerAnimalGateway(DatabaseService.getInstance()));
                register.Register(typeof(AnimalGroupGateway), new SqlServerAnimalGroupGateway(DatabaseService.getInstance()));
                register.Register(typeof(CompanyGateway), new SqlServerCompanyGateway(DatabaseService.getInstance()));
                register.Register(typeof(EventGateway), new SqlServerEventGateway(DatabaseService.getInstance()));
                register.Register(typeof(FoodOrderPendingGateway), new SqlServerFoodOrderPendingGateway(DatabaseService.getInstance()));
                register.Register(typeof(FoodGateway), new SqlServerFoodGateway(DatabaseService.getInstance()));
                register.Register(typeof(RoleGateway), new SqlServerRoleGateway(DatabaseService.getInstance()));
            }
        }
    }
}
