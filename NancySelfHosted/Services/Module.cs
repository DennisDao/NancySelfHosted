using Nancy;
using Nancy.ModelBinding;
using NancySelfHosted.Model;
using NPoco;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace NancySelfHosted.Services
{
    public class Module : NancyModule
    {
        public Module()
        {
            //Todo: Get query from URL
            Get("/Get/{Name}", x => {
                string name = this.Request.Query["name"];
                SQLiteConnection connection = new SQLiteConnection("Data Source=test.sqlite");
                connection.Open();
                IDatabase db = new Database(connection);
                List<Candidate> candidates = db.Fetch<Candidate>("select * from Candidate where Name =" + name);
                return Response.AsJson(candidates);
            });
            Get("/GetAll/", x => {
                SQLiteConnection connection = new SQLiteConnection("Data Source=test.sqlite");
                connection.Open();
                IDatabase db = new Database(connection);
                List<Candidate> candidates = db.Fetch<Candidate>("select Id, Name,Address,Position,ExpectedSalary,Status from Candidate");
                return Response.AsJson(candidates);
            });
            Post("/Add/", x => {
                SQLiteConnection connection = new SQLiteConnection("Data Source=test.sqlite");
                connection.Open();
                IDatabase db = new Database(connection);
                var model = this.Bind<Candidate>();
                db.Insert<Candidate>(model);
                return Response.AsJson("200");
            });
        }
    }
}
