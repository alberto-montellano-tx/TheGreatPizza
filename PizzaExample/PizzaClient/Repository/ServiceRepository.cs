using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PizzaClient.Repository
{
    public class ServiceRepository
    {
        private const string MEDIATYPE_APPLICATION_JSON = "application/json";
        public HttpClient Client { get; set; }

        public ServiceRepository()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ServiceUrl"].ToString());
        }

        public HttpResponseMessage GetResponse(string url)
        {
            return Client.GetAsync(url).Result;
        }
        public HttpResponseMessage PutResponse(string url, JObject model)
        {
            var stringContent = new StringContent(model.ToString(), UnicodeEncoding.UTF8, MEDIATYPE_APPLICATION_JSON);
            return Client.PutAsync(url, stringContent).Result;
        }

        public HttpResponseMessage PostResponse(string url, JObject model)
        {
            var stringContent = new StringContent(model.ToString(), UnicodeEncoding.UTF8, MEDIATYPE_APPLICATION_JSON);
            return Client.PostAsync(url, stringContent).Result;
        }

        public HttpResponseMessage PutResponse(string url, Object model)
        {
            var json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, MEDIATYPE_APPLICATION_JSON);
            return Client.PutAsync(url, stringContent).Result;
        }

        public HttpResponseMessage PostResponse(string url, Object model)
        {
             var json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, MEDIATYPE_APPLICATION_JSON);
            return Client.PostAsync(url, stringContent).Result;
        }
        
        public HttpResponseMessage DeleteResponse(string url)
        {
            return Client.DeleteAsync(url).Result;
        }
    }
}