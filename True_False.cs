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
using Newtonsoft.Json.Linq;
using Android.Graphics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Predict_App
{
    [Activity(Label = "True_False")]
    public class True_False : AppCompatActivity
    {

        string dificulty, category, number_Q, font = "Playball-Regular.ttf";
        string URL, Correct, wrong, Logged_name;
        RadioButton true_radio, false_radio;
        Button btn;
        TextView Title, Dificulty_txt, Category_txt, Type_txt, Question_txt, score_txt, number_Q_txt, Username_txt;
        int counter = 0;
        bool next, GameOver;
        User user;
        Database Database;
        UserLogged logged;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.True_False_layout);
            // Create your application here

            dificulty = Intent.GetStringExtra("dificulty" ?? "not recv");
            category = Intent.GetStringExtra("category" ?? "not recv");
            number_Q = Intent.GetStringExtra("question_num" ?? "not recv");

            btn = FindViewById<Button>(Resource.Id.btn);

            true_radio = FindViewById<RadioButton>(Resource.Id.true_radio);
            false_radio = FindViewById<RadioButton>(Resource.Id.false_radio);

            Title = FindViewById<TextView>(Resource.Id.Title);
            Dificulty_txt = FindViewById<TextView>(Resource.Id.Dificulty_txt);
            Category_txt = FindViewById<TextView>(Resource.Id.Category_txt);
            Type_txt = FindViewById<TextView>(Resource.Id.Type_txt);
            Question_txt = FindViewById<TextView>(Resource.Id.Question_txt);
            score_txt = FindViewById<TextView>(Resource.Id.score_txt);
            number_Q_txt = FindViewById<TextView>(Resource.Id.number_Q_txt);
            Username_txt = FindViewById<TextView>(Resource.Id.Username_txt);

            TextFonts(font, Title);
            TextFonts(font, Dificulty_txt);
            TextFonts(font, Category_txt);
            TextFonts(font, Type_txt);
            TextFonts(font, Question_txt);
            TextFonts(font, number_Q_txt);
            TextFonts(font, score_txt);
            TextFonts(font, Username_txt);


            btn.Click += Btn_Click;

           

            true_radio.Click += True_radio_Click;
            false_radio.Click += False_radio_Click;

            False_True();

            Database = new Database();
            foreach (var item in Database.SelectPlayerlogged(logged))
            {

                //Log.Info("Logged", item.UserNameLogged);
                
                Logged_name = $"{item.UserNameLogged}";

            }
            Username_txt.Text = $"Username: {Logged_name}";


            user = new User()
            {
                UserName = Logged_name


            };


           


        }


        public void TextFonts(string FontType, TextView txt_fonts)
        {

            Typeface txt = Typeface.CreateFromAsset(Assets, FontType);

            txt_fonts.SetTypeface(txt, TypefaceStyle.Normal);

        }


        private void False_radio_Click(object sender, EventArgs e)
        {


            if (false_radio.Checked)
            {
                false_radio.SetTextColor(Color.ParseColor("#BA5536"));
                true_radio.SetTextColor(Color.ParseColor("#46211A"));
            }
          

        }

        private void True_radio_Click(object sender, EventArgs e)
        {
            if (true_radio.Checked)
            {
                true_radio.SetTextColor(Color.ParseColor("#BA5536"));
                false_radio.SetTextColor(Color.ParseColor("#46211A"));
            }
           
        }

        public override void OnBackPressed()
        { }

        private void Btn_Click(object sender, EventArgs e)
        {

            
            if (GameOver)
            {
                Log.Info("Done:", $"Done!");
                btn.Text = $"Out!";
                StartActivity((new Intent(Application.Context, typeof(MainActivity))));

            }
            else
            {
                if (true_radio.Checked)
                {

                    if (next)
                    {

                        if (counter == int.Parse(number_Q))
                        {
                            Log.Info("Done:", $"Done!");
                            btn.Text = $"Done!";
                            GameOver = true;

                        }
                        else
                        {
                            next = false;
                            False_True();
                            btn.Text = $"Play!";
                            btn.Enabled = false;
                        }


                        
                    }
                    else
                    {
                        next = true;
                        btn.Text = $"Next!";
                        CheckAnswer(true_radio);
                    }



                }
                else if (false_radio.Checked)
                {

                    if (next)
                    {


                        if (counter == int.Parse(number_Q))
                        {
                            Log.Info("Done:", $"Done!");
                            btn.Text = $"Done!";
                            GameOver = true;

                        }
                        else
                        {
                            next = false;
                            btn.Text = $"Play!";
                            False_True();
                            btn.Enabled = false;
                        }


                        
                    }
                    else
                    {
                        next = true;
                        btn.Text = $"Next!";
                        CheckAnswer(false_radio);
                    }


                }

            }



            
           
        }

        private void SetCorrect()
        {


            foreach (var username in Database.SelectPlayer(user))
            {
                if (username.UserName.ToString() == Logged_name)
                {
                   
                     

                    int correct_new = username.Corrects;

                    correct_new += 1;

                    User update = new User()
                    {
                        UserName = Logged_name,
                        Corrects = correct_new,


                    };
                    Log.Info("corrects: ", $"{correct_new}");
                    Database.SetCorrects(update);
                    break;
                }


            }

        }

        private void SetIncorrect()
        {
         
            foreach (var username in Database.SelectPlayer(user))
            {
                if (username.UserName.ToString() == Logged_name)
                {
            
                  

                    int incorrects_news = username.Incorrects;

                    incorrects_news += 1;

                    User update = new User()
                    {
                        UserName = Logged_name,
                        Incorrects = incorrects_news,


                    };

                    Database.SetIncorrects(update);


                    Log.Info("Incorrects: ", $"{incorrects_news}");
                    break;
                }


            }


        }

        private void SetTotal()
        {

            foreach (var username in Database.SelectPlayer(user))
            {
                if (username.UserName.ToString() == Logged_name)
                {
        

                    int result = username.Corrects + username.Incorrects;



                    User update = new User()
                    {
                        UserName = Logged_name,
                        Total = result,


                    };

                    Database.SetTotal(update);


                    Log.Info("result: ", $"{result}");
                    break;
                }


            }




        }

        private void CheckAnswer(RadioButton chosen)
        {


            if (chosen.Text == Correct)
            {
                score_txt.Text = $"CORRECT";
                SetCorrect();

            }
            else
            {
                SetIncorrect();
      
                score_txt.Text = $"Incorrect \n  CHOSEN: {chosen.Text} \n CORRECT: {Correct}";
            }
            SetTotal();

        }

        private async Task False_True()
        {
            URL = $"https://opentdb.com/api.php?amount=1&type=boolean";


            switch (dificulty)
            {
                case "Any Dificulty":

                    break;
                default:
                    URL += $"&difficulty={dificulty}";
                    break;

            }

            switch (category)
            {
                case "Any Category":
                    break;
                default:

                    string URL_Category = "https://opentdb.com/api_category.php";
                    int Category_ID = 0;


                    HttpClient client_link = new HttpClient();

                    string All_Data_link = await client_link.GetStringAsync(URL_Category);
                    var data_link = JObject.Parse(All_Data_link);

                    foreach (var item in data_link["trivia_categories"])
                    {

                        if (category == item["name"].ToString())
                        {

                            Category_ID = (int)item["id"];
                            break;

                        }
                      

                    }

                    URL += $"&category={Category_ID}";
                    break;
            }


            HttpClient client = new HttpClient();

            string All_Data = await client.GetStringAsync(URL);
            var data = JObject.Parse(All_Data);



            foreach (var item in data["results"])
            {

   

                Dificulty_txt.Text = $"Difficulty: {item["difficulty"]}";
                Category_txt.Text = $"Category: {item["category"]}";
                Type_txt.Text = $"Type: {item["type"]}";


                string question = "";

                foreach (var letter in item["question"].ToString())
                {

       

                    if (letter.ToString().Contains(";") || letter.ToString().Contains("&") ||
                        letter.ToString().Contains("?") || letter.ToString().Contains("#")
                        )
                    {
                        continue;


                    }
                    else
                    {

                        question += letter.ToString();

                    }


                }

                question.Replace("quot", "");

                Question_txt.Text = $"question: \n {question}?";


                true_radio.Text = item["correct_answer"].ToString();
                false_radio.Text = item["incorrect_answers"][0].ToString();

                true_radio.Checked = false;
                false_radio.Checked = false;
                score_txt.Text = "";

                counter += 1;
                number_Q_txt.Text = $"{counter}/{number_Q}";
                btn.Enabled = true;
                false_radio.SetTextColor(Color.ParseColor("#46211A"));
                true_radio.SetTextColor(Color.ParseColor("#46211A"));
     
            }
        }

    }
}