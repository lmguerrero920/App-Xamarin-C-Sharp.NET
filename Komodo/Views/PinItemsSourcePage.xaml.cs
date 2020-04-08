using Komodo.Services;
using Komodo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Komodo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PinItemsSourcePage : ContentPage
    {
        GeolocatorService geolocatorService;
        public PinItemsSourcePage()
        {
            geolocatorService = new GeolocatorService();

            InitializeComponent();
            BindingContext = new PinItemsSourcePageViewModel();
            
           // map1.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(39.8283459, -98.5794797), Distance.FromMiles(1500)));
             geolocatorService.GetLocation();
            if (geolocatorService.Latitude != 0 ||
                geolocatorService.Longitude != 0)
            {
                var position = new Position(
                    geolocatorService.Latitude,
                    geolocatorService.Longitude
                    );
                map1.MoveToRegion
                    (MapSpan.FromCenterAndRadius(
              position, Distance.FromKilometers(.5)));
            }


        }

        void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"MapClick: {e.Position.Latitude}, {e.Position.Longitude}");
        }
    }
}