using Android.App;
using Android.Content;
using Android.Graphics;
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
    public class AdapterFont : BaseAdapter
    {
        Context context;
        string[] names = new string[] { };
        string[] fonts = new string[] { };
        LayoutInflater inflater;
        Activity activity;

        public AdapterFont(Context context_, string[] names_, string[] fonts_, Activity activity_)
        {
            this.context = context_;
            this.names = names_;
            this.fonts = fonts_;
            this.activity = activity_;
            inflater = LayoutInflater.From(context);


        }


        public override int Count => names.Length;

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            convertView = inflater.Inflate(Resource.Layout.Font_Layout, parent, false);


            TextView FontTextview = convertView.FindViewById<TextView>(Resource.Id.TextFont);

            Typeface txt = Typeface.CreateFromAsset(activity.Assets, fonts[position]);

            FontTextview.SetTypeface(txt, TypefaceStyle.Normal);



            return convertView;
        }
    }
}