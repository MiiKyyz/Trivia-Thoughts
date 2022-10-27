using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.View;
using AndroidX.DrawerLayout.Widget;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Google.Android.Material.Snackbar;
using Android.Content;
using Android.Util;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Threading;
using Java.Lang;
using static Android.Content.ClipData;
using Android.Service.Autofill;

namespace Predict_App
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        DrawerLayout drawer;
        public List<AndroidX.Fragment.App.Fragment> list = new List<AndroidX.Fragment.App.Fragment>()
        {
            new Gender_Prediction_Fragment(),
            new Guess_Fragment(),
            new NationalityActivity(),
            new SettingsFragments()
   



        };
        string username;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);



            drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            username = Intent.GetStringExtra("user" ?? "not recv");

            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.frameLayout, list[0]).Commit();
           


        }

        public override void OnBackPressed()
        {
           
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else if (drawer.IsDrawerOpen(GravityCompat.End))
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }


        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;


            switch (id)
            {

                case Resource.Id.gender:
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.frameLayout, list[0]).Commit();
                    break;
                case Resource.Id.Guess:
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.frameLayout, list[1]).Commit();

                    break;
                case Resource.Id.nationality:
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.frameLayout, list[2]).Commit();
                    break;
                case Resource.Id.account:
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.frameLayout, list[3]).Commit();
                    break;

            }
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

