using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DailyMenu.Interfaces.Modules
{
    public interface IJsonService : IDisposable
    {
        T Get<T>(string uri, object data = null);
        Task<T> GetAsync<T>(string uri, object data = null);
        HttpResponseMessage Get(string uri, object data = null);
        Task<HttpResponseMessage> GetAsync(string uri, object data = null);
        string GetString(string uri, object data = null);
        Task<string> GetStringAsync(string uri, object data = null);
        T Post<T>(string uri, object data = null, bool dontSerialize = false);
        Task<T> PostAsync<T>(string uri, object data = null, bool dontSerialize = false);
        HttpResponseMessage Post(string uri, object data = null, bool dontSerialize = false);
        Task<HttpResponseMessage> PostAsync(string uri, object data = null, bool dontSerialize = false);
        T Put<T>(string uri, object data = null, bool dontSerialize = false);
        Task<T> PutAsync<T>(string uri, object data = null, bool dontSerialize = false);
        HttpResponseMessage Put(string uri, object data = null, bool dontSerialize = false);
        Task<HttpResponseMessage> PutAsync(string uri, object data = null, bool dontSerialize = false);
        T Delete<T>(string uri);
        Task<T> DeleteAsync<T>(string uri);
        HttpResponseMessage Delete(string uri);
        Task<HttpResponseMessage> DeleteAsync(string uri);
        T Patch<T>(string uri, object data = null, bool dontSerialize = false);
        Task<T> PatchAsync<T>(string uri, object data = null, bool dontSerialize = false);
        HttpResponseMessage Patch(string uri, object data = null, bool dontSerialize = false);
        Task<HttpResponseMessage> PatchAsync(string uri, object data = null, bool dontSerialize = false);

        void SetHeader(string header, string value);
        void ClearHeader(string header);
        void ClearHeaders();
        void EnableOnlySuccessOnlyMode(bool successOnly = true);
    }

}
