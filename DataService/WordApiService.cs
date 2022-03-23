using ApiServices.Interface;
using IntegrationService.Models.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiServices
{
    public class WordApiService : IWordApiService
    {
        protected readonly HttpClient _httpClient;

        public WordApiService()
        {
            _httpClient = new HttpClient();
        }
 
        protected async Task<T> GetDataAsync<T>(HttpResponseMessage response)
        {
            await ValidateResponseAsync(response);

            var data = await response.Content.ReadAsStringAsync();

            if (typeof(T) == typeof(string))
            {
                return DeserializeString<T>(data);
            }

            return JsonConvert.DeserializeObject<T>(data);
        }

        private T DeserializeString<T>(string data)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(data);
            }
            catch
            {

                return (T)Convert.ChangeType(data, typeof(T));
            }
        }

        protected virtual async Task ValidateResponseAsync(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                await HandleFailureResponseAsync(response);
            }
        }

        protected async Task HandleFailureResponseAsync(HttpResponseMessage response)
        {
            throw new Exception();
        }

        public async Task<ResponseModel> GetValidWord(string word)
        {
            var response = await _httpClient.GetAsync($"https://api.dictionaryapi.dev/api/v2/entries/en/{word}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result =  (await GetDataAsync<ResponseModel[]>(response)).First();

                result.Valid = true;
                return result;
            }

            return new ResponseModel
            {
                Valid = false
            };
        }
    }
}
