using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Center
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat.NoActionBar", MainLauncher = true)]
    public class IntroActivity : AppCompatActivity
    {


        private List<int> images = new List<int>()
        {

            Resource.Drawable.back_1,
            Resource.Drawable.back_2



        };
        private RelativeLayout panel_back;
        private TextView txt_intro;
        private ImageView img_intro;
        private ObjectAnimator anim_panel_back, anim_txt_intro_x, anim_txt_intro_y, anim_txt_intro_position_y,
            anim_rotation_img;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.intro_layout);
            // Create your application here


            

            panel_back = FindViewById<RelativeLayout>(Resource.Id.panel_back);
            txt_intro = FindViewById<TextView>(Resource.Id.txt_intro);
            img_intro = FindViewById<ImageView>(Resource.Id.img_intro);

            Random random = new Random();


            int number = random.Next(0, images.Count);

            anim_panel_back = ObjectAnimator.OfFloat(panel_back, "alpha", 0f, 1f);
            anim_txt_intro_x = ObjectAnimator.OfFloat(txt_intro, "scaleX", 0f, 1f);
            anim_txt_intro_y = ObjectAnimator.OfFloat(txt_intro, "scaleY", 0f, 1f);
            anim_txt_intro_position_y = ObjectAnimator.OfFloat(txt_intro, "y", 0, 450);
            anim_rotation_img = ObjectAnimator.OfFloat(img_intro, "rotationY", 0, 360);

            AnimatorSet set = new AnimatorSet();


            set.PlayTogether(anim_rotation_img, anim_panel_back, anim_txt_intro_x, anim_txt_intro_y, anim_txt_intro_position_y);
            set.SetDuration(3000);

            set.Start();

            Task startup = new Task(() =>
            {
                Thread.Sleep(4000);
            });


            startup.ContinueWith(t =>
            {

                StartActivity(new Intent(Application.Context, typeof(Log_Activity)));
            

            }, TaskScheduler.FromCurrentSynchronizationContext());
            startup.Start();


        }
    }
}