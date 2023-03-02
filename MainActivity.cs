using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.View;

using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Google.Android.Material.Snackbar;

using Android.Widget;
using AndroidX.DrawerLayout.Widget;
using System.Collections.Generic;

namespace Game_Center
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        
        List<AndroidX.Fragment.App.Fragment> Panels = new List<AndroidX.Fragment.App.Fragment>()
        {
            new Trivia_Activity(),
            new Settings_Activity(),
            new Stats_Activity()

        }; 
        private AudioTrivia audioTrivia;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

  

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();
            audioTrivia = new AudioTrivia(this);
            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.FramePanels, Panels[0]).Commit();

        }

        [Obsolete]
        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
          
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            switch (id)
            {

                case Resource.Id.Game:
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.FramePanels, Panels[0]).Commit();
                    audioTrivia.pressButton();
                    break;
                case Resource.Id.Settings:
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.FramePanels, Panels[1]).Commit();
                    audioTrivia.pressButton();
                    break;
                case Resource.Id.Stats:
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.FramePanels, Panels[2]).Commit();
                    audioTrivia.pressButton();
                    break;


            }


            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
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

