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
using static Android.Icu.Text.CaseMap;

namespace Predict_App
{
    [Obsolete]
    public class NationalityActivity : AndroidX.Fragment.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        View view, poster;
        LinearLayout layout;
        LayoutInflater inflater_ref;
        ViewGroup container_ref;
        EditText Name_txt;
        Button btn_search;
        Predictions predict;
        TextView Display_Name, title;
        string font = "Playball-Regular.ttf";
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            inflater_ref = inflater;
            container_ref = container;
           
            // Use this to return your custom view for this Fragment

            view = inflater.Inflate(Resource.Layout.nationality_layout, container, false);
            Name_txt = view.FindViewById<EditText>(Resource.Id.Name_txt);
            btn_search = view.FindViewById<Button>(Resource.Id.btn_search);
            title = view.FindViewById<TextView>(Resource.Id.title);
            Display_Name = view.FindViewById<TextView>(Resource.Id.Display_Name);
            btn_search.Click += Btn_search_Click;

            predict = new Predictions(inflater_ref, container_ref, view, poster, layout);

            TextFonts(font, Display_Name);
            TextFonts(font, title);
            ButtonFonts(font, btn_search);
            EditFonts(font, Name_txt);


            return view;

        }
        private void Btn_search_Click(object sender, EventArgs e)
        {
            predict.PredictNationality(Name_txt, Display_Name);
        }

        public void TextFonts(string FontType, TextView txt_fonts)
        {

            Typeface txt = Typeface.CreateFromAsset(Activity.Assets, FontType);

            txt_fonts.SetTypeface(txt, TypefaceStyle.Normal);

        }

        public void ButtonFonts(string FontType, Button txt_fonts)
        {

            Typeface txt = Typeface.CreateFromAsset(Activity.Assets, FontType);

            txt_fonts.SetTypeface(txt, TypefaceStyle.Normal);

        }

        public void EditFonts(string FontType, EditText txt_fonts)
        {

            Typeface txt = Typeface.CreateFromAsset(Activity.Assets, FontType);

            txt_fonts.SetTypeface(txt, TypefaceStyle.Normal);

        }



    }
}