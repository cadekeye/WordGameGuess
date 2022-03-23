using ApiServices.Interface;
using DataService.Interface;
using GuessWordWebApp.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataService
{
    public class WordDataService : IWordDataService
    {
        private readonly IWordApiService _wordApiService;
        private readonly ApiConfiguration _apiConfiguration;

        public WordDataService(IWordApiService wordApiService,
            IOptionsMonitor<ApiConfiguration> apiConfiguration)
        {
            _wordApiService = wordApiService;
            _apiConfiguration = apiConfiguration.CurrentValue;
        }

        public async Task<IEnumerable<string>> GetAllWordsAsync()
        {
            //var result  = await _wordApiService.GetAsync<List<string>>($"{_apiConfiguration.ApiServiceBaseUrl}/hello");

            return null;
        }
    }
}
