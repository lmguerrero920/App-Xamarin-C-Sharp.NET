using Komodo.Models;
using Komodo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace Komodo.ViewModels
{
  public  class UbicationsViewModel
    {

        #region Services
        ApiService apiService;

        DialogService dialogService;


        #endregion


        #region Propiedades
        public ObservableCollection<Pin> Pins
        {
            get;
            set;
        }

        #endregion


        #region Constructores
        public UbicationsViewModel()
        {
            instance = this;
            apiService = new ApiService();
            dialogService = new DialogService();
        }


        #endregion
        #region Sigleton
        static UbicationsViewModel instance;

        public static UbicationsViewModel GetInstance()
        {
            if (instance == null)
            {
                return new UbicationsViewModel();
            }

            return instance;
        }
        #endregion


        #region Metodos
        public async Task LoadPins()
        {

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage(
                    "error",connection.Message);
                return;
            }
            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetList<Ubication>(
                "http://192.168.1.4:8093",  
                "/api",
                "/UbicationItems",
              mainViewModel.Token.TokenType,
               mainViewModel.Token.AccessToken
                );

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage(
                    "Error",
                    response.Message);
                return;
            }

            var ubications = (List<Ubication>)response.Result;
            Pins = new ObservableCollection<Pin>();
            foreach (var ubication in ubications)
            {
                Pins.Add(new Pin
                {
                    Address = ubication.Address,
                    Label = ubication.Description,
                    Position = new Position(Convert.ToDouble(ubication.Latitude), Convert.ToDouble(ubication.Longitude))
                    ,
                    Type = PinType.Place,
                });
            }
        }
        #endregion
      

    }
}
