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
    internal class SpinnerAdapter : BaseAdapter
    {
        LayoutInflater inflater;
        Context context;
        string Font;
        Activity activity;
        string[] list= new string[] { };
        public SpinnerAdapter(Context context, string font, Activity activity, string[] list)
        {
            this.list = list;
            this.activity = activity;
            this.Font = font;
            this.context = context;
            inflater = LayoutInflater.From(context);
        }



        public override int Count => list.Length;

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


            TextView TextFont = convertView.FindViewById<TextView>(Resource.Id.TextFont);

            Typeface txt = Typeface.CreateFromAsset(activity.Assets, Font);


            TextFont.SetTypeface(txt, TypefaceStyle.Normal);


            TextFont.Text = list[position];


            return convertView;

        }
    }
}