using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Komodo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Coordenadas : ContentPage
    {
        public Coordenadas()
        {
            InitializeComponent();
          //  InicializePlugin();
        }

        private async void InicializePlugin()
        {

            if (!CrossGeolocator.IsSupported)
            {
             await DisplayAlert("Error","Ha ocurrido un error al cargar","De acuerdo");
                return;
            }

            if (!CrossGeolocator.Current.IsListening ||
                !CrossGeolocator.Current.IsGeolocationEnabled||
               !CrossGeolocator.Current.IsGeolocationAvailable)
            {
          await      DisplayAlert("Advertencia",
                    "Revise la configuracion de su dispositivo y " +
                    "habilite los permisos de ubicación", "De acuerdo");
                return;
                
            }
            await CrossGeolocator.Current.StartListeningAsync(
                   new TimeSpan(0, 0, 2),0.5// medio metro
                   ) ;
          
             CrossGeolocator.Current.PositionChanged += Current_PositionChanged;
        }

        private void Current_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            if (!CrossGeolocator.Current.IsListening)
            {
                return;

            }
           
            var position = CrossGeolocator.Current.GetPositionAsync();

            TxtLat.Text = position.Result.Latitude.ToString();
            TxtLon.Text = position.Result.Longitude.ToString();
            TxtAlt.Text = position.Result.Altitude.ToString();
            TxtTiempo.Text = position.Result.Timestamp.ToString();
            TxtVelocidad.Text = position.Result.Speed.ToString();
        }



        private async void BtnCoordenadas_Clicked(object sender, EventArgs e)
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            //   TimeSpan timeSpan;

            //   var position = await locator.GetPositionAsync(timeSpan);

            try
            {
                var action = await DisplayAlert("Advertencia", "Debe aceptar el uso del GPS  para continuar, para eso habilite los permisos de ubicación", "Autorizo", "No Autorizo");
                if (action)
                {
                    //await DisplayAlert("Advertencia",
                    //"Debe aceptar el uso del GPS  para continuar,para eso  " +
                    //"habilite los permisos de ubicación", "De acuerdo");

                    if (locator.IsGeolocationAvailable)//Devuelve si el servicio existe en el dispositivo
                    {
                        if (locator.IsGeolocationEnabled)//devuelve si el gps esta activado
                        {
                            if (!locator.IsListening)//Comprueba si el dispositivo esta escuchando el servicio
                            {

                                await locator.StartListeningAsync(new TimeSpan(0, 0, 3), 2);//Inicio de escucha
                                                                                            // await locator.StartListeningAsync(TimeSpan.FromSeconds(5), 1000);//Inicio de escucha

                            }

                            locator.PositionChanged += (cambio, args) =>
                            {
                                var loc = args.Position;

                                TxtLat.Text = loc.Latitude.ToString();
                                TxtLon.Text = loc.Longitude.ToString();
                                TxtAlt.Text = loc.Altitude.ToString();
                                TxtPre.Text = loc.Accuracy.ToString();
                                TxtTiempo.Text = loc.Timestamp.ToString();
                                TxtVelocidad.Text = loc.Speed.ToString();

                            };

                        }

                    }
                }

                

             

            }
            catch (Exception ex)
            {

                ex.ToString();
            }
        }
    }
}