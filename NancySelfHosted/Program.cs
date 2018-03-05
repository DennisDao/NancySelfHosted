using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using Nancy;
using Nancy.Hosting.Self;
using NancySelfHosted.Model;
using NPoco;

namespace NancySelfHosted
{
    class Program
    {
        static SQLiteConnection _connection = new SQLiteConnection("Data Source=test.sqlite");

        static void Main(string[] args)
        {
            var host = new NancyHost(new Uri("http://localhost:12345"));
            IntializeDb();
            host.Start(); // start hosting
            Console.ReadKey();
            host.Stop();  // stop hosting
        }

        public static void IntializeDb()
        {
            if (!File.Exists("./test.sqlite"))
            {
                CreateTable();
            }
            return;  
        }

        public static void CreateTable()
        {
            SQLiteConnection.CreateFile("./test.sqlite"); //Create Database
            _connection.Open();
            Console.WriteLine("Create new Database"); 
            //Create Table
            string createQuery = @"CREATE TABLE IF NOT EXISTS [Candidate] (
                                       [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                       [Name] NVARCHAR(255) NULL,
                                       [Address] NVARCHAR(255) NULL,
                                       [Position] NVARCHAR(255) NULL,
                                       [ExpectedSalary] INTEGER NULL, 
                                       [Status] NVARCHAR(255) NULL)";

            SQLiteCommand command = new SQLiteCommand(createQuery, _connection);
            command.ExecuteNonQuery();
            //Populate Test Data
            string sql = "insert into Candidate(Name, Address, Position, ExpectedSalary, Status) values ('Frank', '18 Simpson street', '.NET developer', 90000, 'Rejected')";
            command = new SQLiteCommand(sql, _connection);
            command.ExecuteNonQuery();
            _connection.Close();
        }
    }
}





