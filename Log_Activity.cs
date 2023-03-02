using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using Android.Graphics;
using Android.Util;
using System.Threading.Tasks;
using System.Threading;

namespace Game_Center
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class Log_Activity : AppCompatActivity
    {

        private AutoCompleteTextView Username_Log, Password_Log;
        private static Button Loging, Register, Forgot;
        private static TextView TitleLog;
        private AudioTrivia audioTrivia;
        private Intent intent;
        private ViewGroup viewGroup;
        private RelativeLayout relative_panel;
        private static string CODE = "";
        public static float x, y, x_s, y_s;
        private Database database = new Database();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Log_layout);

            

            Username_Log = FindViewById<AutoCompleteTextView>(Resource.Id.Username_Log);
            Password_Log = FindViewById<AutoCompleteTextView>(Resource.Id.Password_Log);
            TitleLog = FindViewById<TextView>(Resource.Id.TitleLog);
            Loging = FindViewById<Button>(Resource.Id.Log);
            Register = FindViewById<Button>(Resource.Id.Register);
            Forgot = FindViewById<Button>(Resource.Id.Forgot);
            relative_panel = FindViewById<RelativeLayout>(Resource.Id.relative_panel);


            audioTrivia = new AudioTrivia(this);

            Loging.Click += Log_Click;
            Register.Click += Register_Click;
            Forgot.Click += Forgot_Click;


            if(Database.GetAll().Count == 0)
            {

                User user = new User()
                {

                    Name = "Ganymedes1561",
                    Password = "Yankees1224@",
                    FontUser = "Teko-Regular.ttf"

                };


                Database.InsertUser(user);




            }
            else
            {
              
      
            }



        }

       
       


        public void ChangeFont(string FontType, TextView txt_fonts, List<Button> btn)
        {

            Typeface txt = Typeface.CreateFromAsset(Assets, FontType);

            txt_fonts.SetTypeface(txt, TypefaceStyle.Normal);

            btn[0].SetTypeface(txt, TypefaceStyle.Normal);
            btn[1].SetTypeface(txt, TypefaceStyle.Normal);
            btn[2].SetTypeface(txt, TypefaceStyle.Normal);

        }

        private void Forgot_Click(object sender, EventArgs e)
        {
            CODE = "DONE";
            audioTrivia.pressButton();
            intent = new Intent(this, typeof(FogotPass_Activity));
          
            StartActivity(intent);
        }

        private void Register_Click(object sender, EventArgs e)
        {
            CODE = "DONE";
            audioTrivia.pressButton();
            intent = new Intent(this, typeof(Register_Activity));
            StartActivity(intent);
        }

        private void Log_Click(object sender, EventArgs e)
        {
            audioTrivia.pressButton();
    
            string user_existed="", pass_existed="";

            try
            {
                var data = Database.GetAll();
                Android.Util.Log.Info("data", $"{Username_Log.Text}");
                Android.Util.Log.Info("data", $"{Password_Log.Text}");
                user_existed = data.Find(e => e.Name == Username_Log.Text).Name;
                pass_existed = data.Find(e => e.Password == Password_Log.Text).Password;
                Database.Userlogged = user_existed;
                Username_Log.Text = "";
                Password_Log.Text = "";
                CODE = "DONE";
                intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            }
            catch(Exception ex) 
            {
                Android.Util.Log.Info("data", $"{ex.Message}");
                

                ToastManager.ToastStyble(this, viewGroup, "Username/Password not found!", Resource.Drawable.icon_wrong, Resource.Drawable.toast_wrong);

                Password_Log.Text = "";
            }
        }
    }
}