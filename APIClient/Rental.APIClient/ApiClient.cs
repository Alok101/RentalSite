using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using Rental.Schema.Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Rental.APIClient
{
    public partial class ApiClient
    {

        private readonly HttpClient _httpClient;
        private Uri BaseEndpoint { get; set; }

        public ApiClient(Uri baseEndpoint)
        {
            if (baseEndpoint == null)
            {
                throw new ArgumentNullException("baseEndpoint");
            }
            BaseEndpoint = baseEndpoint;
            _httpClient = new HttpClient();
        }

        private async Task<Message<T>> GetAsync<T>(Uri requestUrl)
        {
            addHeaders();
            var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Message<T>>(data);
        }

        /// <summary>
        /// Common method for making POST calls
        /// </summary>
        private async Task<Message<T>> PostAsync<T>(Uri requestUrl, T content)
        {
            //addHeaders();
            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Message<T>>(data);
        }

        /// <summary>
        /// Common method for making POST calls
        /// </summary>
        private async Task<Message<T>> PutAsync<T>(Uri requestUrl, T content)
        {
            //addHeaders();
            var response = await _httpClient.PutAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Message<T>>(data);
        }

        /// <summary>
        /// Common method for making Patch calls
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private async Task<Message<T1>> PatchAsync<T1,T2>(Uri requestUrl, T2 content)
        {
            //addHeaders();
            var response = await _httpClient.PatchAsync(requestUrl.ToString(), CreateHttpPatchContent<T2>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Message<T1>>(data);
        }

        private async Task<Message<T1>> PostAsync<T1, T2>(Uri requestUrl, T2 content)
        {
            addHeaders();
            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T2>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Message<T1>>(data);
        }
        private async Task<Message<T>> DeleteAsync<T>(Uri requestUrl)
        {
            //addHeaders();
            var response = await _httpClient.DeleteAsync(requestUrl.ToString());
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Message<T>>(data);
        }
        private Uri CreateRequestUri(string relativePath, string queryString = "")
        {
            var endpoint = new Uri(BaseEndpoint, relativePath);
            var uriBuilder = new UriBuilder(endpoint);
            uriBuilder.Query = queryString;
            return uriBuilder.Uri;
        }

        private string CreateQueryStringParams(Dictionary<string,string> queryCollection)
        {
            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            foreach (var para in queryCollection)
            {
                queryString.Add(para.Key, para.Value);
            }
            return queryString.ToString();
        }

        private HttpContent CreateHttpContent<T>(T content)
        {
            var settings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ" };
            var json = JsonConvert.SerializeObject(content, settings);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private HttpContent CreateHttpPatchContent<T>(T content)
        {
            var settings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mmss.ffZ" };
            var json = JsonConvert.SerializeObject(content, settings);
            return new StringContent(json, Encoding.UTF8, "application/json-patch+json");
        }

        private static JsonSerializerSettings MicrosoftDateFormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }

        private void addHeaders()
        {
            _httpClient.DefaultRequestHeaders.Remove("userIP");
            _httpClient.DefaultRequestHeaders.Add("userIP", "192.168.1.1");
        }
    }
}
