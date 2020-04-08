using GalaSoft.MvvmLight.Command;
using Komodo.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Komodo.ViewModels
{
   public class CoordenadasViewModel
    {


        #region Commands

        public ICommand CoordenadasCommand
        {
            get
            {
                return new RelayCommand(GoCoordenadas);
            }
        }

        private async void GoCoordenadas()
        {

       
           

            await Application.Current.MainPage.Navigation.PushAsync(new Coordenadas());
        }

        #endregion



    }
}
