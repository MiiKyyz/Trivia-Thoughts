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
using static AndroidX.RecyclerView.Widget.RecyclerView;
using static System.Net.Mime.MediaTypeNames;

namespace Game_Center
{
    public class Settings_Activity : AndroidX.Fragment.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        
        public static Dictionary<string, string> Fonts = new Dictionary<string, string> {


            {"Normal","OpenSans-VariableFont_wdth,wght.ttf" },
            {"Source Code Pro","SourceCodePro-VariableFont_wght.ttf" },
            {"Signika Negative","SignikaNegative-VariableFont_wght.ttf" },
            {"Exo 2","Exo2-VariableFont_wght.ttf" },
            {"Teko","Teko-Regular.ttf" },
            {"Fjalla One","FjallaOne-Regular.ttf" },
            {"Righteous","Righteous-Regular.ttf" },
            {"Rubik Wet Paint","RubikWetPaint-Regular.ttf" },
            {"Rubik Vinyl","RubikWetPaint-Regular.ttf" },
            {"Rubik Bubbles","RubikBubbles-Regular.ttf" },
            {"Rubik 80s Fade","Rubik80sFade-Regular.ttf" },
            {"Rubik Gemstones","RubikGemstones-Regular.ttf" },
            {"Rubik Glitch","RubikGlitch-Regular.ttf" },
            {"Chakra Petch","ChakraPetch-Regular.ttf" },
            {"Cinzel","Cinzel-VariableFont_wght.ttf" },
            {"Orbitron","Orbitron-VariableFont_wght.ttf" },
            {"Gloria Hallelujah","GloriaHallelujah-Regular.ttf" },
            {"Press Start 2P","PressStart2P-Regular.ttf" },



        };

        private View view;
        private Button Log_out;
        private TextView Title_settings, font_label;
        private Spinner FontSpinner;
        private AudioTrivia audioTrivia;
        private ViewGroup viewGroup;
        private bool ToastShow;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            view = inflater.Inflate(Resource.Layout.Settings_Layout, container, false);

            Log_out = view.FindViewById<Button>(Resource.Id.Log_out);
            FontSpinner = view.FindViewById<Spinner>(Resource.Id.FontSpinner);
            Title_settings = view.FindViewById<TextView>(Resource.Id.Title_settings);
            font_label = view.FindViewById<TextView>(Resource.Id.font_label);

            //ArrayAdapter adapter = new ArrayAdapter(Context, Android.Resource.Layout.SimpleSpinnerDropDownItem, Fonts.Keys.ToArray());
            AdapterFont adapterFont = new AdapterFont(Context, Fonts.Keys.ToArray(), Fonts.Values.ToArray(), Activity);
         
            FontSpinner.Adapter = adapterFont;

            FontSpinner.ItemSelected += FontSpinner_ItemSelected;
          


            audioTrivia = new AudioTrivia(Context);


            string FontSaved = Database.GetAll().Find(e => e.Name == Database.Userlogged).FontUser;
            int indexSaved = GetIndex(FontSaved, Fonts.Values.ToList());

            FontSpinner.SetSelection(indexSaved);

            ChangeFont(FontSaved, new TextView[] { Title_settings, font_label }, Log_out);


            Log_out.Click += Log_out_Click;

            return view;
        }

      

        public void ChangeFont(string FontType, TextView[] txt_fonts, Button btn)
        {

            Typeface txt = Typeface.CreateFromAsset(Activity.Assets, FontType);

            txt_fonts[0].SetTypeface(txt, TypefaceStyle.Normal);
            txt_fonts[1].SetTypeface(txt, TypefaceStyle.Normal);
            btn.SetTypeface(txt, TypefaceStyle.Normal);

        }

        private void FontSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            

            if (ToastShow)
            {
                ToastManager.ToastStyble(Context, viewGroup, "Font Changed!", Resource.Drawable.icon_correct, Resource.Drawable.toast_correct);
                audioTrivia.TriviaCorrectSound();

                string font = Fonts[$"{Fonts.Keys.ToArray()[e.Position]}"];
                User user = new User()
                {
                    Name = Database.Userlogged,
                    FontUser = font
                };
                Database.SaveFont(user);
                ChangeFont(font, new TextView[] { Title_settings, font_label }, Log_out);

            }

            ToastShow = true;


        }

        private void Log_out_Click(object sender, EventArgs e)
        {
            audioTrivia.pressButton();

            Intent intent = new Intent(Context, typeof(Log_Activity));
            StartActivity(intent);


        }
        private int GetIndex(string name, List<string> values)
        {

            int counter = 0;

            foreach (string v in values)
            {

                if (v == name)
                {

                    break;

                }

                counter++;

            }


            return counter;
        }

    }
}