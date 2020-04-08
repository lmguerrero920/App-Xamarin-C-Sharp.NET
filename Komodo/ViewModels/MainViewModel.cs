using Komodo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Komodo.ViewModels
{
    public class MainViewModel
    {

        #region ViewModels
        public LoginViewModel Login
        {
            get;
            set;
        }

        public QrReaderViewModel Lands
        {
            get;
            set;
        }

        public UbicationsViewModel Ubications
        {
            get;
            set; 
        }
       

        public TokenResponse Token
        {
            get;
            set;

        }

        public CoordenadasViewModel CoordenadasCommand
        {
            get;
            set;
        }





        #endregion

        #region Constructores
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();




        }

        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if(instance==null)
            {
                return new MainViewModel();
            }

            return instance;
        }
       
        #endregion

    }
}
