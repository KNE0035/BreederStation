﻿
using BreederStationDAOLayer.Database;
using BreederStationDataLayer.Database;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.Common;

namespace BreederStationDataLayer.Database
{
    public class DatabaseService : IDatabaseService
    {
        private static string connectionString;

        private static DbConnection connection;
        private DbTransaction SqlTransaction { get; set; }
        private static IDatabaseService instance;
        private DatabaseService()
        {
            if(connection == null)
            {
                throw new Exception("Before you need to call init method");
            }
        }

        public static IDatabaseService GetInstance() {
            if(DatabaseService.instance == null)
            {
                DatabaseService.instance = new DatabaseService();
            }
            return DatabaseService.instance;
        }

        public bool Connect()
        {
            bool ret = true;

            if (connection.State != System.Data.ConnectionState.Open)
            {
                ret = ConnectIntern(DatabaseService.connectionString);
            }

            return ret;
        }

        private bool ConnectIntern(String conString)
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.ConnectionString = conString;
                connection.Open();
            }
            return true;
        }

        public void Close()
        {
            connection.Close();
        }

        public void BeginTransaction()
        {
            SqlTransaction = connection.BeginTransaction(IsolationLevel.Serializable);
        }

        public void EndTransaction()
        {

            SqlTransaction.Commit();
            Close();
        }

        public void Rollback()
        {
            SqlTransaction.Rollback();
        }

        public int ExecuteNonQuery(DbCommand command)
        {
            int rowNumber = 0;
            try
            {
                rowNumber = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            return rowNumber;
        }

        public DbCommand CreateCommand(string strCommand)
        {
            DbCommand command = DatabaseResourceCreator.createCommand(connection, strCommand);

            if (SqlTransaction != null)
            {
                command.Transaction = SqlTransaction;
            }
            return command;
        }

        public DbDataReader Select(DbCommand command)
        {
            //command.Prepare();
            DbDataReader sqlReader = command.ExecuteReader();
            return sqlReader;
        }

        public static void Init(DbConnection connection)
        {
            DatabaseService.connection = connection;
            DatabaseService.connectionString = DatabaseResourceCreator.createConnectionString(connection);
        }
    }
}