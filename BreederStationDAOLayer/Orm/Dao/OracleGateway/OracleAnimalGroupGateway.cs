using BreederStationDataLayer.Orm.Dto;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System;
using BreederStationDataLayer.Database;

namespace BreederStationDataLayer.Orm.Dao
{
    public class OracleAnimalGroupGateway : AnimalGroupGateway
    {
        public static string TABLE_NAME = "Animal_Group";

        private static string SQL_INSERT = "INSERT INTO ANIMAL_GROUP (DESCRIPTION) VALUES (:p_description)";

        private static string SQL_SELECT = "SELECT ID, DESCRIPTION FROM ANIMAL_GROUP ORDER BY ID";

        private static string SQL_SELECT_ALL_INFO = @"SELECT ag.ID, ag.DESCRIPTION, COALESCE(breederTmp.breederList, ' ') as breederList, COALESCE(animalTmp.animalList, ' ') as animalList
                                                      FROM ANIMAL_GROUP ag
                                                      LEFT JOIN (SELECT LISTAGG(p.FIRST_NAME || ' ' || p.LAST_NAME, ', ') WITHIN GROUP (ORDER BY  b.ANIMAL_GROUP_ID) as breederList,
                                                                        b.ANIMAL_GROUP_ID
                                                                 FROM PERSON p
                                                                 JOIN BREEDER b ON b.person_id = p.id
                                                                 WHERE p.ACTIVE = 1
                                                                 GROUP BY b.ANIMAL_GROUP_ID
                                                                ) breederTmp ON breederTmp.ANIMAL_GROUP_ID = ag.ID
                                                      LEFT JOIN (SELECT LISTAGG(a.NAME || ' (id: ' || a.ID || ')', ', ') WITHIN GROUP (ORDER BY  a.ANIMAL_GROUP_ID) as animalList,
                                                                        a.ANIMAL_GROUP_ID
                                                                 FROM ANIMAL a
                                                                 WHERE a.ACTIVE = 1
                                                                 GROUP BY a.ANIMAL_GROUP_ID
                                                                ) animalTmp ON animalTmp.ANIMAL_GROUP_ID = ag.ID
                                                                ORDER BY ag.ID";

        private static string SQL_SELECT_ALL_INFO_ID = @"SELECT ag.ID, ag.DESCRIPTION, COALESCE(breederTmp.breederList, ' ') as breederList, COALESCE(animalTmp.animalList, ' ') as animalList
                                                      FROM ANIMAL_GROUP ag
                                                      LEFT JOIN (SELECT LISTAGG(p.FIRST_NAME || ' ' || p.LAST_NAME, ', ') WITHIN GROUP (ORDER BY  b.ANIMAL_GROUP_ID) as breederList,
                                                                        b.ANIMAL_GROUP_ID
                                                                 FROM PERSON p
                                                                 JOIN BREEDER b ON b.person_id = p.id
                                                                 WHERE p.ACTIVE = 1
                                                                 GROUP BY b.ANIMAL_GROUP_ID
                                                                ) breederTmp ON breederTmp.ANIMAL_GROUP_ID = ag.ID
                                                      LEFT JOIN (SELECT LISTAGG(a.NAME || ' (id: ' || a.ID || ')', ', ') WITHIN GROUP (ORDER BY  a.ANIMAL_GROUP_ID) as animalList,
                                                                        a.ANIMAL_GROUP_ID
                                                                 FROM ANIMAL a
                                                                 WHERE a.ACTIVE = 1
                                                                 GROUP BY a.ANIMAL_GROUP_ID
                                                                ) animalTmp ON animalTmp.ANIMAL_GROUP_ID = ag.ID
                                                      WHERE ag.ID=:p_id";

        private static string SQL_UPDATE = "UPDATE ANIMAL_GROUP SET DESCRIPTION=:p_description WHERE ID=:p_id";

        private static string SQL_DELETE = "DELETE ANIMAL_GROUP WHERE ID=:p_id";

        public OracleAnimalGroupGateway(IDatabaseService db) : base(db)
        {

        }

        protected override void PrepareInsertUpdateCommand(DbCommand command, AnimalGroup animalGroup)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_id", OracleDbType.Int32).Value = animalGroup.Id;
            oracleCommand.Parameters.Add("p_description", OracleDbType.Varchar2).Value = animalGroup.Description;
        }

        protected override void PrepareGroupIdCommand(DbCommand command, int animalGroupId)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_id", OracleDbType.Int32).Value = animalGroupId;
        }

        protected override IList<AnimalGroup> Read(DbDataReader reader, bool allInfo)
        {
            IList<AnimalGroup> animalGroups = new List<AnimalGroup>();
            OracleDataReader oracleReader = (OracleDataReader)reader;

            while (oracleReader.Read())
            {
                int i = -1;
                AnimalGroup animalGroup = new AnimalGroup();
                animalGroup.Id = oracleReader.GetInt32(++i);
                animalGroup.Description = oracleReader.GetString(++i);

                if (allInfo)
                {
                    animalGroup.BreedersInfo = oracleReader.GetString(++i);
                    animalGroup.AnimalsInfo = oracleReader.GetString(++i);
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
