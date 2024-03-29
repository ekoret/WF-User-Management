﻿

using Microsoft.Data.Sqlite;
using System.Data;

namespace UserManagementSystem
{
    class Database
    {

        private const string CONNECTION_DATA_SOURCE = "DataSource=database.db";

        public static void AddRow(string username, string password)
        {
            SqliteConnection connection = new SqliteConnection(CONNECTION_DATA_SOURCE);

            string query = @"
                            INSERT INTO User (USERNAME, PASSWORD)
                            VALUES (@username, @password);
                            ";

            SqliteCommand command = new SqliteCommand(query, connection);

            SqliteParameter[] @params = {
                CreateSqliteParam("@username", 3, username),
                CreateSqliteParam("@password", 3, password)
                    };

            command.Parameters.AddRange(@params);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void SetUp()
        {  
                try
                {
                    // create the table with default schema
                    SqliteConnection connection = new SqliteConnection(CONNECTION_DATA_SOURCE);
                    string commandText = @"
                                      DROP TABLE User;
                                      CREATE TABLE IF NOT EXISTS User (ID integer primary key autoincrement,
                                                         USERNAME text not null,
                                                         PASSWORD text not null);
                                      INSERT INTO User (USERNAME, PASSWORD)
                                      VALUES ('john doe','123');
                                      INSERT INTO User (USERNAME, PASSWORD)
                                      VALUES ('jane doe','456');
                                      ";

                    SqliteCommand command = new SqliteCommand(commandText, connection);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch(SqliteException e)
                {
                    Console.WriteLine("SqliteException");
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("UnhandledException");
                    Console.WriteLine(e.Message);
                }
        }

        private static SqliteParameter CreateSqliteParam(string variable, int type, string value)
        {
            SqliteParameter param = new SqliteParameter(variable, type);
            param.Value = value;

            return param;
        }
    }
}
