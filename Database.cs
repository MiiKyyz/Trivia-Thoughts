using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Content.ClipData;

namespace Predict_App
{
    public class Database
    {
        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        SQLiteConnection conn;

        public Database()
        {
            try
            {
                conn = new SQLiteConnection(path + "Data_Test.db"); 
                conn.Query<User>($"CREATE TABLE IF NOT EXISTS User(ID integer primary key, UserName text, Password text, Corrects integer, Incorrects integer, Total integer)");
                conn.Query<UserLogged>($"CREATE TABLE IF NOT EXISTS UserLogged(ID integer primary key, UserNameLogged text)");
                //conn.CreateTable<User>();
                Log.Info("Database", "created!");
            }
            catch
            {

                Log.Info("Database", "ERROR!");

            }
        
            
        
        }


        public void SetCorrects(User u)
        {

            conn.Query<User>($"UPDATE User SET Corrects='{u.Corrects}' WHERE UserName='{u.UserName}'");

         

        }

        public void SetIncorrects(User u)
        {


            conn.Query<User>($"UPDATE User SET Incorrects='{u.Incorrects}' WHERE UserName='{u.UserName}'");
           

        }

        public void SetTotal(User u)
        {

            conn.Query<User>($"UPDATE User SET Total='{u.Total}' WHERE UserName='{u.UserName}'");
            


        }
        public void InsertUser(User u)
        {

            conn.Query<User>($"INSERT INTO User(UserName, Password) VALUES('{u.UserName}', '{u.Password}')");

            //conn.Insert(u);


        }

        public void InsertUserLogged(UserLogged u)
        {

            conn.Query<UserLogged>($"INSERT INTO UserLogged(UserNameLogged) VALUES('{u.UserNameLogged}')");

            //conn.Insert(u);


        }
        public List<UserLogged> SelectPlayerlogged(UserLogged p)
        {

            return conn.Query<UserLogged>($"select * from UserLogged");


        }

        public void DeleteData(UserLogged p)
        {

            conn.Query<UserLogged>($"DROP TABLE UserLogged");
            //conn.Delete<UserLogged>(p);
        }



        public List<User> SelectPlayer(User p)
        {
           
            return conn.Query<User>($"select * from User");

            
        }

        public void UpdatepPassword(User u)
        {


            conn.Query<User>($"UPDATE User SET Password='{u.Password}' WHERE UserName='{u.UserName}'");


        }





    }
}