using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Diagnostics;
using BreederStationDataLayer.Orm.Dto;
using System.Data.Common;
using System;
using BreederStationDataLayer.Database;

namespace BreederStationDataLayer.Orm.Dao
{
    public class OracleCageGateway : CageGateway
    {
        public static string TABLE_NAME = "Cage";

        private static string SQL_INSERT = "INSERT INTO CAGE (LENGTH_M, WIDTH_M) VALUES (:p_length_m, :p_width_m)";

        private static string SQL_SELECT = "SELECT c.ID, COALESCE(cleanerTmp.cleanerList, ' ') as cleanerList, COALESCE(animalTmp.animalList, ' ') as animalList, c.LENGTH_M, c.WIDTH_M" +
                                           " FROM CAGE c" +
                                           " LEFT JOIN(SELECT LISTAGG(p.FIRST_NAME || ' ' || p.LAST_NAME, ', ') WITHIN GROUP(ORDER BY  cc.cage_id) as cleanerList, cc.cage_id" +
                                                       " FROM PERSON p" +
                                                       " JOIN CAGE_CLEANER cc ON p.id = cc.cleaner_id" +
                                                       " WHERE p.ACTIVE = 1" +
                                                       " GROUP BY cc.cage_id" +
                                                       ") cleanerTmp" +
                                                       " ON cleanerTmp.CAGE_ID = c.ID" +
                                           " LEFT JOIN(SELECT LISTAGG(a.NAME || ' (id: ' || a.ID || ')', ', ') WITHIN GROUP(ORDER BY  a.CAGE_ID) as animalList, a.cage_id" +
                                                       " FROM ANIMAL a" +
                                                       " WHERE a.ACTIVE = 1" +
                                                       " GROUP BY a.CAGE_ID" +
                                                       ") animalTmp" +
                                                       " ON animalTmp.cage_id = c.ID";

        private static string SQL_UPDATE = "UPDATE CAGE SET LENGTH_M=:p_length_m, WIDTH_M=:p_width_m  WHERE id=:p_id";

        private static string SQL_DELETE_ID = "DELETE FROM CAGE WHERE id=:p_id";

        public OracleCageGateway(IDatabaseService db) : base(db)
        {

        }

        protected override void PrepareCommand(DbCommand command, Cage cage)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_length_m", OracleDbType.Int32).Value = cage.LengthM;
            oracleCommand.Parameters.Add("p_width_m", OracleDbType.Int32).Value = cage.WidthM;
            oracleCommand.Parameters.Add("p_id", OracleDbType.Int32).Value = cage.Id;
        }

        protected override string GetInsertSql()
        {
            return SQL_INSERT;
        }

        protected override string GetDeleteSql()
        {
            return SQL_DELETE_ID;
        }

        protected override string GetUpdateSql()
        {
            return SQL_UPDATE;
        }

        protected override string GetSelectSql()
        {
            return SQL_SELECT;
        }

        protected override IList<Cage> Read(DbDataReader reader)
        {
            IList<Cage> cages = new List<Cage>();
            OracleDataReader oracleReader = (OracleDataReader)reader;

            while (oracleReader.Read())
            {
                int i = -1;
                Cage cage = new Cage();
                cage.Id = oracleReader.GetInt32(++i);

                if (oracleReader.GetString(++i) != " ") {
                    cage.Cleaners = oracleReader.GetString(i);
                }
                else
                {
                    cage.Cleaners = " ";
                }

                if (oracleReader.GetString(++i) != " ")
                {
                    cage.Animals = oracleReader.GetString(i);
                }
                else
                {
                    cage.Animals = "";
                }

                cage.LengthM = oracleReader.GetInt32(++i);
                cage.WidthM = oracleReader.GetInt32(++i);

                cages.Add(cage);
            }
            return cages;
        }

        protected override void PrepareDeleteCommand(DbCommand command, int cageId)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_id", OracleDbType.Int32).Value = cageId;
        }
    }
}
