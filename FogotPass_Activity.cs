using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using AndroidX.AppCompat.App;
using System.Collections.Generic;


namespace Game_Center
{
    [Activity(Label = "FogotPass_Activity", Theme = "@style/AppTheme.NoActionBar")]
    public class FogotPass_Activity : AppCompatActivity
    {
        private static AutoCompleteTextView User, New_Pass, new_repass;
        private static Button Verify, change;
        private TextView ForgotTitle;
        private User user = new User();
        private ViewGroup viewGroup;
        private AudioTrivia audioTrivia;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ForgotPass_layout);


            User = FindViewById<AutoCompleteTextView>(Resource.Id.User);
            New_Pass = FindViewById<AutoCompleteTextView>(Resource.Id.New_Pass);
            new_repass = FindViewById<AutoCompleteTextView>(Resource.Id.new_repass);
            ForgotTitle = FindViewById<TextView>(Resource.Id.ForgotTitle);
            Verify = FindViewById<Button>(Resource.Id.Verify);
            change = FindViewById<Button>(Resource.Id.change);

            

            change.Enabled = false;
            New_Pass.Enabled = false;
            new_repass.Enabled = false;

            audioTrivia = new AudioTrivia(this);

            Verify.Click += Verify_Click;


            change.Click += Change_Click;


            foreach(var data in Database.GetAll())
            {

                Log.Info($"User:{data.Id}", $"name: {data.Name}, Lastname: {data.Lastname}, Password:{data.Password}");


            }


        }
        public void ChangeFont(string FontType, TextView txt_fonts, List<Button> btn)
        {

            Typeface txt = Typeface.CreateFromAsset(Assets, FontType);

            txt_fonts.SetTypeface(txt, TypefaceStyle.Normal);



            btn[0].SetTypeface(txt, TypefaceStyle.Normal);
            btn[1].SetTypeface(txt, TypefaceStyle.Normal);

        }
        private void Change_Click(object sender, EventArgs e)
        {

          
            string pass = "";

            pass = Grammar(New_Pass.Text);

            if (pass != null)
            {
                if(pass.Length >= 6)
                {
                    if (pass == new_repass.Text)
                    {
                        Log.Info($"User:{user.Id}", $"name: {user.Name}");
                        user.Password = pass;

                        Database.UpdateData(user);

                        change.Enabled = false;
                        New_Pass.Enabled = false;
                        new_repass.Enabled = false;


                        Verify.Enabled = true;
                        User.Enabled = true;

                        New_Pass.Text = "";
                        new_repass.Text = "";
                        User.Text = "";

                        audioTrivia.ForgotPassCorrectSound();
                       
                        ToastManager.ToastStyble(this, viewGroup, "Password Changed!", Resource.Drawable.icon_correct, Resource.Drawable.toast_correct);

                        foreach (var data in Database.GetAll())
                        {

                            Log.Info($"User:{data.Id}", $"name: {data.Name}, Lastname: {data.Lastname}, Password:{data.Password}");


                        }
                    }
                    else
                    {

                        audioTrivia.ForgotPassWrongSound();
                        ToastManager.ToastStyble(this, viewGroup, "Password must be Equal", Resource.Drawable.icon_wrong, Resource.Drawable.toast_wrong);
                        
                    }

                }
                else
                {
                    audioTrivia.ForgotPassWrongSound();
                    ToastManager.ToastStyble(this, viewGroup, "Password must 6 characters or more", Resource.Drawable.icon_wrong, Resource.Drawable.toast_wrong);
                   

                }
                
            }
        }

        private string Grammar(string password)
        {


            if (password.Contains(" "))
            {
                audioTrivia.ForgotPassWrongSound();
            
                ToastManager.ToastStyble(this, viewGroup, "Username/password cannot have spaces", Resource.Drawable.icon_wrong, Resource.Drawable.toast_wrong);

                return null;
            }
            else
            {

                return  password ;
            }
        }

        private void Verify_Click(object sender, EventArgs e)
        {
            string existed = "";
            audioTrivia.pressButton();
            try
            {
                user.Name = User.Text;

                var data = Database.GetAll();
                existed = data.Find(e => e.Name == user.Name).Name;


                change.Enabled = true;
                New_Pass.Enabled = true;
                new_repass.Enabled = true;


                Verify.Enabled = false;
                User.Enabled = false;

            }
            catch
            {
                audioTrivia.ForgotPassWrongSound();
                existed = "";
   
                ToastManager.ToastStyble(this, viewGroup, "User was not found!", Resource.Drawable.icon_wrong, Resource.Drawable.toast_wrong);

            }
        }
    }
}