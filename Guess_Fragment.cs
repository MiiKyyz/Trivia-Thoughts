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

namespace Predict_App
{
    [Obsolete]
    public class Guess_Fragment : AndroidX.Fragment.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        EditText question_num;
        TextView title, title2;
        Spinner spinner_category, spinner_dificulty, spinner_type;
        Button play_btn;
        string category, type, dificulty, username;
        Intent StartActivity;
        string font = "Playball-Regular.ttf";
        List<string> list_category = new List<string>() { "Any Category" };
        List<string> list_dificulty = new List<string>() { "Any Dificulty", "easy", "medium", "hard" };
        List<string> list_type = new List<string>() { "Select Type", "Multiple Choice", "True/False" };

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment

            View view = inflater.Inflate(Resource.Layout.guess_layout, container, false);

            question_num = view.FindViewById<EditText>(Resource.Id.question_num);
            title = view.FindViewById<TextView>(Resource.Id.title);
            title2 = view.FindViewById<TextView>(Resource.Id.title2);

            spinner_category = view.FindViewById<Spinner>(Resource.Id.spinner_category);

            spinner_dificulty = view.FindViewById<Spinner>(Resource.Id.spinner_dificulty);
            ArrayAdapter adapter_dificulty = new ArrayAdapter(Context, Resource.Layout.support_simple_spinner_dropdown_item, list_dificulty);
            spinner_dificulty.Adapter = adapter_dificulty;


            spinner_type = view.FindViewById<Spinner>(Resource.Id.spinner_type);
            ArrayAdapter adapter_type = new ArrayAdapter(Context, Resource.Layout.support_simple_spinner_dropdown_item, list_type);
            spinner_type.Adapter = adapter_type;


            TextFonts(font, title);
            TextFonts(font, title2);

            play_btn = view.FindViewById<Button>(Resource.Id.play_btn);

            play_btn.Click += Play_btn_Click;

            spinner_category.ItemSelected += Spinner_category_ItemSelected;
            spinner_dificulty.ItemSelected += Spinner_dificulty_ItemSelected;
            spinner_type.ItemSelected += Spinner_type_ItemSelected;


            _ = ListCategory(spinner_category);

            


            return view;

        }

        private void Spinner_type_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            type = list_type[e.Position];

        }

        private void Spinner_dificulty_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            dificulty = list_dificulty[e.Position];
       
        }

        private void Spinner_category_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            category = list_category[e.Position];
            
        }
        public void TextFonts(string FontType, TextView txt_fonts)
        {

            Typeface txt = Typeface.CreateFromAsset(Activity.Assets, FontType);

            txt_fonts.SetTypeface(txt, TypefaceStyle.Normal);

        }

        private void Play_btn_Click(object sender, EventArgs e)
        {

            

            if (question_num.Text != "")
            {
                int number = int.Parse(question_num.Text);
                if (number > 0  && number < 20)
                {
                    Play();
                }
                else
                {
                    Toast.MakeText(Context, $"Cannot be zero/greater than 20", ToastLength.Short).Show();
                }

                
                
            }
            else
            {

                Toast.MakeText(Context, $"Empty Box", ToastLength.Short).Show();

            }

            
        }
       

        private void Play()
        {
            //Type
            switch(type)
            {


                case "Multiple Choice":
                    StartActivity = new Intent(Application.Context, typeof(MultipleChoice));
                    StartActivity.PutExtra("category", category);
                    StartActivity.PutExtra("dificulty", dificulty);
                    StartActivity.PutExtra("question_num", question_num.Text.ToString());
                    StartActivity(StartActivity);
                    break;

                case "True/False":
                    StartActivity = new Intent(Application.Context, typeof(True_False));
                    StartActivity.PutExtra("category", category);
                    StartActivity.PutExtra("dificulty", dificulty);
                    StartActivity.PutExtra("question_num", question_num.Text.ToString());
                    StartActivity(StartActivity);
                    break;

                default:
                    Toast.MakeText(Context, $"Select One Type!", ToastLength.Short).Show();
                    break;
            }
        }


        public async Task ListCategory(Spinner category)
        {

            string URL = $"https://opentdb.com/api_category.php";
            var handler = new HttpClientHandler();
            HttpClient client = new HttpClient();


            try
            {
                
                string All_Data = await client.GetStringAsync(URL);
                var data = JObject.Parse(All_Data);

                foreach(var item in data["trivia_categories"])
                {

                    //Log.Info("Category", $"Name: {item["name"]} ID: {item["id"]}");
                    list_category.Add(item["name"].ToString());

                }
                ArrayAdapter adapter = new ArrayAdapter(Context, Resource.Layout.support_simple_spinner_dropdown_item, list_category);
                category.Adapter = adapter;
            }
            catch(Exception ex)
            {

                Log.Info("Category", $"{ex.Message}");

            }
        }
    }
}