using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using AndroidX.AppCompat.App;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Android.Util;
using System.Threading;
using Android.Graphics;
using Java.Util.Zip;

namespace Predict_App
{
    [Activity(Label = "MultipleChoice")]
    public class MultipleChoice : AppCompatActivity
    {

        string dificulty, category, number_Q, font= "ebrimabd.ttf";
        string URL, Correct, Logged_name;
        RadioButton A1, A2, A3, A4;
        TextView Title, Dificulty_txt, Category_txt, Type_txt, Question_txt, score_txt, number_Q_txt, Username_txt;
        List<string> list = new List<string>();
        Button btn;
        int counter = 0;
        bool next, GameOver;
        Database Database;
        UserLogged logged;
        User user;
        MainActivity mainActivity = new MainActivity();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MultipleChoice_layout);
            // Create your application here


            dificulty = Intent.GetStringExtra("dificulty" ?? "not recv");
            category = Intent.GetStringExtra("category" ?? "not recv");
            number_Q = Intent.GetStringExtra("question_num" ?? "not recv");

            btn = FindViewById<Button>(Resource.Id.btn_play);

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



            A1 = FindViewById<RadioButton>(Resource.Id.A1);
            A2 = FindViewById<RadioButton>(Resource.Id.A2);
            A3 = FindViewById<RadioButton>(Resource.Id.A3);
            A4 = FindViewById<RadioButton>(Resource.Id.A4);

            A1.Click += A1_Click;
            A2.Click += A2_Click;
            A3.Click += A3_Click;
            A4.Click += A4_Click;

            btn.Click += Btn_Click;


            MultipleAsync();

