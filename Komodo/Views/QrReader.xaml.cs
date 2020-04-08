using Komodo.ViewModels;
using Newtonsoft.Json;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Komodo.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace Komodo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QrReader : ContentPage
    {

        Cadenas cadena = new Cadenas();

        public QrReader()
        {
            InitializeComponent();
       //     DisplayAlert("Bienvenido","Hola","De acuerdo");
        }

        private void BtnQr_Clicked(object sender, EventArgs e)
        {
            scanner();
        }


        #region Scanner Metodo
        private void scanner()
        {

            var scannerPage = new ZXingScannerPage();

            scannerPage.Title = cadena.Lectorqr;

            QrInformation qr = new QrInformation();

            scannerPage.OnScanResult += (result) =>
            {
                scannerPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopAsync();

                    
                    urllink.Text = result.Text;
                    urllink.IsVisible = true;

                    if (!String.IsNullOrEmpty(result.Text))
                    {

                        UrlStatic urlstatic = new UrlStatic();
                        urlstatic.Valor = qr.Mensaje;

                        //ESTA LA USAMOS PARA EL ENCAMPULAMIENTO

                        qr = JsonConvert.DeserializeObject<QrInformation>(result.Text);

                        if (!App.Current.Properties.ContainsKey("valormensaje"))
                        {

                            App.Current.Properties.Add("valormensaje", qr.Mensaje);

                        }
                        else
                        {
                            App.Current.Properties["valormensaje"] = qr.Mensaje;


                        }
                        App.Current.SavePropertiesAsync();


                        if (qr != null)
                        {

                            if (qr.Tipo == "Mensaje")
                            {

                                var a = urlstatic.Valor;
                                DisplayAlert("Correcto","Carga correcta", cadena.Ok);
                                //   ((NavigationPage)this.Parent).PushAsync(new LoginPaciente());
                            }
                            else
                            {
                                DisplayAlert(cadena.Error, cadena.MensajeError, cadena.Ok);

                            }


                        }


                    }
                    else
                    {
                        DisplayAlert(cadena.Error, cadena.MensajeError, cadena.Ok);

                    }

                    

                    DisplayAlert("Valor", urllink.Text, "Ok");

                    


              });

            };

            Navigation.PushAsync(scannerPage);


            scannerPage.OnScanResult -= null;





        }
        #endregion
        #region Btn Ubicacion
        private void BtnUbications_Clicked(object sender, EventArgs e)
        {
           


            MainViewModel.GetInstance().Ubications =
                        new UbicationsViewModel();

              Application.Current.MainPage.Navigation.PushAsync(new UbicationsView());


        }
        #endregion

        private  async void BtnCoordenadas_Clicked(object sender, EventArgs e)
        {

            await Application.Current.MainPage.Navigation.PushAsync(new Coordenadas());


        }


        private async void BtnGoogleMaps_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new MapsPage());

        }

        private void BtnPin1_Clicked(object sender, EventArgs e)
        {
              Application.Current.MainPage.Navigation.PushAsync(new PinItemsSourcePage());

        }

        private void BtnPin1_Clicked_1(object sender, EventArgs e)
        {

        }

        private void BtnItems_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PushAsync(new ListUbications());
        }
    }
}