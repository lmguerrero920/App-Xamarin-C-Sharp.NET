using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Komodo.Droid
{

    [Activity(Label = "Komodo", Icon = "@drawable/sc1",
           Theme = "@style/MainTheme", MainLauncher = false,
           ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)

           ]


    //Icon = "@drawable/integracion",<item name="android:windowBackground">@drawable/sc1</item>
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);


            // TextView txt = FindViewById<TextView>(Resource.Id.text1);

          global::ZXing.Net.Mobile.Forms.Android.Platform.Init();

            Xamarin.Essentials.Platform.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);
           
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());



        }
         





        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            


 
        }


       
    }
}