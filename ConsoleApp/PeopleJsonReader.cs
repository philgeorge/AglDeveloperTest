using AglDeveloperTest.InputModel;
using AglDeveloperTest.Output;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AglDeveloperTest
{
    public class PeopleJsonReader
    {
        // this would more normally be an app setting, if we were working with a API that exposed separate test and production end points
        private const string _baseUrl = "http://agl-developer-test.azurewebsites.net/";

        private ILogger _logger;

        public PeopleJsonReader(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<People> GetModel()
        {
            var json = await GetJsonString();
            if (json == null)
            {
                return null;
            }
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            options.Converters.Add(new JsonStringEnumConverter());
            var owners = JsonSerializer.Deserialize<ICollection<Owner>>(json, options);
            return new People { Owners = owners };
        }

        private async Task<string> GetJsonString()
        {
            using (var httpClient = new HttpClient()) // see not on HttpClient usage in README.md file
            {
                httpClient.BaseAddress = new Uri(_baseUrl);
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.Timeout = TimeSpan.FromSeconds(10);

                var response = await httpClient.GetAsync("people.json");
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }

                _logger.Error($"JSON Request failed with {response.StatusCode}");
                return null;
            }
        }
    }
}
