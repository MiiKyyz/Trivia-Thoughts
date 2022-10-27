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
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Java.Util.Jar.Attributes;

namespace Predict_App
{
    public class Predictions
    {



        string country = "";
        ViewGroup container_ref;
        LayoutInflater inflater_ref;
        View view_ref, poster_ref;
        LinearLayout layout_ref;
        View loading_layout;

        public Predictions(LayoutInflater inflater=null, ViewGroup container = null, View view = null, View poster = null, LinearLayout layout = null)
        {
            view_ref = view;
            poster_ref = poster;
            layout_ref = layout;
            container_ref = container;
            inflater_ref = inflater;



        }



        public async Task PredictGender(TextView gender_txt, EditText Name)
        {
            string URL = $"https://api.genderize.io/?name={Name.Text}";


            var handle = new HttpClientHandler();

            HttpClient client = new HttpClient();

            try
            {

                string all_data = await client.GetStringAsync(URL);
                var data = JObject.Parse(all_data);
                float probability = (float)data["probability"] * 100;
                gender_txt.Text = $"Name: {data["name"]} \n Gender Predicted: {data["gender"]} \n Probability: {probability}%";

                Log.Info("data:", $"all:{data["gender"]}");

            }
            catch(Exception ex)
            {

                Log.Info("ERROR:", $"all:{ex.Message}");


            }


        }

        
        public async Task<string[]> CountryAsync(string country_code)
        {

            string URL = $"https://restcountries.com/v2/alpha/{country_code}";

            string img_url = "";


            
            string country="", img="";
            string[] list = new string[] { country, img };
            //var handle = new HttpClientHandler();

            HttpClient client = new HttpClient();

            try
            {

                string all_data = await client.GetStringAsync(URL);
                var data = JObject.Parse(all_data);

                //nation_txt.Text = $"Name: {data["name"]} \n Gender Predicted: {data["gender"]} \n Probability: {data["probability"]}";

                list[0] = data["name"].ToString();

                list[1] = data["flags"]["png"].ToString();
                



            }
            catch (Exception ex)
            {

                Log.Info("ERROR Country:", $"all:{ex.Message}");

                Loading("error");
            }
            Log.Info("miky", $"{list[0]}, url: {list[1]}");
            return list;


        }

        public void Panels(string Country="", Bitmap bitmap = null )
        {
            layout_ref = view_ref.FindViewById<LinearLayout>(Resource.Id.Layout_panel);


            poster_ref = inflater_ref.Inflate(Resource.Layout.panel,
                (ViewGroup)container_ref.FindViewById(Resource.Layout.panel), false);

            TextView txt_country = poster_ref.FindViewById<TextView>(Resource.Id.Country_txt);
            ImageView country_img = poster_ref.FindViewById<ImageView>(Resource.Id.img_country);

            country_img.SetImageBitmap(bitmap);
            txt_country.Text = $"{Country}" ;
            

            layout_ref.AddView(poster_ref);

        }




        private Bitmap ImageWeb(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                byte[] bytes = webClient.DownloadData(url);
                if (bytes != null && bytes.Length > 0)
                {
                    return BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
                }
            }
            return null;
        }


        public void Loading(string state)
        {


            switch (state)
            {

                case "loading":
                    layout_ref = view_ref.FindViewById<LinearLayout>(Resource.Id.Layout_panel);


                     loading_layout = inflater_ref.Inflate(Resource.Layout.loading_panel,
                        (ViewGroup)container_ref.FindViewById(Resource.Layout.loading_panel), false);

                    TextView txt_ = loading_layout.FindViewById<TextView>(Resource.Id.textView1);
                    ImageView img_ = loading_layout.FindViewById<ImageView>(Resource.Id.loading_img);
                    txt_.Text = "Loading...";

                    img_.SetBackgroundResource(Resource.Drawable.loading);


                    layout_ref.AddView(loading_layout);
                    break;
                case "done":


                    layout_ref.RemoveAllViews();
                    break;
                case "error":

                    layout_ref = view_ref.FindViewById<LinearLayout>(Resource.Id.Layout_panel);



                    loading_layout = inflater_ref.Inflate(Resource.Layout.loading_panel,
                        (ViewGroup)container_ref.FindViewById(Resource.Layout.loading_panel), false);

                    TextView txt = loading_layout.FindViewById<TextView>(Resource.Id.textView1);
                    ImageView img = loading_layout.FindViewById<ImageView>(Resource.Id.loading_img);
                    txt.Text = "Error";
                    
                    img.SetBackgroundResource(Resource.Drawable.error_logo);
                    layout_ref.AddView(loading_layout);

                    break;


            }


        }


        public async Task PredictNationality(EditText Name, TextView Display_Name)
        {

            string URL = $"https://api.nationalize.io?name={Name.Text}";
            


            //var handle = new HttpClientHandler();

            HttpClient client = new HttpClient();
            Loading("loading");
            try
            {

                string all_data = await client.GetStringAsync(URL);
                var data = JObject.Parse(all_data);



                //Log.Info("miky", $"{all_data}");
                Loading("done");
                Display_Name.Text = $"Name: {Name.Text}";
                Name.Text = "";
                foreach (var item in data["country"])
                {
                    float probability = (float)item["probability"] * 100;
                    string[] list = await CountryAsync($"{item["country_id"]}");
                    string txt_country = $"{list[0]} \n Probability: {probability}%";
                    //Log.Info("data:", $"country:{c} country Code:{item["country_id"]}, Probability: {item["probability"]}");
                    //Log.Info("miky", Country(item["country_id"].ToString()).ToString());
                    Bitmap bitmap = ImageWeb(list[1]);
                    Panels(txt_country, bitmap);
                }
                

            }
            catch (Exception ex)
            {
                Loading("error");
                Log.Info("ERROR:", $"all:{ex.Message}");


            }


        }

   
    }
}