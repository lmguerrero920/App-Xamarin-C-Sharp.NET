using Komodo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Komodo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private  async void BtnIngresar_Clicked(object sender, EventArgs e)
        {

            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:8093");
            //string url = string.Format("api/UbicationItems/{0}/{1}",
            //    Email.Text,Password.Text);

            //var response = await client.GetAsync(url);
            //var result = response.Content.ReadAsStringAsync().Result;

            //if(string.IsNullOrEmpty(result) || result == "null")
            //{
            //    await DisplayAlert("error", "usuario o clave no valido",
            //        "aceptar");
            //    Password.Text = string.Empty;
            //    Password.Focus();
            //    return;
            //}
         //   await LoginAsync(Email.Text, Password.Text);

            //var apiUrl = "http://127.0.0.1:8093/token";
            //var client = new HttpClient();
            //client.Timeout = new TimeSpan(1, 0, 0);
            //var loginData = new Dictionary<string, string>
            //    {
            //        {"UserName", Email.Text},
            //        {"Password", Password.Text},
            //        {"grant_type", "password"}
            //    };
            //var content = new FormUrlEncodedContent(loginData);
            //var response = client.PostAsync(apiUrl, content).Result;
         //   var apiUrl = "http://127.0.0.1:8093/";
          //  GetLoginToken(apiUrl,Email.Text,Password.Text);

        }



        //public TokenResponse GetLoginToken(string urlBase, string username, string password)
        //{
        //    //  var apiUrl = "http://127.0.0.1:8093/";
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri(urlBase);
        //    //TokenRequestViewModel tokenRequest = new TokenRequestViewModel() { 
        //    //password=userInfo.Password, username=userInfo.UserName};
        //    HttpResponseMessage response =
        //       client.PostAsync("Token",
        //        new StringContent(string.Format("grant_type=password&username={0}&password={1}",
        //          HttpUtility.UrlEncode(username),
        //          HttpUtility.UrlEncode(password)), Encoding.UTF8,
        //          "application/x-www-form-urlencoded")).Result;

        //    string resultJSON = response.Content.ReadAsStringAsync().Result;
        //    TokenResponse result = JsonConvert.DeserializeObject<TokenResponse>(resultJSON);

        //    return result;
        //}


        public async Task LoginAsync(string userName, string password)
        {

            try
            {

                var keyValues = new List<KeyValuePair<string, string>>
            {

                new KeyValuePair<string, string>("grant_type","password"),
                new KeyValuePair<string, string>("&username",userName),
                new KeyValuePair<string, string>("&password",password)

            };

                var request = new HttpRequestMessage(

                 HttpMethod.Post, "http://192.168.1.4:8093/Token");

                request.Content = new FormUrlEncodedContent(keyValues);

                var client = new HttpClient();
                var response = await client.SendAsync(request);

                var content = await response.Content.ReadAsStringAsync();
                //     Debug.WriteLine(content);
                await DisplayAlert("msj", content.ToString(), "ok");
            }
            catch (Exception ex)
            {

                ex.ToString();
            }



           }




            private void BtnRestablecer_Clicked(object sender, EventArgs e)
            {

                //   ((NavigationPage)this.Parent).PushAsync(new PasswordRecoveryView());


            }



       



    }
}