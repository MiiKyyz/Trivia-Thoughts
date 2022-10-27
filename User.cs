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

namespace Predict_App
{
    public class User
    {

        [PrimaryKey, AutoIncrement]
        [MaxLength(50)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        [MaxLength(50)]
        public int Corrects { get; set; }

        [MaxLength(50)]
        public int Incorrects { get; set; }

        [MaxLength(50)]
        public int Total { get; set; }



    }
}