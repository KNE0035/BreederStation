using BreederStationDataLayer.Orm.Dto;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System;
using BreederStationDataLayer.Database;
using System.Data.SqlClient;
using System.Data;

namespace BreederStationDataLayer.Orm.Dao
{
    public class SqlServerAnimalGroupGateway : AnimalGroupGateway
    {
        public static string TABLE_NAME = "Animal_Group";

        private static string SQL_INSERT = "INSERT INTO ANIMAL_GROUP (DESCRIPTION) VALUES (@p_description)";

        private static string SQL_SELECT = "SELECT ID, DESCRIPTION FROM ANIMAL_GROUP ORDER BY ID";

        private static string SQL_SELECT_ALL_INFO = @"SELECT ag.ID, ag.description, 
                                                      COALESCE(SUBSTRING(( SELECT   ', ' + p.FIRST_NAME + ' ' + p.LAST_NAME FROM PERSON p JOIN BREEDER b ON b.person_id = p.id where ag.id = b.ANIMAL_GROUP_ID and p.active = 1
                                                      FOR XML PATH('')),2 ,1000), ' '),
                                                      COALESCE(SUBSTRING(( SELECT   ', ' + a.name + ' (id:' + CAST(a.id as varchar(10)) + ')'  FROM animal a where a.animal_group_id = ag.id and a.active = 1
                                                      FOR XML PATH('')),2 ,1000), ' ')
                                                      FROM ANIMAL_GROUP ag";

        private static string SQL_SELECT_ALL_INFO_ID = @"SELECT ag.ID, ag.description, 
                                                        COALESCE(SUBSTRING(( SELECT   ', ' + p.FIRST_NAME + ' ' + p.LAST_NAME FROM PERSON p JOIN BREEDER b ON b.person_id = p.id where ag.id = b.ANIMAL_GROUP_ID and p.active = 1
                                                        FOR XML PATH('')),2 ,1000), ' '),
                                                        COALESCE(SUBSTRING(( SELECT   ', ' + a.name + ' (id:' + CAST(a.id as varchar(10)) + ')'  FROM animal a where a.animal_group_id = ag.id and a.active = 1
                                                        FOR XML PATH('')),2 ,1000), ' ')
                                                        FROM ANIMAL_GROUP ag
                                                        WHERE ag.ID=@p_id";

        private static string SQL_UPDATE = "UPDATE ANIMAL_GROUP SET DESCRIPTION=@p_description WHERE ID=@p_id";

        private static string SQL_DELETE = "DELETE ANIMAL_GROUP WHERE ID=@p_id";

        public SqlServerAnimalGroupGateway(IDatabaseService db) : base(db)
        {

        }

        protected override void PrepareInsertUpdateCommand(DbCommand command, AnimalGroup animalGroup)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.Parameters.Add("@p_id", SqlDbType.Int).Value = animalGroup.Id;
            sqlCommand.Parameters.Add("@p_description", SqlDbType.VarChar).Value = animalGroup.Description;
        }

        protected override void PrepareGroupIdCommand(DbCommand command, int animalGroupId)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.Parameters.Add("@p_id", SqlDbType.Int).Value = animalGroupId;
        }

        protected override IList<AnimalGroup> Read(DbDataReader reader, bool allInfo)
        {
            IList<AnimalGroup> animalGroups = new List<AnimalGroup>();
            SqlDataReader sqlReader = (SqlDataReader)reader;

            while (sqlReader.Read())
            {
                int i = -1;
                AnimalGroup animalGroup = new AnimalGroup();
                animalGroup.Id = sqlReader.GetInt32(++i);
                animalGroup.Description = sqlReader.GetString(++i);

                if (allInfo)
                {
                    animalGroup.BreedersInfo = sqlReader.GetString(++i);
                    animalGroup.AnimalsInfo = sqlReader.GetString(++i);
                }
                animalGroups.Add(animalGroup);
            }
            return animalGroups;
        }

        protected override string GetInsertSql()
        {
            return SQL_INSERT;
        }

        protected override string GetSelectAllInfoSql()
        {
            return SQL_SELECT_ALL_INFO;
        }

        protected override string GetSelectAllInfoByIdSql()
        {
            return SQL_SELECT_ALL_INFO_ID;
        }

        protected override string GetDeleteSql()
        {
            return SQL_DELETE;
        }

        protected override string GetUpdateSql()
        {
            return SQL_UPDATE;
        }

        protected override string GetSelectSql()
        {
            return SQL_SELECT;
        }
    }
}
