using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Util;
using Android.Widget;
using Java.Lang;
using System;
using System.Collections.Generic;
using AndroidX.AppCompat.App;
using System.Threading.Tasks;
using AndroidX.Core.Widget;

namespace Game_Center
{
    [Activity(Label = "Game_Activity", Theme = "@style/AppTheme.NoActionBar")]
    public class Multiple_Activity : AppCompatActivity
    {

        public static Button Next;
        public static TextView Title, Count, UserName, Difficulty, Categary, Type, Question, TimerGame;
        public static RadioButton answer_1, answer_2, answer_3, answer_4;
        private static string Answer_selected;
        private List<TextView> list_testview = new List<TextView>();
        private List<RadioButton> radioButtons = new List<RadioButton>();
        private AudioTrivia audioTrivia;
        public override void OnBackPressed() { }
        private static bool changer, state = true, TimeOut;
        private static int CounterTimer= 30;
        private string breaker = "";
        Questions trivia;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Multiple_Layout);
            // Create your application here
            trivia = new Questions(this);
            Next = FindViewById<Button>(Resource.Id.Next);

            Title = FindViewById<TextView>(Resource.Id.Title);
            Count = FindViewById<TextView>(Resource.Id.Count);
            UserName = FindViewById<TextView>(Resource.Id.UserName);
            Difficulty = FindViewById<TextView>(Resource.Id.Difficulty);
            Categary = FindViewById<TextView>(Resource.Id.Categary);
            Type = FindViewById<TextView>(Resource.Id.Type);
            Question = FindViewById<TextView>(Resource.Id.Question);
            TimerGame = FindViewById<TextView>(Resource.Id.TimerGame);

            list_testview.Add(Title);
            list_testview.Add(Count);
            list_testview.Add(UserName);
            list_testview.Add(Difficulty);
            list_testview.Add(Categary);
            list_testview.Add(Type);
            list_testview.Add(Question);
            //list_testview.Add(TimerGame);


            answer_1 = FindViewById<RadioButton>(Resource.Id.answer_1);
            answer_2 = FindViewById<RadioButton>(Resource.Id.answer_2);
            answer_3 = FindViewById<RadioButton>(Resource.Id.answer_3);
            answer_4 = FindViewById<RadioButton>(Resource.Id.answer_4);


            answer_1.CheckedChange += Answer_1_CheckedChange;
            answer_2.CheckedChange += Answer_2_CheckedChange;
            answer_3.CheckedChange += Answer_3_CheckedChange;
            answer_4.CheckedChange += Answer_4_CheckedChange;


            audioTrivia = new AudioTrivia(this);

            radioButtons.Add(answer_1);
            radioButtons.Add(answer_2);
            radioButtons.Add(answer_3);
            radioButtons.Add(answer_4);

            
            Next.Click += Next_Click;
            trivia.MultipleChoiceNextQuestion(list_testview, radioButtons, Next,null);

   

            UserName.Text = $"Username: {Database.Userlogged}";

