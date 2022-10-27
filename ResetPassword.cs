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
using Android.Util;
using Android.Graphics;

namespace Predict_App
{
    [Activity(Label = "ResetPassword")]
    public class ResetPassword : AppCompatActivity
    {

        Database database;
        EditText Username_edit, new_pass, new_retype_pass;
        Button btn_verify, btn_update;
        string font = "Playball-Regular.ttf";
        TextView title;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.reset_password);
            // Create your application here


            Username_edit = FindViewById<EditText>(Resource.Id.Username_edit);
            new_pass = FindViewById<EditText>(Resource.Id.new_pass);
            new_retype_pass = FindViewById<EditText>(Resource.Id.new_retype_pass);

            btn_verify = FindViewById<Button>(Resource.Id.btn_verify);
            btn_update = FindViewById<Button>(Resource.Id.btn_update);

            title = FindViewById<TextView>(Resource.Id.title);

            database = new Database();


            btn_update.Enabled = false;
            new_pass.Enabled = false;
            new_retype_pass.Enabled = false;

            btn_verify.Click += Btn_verify_Click;
            btn_update.Click += Btn_update_Click;

            TextFonts(font, title);

        }

        private void Btn_update_Click(object sender, EventArgs e)
        {

            UpdatePass();

        }

        public void TextFonts(string FontType, TextView txt_fonts)
        {

            Typeface txt = Typeface.CreateFromAsset(Assets, FontType);

            txt_fonts.SetTypeface(txt, TypefaceStyle.Normal);

        }

        private void Btn_verify_Click(object sender, EventArgs e)
        {

            VerifyUser();

        }

        public void UpdatePass()
        {

            


            if(new_pass.Text != "" || new_retype_pass.Text != "")
            {

                if(new_pass.Text == new_retype_pass.Text)
                {

                    User u = new User()
                    {
                        UserName= Username_edit.Text,
                        Password = new_pass.Text



                    };

                    database.UpdatepPassword(u);


                    foreach (var user in database.SelectPlayer(u))
                    {


                        Log.Info("User:", $"User:{user.UserName} Pass:{user.Password}");


                    }


                    Toast.MakeText(this, "Password Changed!", ToastLength.Short).Show();
                    StartActivity(new Intent(Application.Context, typeof(Login_Activity)));
                }
                else
                {
                    Toast.MakeText(this, "Password must be equal!", ToastLength.Short).Show();
                }



            }
            else
            {

                Toast.MakeText(this, "Empty!", ToastLength.Short).Show();
            }

        }

        public void VerifyUser()
        {
            User u = new User();
            bool notify = false;


            foreach(var user in database.SelectPlayer(u))
            {

                if(Username_edit.Text != "")
                {
                    if (Username_edit.Text == user.UserName)
                    {
                        Log.Info("User:", $"User:{user.UserName} Pass:{user.Password}");
                        //Toast.MakeText(this, "Found!", ToastLength.Short).Show();
                        btn_update.Enabled = true;
                        new_pass.Enabled = true;
                        new_retype_pass.Enabled = true;
                        btn_verify.Enabled = false;
                        Username_edit.Enabled = false;
                        notify = true;
                        break;


                    }
                    else if(Username_edit.Text != user.UserName)
                    {

                     
                        notify = false;
                    }
                }
                else
                {
                    Toast.MakeText(this, "Empty!", ToastLength.Short).Show();
                }

            }

            if (!notify)
            {

                Toast.MakeText(this, "Account not found!", ToastLength.Short).Show();
            }
        }


    }
}