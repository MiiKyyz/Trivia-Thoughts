using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Android.Content.ClipData;
using static Java.Util.Jar.Attributes;

namespace Predict_App
{
    [Obsolete]
    public class SettingsFragments : AndroidX.Fragment.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

       
        View view;
        Database Database;
        UserLogged logged;
        User user;
        string Logged_name;
        TextView Name_user, Corrects_user, Incorrects_user, Total_user, title;
        string font = "Playball-Regular.ttf";
    
        //TableRow rows;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
           
            view = inflater.Inflate(Resource.Layout.settings_layout, container, false);

            Name_user = view.FindViewById<TextView>(Resource.Id.Name_user);
            Corrects_user = view.FindViewById<TextView>(Resource.Id.Corrects_user);
            Incorrects_user = view.FindViewById<TextView>(Resource.Id.Incorrects_user);
            Total_user = view.FindViewById<TextView>(Resource.Id.Total_user);
            title = view.FindViewById<TextView>(Resource.Id.title);


            Database = new Database();
            foreach (var item in Database.SelectPlayerlogged(logged))
            {

               
                //Log.Info("Logged", item.UserNameLogged);
                Logged_name = $"{item.UserNameLogged}";
            }


            user = new User()
            {
                UserName = Logged_name


            };

            Log.Info("Logged", Logged_name);

            foreach (var user in Database.SelectPlayer(user))
            {

                if(user.UserName.ToString() == Logged_name)
                {
                    Name_user.Text = $"Username: {user.UserName}";
                    Corrects_user.Text = $"Corrects: {user.Corrects}";
                    Incorrects_user.Text = $"Incorrects: {user.Incorrects}";
                    Total_user.Text = $"Total: {user.Total}";
                    break;
                }


                

                
            }

            TextFonts(font, Name_user);
            TextFonts(font, Corrects_user);
            TextFonts(font, Incorrects_user);
            TextFonts(font, Total_user);
            TextFonts(font, title);
          
            return view;



        }

        public void TextFonts(string FontType, TextView txt_fonts)
        {

            Typeface txt = Typeface.CreateFromAsset(Activity.Assets, FontType);

            txt_fonts.SetTypeface(txt, TypefaceStyle.Normal);

        }








    }
}