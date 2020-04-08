using Xamarin.Forms;
using Xamarin.Forms.Maps;

using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;
using Komodo.Services;
using System;
using System.Threading.Tasks;
using Komodo.ViewModels;
using System.Collections.Generic;
using Komodo.Models;

using System.Data.SqlTypes;
using System.Net.Http;

namespace Komodo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UbicationsView : ContentPage
    {

        //static string cadenaConexion = @"data source=internal.medvision.com.co\TESTSQL,25800;initial catalog=Komodo1;
        //           user id=sa;password=Zapato General*;";

       

        #region
        GeolocatorService geolocatorService;
        #endregion

        #region Constructor

        public UbicationsView()
        {
            InitializeComponent();

            geolocatorService = new GeolocatorService();

            MoveMapToCurrentPosition();
          

        }

        #endregion

        #region Metodos
        async  void MoveMapToCurrentPosition()
        {

            try
            {
                await geolocatorService.GetLocation();
                if (geolocatorService.Latitude != 0 ||
                    geolocatorService.Longitude != 0)
                {
                    var position = new Xamarin.Forms.Maps.Position(
                        geolocatorService.Latitude,
                        geolocatorService.Longitude
                        );
                    MyMap.MoveToRegion
                        (Xamarin.Forms.Maps.MapSpan.FromCenterAndRadius(
                  position, Xamarin.Forms.Maps.Distance.FromKilometers(.5)));

                }

                await LoadPins();
            }

            catch (Exception e)
            {
                e.Message.ToString();
                
            }
          
    
          
        }

        async Task LoadPins()
        {
            var ubicationsViewModel = UbicationsViewModel.GetInstance();
            await ubicationsViewModel.LoadPins();
            foreach (var pin in ubicationsViewModel.Pins)
            {
                MyMap.Pins.Add(pin);
            }
        }






        #endregion
    }
}