            string FontSaved = Database.GetAll().Find(e => e.Name == Database.Userlogged).FontUser;
            Log.Info("Userlogged: Userlogged: Userlogged:", $"{Database.Userlogged}");
            Log.Info("FontSaved: FontSaved: FontSaved:", $"{FontSaved}");
            ChangeFont(FontSaved, list_testview, Next, radioButtons);
            timer();
        }
        public void ChangeFont(string FontType, List<TextView>  txt_fonts, Button btn, List<RadioButton> radio)
        {

            Typeface txt = Typeface.CreateFromAsset(Assets, FontType);


            foreach (var text in txt_fonts)
            {
                text.SetTypeface(txt, TypefaceStyle.Normal);

            }
            foreach (var radio_obj in radio)
            {
                radio_obj.SetTypeface(txt, TypefaceStyle.Normal);

            }

            TimerGame.SetTypeface(txt, TypefaceStyle.Normal);

            btn.SetTypeface(txt, TypefaceStyle.Normal);
            
        }
        

        [Obsolete]
        
        private async void Next_Click(object sender, EventArgs e)
        {



            CounterTimer = 30;
       


            if (state)
            {
                state = false;
                NextQuestion();
            }
            else
            {
                TimeOut = false;
                state = true;
                NextQuestion();
            }
    
        }


        public async Task timer()
        {
   
            await Task.Run(() =>
            {

                while (true)
                {
                    try
                    {
                        if (state)
                        {

                            if (CounterTimer <= 0)
                            {
                                Next.Enabled = true;
                                state = false;
                                NextQuestion();


                            }else if(breaker == "BREAK")
                            {


                                break;
                            }

                            else
                            {

                                CounterTimer--;
                                Log.Info("time", $"{CounterTimer}");
                                TimerGame.Text = $"{CounterTimer}";
                            }

                        }
                        else
                        {
                            Log.Info("time", $"None {TimeOut}");

                        }
                    }
                    catch(System.Exception ex)
                    {

                    }

                    Thread.Sleep(1000);
                }

            });
        }


        private void NextQuestion()
        {

            changer = trivia.MultipleChoiceNextQuestion(list_testview, radioButtons, Next, Answer_selected);
            Answer_selected = null;

            /*foreach (var user in Database.GetAll())
            {

                Log.Info("User", $"Name: {user.Name}, Lastname: {user.Lastname}, Corrects: {user.Corrects}, Icorrects: {user.Incorrects}, NoSelection: {user.NoSelection}");

            }*/


            if (changer)
            {
                breaker = "BREAK";
                Intent next = new Intent(this, typeof(MainActivity));
                StartActivity(next);
                
               
            }

        }

        private void Answer_4_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {

            //Log.Info("Answer_4_CheckedChange: ", $"{Answer_selected}");
            if (e.IsChecked)
            {
                Answer_selected = "";
                Answer_selected = answer_4.Text;
                Next.Enabled = true;
   
                answer_1.SetBackgroundResource(Resource.Drawable.OnFalse);
                answer_2.SetBackgroundResource(Resource.Drawable.OnFalse);
                answer_3.SetBackgroundResource(Resource.Drawable.OnFalse);
                answer_4.SetBackgroundResource(Resource.Drawable.OnTrue);
            }


        }

        private void Answer_3_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {

            //Log.Info("Answer_3_CheckedChange: ", $"{Answer_selected}");
            if (e.IsChecked)
            {
                Answer_selected = "";
                Answer_selected = answer_3.Text;
                Next.Enabled = true;
     
                answer_1.SetBackgroundResource(Resource.Drawable.OnFalse);
                answer_2.SetBackgroundResource(Resource.Drawable.OnFalse);
                answer_3.SetBackgroundResource(Resource.Drawable.OnTrue);
                answer_4.SetBackgroundResource(Resource.Drawable.OnFalse);
            }


        }

        private void Answer_2_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {

            //Log.Info("Answer_2_CheckedChange: ", $"{Answer_selected}");
            if (e.IsChecked)
            {
                Answer_selected = "";
                Answer_selected = answer_2.Text;
                Next.Enabled = true;
              
                answer_1.SetBackgroundResource(Resource.Drawable.OnFalse);
                answer_2.SetBackgroundResource(Resource.Drawable.OnTrue);
                answer_3.SetBackgroundResource(Resource.Drawable.OnFalse);
                answer_4.SetBackgroundResource(Resource.Drawable.OnFalse);
            }
        }

        private void Answer_1_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {


            //Log.Info("Answer_1_CheckedChange: ", $"{Answer_selected}");
            if (e.IsChecked)
            {
                Answer_selected = "";
                Answer_selected = answer_1.Text;
                Next.Enabled = true;
         
                answer_1.SetBackgroundResource(Resource.Drawable.OnTrue);
                answer_2.SetBackgroundResource(Resource.Drawable.OnFalse);
                answer_3.SetBackgroundResource(Resource.Drawable.OnFalse);
                answer_4.SetBackgroundResource(Resource.Drawable.OnFalse);
            }

        }
    }
}