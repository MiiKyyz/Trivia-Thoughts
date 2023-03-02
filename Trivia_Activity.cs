using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Google.Android.Material.Snackbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_Center
{
    public class Trivia_Activity : AndroidX.Fragment.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

       
        public Dictionary<string, int> CategoryList = new Dictionary<string, int>() {

            {"Random Category", 0 },
            {"General Knowledge", 9 },
            {"Film", 11 },
            {"Music", 12},
            {"Television", 14},
            {"Video Games", 15 },
            {"Science & Nature", 17 },
            {"Computers", 18 },
            {"Mathematics", 19 },
            {"Mythology", 20 },
            {"Sports", 21 },
            {"Geography", 22 },
            {"History", 23 },
            {"Politics",24 },


            {"Animals", 27 },
            { "Vehicles", 28},
          
            {"Japanese Anime & Manga", 31 },
            {"Cartoon & Animations", 32 },

        };
        private ViewGroup viewGroup;
        private View view;
        private Questions trivia;
        private Button StartGame;
        private AutoCompleteTextView InputNumber;
        private Spinner Category, Difficulty, Type;
        private string Category_str, Difficulty_str, Type_str;
        private TextView Title_Trivia;
        private string FontSaved;
        private AudioTrivia audioTrivia;
        private SpinnerAdapter adapter_category, adapter_difficulty, adapter_type;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            view = inflater.Inflate(Resource.Layout.Trivia_Layout, container, false);

            StartGame = view.FindViewById<Button>(Resource.Id.StartGame);
            InputNumber = view.FindViewById<AutoCompleteTextView>(Resource.Id.InputNumber);

            Category = view.FindViewById<Spinner>(Resource.Id.Category);
            Difficulty = view.FindViewById<Spinner>(Resource.Id.Difficulty);
            Type = view.FindViewById<Spinner>(Resource.Id.Type);
            Title_Trivia = view.FindViewById<TextView>(Resource.Id.Title_Trivia);

            StartGame.Click += StartGame_Click;
            trivia = new Questions(Context);
            FontSaved = Database.GetAll().Find(e => e.Name == Database.Userlogged).FontUser;

            audioTrivia = new AudioTrivia(Context);

            adapter_category = new SpinnerAdapter(Context, FontSaved, Activity, trivia.CategoryList.Keys.ToArray());
             adapter_difficulty = new SpinnerAdapter(Context, FontSaved, Activity, trivia.Difficulty.Keys.ToArray());
             adapter_type = new SpinnerAdapter(Context, FontSaved, Activity, trivia.Type.Keys.ToArray());

            Category.Adapter = adapter_category;
            Difficulty.Adapter = adapter_difficulty;
            Type.Adapter = adapter_type;


            Category.ItemSelected += Category_ItemSelected;
            Difficulty.ItemSelected += Difficulty_ItemSelected;
            Type.ItemSelected += Type_ItemSelected;

            
            
            ChangeFont(FontSaved, Title_Trivia, StartGame);
            return view;
        }
        public void ChangeFont(string FontType, TextView txt_fonts, Button btn)
        {

            Typeface txt = Typeface.CreateFromAsset(Activity.Assets, FontType);


            txt_fonts.SetTypeface(txt, TypefaceStyle.Bold);
            btn.SetTypeface(txt, TypefaceStyle.Bold);


        }
        private void Type_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            audioTrivia.pressButton();
            Type_str = trivia.Type.Keys.ToArray()[e.Position];
            if (Type_str == "True / False")
            {
                adapter_category = new SpinnerAdapter(Context, FontSaved, Activity, CategoryList.Keys.ToArray());
                
                Category.Adapter = adapter_category;
                
            }
            else
            {
                adapter_category = new SpinnerAdapter(Context, FontSaved, Activity, trivia.CategoryList.Keys.ToArray());
               
                Category.Adapter = adapter_category;
                

            }

        }

        private void Difficulty_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            audioTrivia.pressButton();
            Difficulty_str = trivia.Difficulty.Keys.ToArray()[e.Position];
        }

        private void Category_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

            audioTrivia.pressButton();
            Category_str = trivia.CategoryList.Keys.ToArray()[e.Position];
        }

        private async void StartGame_Click(object sender, EventArgs e)
        {
            
            if (InputNumber.Text != "")
            {
                int number = int.Parse(InputNumber.Text);
                if (number > 0 && number <= 20)
                {


                    if(Type_str == "Multiple Choice")
                    {
                        await trivia.QuestionGenerator(InputNumber.Text, Category_str, Difficulty_str, Type_str);

                        Intent intent = new Intent(Context, typeof(Multiple_Activity));
                        StartActivity(intent);

                        audioTrivia.pressButton();
                    }
                    else if (Type_str == "True / False")
                    {

                        await trivia.QuestionGenerator(InputNumber.Text, Category_str, Difficulty_str, Type_str);

                        Intent intent = new Intent(Context, typeof(TrueAndFalse));
                        StartActivity(intent);
                        audioTrivia.TriviaCorrectSound();
                    }
                    else
                    {

                 
                        ToastManager.ToastStyble(Context, viewGroup, "Select type mode", Resource.Drawable.icon_wrong, Resource.Drawable.toast_wrong);

                        audioTrivia.TriviawrongSound();
                    }

                }
                else
                {
                    audioTrivia.TriviawrongSound();
                 
                    ToastManager.ToastStyble(Context, viewGroup, "the number should be betwen 1 and 20", Resource.Drawable.icon_wrong, Resource.Drawable.toast_wrong);

                }

            }
            else
            {
                audioTrivia.TriviawrongSound();
             
                ToastManager.ToastStyble(Context, viewGroup, "type the number of questions", Resource.Drawable.icon_wrong, Resource.Drawable.toast_wrong);

            }




        }
    }
}