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
    public class UserLogged
    {

        [PrimaryKey, AutoIncrement]
        [MaxLength(50)]
        public string UserNameLogged { get; set; }

    }
}