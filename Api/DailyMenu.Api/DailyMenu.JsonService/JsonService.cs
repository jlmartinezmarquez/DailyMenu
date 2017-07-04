using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DailyMenu.Extensions;
using DailyMenu.Interfaces.Modules;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Nito.AsyncEx;

namespace DailyMenu.Json
{
    public class JsonService : IJsonService
    {
        private static readonly ConcurrentDictionary<string, string> Headers;

        private readonly HttpClient _client;

        private readonly JsonSerializerSettings _settings;

        private bool _successOnly;

        static JsonService()
        {
            Headers = new ConcurrentDictionary<string, string>();
        }

        public JsonService()
        {
            _client = new HttpClient();

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        }

        public JsonService(HttpClient client)
        {
            this._client = client;

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        }

        public void ClearHeader(string header)
        {
            Headers[header] = null;
        }

        public void ClearHeaders()
        {
            Headers.Clear();
        }

        public HttpResponseMessage Delete(string uri)
        {
            return AsyncContext.Run(() => this.DeleteAsync(uri));
        }

        public T Delete<T>(string uri)
        {
            return AsyncContext.Run(() => this.DeleteAsync<T>(uri));
        }

        public async Task<HttpResponseMessage> DeleteAsync(string uri)
        {
            var request = CreateRequest(HttpMethod.Delete, uri);

            request.Content = SerializeRequest();

            return await _client.SendAsync(request);
        }

        public async Task<T> DeleteAsync<T>(string uri)
        {
            var result = await DeleteAsync(uri);

            if (_successOnly)

                result.EnsureSuccessStatusCode();

            return await DeserialiseResponse<T>(result);
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public void EnableOnlySuccessOnlyMode(bool successOnly = true)
        {
            _successOnly = successOnly;
        }

        public HttpResponseMessage Get(string uri, object data = null)
        {
            return AsyncContext.Run(() => this.GetAsync(uri, data));
        }

        public T Get<T>(string uri, object data = null)
        {
            return AsyncContext.Run(() => this.GetAsync<T>(uri, data));
        }

        public async Task<HttpResponseMessage> GetAsync(string uri, object data = null)
        {
            var fullUri = uri + data.ToQueryString();

            var requestMessage = CreateRequest(HttpMethod.Get, fullUri);

            return await _client.SendAsync(requestMessage);
        }

        public async Task<T> GetAsync<T>(string uri, object data = null)
        {
            var result = await GetAsync(uri, data);

            if (_successOnly)

                result.EnsureSuccessStatusCode();

            return await DeserialiseResponse<T>(result);
        }

        public string GetString(string uri, object data = null)
        {
            return AsyncContext.Run(() => this.GetStringAsync(uri, data));
        }

        public async Task<string> GetStringAsync(string uri, object data = null)
        {
            var fullUri = uri + data.ToQueryString();

            var requestMessage = CreateRequest(HttpMethod.Get, fullUri);

            var result = await _client.SendAsync(requestMessage);

            if (_successOnly)

                result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsStringAsync();
        }

        public HttpResponseMessage Patch(string uri, object data = null, bool dontSerialize = false)
        {
            return AsyncContext.Run(() => PatchAsync(uri, data, dontSerialize));
        }

        public T Patch<T>(string uri, object data = null, bool dontSerialize = false)
        {
            return AsyncContext.Run(() => PatchAsync<T>(uri, data, dontSerialize));
        }

        public async Task<HttpResponseMessage> PatchAsync(string uri, object data = null, bool dontSerialize = false)
        {
            var request = CreateRequest(new HttpMethod("PATCH"), uri);

            if (dontSerialize)
            {
                request.Content = new StringContent(data.SafeToString(), Encoding.UTF8, "application/json");
            }
            else
            {
                if (data != null)

                    request.Content = SerializeRequest(data);
            }

            return await _client.SendAsync(request);
        }

        public async Task<T> PatchAsync<T>(string uri, object data = null, bool dontSerialize = false)
        {
            var result = await PatchAsync(uri, data, dontSerialize);

            if (_successOnly)

                result.EnsureSuccessStatusCode();

            return await DeserialiseResponse<T>(result);
        }

        public HttpResponseMessage Post(string uri, object data = null, bool dontSerialize = false)
        {
            return AsyncContext.Run(() => this.PostAsync(uri, data, dontSerialize));
        }

        public T Post<T>(string uri, object data = null, bool dontSerialize = false)
        {
            return AsyncContext.Run(() => this.PostAsync<T>(uri, data, dontSerialize));
        }

        public async Task<HttpResponseMessage> PostAsync(string uri, object data = null, bool dontSerialize = false)
        {
            var request = CreateRequest(HttpMethod.Post, uri);

            if (dontSerialize)
            {
                request.Content = new StringContent(data.SafeToString(), Encoding.UTF8, "application/json");
            }
            else
            {
                if (data != null)

                    request.Content = SerializeRequest(data);
            }

            return await _client.SendAsync(request);
        }

        public async Task<T> PostAsync<T>(string uri, object data = null, bool dontSerialize = false)
        {
            var result = await PostAsync(uri, data, dontSerialize);

            if (_successOnly) result.EnsureSuccessStatusCode();

            return await DeserialiseResponse<T>(result);
        }

        public HttpResponseMessage Put(string uri, object data = null, bool dontSerialize = false)
        {
            return AsyncContext.Run(() => this.PutAsync(uri, data, dontSerialize));
        }

        public T Put<T>(string uri, object data = null, bool dontSerialize = false)
        {
            return AsyncContext.Run(() => this.PutAsync<T>(uri, data, dontSerialize));
        }

        public async Task<HttpResponseMessage> PutAsync(string uri, object data = null, bool dontSerialize = false)
        {
            var request = CreateRequest(HttpMethod.Put, uri);

            if (dontSerialize)
            {
                request.Content = new StringContent(data.SafeToString(), Encoding.UTF8, "application/json");
            }
            else
            {
                if (data != null)
                    request.Content = SerializeRequest(data);
            }

            return await _client.SendAsync(request);
        }

        public async Task<T> PutAsync<T>(string uri, object data = null, bool dontSerialize = false)
        {
            var result = await PutAsync(uri, data, dontSerialize);

            if (_successOnly)

                result.EnsureSuccessStatusCode();

            return await DeserialiseResponse<T>(result);
        }

        public void SetHeader(string header, string value)
        {
            Headers[header] = value;
        }

        private HttpContent SerializeRequest(object data = null)
        {
            StringContent request;

            if (data == null)
            {
                request = new StringContent(string.Empty);
            }
            else
            {
                var serializeObject = JsonConvert.SerializeObject(data, _settings);

                request = new StringContent(serializeObject);
            }

            request.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return request;
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string uri)
        {
            var requestMessage = new HttpRequestMessage(method, uri);

            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            foreach (var headerValue in Headers.Where(xy => !string.IsNullOrEmpty(xy.Value)))

                requestMessage.Headers.Add(headerValue.Key, headerValue.Value);

            return requestMessage;
        }

        private async Task<T> DeserialiseResponse<T>(HttpResponseMessage result)
        {
            var payload = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(payload, _settings);
        }

    }
}
