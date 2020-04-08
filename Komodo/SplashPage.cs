using Komodo.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Komodo
{
   public  class SplashPage : ContentPage
    {

        Image splashImage;

        public SplashPage()
        {


            NavigationPage.SetHasNavigationBar(this, false);

            var sub = new AbsoluteLayout();
            splashImage = new Image
            {

                Source = "drawable/logokm1.png",
                WidthRequest = 600,
                HeightRequest = 815

            };
            AbsoluteLayout.SetLayoutFlags(splashImage, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(splashImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize,
                AbsoluteLayout.AutoSize));

            sub.Children.Add(splashImage);

            this.BackgroundColor = Color.FromHex("#FFFFF");
            this.Content = sub;


        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await splashImage.ScaleTo(1, 2000);
            await splashImage.ScaleTo(1, 1890);
            await splashImage.ScaleTo(1.2, 1950);

            Application.Current.MainPage = new NavigationPage(new LoginPage());

        }



    }
}
