using Android;
using Android.OS;
using Android.Widget;
using Komodo.Infrastructure;
using Komodo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Komodo.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Collections.ObjectModel;

namespace Komodo.Views
{

    public class Post
    {

        public int Id { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }


        public string Consolidado
        {
            get
            {
                return $"{Description}\r,\n,{Phone}\r,\n,{Address}\r,\n,{Latitude}\r,\n,{Longitude}";
            }
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListUbications : ContentPage
    {
     

        //List<string> Lista = new List<string>();

        // private const string url = "https://jsonplaceholder.typicode.com/posts";





        private const string url = "http://192.168.1.4:8093/api/UbicationItems";
        private HttpClient _Client = new HttpClient();
        private ObservableCollection<Post> _post;



        public ListUbications()
        {
            InitializeComponent();

          //   XamSharpWebApi.ItemsSource = Lista;

           // LoadList();
        }

        protected override async void OnAppearing()
        {
            var content = await _Client.GetStringAsync(url);
            var post = JsonConvert.DeserializeObject<List<Post>>(content);
            _post = new ObservableCollection<Post>(post);
            Post_List.ItemsSource = _post;
            base.OnAppearing();
        }




        //public void LoadList()
        //{
        //    try
        //    {
        //      string UsuarioEntry = "luguerrero10@poligran.edu.co";
        //       var person = new PersonLogin { Usuario = UsuarioEntry, TipoAccion = "LOGIN" };
        //       var json = JsonConvert.SerializeObject(person);

        //        HttpWebRequest request = WebRequest.Create(RestService.Servidor + 
        //        RestService.Methods.LoginMethod + "?id=0&data=" + json) as HttpWebRequest;

        //        request.Method = RestService.HTTPMethods.Get;
        //        //  request.Headers.Add("ApiKey", RestService.ApiKey);
        //        request.ContentType = RestService.ContentType;
        //        string resp;
        //        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
        //        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        //        {
        //            resp = reader.ReadToEnd();
        //            var obj = JsonConvert.DeserializeObject<object>(resp);
        //            string data = (string)obj;
        //            JObject json2 = JObject.Parse(data);
        //            var respuesta_data = json2;

        //            string Response = respuesta_data.GetValue("Response").ToString();
        //            if (Response == "SUCCESS")
        //            {
        //                var ListaFlujosJS = respuesta_data.GetValue("Objeto").ToString();
        //                try
        //                {
        //                    var objResponse1 = JsonConvert.DeserializeObject<Ubication>(ListaFlujosJS);
        //                    if (objResponse1.Id != 0)
        //                    {
        //                        Id = objResponse1.Id;
        //                        Lista.Add(Id.ToString());
        //                        Usuario = objResponse1.Usuario;
        //                        Lista.Add(Usuario.ToString());
        //                        Contrasenia = objResponse1.Contrasenia;
        //                        Description = objResponse1.Description;
        //                        IdTipoUsuario = objResponse1.UsuarioID;
        //                        Lista.Add(IdTipoUsuario.ToString());

        //                        Phone = objResponse1.Phone;
        //                        Lista.Add(Phone.ToString());
        //                        Address = objResponse1.Address;
        //                        Lista.Add(Address.ToString());
        //                        Latitude = objResponse1.Latitude;
        //                        Lista.Add(Latitude.ToString());
        //                        Longitude = objResponse1.Longitude;
        //                        Lista.Add(Latitude.ToString());


        //                    }
        //                }
        //                catch (Exception ex)
        //                {

        //                    throw ex;
        //                }
        //            }
        //            else
        //            {
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}





        //  public async  void OnCreate(  )
        //  {
        //      ImyAPI myAPI;

        //      myAPI = RestService.For<ImyAPI>(
        //          "http://localhost:8093/api/UbicationItems");

        //      try
        //      {

        //          List<Ubication> ubications = await myAPI.GetUser();

        //          List<string> itemsfull = new List<string>();

        //          foreach (var item in ubications)
        //          {
        //              itemsfull.Add(item.Address);
        //              itemsfull.Add(item.Description);
        //              itemsfull.Add(item.Latitude);
        //              itemsfull.Add(item.Longitude);

        ////ArrayAdapter<string> adapter = new ArrayAdapter<string>(this,
        ////   Android.Resource.Layout.SimpleListItem1,
        ////                  itemsfull);


        //                  }

        //      }
        //      catch (Exception)
        //      {

        //          throw;
        //      }

        //  }

        private void BtnList_Clicked(object sender, EventArgs e)
        {
          //  OnCreate();

        }
    }
}