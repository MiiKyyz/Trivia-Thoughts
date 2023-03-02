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

namespace Game_Center
{
    public class ToastManager
    {


        private static LayoutInflater layoutInflater;
        public static void ToastStyble(Context  context, ViewGroup viewGroup, string txt, int Icon, int Backgroung)
        {


            layoutInflater = LayoutInflater.From(context);

            View view = layoutInflater.Inflate(Resource.Layout.toast_layout, viewGroup, true);

            ImageView img = view.FindViewById<ImageView>(Resource.Id.Icon);
            TextView Text_toast = view.FindViewById<TextView>(Resource.Id.toast_text);

            LinearLayout linearLayout = view.FindViewById<LinearLayout>(Resource.Id.linearLayout);

            img.SetImageResource(Icon);

            linearLayout.SetBackgroundResource(Backgroung);
       

            Text_toast.Text = $"{txt}";

            Toast toast = new Toast(context);
            toast.SetGravity(GravityFlags.Bottom, 0, 0);
            toast.Duration = ToastLength.Short;
            toast.View = view;
            toast.Show();

        }




    }
}