            Database = new Database();
            foreach (var item in Database.SelectPlayerlogged(logged))
            {

                //og.Info("Logged", item.UserNameLogged);
                
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



        private void A4_Click(object sender, EventArgs e)
        {
            if (A4.Checked)
            {
                A4.SetTextColor(Color.ParseColor("#BA5536"));
                A3.SetTextColor(Color.ParseColor("#46211A"));
                A2.SetTextColor(Color.ParseColor("#46211A"));
                A1.SetTextColor(Color.ParseColor("#46211A"));


            }
        }

        private void A3_Click(object sender, EventArgs e)
        {
            if (A3.Checked)
            {
                A3.SetTextColor(Color.ParseColor("#BA5536"));
                A4.SetTextColor(Color.ParseColor("#46211A"));
                A2.SetTextColor(Color.ParseColor("#46211A"));
                A1.SetTextColor(Color.ParseColor("#46211A"));


            }
        }

        private void A2_Click(object sender, EventArgs e)
        {
            if (A2.Checked)
            {
                A2.SetTextColor(Color.ParseColor("#BA5536"));
                A3.SetTextColor(Color.ParseColor("#46211A"));
                A4.SetTextColor(Color.ParseColor("#46211A"));
                A1.SetTextColor(Color.ParseColor("#46211A"));


            }
        }

        private void A1_Click(object sender, EventArgs e)
        {
            if (A1.Checked)
            {
                A1.SetTextColor(Color.ParseColor("#BA5536"));
                A2.SetTextColor(Color.ParseColor("#46211A"));
                A3.SetTextColor(Color.ParseColor("#46211A"));
                A4.SetTextColor(Color.ParseColor("#46211A"));
                


            }
        }

        public override void OnBackPressed()
        {}

        private void SetCorrect()
        {
            

            foreach (var username in Database.SelectPlayer(user))
            {
                if (username.UserName.ToString() == Logged_name)
                {
                    //Log.Info("corrects: ", $"{username.UserName}");
                    int new_correct = username.Corrects;
                    Log.Info("corrects before: ", $"{new_correct}");
                    new_correct += 1;
                    Log.Info("corrects after: ", $"{new_correct}");
                    User update = new User()
                    {
                        UserName = Logged_name,
                        Corrects = new_correct,


                    };
                    //Log.Info("corrects: ", $"{new_correct}");
                    Database.SetCorrects(update);
                    break;
                }


            }

        }

        private void SetIncorrect()
        {
            //Log.Info("Inco: ", $"{Logged_name}");
            foreach (var username in Database.SelectPlayer(user))
            {
                if (username.UserName.ToString() == Logged_name)
                {
                    //Log.Info("Incorrects: ", $"{username.UserName}");
                    int new_Incorrects = username.Incorrects;
                    Log.Info("Incorrects before: ", $"{new_Incorrects}");
                    new_Incorrects += 1;
                    Log.Info("Incorrects before: ", $"{new_Incorrects}");
                    User update = new User()
                    {
                        UserName = Logged_name,
                        Incorrects = new_Incorrects,


                    };

                    Database.SetIncorrects(update);


                    //Log.Info("Incorrects: ", $"{new_Incorrects}");
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
                    //Log.Info("Incorrects: ", $"{username.UserName}");

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

        private void CheckAnswer( RadioButton chosen)
        {


            if(chosen.Text == Correct)
            {

                //Log.Info("Correct", $"CHOSEN:{chosen.Text}, CORRECT:{Correct}");
                score_txt.Text = $"CORRECT";
              
                SetCorrect();
            }
            else
            {

                //Log.Info("InCorrect", $"chosen:{chosen.Text}, correct:{Correct}");
                score_txt.Text = $"Incorrect \n \n CHOSEN: {chosen.Text} \n \n CORRECT: {Correct}";
        
                SetIncorrect();
            }
            SetTotal();

        }

        private async void Btn_Click(object sender, EventArgs e)
        {
            
            if (GameOver)
            {

                Log.Info("Done:", $"Done!");
                btn.Text = $"Out!";
                StartActivity((new Intent(Application.Context, typeof(MainActivity))));

            }

            else 
            {

                if (A1.Checked)
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
                            btn.Text = $"Play!";
                            next = false;
                            MultipleAsync();
                            btn.Enabled = false;
                        }


                            
                    }
                    else
                    {
                        next = true;
                        btn.Text = $"Next!";
                        CheckAnswer(A1);
                    }


                    
                }
                else if (A2.Checked)
                {

                    if (next)
                    {
                        if (counter == int.Parse(number_Q))
                        {
                            btn.Text = $"Done!";
                            Log.Info("Done:", $"Done!");
                            GameOver = true;
                        }
                        else
                        {
                            btn.Text = $"Play!";
                            next = false;
                            MultipleAsync();
                            btn.Enabled = false;
                        }
                        
                    }
                    else
                    {
                        next = true;
                        btn.Text = $"Next!";
                        CheckAnswer(A2);
                    }

                    
                }
                else if (A3.Checked)
                {
                    if (next)
                    {
                        if (counter == int.Parse(number_Q))
                        {
                            btn.Text = $"Done!";
                            Log.Info("Done:", $"Done!");
                            GameOver = true;
                        }
                        else
                        {
                            btn.Text = $"Play!";
                            next = false;
                            MultipleAsync();
                            btn.Enabled = false;
                        }
                        
                    }
                    else
                    {
                        next = true;
                        btn.Text = $"Next!";
                        CheckAnswer(A3);
                    }

                    
                }
                else if (A4.Checked)
                {
                    if (next)
                    {
                        if (counter == int.Parse(number_Q))
                        {
                            btn.Text = $"Done!";
                            Log.Info("Done:", $"Done!");
                            GameOver = true;
                        }
                        else
                        {
                            btn.Text = $"Play!";
                            next = false;
                            MultipleAsync();
                            btn.Enabled = false;
                        }
                        
                    }
                    else
                    {
                        next = true;
                        btn.Text = $"Next!";
                        CheckAnswer(A4);
                    }

                    
                }
            }

            

        }

            private async Task MultipleAsync()
        {

            A1.Checked = false;
            A2.Checked = false;
            A3.Checked = false;
            A4.Checked = false;

            URL = $"https://opentdb.com/api.php?amount=1&type=multiple";
            score_txt.Text = "";


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
            
            //Log.Info("DATA:", data.ToString());

            foreach(var item in data["results"])
            {

         

                Dificulty_txt.Text = $"difficulty: {item["difficulty"]}";
                Category_txt.Text = $"category: {item["category"]}";
                Type_txt.Text = $"type: {item["type"]}";

                string question = "";

                

                foreach (var letter in item["question"].ToString())
                {

                    //Log.Info("letter", letter.ToString());

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

                Correct = "";


                foreach(var good in item["correct_answer"].ToString())
                {

           
                    if (good.ToString().Contains(";") || good.ToString().Contains("&") ||
                       good.ToString().Contains("?") || good.ToString().Contains("#")
                       )
                    {
                        continue;


                    }
                    else
                    {


                        Correct += good;
                    }



                }



                Log.Info("correct_answer:", item["correct_answer"].ToString());
                //Log.Info("incorrect_answers:", item["incorrect_answers"].ToString());
                list.Add(Correct);

                foreach(var incorrect in item["incorrect_answers"])
                {

                    //Log.Info("incorrect_answers:", incorrect.ToString());
                    string Incorrects = "";
                    foreach(var letters in incorrect)
                    {


                        if (letters.ToString().Contains(";") || letters.ToString().Contains("&") ||
                        letters.ToString().Contains("?") || letters.ToString().Contains("#")
                        )
                        {
                            continue;


                        }
                        else
                        {


                            Incorrects += letters;
                        }

                    }


                    list.Add(incorrect.ToString());
                }
                
        
                list.Sort();

               


                A1.Text = list[0];
                A2.Text = list[1];
                A3.Text = list[2];
                A4.Text = list[3];

                A1.SetTextColor(Color.ParseColor("#46211A"));
                A2.SetTextColor(Color.ParseColor("#46211A"));
                A3.SetTextColor(Color.ParseColor("#46211A"));
                A4.SetTextColor(Color.ParseColor("#46211A"));


                counter += 1;

                

                number_Q_txt.Text = $"{counter}/{number_Q}";

               





                list.Clear();
                btn.Enabled = true;

            }
        }
    }
}