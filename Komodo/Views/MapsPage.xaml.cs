using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Komodo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapsPage : ContentPage
    {
        public MapsPage()
        {
            InitializeComponent();
        }

        private async void BtnOpenCoords_Clicked(object sender, EventArgs e)
        {
            if (!double.TryParse(EntryLatitud.Text, out double lat))
                return;
            
            if (!double.TryParse(EntryLongitud.Text, out double lng))
                return;
 
            await     Map.OpenAsync(lat, lng, new MapLaunchOptions
            {
                Name = EntryName.Text ,
           NavigationMode=NavigationMode.Driving
          

            });;
            ;
          
        }
    }
}