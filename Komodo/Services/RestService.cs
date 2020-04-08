using System;
using System.Collections.Generic;
using System.Text;

namespace Komodo.Services
{
    public static class RestService
    {
        public static string Servidor = "http://127.0.0.1:8093";
        public static string Metodo = "api/UbicationItems";
        public static string ContentType = "application/json";
        //public static string ApiKey = "TUAPIKEYASHSAHSAKHKSAHK678989";
        public static class HTTPMethods
        {
            public static string Get = "GET";
            public static string Post = "POST";
            public static string Put_Modify = "PUT";
            public static string Patch_Modify = "PATCH";
            public static string Delete = "DELETE";
        }
        public static class Methods
        {
            //  public static string LoginMethod = "api/Usuario";
            public static string LoginMethod = "api/Account/";
        }


    }
}
