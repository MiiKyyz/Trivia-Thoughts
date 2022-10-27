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

namespace Predict_App.Resources.layout
{
    [Activity(Label = "Resgistration", Theme = "@style/AppTheme.NoActionBar")]
    public class ResgistrationActivity : AppCompatActivity
    {


        Button  btn_resgistration;
        EditText user, pass, re_pass;
        Database DATA;
        User user_data;
        string font = "Playball-Regular.ttf";
        TextView title;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.registration_layout);

        
            btn_resgistration = FindViewById<Button>(Resource.Id.btn_Register);

            user = FindViewById<EditText>(Resource.Id.edit_Username);
            pass = FindViewById<EditText>(Resource.Id.edit_Password);
            re_pass = FindViewById<EditText>(Resource.Id.edit_re_Password);

            btn_resgistration.Click += Btn_resgistration_Click;
            // Create your application here

            title = FindViewById<TextView>(Resource.Id.title);


            DATA = new Database();

            TextFonts(font, title);

        }

        public void TextFonts(string FontType, TextView txt_fonts)
        {

            Typeface txt = Typeface.CreateFromAsset(Assets, FontType);

            txt_fonts.SetTypeface(txt, TypefaceStyle.Normal);

        }


        public void Registration()
        {


            if (user.Text == "" || pass.Text == "" || re_pass.Text == "")
            {



                Toast.MakeText(this, "missing informaiton!", ToastLength.Short).Show();


            }
            else if (pass.Text != re_pass.Text)
            {

                Toast.MakeText(this, "Password must be equal!", ToastLength.Short).Show();


            }
            else if (user.Text != "" || pass.Text != "" || re_pass.Text != "")
            {


                if (pass.Text == re_pass.Text)
                {
                    string copy = "";
                    foreach (var item in DATA.SelectPlayer(user_data))
                    {

                        if (item.UserName == user.Text)
                        {

                            copy = user.Text;


                        }

                        //Log.Info("miky", $"name:{item.UserName}, pass: {item.Password}");

                    }


                    if (copy != user.Text)
                    {

                        user_data = new User()
                        {

                            UserName = user.Text,
                            Password = pass.Text,
                            Corrects = 0,
                            Incorrects = 0,
                            Total = 0



                        };

                        DATA.InsertUser(user_data);

                        foreach (var item in DATA.SelectPlayer(user_data))
                        {



                            Log.Info("miky", $"name:{item.UserName}, pass: {item.Password}");

                        }


                        Toast.MakeText(this, "Account Created!", ToastLength.Short).Show();
                        StartActivity(new Intent(Application.Context, typeof(Login_Activity)));
                    }
                    else if (copy == user.Text)
                    {

                        Toast.MakeText(this, "Username In Used!", ToastLength.Short).Show();

                    }


                }
            }
        }


        private void Btn_resgistration_Click(object sender, EventArgs e)
        {

            if (pass.Text.Length >=7)
            {


                Registration();

            }
            else
            {

                Toast.MakeText(this, "Password must have 7 characters", ToastLength.Short).Show();


            }

            



        }
    }
}