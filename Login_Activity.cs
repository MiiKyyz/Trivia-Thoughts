using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroidX.AppCompat.App;
using Predict_App.Resources.layout;
using Android.Util;
using Android.Service.Autofill;
using Android.Graphics;

namespace Predict_App
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true, Icon = "@drawable/logo")]
    public class Login_Activity : AppCompatActivity
    {

        Button btn_login, btn_resgistration, Reset_Password;
        EditText user, pass;
        Database database;
        User user_data;
        UserLogged log;
        string font = "Playball-Regular.ttf";
        TextView title, link;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login_layout);


            btn_login = FindViewById<Button>(Resource.Id.btn_login);
            Reset_Password = FindViewById<Button>(Resource.Id.Reset_Password);
            btn_resgistration = FindViewById<Button>(Resource.Id.Sign_Up);

            user = FindViewById<EditText>(Resource.Id.edit_Username);
            pass = FindViewById<EditText>(Resource.Id.edit_Password);
            title = FindViewById<TextView>(Resource.Id.title);
            link = FindViewById<TextView>(Resource.Id.link);


            btn_login.Click += Btn_login_Click;
            btn_resgistration.Click += Btn_resgistration_Click;
            Reset_Password.Click += Reset_Password_Click;

            database = new Database();
            log = new UserLogged()
            { };
            database.DeleteData(log);

            database = new Database();

            TextFonts(font, title);
            TextFonts(font, link);

        }
        public void TextFonts(string FontType, TextView txt_fonts)
        {

            Typeface txt = Typeface.CreateFromAsset(Assets, FontType);

            txt_fonts.SetTypeface(txt, TypefaceStyle.Normal);

        }

        private void Reset_Password_Click(object sender, EventArgs e)
        {
            StartActivity(new Intent(Application.Context, typeof(ResetPassword)));
        }

        private void Btn_resgistration_Click(object sender, EventArgs e)
        {
            StartActivity(new Intent(Application.Context, typeof(ResgistrationActivity)));
        }

        private void Btn_login_Click(object sender, EventArgs e)
        {
            
            Login();
        }


        public void Login()
        {
            user_data = new User();
         


            bool notify = false;
            foreach(var item in database.SelectPlayer(user_data))
            {
                if (user.Text == item.UserName && pass.Text == item.Password)
                {

              
                    Toast.MakeText(this, "Welcome!", ToastLength.Short).Show();
                    notify = true;

                    log.UserNameLogged = item.UserName;
                    database.InsertUserLogged(log);

                    foreach (var logER in database.SelectPlayerlogged(log))
                    {

                        Log.Info("lOGGED: ", logER.UserNameLogged);


                    }

                 

                    Intent next = new Intent(Application.Context, typeof(MainActivity));
                    


                    StartActivity(next);
                    
                    break;

                }
                else
                {
                    notify = false;
                }


            }

            if (!notify)
            {

                Toast.MakeText(this, "User was not found", ToastLength.Short).Show();

            }


        }


    }
}