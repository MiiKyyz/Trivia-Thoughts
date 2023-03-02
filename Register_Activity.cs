using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using AndroidX.AppCompat.App;


namespace Game_Center
{
    [Activity(Label = "Registration", Theme = "@style/AppTheme.NoActionBar")]
    public class Register_Activity : AppCompatActivity
    {
        private AutoCompleteTextView User, Lastname, Pass, repass;
        private TextView Registration_title;
        private Button resgister;
        private User user = new User();
        private AudioTrivia audioTrivia;
        private ViewGroup viewGroup;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Register_Layout);

            User = FindViewById<AutoCompleteTextView>(Resource.Id.User);
            Lastname = FindViewById<AutoCompleteTextView>(Resource.Id.Lastname);
            Pass = FindViewById<AutoCompleteTextView>(Resource.Id.Pass);
            repass = FindViewById<AutoCompleteTextView>(Resource.Id.repass);
            resgister = FindViewById<Button>(Resource.Id.resgister);

            Registration_title = FindViewById<TextView>(Resource.Id.Registration_title);

            resgister.Click += Resgister_Click;
            audioTrivia = new AudioTrivia(this);
         
        }

      

        private string[] Grammar(string Username, string lastname, string password)
        {
          

            if (Username.Contains(" ") || password.Contains(" "))
            {
                ToastManager.ToastStyble(this, viewGroup, "Username/password cannot have spaces", Resource.Drawable.icon_wrong, Resource.Drawable.toast_wrong);
               

                return null;
            }
            else 
            {

                string[] letters = new string[] { };
                letters = lastname.Split(" ");

                lastname = "";

                foreach (string s in letters)
                {

                    if(s != "")
                    {

                        lastname += $" {s}";
                    }
                }
                lastname = lastname.Remove(0, 1);
                return new string[] { Username, lastname, password };
            }
        }

        private void Resgister_Click(object sender, EventArgs e)
        {
       
            if (User.Text != "" && Lastname.Text != "" && Pass.Text != "")
            {

                if(Pass.Text.Length >= 6)
                {

                    if(Pass.Text == repass.Text)
                    {
                        string[] data = new string[] { };
                        data = Grammar(User.Text, Lastname.Text, Pass.Text);

                       
                        if(data != null)
                        {
                            Data(data[0], data[1], data[2]);
                        }

                    }
                    else
                    {
                        audioTrivia.RegisterWrongSound();
                        ToastManager.ToastStyble(this, viewGroup, "Password must be equal", Resource.Drawable.icon_wrong, Resource.Drawable.toast_wrong);

                 
                    }


                }
                else
                {
                    ToastManager.ToastStyble(this, viewGroup, "Password should 6 character", Resource.Drawable.icon_wrong, Resource.Drawable.toast_wrong);

                   
                    audioTrivia.RegisterWrongSound();
                }

            }
            else
            {
                ToastManager.ToastStyble(this, viewGroup, "Information missing", Resource.Drawable.icon_wrong, Resource.Drawable.toast_wrong);

   
                audioTrivia.RegisterWrongSound();
            }

            /*foreach (var usernames in Database.GetAll())
            {


                Log.Info("Users", $"ID: {usernames.Id} Name: {usernames.Name} Lastname: {usernames.Lastname} Password: {usernames.Password}, FontUser: {usernames.FontUser}");


            }*/

        }

        private void Data(string username, string lastname, string password)
        {
     
            user.Name = $"{username}";
            user.Lastname = $"{lastname}";
            user.Password = $"{password}";
            user.FontUser = $"{Settings_Activity.Fonts["Normal"]}";
            
            string existed = "";

            if (Database.GetAll().Count == 0)
            {
    
                Database.InsertUser(user);
                User.Text = "";
                Lastname.Text = "";
                Pass.Text = "";
                repass.Text = "";
                audioTrivia.RegisterCorrectSound();
                ToastManager.ToastStyble(this, viewGroup, "Account Created!", Resource.Drawable.icon_correct, Resource.Drawable.toast_correct);


            }
            else
            {

                try
                {

                    var data = Database.GetAll();
                    existed = data.Find(e => e.Name == user.Name).Name;
                    audioTrivia.RegisterWrongSound();
                    ToastManager.ToastStyble(this, viewGroup, "Username in Used!", Resource.Drawable.icon_wrong, Resource.Drawable.toast_wrong);

           
                }
                catch
                {
                    existed = "";
                    Database.InsertUser(user);
                    User.Text = "";
                    Lastname.Text = "";
                    Pass.Text = "";
                    repass.Text = "";
                    audioTrivia.RegisterCorrectSound();
                    ToastManager.ToastStyble(this, viewGroup, "Account Created!", Resource.Drawable.icon_correct, Resource.Drawable.toast_correct);

                   
                }

            }
        }
    }
}