using GalaSoft.MvvmLight.Command;
using Komodo.Services;
using Komodo.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using Komodo.Models;

namespace Komodo.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        //BaseViewModel

        //#region Eventos
        //public event PropertyChangedEventHandler PropertyChanged;
        //#endregion

        #region Atributos
        private string email;
        private string password;
        private bool isRunning;
        private bool isEnabled;


        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Prpiedades

        public string Email
        {

            //get
            //{
            //    return email;
            //}
            //set
            //{
            //    SetValue(ref this.email, value);
            //}
            get
            {
                return this.email;
            }
            set
            {
                if (this.email != value)
                {
                    this.email = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(this.Email)));

                }
            }
        }

        public string Password
        {

            //get
            //{
            //    return password;
            //}
            //set
            //{
            //    SetValue(ref this.password, value);
            //}
            get
            {
                return this.password;
            }
            set
            {
                if (this.password != value)
                {
                    this.password = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(this.Password)));

                }
            }
        }

        public bool IsToggled
        {
            get;
            set;
        }
        public bool IsRunning
        {
            //get
            //{
            //    return this.isRunning;
            //}
            //set
            //{
            //    SetValue(ref this.isRunning, value);
            //}

            get
            {
                return this.isRunning;
            }
            set
            {
                if (this.isRunning != value)
                {
                    this.isRunning = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(this.IsRunning)));

                }
            }
        }
        public bool IsEnabled
        {
            //isEnabled

            //get
            //{
            //    return this.isEnabled;
            //}
            //set
            //{
            //    SetValue(ref this.isEnabled, value);
            //}
            get
            {
                return this.isEnabled;
            }
            set
            {
                if (this.isEnabled != value)
                {
                    this.isEnabled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(this.IsEnabled)));

                }
            }
        }



        #endregion

        #region Comandos

        public ICommand LoginCommand
        {
            get
            {
                 return new RelayCommand(Login);
               // return new Command(async () =>
               //{
               //    await apiService.LoginAsync(Email, Password);
               //});

            }

        }

        public ICommand RecoverPasswordCommand
        {
            get
            {
                return new RelayCommand(Restablecer);
            }
        }

  




        #endregion
        #region Constructores
        public LoginViewModel()
        {
            
            IsEnabled = true;
            IsToggled = true;
            apiService = new ApiService();

            //this.Email = "lmguerrero920@gmail.com";
            //this.Password = "1234";

            //"  http://restcountries.eu/rest/v2/all

        }



        #endregion



        #region Constructor async Login
        async void Login()
        {

            try
            {
                if (string.IsNullOrEmpty(this.Email))
                {

                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Por favor ingrese un usuario",
                        "Aceptar");
                    return;
                }

                if (string.IsNullOrEmpty(this.Password))
                {

                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Por favor ingrese la contraseña",
                        "Aceptar");
                    return;
                }
                //  await dialogService.ShowMessage("Ok","Bienvenido");
                IsRunning = true;
                IsEnabled = true;
                var connection = await apiService.CheckConnection();
                if (!connection.IsSuccess)
                {
                    IsRunning = false;
                    isEnabled = true;
                    await dialogService.ShowMessage("Error", connection.Message);
                    return;
                }


                //var response =  apiService.LoginAsync(
                //     Email,
                //    Password
                //    );


                var urlAPI = Application.Current.Resources["URLAPI"].ToString();

                //IP en Get Token
                var response = await apiService.GetToken(
                   urlAPI,
                    Email,
                    Password
                    );




                //bom
                if (response == null)
                {
                    IsRunning = false;
                    isEnabled = true;
                    await dialogService.ShowMessage("Error", response.ErrorDescription);
                    Password = null;
                    return;

                }
                if (string.IsNullOrEmpty(
                 response.AccessToken))
                {
                    IsRunning = false;
                    isEnabled = true;
                    await dialogService.ShowMessage(
                        "Error", response.ErrorDescription);
                    Password = null;
                    return;

                }
                var mainViewModel = MainViewModel.GetInstance();
                response.IsToggled = IsToggled;
                response.Password = Password;
                mainViewModel.Token = response;
                //  await dialogService.ShowMessage("Ok","Bienvenido");

                if (password == Password && response.UserName == Email)
                {
                    
                    IsRunning = false;
                    IsEnabled = true;
                    IsToggled = true;
                    await Application.Current.MainPage.Navigation.PushAsync(new QrReader());

                }
                else
                {
                    return;
                  
                }

            }
            catch (Exception e )
            {

                e.ToString();
            }


        }

        #endregion

        #region Constructor async Restablecer
        private async void Restablecer()
        {

            this.IsRunning = false;
            this.IsEnabled = true;
 

            await Application.Current.MainPage.Navigation.PushAsync(new PasswordRecoveryView());

        }

        #endregion







    }
}
