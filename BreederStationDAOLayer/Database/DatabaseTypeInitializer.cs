using BreederStationDataLayer;
using BreederStationDataLayer.Database;
using BreederStationDataLayer.Orm.Dao;
using BreederStationDataLayer.Orm.Dto;
using BreederStationDataLayer.Xml;
using BreederStationDataLayer.Xml.Impl;
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
                DatabaseService.Init(new OracleConnection());
                register.Register(typeof(PersonGateway), new OraclePersonGateway(DatabaseService.GetInstance()));
                register.Register(typeof(AddressGateway), new OracleAddressGateway(DatabaseService.GetInstance()));
                register.Register(typeof(CageGateway), new OracleCageGateway(DatabaseService.GetInstance()));
                register.Register(typeof(AnimalGateway), new OracleAnimalGateway(DatabaseService.GetInstance()));
                register.Register(typeof(AnimalGroupGateway), new OracleAnimalGroupGateway(DatabaseService.GetInstance()));
                register.Register(typeof(CompanyGateway), new OracleCompanyGateway(DatabaseService.GetInstance()));
                register.Register(typeof(EventGateway), new OracleEventGateway(DatabaseService.GetInstance()));
                register.Register(typeof(FoodOrderPendingGateway), new OracleFoodOrderPendingGateway(DatabaseService.GetInstance()));
                register.Register(typeof(FoodGateway), new OracleFoodGateway(DatabaseService.GetInstance()));
                register.Register(typeof(RoleGateway), new OracleRoleGateway(DatabaseService.GetInstance()));

            } else if(databaseType == DatabaseTypeEnum.SQL_SERVER_DATABASE)
            {
                DatabaseService.Init(new SqlConnection());
                register.Register(typeof(PersonGateway), new SqlServerPersonGateway(DatabaseService.GetInstance()));
                register.Register(typeof(AddressGateway), new SqlServerAddressGateway(DatabaseService.GetInstance()));
                register.Register(typeof(CageGateway), new SqlServerCageGateway(DatabaseService.GetInstance()));
                register.Register(typeof(AnimalGateway), new SqlServerAnimalGateway(DatabaseService.GetInstance()));
                register.Register(typeof(AnimalGroupGateway), new SqlServerAnimalGroupGateway(DatabaseService.GetInstance()));
                register.Register(typeof(CompanyGateway), new SqlServerCompanyGateway(DatabaseService.GetInstance()));
                register.Register(typeof(EventGateway), new SqlServerEventGateway(DatabaseService.GetInstance()));
                register.Register(typeof(FoodOrderPendingGateway), new SqlServerFoodOrderPendingGateway(DatabaseService.GetInstance()));
                register.Register(typeof(FoodGateway), new SqlServerFoodGateway(DatabaseService.GetInstance()));
                register.Register(typeof(RoleGateway), new SqlServerRoleGateway(DatabaseService.GetInstance()));
            }
            register.Register(typeof(AddressXmlGateway), new AddressXmlGatewayImpl());
        }
    }
}
