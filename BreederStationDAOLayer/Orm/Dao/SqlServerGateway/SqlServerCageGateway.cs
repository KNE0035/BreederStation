using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Diagnostics;
using BreederStationDataLayer.Orm.Dto;
using System.Data.Common;
using System;
using BreederStationDataLayer.Database;
using System.Data.SqlClient;
using System.Data;

namespace BreederStationDataLayer.Orm.Dao
{
    public class SqlServerCageGateway : CageGateway
    {
        public static string TABLE_NAME = "Cage";

        private static string SQL_INSERT = "INSERT INTO CAGE (LENGTH_M, WIDTH_M) VALUES (@p_length_m, @p_width_m)";

        private static string SQL_SELECT = @"SELECT c.ID, 
                                             COALESCE(SUBSTRING(( SELECT   ', ' + p.FIRST_NAME + ' ' + p.LAST_NAME FROM person p JOIN CAGE_CLEANER cc ON p.id = cc.cleaner_id where cc.cage_id = c.id and p.active = 1
                                             FOR XML PATH('')),2 ,1000), ' '),
                                             COALESCE(SUBSTRING(( SELECT   ', ' + a.name + ' (id:' + CAST(a.id as varchar(10)) + ')'  FROM animal a where a.cage_id = c.id and a.active = 1
                                             FOR XML PATH('')),2 ,1000), ' '), c.LENGTH_M, c.WIDTH_M
                                             FROM cage c";

        private static string SQL_UPDATE = "UPDATE CAGE SET LENGTH_M=@p_length_m, WIDTH_M=@p_width_m  WHERE id=@p_id";

        private static string SQL_DELETE_ID = "DELETE FROM CAGE WHERE id=@p_id";

        public SqlServerCageGateway(IDatabaseService db) : base(db)
        {

        }

        protected override void PrepareCommand(DbCommand command, Cage cage)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.Parameters.Add("@p_length_m", SqlDbType.Int).Value = cage.LengthM;
            sqlCommand.Parameters.Add("@p_width_m", SqlDbType.Int).Value = cage.WidthM;
            sqlCommand.Parameters.Add("@p_id", SqlDbType.Int).Value = cage.Id;
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
            SqlDataReader sqlReader = (SqlDataReader)reader;

            while (sqlReader.Read())
            {
                int i = -1;
                Cage cage = new Cage();
                cage.Id = sqlReader.GetInt32(++i);

                if (sqlReader.GetString(++i) != " ") {
                    cage.Cleaners = sqlReader.GetString(i);
                }
                else
                {
                    cage.Cleaners = " ";
                }

                if (sqlReader.GetString(++i) != " ")
                {
                    cage.Animals = sqlReader.GetString(i);
                }
                else
                {
                    cage.Animals = "";
                }

                cage.LengthM = sqlReader.GetInt32(++i);
                cage.WidthM = sqlReader.GetInt32(++i);

                cages.Add(cage);
            }
            return cages;
        }

        protected override void PrepareDeleteCommand(DbCommand command, int cageId)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.Parameters.Add("@p_id", SqlDbType.Int).Value = cageId;
        }
    }
}
