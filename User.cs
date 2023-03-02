using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_Center
{
    public  class User
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Lastname { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        [MaxLength(50)]
        public int Corrects { get; set; }

        [MaxLength(50)]
        public int Incorrects { get; set; }

        [MaxLength(50)]
        public string FontUser { get; set; }


        [MaxLength(50)]
        public int NoSelection { get; set; }




    }
}