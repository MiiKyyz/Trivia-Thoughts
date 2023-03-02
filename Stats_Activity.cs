using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_Center
{
    public class Stats_Activity : AndroidX.Fragment.App.Fragment
    {

        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        



            // Create your fragment here
        }
        private TextView InCorrects_answers, Corrects_answers, Name_stats, Title_Stats, NoSelected_answers, Ratio_winning;

        private View view;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return 

            view = inflater.Inflate(Resource.Layout.Stats_Layout, container, false);


            InCorrects_answers = view.FindViewById<TextView>(Resource.Id.InCorrects_answers);
            Corrects_answers = view.FindViewById<TextView>(Resource.Id.Corrects_answers);
            Name_stats = view.FindViewById<TextView>(Resource.Id.Name_stats);
            Title_Stats = view.FindViewById<TextView>(Resource.Id.Title_Stats);
            NoSelected_answers = view.FindViewById<TextView>(Resource.Id.NoSelected_answers);
            Ratio_winning = view.FindViewById<TextView>(Resource.Id.Ratio_winning);


            Name_stats.Text = $"Username: {Database.Userlogged}";
            int corrects = Database.GetAll().Find(e => e.Name == Database.Userlogged).Corrects;
            int incorrects = Database.GetAll().Find(e => e.Name == Database.Userlogged).Incorrects;
            int no_selected = Database.GetAll().Find(e => e.Name == Database.Userlogged).NoSelection;
            string FontSaved = Database.GetAll().Find(e => e.Name == Database.Userlogged).FontUser;

            float ratio = (float)corrects / (float)incorrects;

            Corrects_answers.Text = $"Corrects: {corrects}";
            InCorrects_answers.Text = $"Incorrects: {incorrects}";
            NoSelected_answers.Text = $"No Answer: {no_selected}";
            Ratio_winning.Text = $"Ratio: {ratio} WR";
            ChangeFont(FontSaved, new List<TextView> { InCorrects_answers, Corrects_answers, Name_stats, Title_Stats, NoSelected_answers, Ratio_winning });
            return view;
        }

        public void ChangeFont(string FontType, List<TextView> txt_fonts)
        {

            Typeface txt = Typeface.CreateFromAsset(Activity.Assets, FontType);


            foreach(TextView txt_font in txt_fonts)
            {

                txt_font.SetTypeface(txt, TypefaceStyle.Normal);
            }

        }

    }
}