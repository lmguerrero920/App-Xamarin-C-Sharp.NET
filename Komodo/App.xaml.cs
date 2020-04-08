using Komodo.Services;
using Komodo.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Komodo
{
    public partial class App : Application
    {
     

        public App()
        {
            InitializeComponent();

            //  this.MainPage = new NavigationPage(new NavigationPage(new LoginPage()));
           // MainPage = new NavigationPage(new SplashPage());
          //  this.MainPage = new NavigationPage(new NavigationPage(new SplashPage()));
            this.MainPage = new NavigationPage(new
                NavigationPage(new LoginPage()));

            //https://developer.android.com/training/permissions/requesting?hl=es-419

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
