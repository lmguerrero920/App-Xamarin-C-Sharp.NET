using Komodo.Models;
using Komodo.Views;
using Newtonsoft.Json;
using Plugin.Connectivity;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Komodo.Services
{
    public class ApiService
    {

        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Please turn on your internet settings.",
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable(
                "google.com");
            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Check you internet connection.",
                };
            }

            return new Response
            {
                IsSuccess = true,
                Message = "Ok",
            };
        }

        public async Task<Response> PasswordRecovery(
            string urlBase,
            string servicePrefix,
            string controller,
            string email)
        {
            try
            {
                var userRequest = new UserRequest { Email = email, };
                var request = JsonConvert.SerializeObject(userRequest);
                var content = new StringContent(
                    request,
                    Encoding.UTF8,
                    "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }


        //public async Task<Response> ChangePassword(
        //    string urlBase,
        //    string servicePrefix,
        //    string controller,
        //    string tokenType,
        //    string accessToken,
        //    ChangePasswordRequest changePasswordRequest)
        //{
        //    try
        //    {
        //        var request = JsonConvert.SerializeObject(
        //            changePasswordRequest);
        //        var content = new StringContent(
        //            request,
        //            Encoding.UTF8,
        //            "application/json");
        //        var client = new HttpClient();
        //        client.DefaultRequestHeaders.Authorization =
        //            new AuthenticationHeaderValue(tokenType, accessToken);
        //        client.BaseAddress = new Uri(urlBase);
        //        var url = string.Format("{0}{1}", servicePrefix, controller);
        //        var response = await client.PostAsync(url, content);

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = response.StatusCode.ToString(),
        //            };
        //        }

        //        return new Response
        //        {
        //            IsSuccess = true,
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response
        //        {
        //            IsSuccess = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        public async Task LoginAsync(string userName, string password)
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
            Debug.WriteLine(content);

        }



        public async Task<TokenResponse> GetToken(
            string urlBase,
            string username,
            string password)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var response = await client.PostAsync("Token",
                    new StringContent(string.Format(
                    "grant_type=password" +
                    "&username={0}" +
                    "&password={1}",
                    username, password),
                    Encoding.UTF8, "application/x-www-form-urlencoded"));
                var resultJSON =  await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TokenResponse>(
                    resultJSON);
                return result;
            }
            catch
            {
                return null;
            }
        }



        public async Task<Response> Get<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            int id)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    id);
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = model,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetList<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetList<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            int id)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    id);
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }





        public async Task<Response> Post<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(
                    request, Encoding.UTF8,
                    "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.IsSuccess = false;
                    return error;
                }

                var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Record added OK",
                    Result = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Post<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(
                    request,
                    Encoding.UTF8,
                    "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Record added OK",
                    Result = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Put<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(
                    request,
                    Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    model.GetHashCode());
                var response = await client.PutAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.IsSuccess = false;
                    return error;
                }

                var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSuccess = true,
                    Result = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Delete<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            T model)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    model.GetHashCode());
                var response = await client.DeleteAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.IsSuccess = false;
                    return error;
                }

                return new Response
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        //    #region CheckConnection
        //    public async Task<Response> CheckConnection()
        //    {
        //        if (!CrossConnectivity.Current.IsConnected)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = "Please turn on your internet settings.",
        //            };
        //        }

        //        var isReachable = await CrossConnectivity.Current.IsRemoteReachable(
        //            "google.com");
        //        if (!isReachable)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = "Check you internet connection.",
        //            };
        //        }

        //        return new Response
        //        {
        //            IsSuccess = true,
        //            Message = "Ok",
        //        };
        //    }
        //    #endregion

        //    //public async Task<TokenResponse> GetLoginToken(string urlBase, string username, string password)
        //    //{
        //    //  //  var apiUrl = "http://127.0.0.1:8093/token";
        //    //    HttpClient client = new HttpClient();
        //    //    client.BaseAddress = new Uri(urlBase);
        //    //    //TokenRequestViewModel tokenRequest = new TokenRequestViewModel() { 
        //    //    //password=userInfo.Password, username=userInfo.UserName};
        //    //    HttpResponseMessage response =
        //    //       client.PostAsync("Token",
        //    //        new StringContent(string.Format("grant_type=password&username={0}&password={1}",
        //    //          HttpUtility.UrlEncode(username),
        //    //          HttpUtility.UrlEncode(password)), Encoding.UTF8,
        //    //          "application/x-www-form-urlencoded")).Result;

        //    //    string resultJSON = response.Content.ReadAsStringAsync().Result;
        //    //    TokenResponse result = JsonConvert.DeserializeObject<TokenResponse>(resultJSON);

        //    //    return result;
        //    //}







        //    public   TokenResponse GetLoginToken(
        //        string urlBase,
        //        string username,
        //        string password)
        //    {

        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(urlBase);

        //        HttpResponseMessage response =
        //           client.PostAsync("Token",
        //            new StringContent(string.Format("grant_type=password&username={0}&password={1}",
        //              HttpUtility.UrlEncode(username),
        //              HttpUtility.UrlEncode(password)), Encoding.UTF8,
        //              "application/x-www-form-urlencoded")).Result;

        //        string resultJSON =  response.Content.ReadAsStringAsync().Result;

        //        TokenResponse result = JsonConvert.DeserializeObject<TokenResponse>(resultJSON);

        //        return result;




        //    }

        //    //GET TOKEN  OK



        //  public async Task LoginAsync(string userName,string password)
        //    {
        //        var keyValues = new List<KeyValuePair<string, string>>
        //        {

        //            new KeyValuePair<string, string>("grant_type","password"),
        //            new KeyValuePair<string, string>("&username",userName),
        //            new KeyValuePair<string, string>("&password",password)

        //        };

        //        var request = new HttpRequestMessage(

        //            HttpMethod.Post, "http://127.0.0.1:8093//Token");

        //        request.Content = new FormUrlEncodedContent(keyValues);

        //        var client = new HttpClient();
        //        var response = await client.SendAsync(request);

        //        var content= await response.Content.ReadAsStringAsync();
        //        Debug.WriteLine(content);

        //    }








        //    public async Task<TokenResponse> GetToken(
        //        string urlBase,
        //        string username,
        //        string password)
        //    {
        //        try
        //        {

        //            var client = new HttpClient();
        //        //  client.BaseAddress = new Uri(urlBase);


        //            var response = await client.PostAsync(urlBase,
        //                  new StringContent(string.Format(
        //                  "grant_type=password" +
        //                  "&username={0}" +
        //                  "&password={1}",
        //                  username, password),
        //                  Encoding.UTF8, "application/x-www-form-urlencoded"));
        //            var resultJSON = await response.Content.ReadAsStringAsync();
        //            var result = JsonConvert.DeserializeObject<TokenResponse>(
        //                resultJSON);
        //            return result;




        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }











        //    #region Ghhh
        //    public async Task<Response> Get<T>(
        //        string urlBase,
        //        string servicePrefix,
        //        string controller,
        //          int id)
        //    {
        //        try
        //        {


        //            var client = new HttpClient();


        //            client.DefaultRequestHeaders.Authorization =
        //               new AuthenticationHeaderValue(servicePrefix, controller);
        //            client.BaseAddress = new Uri(urlBase);
        //            var url = string.Format(
        //                "{0}{1}/{2}",
        //                servicePrefix,
        //                controller,
        //                id);
        //            var response = await client.GetAsync(url);

        //            if (!response.IsSuccessStatusCode)
        //            {
        //                return new Response
        //                {
        //                    IsSuccess = false,
        //                    Message = response.StatusCode.ToString(),
        //                };
        //            }

        //            var result = await response.Content.ReadAsStringAsync();
        //            var model = JsonConvert.DeserializeObject<T>(result);
        //            return new Response
        //            {
        //                IsSuccess = true,
        //                Message = "Ok",
        //                Result = model,
        //            };
        //        }
        //        catch (Exception ex)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = ex.Message,
        //            };
        //        }
        //    }
        //    #endregion

        //    public async Task<Response> GetList<T>(
        //   string urlBase,
        //   string servicePrefix,
        //   string controller,
        //   string tokenType,
        //   string accessToken)
        //    {
        //        try
        //        {
        //            var client = new HttpClient();
        //            client.DefaultRequestHeaders.Authorization =
        //                new AuthenticationHeaderValue(tokenType, accessToken);
        //            client.BaseAddress = new Uri(urlBase);
        //            var url = string.Format("{0}{1}", servicePrefix, controller);
        //            var response = await client.GetAsync(url);
        //            var result = await response.Content.ReadAsStringAsync();

        //            if (!response.IsSuccessStatusCode)
        //            {
        //                return new Response
        //                {
        //                    IsSuccess = false,
        //                    Message = result,
        //                };
        //            }

        //            var list = JsonConvert.DeserializeObject<List<T>>(result);
        //            return new Response
        //            {
        //                IsSuccess = true,
        //                Message = "Ok",
        //                Result = list,
        //            };
        //        }
        //        catch (Exception ex)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = ex.Message,
        //            };
        //        }
        //    }

        //    public async Task<Response> GetList<T>(
        //        string urlBase,
        //        string servicePrefix,
        //        string controller,
        //        string tokenType,
        //        string accessToken,
        //        int id)
        //    {
        //        try
        //        {
        //            var client = new HttpClient();
        //            client.DefaultRequestHeaders.Authorization =
        //                new AuthenticationHeaderValue(tokenType, accessToken);
        //            client.BaseAddress = new Uri(urlBase);
        //            var url = string.Format(
        //                "{0}{1}/{2}",
        //                servicePrefix,
        //                controller,
        //                id);
        //            var response = await client.GetAsync(url);

        //            if (!response.IsSuccessStatusCode)
        //            {
        //                return new Response
        //                {
        //                    IsSuccess = false,
        //                    Message = response.StatusCode.ToString(),
        //                };
        //            }

        //            var result = await response.Content.ReadAsStringAsync();
        //            var list = JsonConvert.DeserializeObject<List<T>>(result);
        //            return new Response
        //            {
        //                IsSuccess = true,
        //                Message = "Ok",
        //                Result = list,
        //            };
        //        }
        //        catch (Exception ex)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = ex.Message,
        //            };
        //        }
        //    }




        //    //#region GetList 1
        //    //public async Task<Response> GetList<T>(
        //    //   string urlBase,
        //    //   string servicePrefix,
        //    //   string controller

        //    //    )
        //    //{
        //    //    try
        //    //    {
        //    //        var client = new HttpClient();

        //    //        client.BaseAddress = new Uri(urlBase);
        //    //        var url = string.Format("{0}{1}", servicePrefix, controller);
        //    //        var response = await client.GetAsync(url);
        //    //        var result = await response.Content.ReadAsStringAsync();

        //    //        if (!response.IsSuccessStatusCode)
        //    //        {
        //    //            return new Response
        //    //            {
        //    //                IsSuccess = false,
        //    //                Message = result,
        //    //            };
        //    //        }

        //    //        var list = JsonConvert.DeserializeObject<List<T>>(result);
        //    //        return new Response
        //    //        {
        //    //            IsSuccess = true,
        //    //            Message = "Ok",
        //    //            Result = list,
        //    //        };
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        return new Response
        //    //        {
        //    //            IsSuccess = false,
        //    //            Message = ex.Message,
        //    //        };
        //    //    }
        //    //}

        //    //#endregion

        //    //#region GestList 2
        //    //public async Task<Response> GetList<T>(
        //    //  string urlBase,
        //    //  string servicePrefix,
        //    //  string controller,
        //    //  string tokenType,
        //    //  string accessToken,
        //    //  int id)
        //    //{
        //    //    try
        //    //    {
        //    //        var client = new HttpClient();
        //    //        client.DefaultRequestHeaders.Authorization =
        //    //            new AuthenticationHeaderValue(tokenType, accessToken);
        //    //        client.BaseAddress = new Uri(urlBase);
        //    //        var url = string.Format(
        //    //            "{0}{1}/{2}",
        //    //            servicePrefix,
        //    //            controller,
        //    //            id);
        //    //        var response = await client.GetAsync(url);

        //    //        if (!response.IsSuccessStatusCode)
        //    //        {
        //    //            return new Response
        //    //            {
        //    //                IsSuccess = false,
        //    //                Message = response.StatusCode.ToString(),
        //    //            };
        //    //        }

        //    //        var result = await response.Content.ReadAsStringAsync();
        //    //        var list = JsonConvert.DeserializeObject<List<T>>(result);
        //    //        return new Response
        //    //        {
        //    //            IsSuccess = true,
        //    //            Message = "Ok",
        //    //            Result = list,
        //    //        };
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        return new Response
        //    //        {
        //    //            IsSuccess = false,
        //    //            Message = ex.Message,
        //    //        };
        //    //    }
        //    //}
        //    //#endregion

        //    #region Post 1 
        //    public async Task<Response> Post<T>(
        //   string urlBase,
        //   string servicePrefix,
        //   string controller,
        //   string tokenType,
        //   string accessToken,
        //   T model)
        //    {
        //        try
        //        {
        //            var request = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(
        //                request, Encoding.UTF8,
        //                "application/json");
        //            var client = new HttpClient();
        //            client.DefaultRequestHeaders.Authorization =
        //                new AuthenticationHeaderValue(tokenType, accessToken);
        //            client.BaseAddress = new Uri(urlBase);
        //            var url = string.Format("{0}{1}", servicePrefix, controller);
        //            var response = await client.PostAsync(url, content);
        //            var result = await response.Content.ReadAsStringAsync();

        //            if (!response.IsSuccessStatusCode)
        //            {
        //                var error = JsonConvert.DeserializeObject<Response>(result);
        //                error.IsSuccess = false;
        //                return error;
        //            }

        //            var newRecord = JsonConvert.DeserializeObject<T>(result);

        //            return new Response
        //            {
        //                IsSuccess = true,
        //                Message = "Record added OK",
        //                Result = newRecord,
        //            };
        //        }
        //        catch (Exception ex)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = ex.Message,
        //            };
        //        }
        //    }
        //    #endregion

        //    #region
        //    public async Task<Response> Post<T>(
        //        string urlBase,
        //        string servicePrefix,
        //        string controller,
        //        T model)
        //    {
        //        try
        //        {
        //            var request = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(
        //                request,
        //                Encoding.UTF8,
        //                "application/json");
        //            var client = new HttpClient();
        //            client.BaseAddress = new Uri(urlBase);
        //            var url = string.Format("{0}{1}", servicePrefix, controller);
        //            var response = await client.PostAsync(url, content);

        //            if (!response.IsSuccessStatusCode)
        //            {
        //                return new Response
        //                {
        //                    IsSuccess = false,
        //                    Message = response.StatusCode.ToString(),
        //                };
        //            }

        //            var result = await response.Content.ReadAsStringAsync();
        //            var newRecord = JsonConvert.DeserializeObject<T>(result);

        //            return new Response
        //            {
        //                IsSuccess = true,
        //                Message = "Record added OK",
        //                Result = newRecord,
        //            };
        //        }
        //        catch (Exception ex)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = ex.Message,
        //            };
        //        }
        //    }
        //    #endregion


    }

}
