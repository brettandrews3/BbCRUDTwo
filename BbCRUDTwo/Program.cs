using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BbCRUDTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            var departments = GetAllDepartments();

            foreach(var dept in departments)
            {
                Console.WriteLine(dept);
            }
        }

        //Let's establish our secure connection to the database:
        static IEnumerable GetAllDepartments()
        {
            MySqlConnection conn = new MySqlConnection();
            //Our conn here tells the app to use ConnectionString.txt to see how
            //to access the database:
            conn.ConnectionString = System.IO.File.ReadAllText("ConnectionString.txt");

            //Here, use the connection conn to submit the 1st SQL command: SELECT NAME...;
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Name FROM Departments;";

            using(conn) //start the using statement and utilize MySqlConnection conn
            {
                conn.Open(); //Connection opens
                List<string> allDepartments = new List<string>();
                //Create new List<strin> to hold the departments from Departments table

                //Reads a forward-only stream of rows, one at a time
                MySqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read() == true) //read while there's more depts to show
                {
                    var currentDepartment = reader.GetString("Name");
                    allDepartments.Add(currentDepartment);
                }
                //List is now done, connection is closed, and show allDepartments
                return allDepartments;
            }
        }
    }
}
