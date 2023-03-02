using Android.App;
using Android.Content;
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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Game_Center
{
    public class Questions
    {

        private static int NumberOfQuestion , counter;
        private static string All_Data, Category_Textview, Type_Textview;
        private string URL;
        private bool check_answer = true;
        private static string Correct = "";
        private static AudioTrivia audioTrivia;
        private static Context context1;
        private User user = new User();
        private List<string> answers = new List<string>();
        public Dictionary<string, int> CategoryList = new Dictionary<string, int>() {

            {"Random Category", 0 },
            {"General Knowledge", 9 },
            {"Books", 10 },
            {"Film", 11 },
            {"Music", 12},
            {"Musicals & Theatres", 13},
            {"Television", 14},
            {"Video Games", 15 },
            {"Board Games", 16 },
            {"Science & Nature", 17 },
            {"Computers", 18 },
            {"Mathematics", 19 },
            {"Mythology", 20 },
            {"Sports", 21 },
            {"Geography", 22 },
            {"History", 23 },
            {"Politics",24 },
            {"Art",25 },
            {"Celebrities", 26 },
            {"Animals", 27 },
            {"Vehicles", 28},
            {"Comics", 29},
            {"Gadgets", 30 },
            {"Japanese Anime & Manga", 31 },
            {"Cartoon & Animations", 32 },

        };

        public Dictionary<string, string> Difficulty = new Dictionary<string, string>() {

            {"Random Difficulty", "" },
            {"Easy","easy" },
            { "Medium","medium"},
            {"Hard","hard" },
        

        };

        public Dictionary<string, string> Type = new Dictionary<string, string>() {

            {"Select Type", "" },
            {"Multiple Choice", "multiple" },
            {"True / False", "boolean" }
        
        };


        public Questions(Context context)
        {
            audioTrivia = new AudioTrivia(context);
        }


        public async Task QuestionGenerator(string Number=null, string Category = null, string difficulty = null, string type = null)
        {
            counter = 0;
            NumberOfQuestion = int.Parse(Number);
            URL = $"https://opentdb.com/api.php?amount={NumberOfQuestion}&type={Type[type]}";
            if (Category != "Random Category")
            {

                URL += $"&category={CategoryList[Category]}";
                Category_Textview = Category;
            }
            if(difficulty != "Random Difficulty")
            {
                URL += $"&difficulty={Difficulty[difficulty]}";

            }
            Type_Textview = type;



            var handler = new HttpClientHandler();

            HttpClient client = new HttpClient();
            All_Data = await client.GetStringAsync(URL);

        }



        public static string Grammar(string sentence)
        {

            
            string NewSentence = "";
            string[] letters = new string[] { };
            letters = sentence.Split(' ');

            foreach (string l in letters)
            {

                if (l.Contains("quot"))
                {
                    string passer = "";
                    passer = l.Replace("quot", "");

                    NewSentence += $" {passer}";

                }
                else if (l.Contains("039"))
                {
                    string passer = "";

                    passer = l.Replace("039", "");

                    NewSentence += $" {passer}";
                    //Console.WriteLine(l);
                }
                else
                {
                    NewSentence += $" {l}";
                }



            }

            string RecoverSentence = "";
            foreach (char l in NewSentence)
            {

                if (l != '&' && l != ';' && l != '#' )
                    {

           
                    RecoverSentence += $"{l}";
                }

            }

            return RecoverSentence.Remove(0,1);


        }

        


        public bool TrueAndFalseNextQuestion(List<TextView> list_testview, List<RadioButton> radioButtons, Button btn, string Answer_selected = null)
        {
           
            bool state = false;
            if (counter <= NumberOfQuestion - 1)
            {
                if (check_answer)
                {
                    btn.Enabled = false;
                    check_answer = false;
                    answers.Clear();
                    foreach (var item in list_testview)
                    {
                        item.Text = "Loading...";


                    }
                    foreach (var item in radioButtons)
                    {
                        item.Text = "Loading...";
                        item.Checked = false;
                        item.SetBackgroundResource(Resource.Drawable.OnFalse);

                        item.Enabled = true;
                    }

                    var data_in_string = JObject.Parse(All_Data);


                    //Log.Info("check_answer", $"{check_answer}");
                    btn.Text = "Check Answer";
                    //Log.Info("counter:", $"{counter}");
                    //Log.Info("question: ", $"{data_in_string["results"][counter]}");
                    list_testview[0].Text = $"True And False";
                    list_testview[1].Text = $"{counter + 1}/{NumberOfQuestion}";
                    list_testview[2].Text = $"UserName: {Database.Userlogged}";
                    list_testview[3].Text = $"Difficulty: {data_in_string["results"][counter]["difficulty"]}";
                    list_testview[4].Text = $"Category: {data_in_string["results"][counter]["category"]}";
                    list_testview[5].Text = $"Type: {Type_Textview}";
                    list_testview[6].Text = $"{Grammar(data_in_string["results"][counter]["question"].ToString())}";

                    answers.Add($"{data_in_string["results"][counter]["correct_answer"]}");
                    answers.Add($"{data_in_string["results"][counter]["incorrect_answers"][0]}");
                    

                    Correct = Grammar(data_in_string["results"][counter]["correct_answer"].ToString());


                    if (Grammar(answers[0]) != "True" && Grammar(answers[1]) != "False")
                    {

                        radioButtons[0].Text = $"{Grammar(answers[1])}";
                        radioButtons[1].Text = $"{Grammar(answers[0])}";

                    }
                    else
                    {
                        radioButtons[0].Text = $"{Grammar(answers[0])}";
                        radioButtons[1].Text = $"{Grammar(answers[1])}";
                    }


                    



                }
                
                else
                {
                    foreach (var item in radioButtons)
                    {
                        item.Enabled = false;


                    }

                    btn.Text = "Next";
                    check_answer = true;
                    AnswerChecked(Correct, Answer_selected, list_testview, radioButtons);
                    //Log.Info("Answer Selected:", $"{Answer_selected}");
                    counter++;
                    Log.Info("Animation", $" Stop{NumberOfQuestion} {counter}");
                    

                }


                if (counter == NumberOfQuestion - 1)
                {
                    btn.Text = $"Done!";

                    Log.Info("Animation", " Stop");
                }

            }
            else if (counter >= NumberOfQuestion)
            {

                state = true;
            }
            return state;
        }

        
            public bool MultipleChoiceNextQuestion(List<TextView> list_testview, List<RadioButton> radioButtons, Button btn, string Answer_selected = null)
            {
         
                bool Change = false;

               

            if (counter <= NumberOfQuestion-1 )
            {
               
                if (check_answer)
                {


                    btn.Enabled = false;
                    check_answer = false;
                    answers.Clear();
                    foreach (var item in list_testview)
                    {
                        item.Text = "Loading...";


                    }
                    foreach (var item in radioButtons)
                    {
                        item.Text = "Loading...";
                        item.Checked = false;
                        item.SetBackgroundResource(Resource.Drawable.OnFalse);

                        item.Enabled = true;
                    }

                    var data_in_string = JObject.Parse(All_Data);

                    
                    //Log.Info("check_answer", $"{check_answer}");
                    btn.Text = "Check Answer";

                    //Log.Info("question: ", $"{data_in_string["results"][counter]}");
                    list_testview[0].Text = $"Multiple Choice";
                    list_testview[1].Text = $"{counter +1 }/{NumberOfQuestion}";
                    list_testview[2].Text = $"UserName: {Database.Userlogged}";
                    list_testview[3].Text = $"Difficulty: {data_in_string["results"][counter]["difficulty"]}";
                    list_testview[4].Text = $"Category: {data_in_string["results"][counter]["category"]}";
                    list_testview[5].Text = $"Type: {Type_Textview}";
                    list_testview[6].Text = $"{Grammar(data_in_string["results"][counter]["question"].ToString())}";


                    answers.Add($"{data_in_string["results"][counter]["correct_answer"]}");
                    answers.Add($"{data_in_string["results"][counter]["incorrect_answers"][0]}");
                    answers.Add($"{data_in_string["results"][counter]["incorrect_answers"][1]}");
                    answers.Add($"{data_in_string["results"][counter]["incorrect_answers"][2]}");

                   
                    Correct = Grammar(data_in_string["results"][counter]["correct_answer"].ToString());
                    answers.Sort();

                    for (int i = 0; i < answers.Count; i++)
                    {

                        radioButtons[i].Text = $"{Grammar(answers[i])}";

                    }

                }
                else
                {
                    foreach (var item in radioButtons)
                    {
                        item.Enabled = false;


                    }

                    btn.Text = "Next";
                    btn.Enabled = true;
                    check_answer = true;
                    AnswerChecked(Correct, Answer_selected, list_testview, radioButtons);
                    //Log.Info("Answer Selected:", $"{Answer_selected}");
                    counter++;



                }
            }
            else if (counter >= NumberOfQuestion)
            {

                Change = true;
            }
            

            
            return Change;
        }

        
        private void AnswerChecked(string Correct_answer, string answer_selected, List<TextView> list_testview, List<RadioButton> radioButtons)
        {

            user.Name = Database.Userlogged;

            
       
            if (answer_selected == null)
            {

                foreach (var answers in radioButtons)
                {
                    answers.SetBackgroundResource(Resource.Drawable.Incorrect);
             
                }

                //Log.Info("No selection", $"No selection{answer_selected}");
                int no_selection = Database.GetAll().Find(e => e.Name == user.Name).NoSelection;
                no_selection += 1;
                user.NoSelection = no_selection;
                Database.SetNoSelection(user);

                audioTrivia.BadAnswerSound();

            } else if(Correct_answer == answer_selected) 
            {


                //Log.Info("Correct", $"{answer_selected}");
                int corrects = Database.GetAll().Find(e => e.Name == user.Name).Corrects;
                corrects += 1;
                user.Corrects = corrects;
                Database.Setcorrect(user);
                audioTrivia.GoodAnswerSound();

                foreach (var answers in radioButtons)
                {

                    if (answers.Text == answer_selected)
                    {

                        answers.SetBackgroundResource(Resource.Drawable.Correct);

                    }
                    else
                    {

                        answers.SetBackgroundResource(Resource.Drawable.Incorrect);
                    }


                }

            }
            else if (Correct_answer != answer_selected)
            {
                //Log.Info("Incorrect", $"{answer_selected} Correct: {Correct_answer}");
                int incorrects = Database.GetAll().Find(e => e.Name == user.Name).Incorrects;
                incorrects += 1;
                user.Incorrects = incorrects;
                Database.SetIncorrect(user);


                audioTrivia.BadAnswerSound();
                foreach (var answers in radioButtons)
                {

                    if (answers.Text == answer_selected)
                    {
                        
                        answers.SetBackgroundResource(Resource.Drawable.OnFalse);

                    }else if (answers.Text == Correct_answer)
                    {

                        answers.SetBackgroundResource(Resource.Drawable.Correct);

                    }
                    else
                    {

                        answers.SetBackgroundResource(Resource.Drawable.Incorrect);
                    }


                }
            }


        }


    }
}