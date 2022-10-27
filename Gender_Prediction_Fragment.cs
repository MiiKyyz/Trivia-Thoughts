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
    public class Gender_Prediction_Fragment : AndroidX.Fragment.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }



  
        Button Predict_btn;
        TextView Display_Predict, title;
        EditText Name_edit;
        List<string> Predictions_list;
        Predictions predict = new Predictions();
        string font = "Playball-Regular.ttf";
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment

            View view = inflater.Inflate(Resource.Layout.gender_prediction_layout, container, false);


         

            Predict_btn = view.FindViewById<Button>(Resource.Id.Predict_btn);

            title = view.FindViewById<TextView>(Resource.Id.title);

            Display_Predict = view.FindViewById<TextView>(Resource.Id.Display_Predict);

            Name_edit = view.FindViewById<EditText>(Resource.Id.Name_edit);



            Predict_btn.Click += Predict_btn_Click;

            TextFonts(font, Display_Predict);
            TextFonts(font, title);

            ButtonFonts(font, Predict_btn);
            EditFonts(font, Name_edit);



            return view;

          
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



        private void Predict_btn_Click(object sender, EventArgs e)
        {
            if(Display_Predict.Text == "")
            {
                


                Toast.MakeText(Context, "Type a name", ToastLength.Short).Show();
            }
            else
            {
                predict.PredictGender(Display_Predict, Name_edit);

                Name_edit.Text = "";



            }
            

        }
    }
}