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
using System.IO;
using System.Linq;
using System.Text;

namespace Game_Center
{

    
    public class Database
    {
        public static string Userlogged;

        private static string PathFile = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        private static SQLiteConnection conn;
        

        public Database()
        {

            try
            {

                conn = new SQLiteConnection(Path.Combine(PathFile, "PlayerDatabase.db"));
                conn.Query<User>($"CREATE TABLE IF NOT EXISTS Player(ID integer primary key, Name text, Lastname text, Password text, Corrects integer, Incorrects integer, FontUser text, NoSelection integer)");
                Log.Info("DATA", $"Created");

            }
            catch (Exception e) {


                Log.Info("DATA", $"{e.Message}");
            }

        }

        public static void InsertUser(User u)
        {

            conn.Query<User>($"INSERT INTO Player(Name, Lastname, Password, FontUser) VALUES('{u.Name}', '{u.Lastname}', '{u.Password}', '{u.FontUser}')");

        }


        public static void SaveFont(User user)
        {


            conn.Query<User>($"UPDATE Player set FontUser='{user.FontUser}' where Name='{user.Name}'");

        }

        public static List<User> GetAll()
        {


            return conn.Query<User>($"select * from Player");
        }


        public static void Setcorrect(User user)
        {

            conn.Query<User>($"UPDATE Player set Corrects='{user.Corrects}' where Name='{user.Name}'");


        }

        public static void SetIncorrect(User user)
        {


            conn.Query<User>($"UPDATE Player set Incorrects='{user.Incorrects}' where Name='{user.Name}'");

        }

        public static void SetNoSelection(User user)
        {

            conn.Query<User>($"UPDATE Player set NoSelection='{user.NoSelection}' where Name='{user.Name}'");


        }

        public static void UpdateData(User user)
        {

            conn.Query<User>($"UPDATE Player set Password='{user.Password}' where Name='{user.Name}'");


        }

    }
}