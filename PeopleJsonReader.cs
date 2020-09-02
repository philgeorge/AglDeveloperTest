using AglDeveloperTest.InputModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AglDeveloperTest
{
    public class PeopleJsonReader
    {
        private const string _baseUrl = "http://agl-developer-test.azurewebsites.net/"; // this would more normally be an app setting, if we were working with a API that exposed test and production end points

        private async Task<string> GetJsonString()
        {
            // I am well aware of the problems with using HttpClient directly
            // https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
            // However in the case of a simple console app which makes one API call and then exits, it is a non-issue. For longer running processes, the HttpClientFactory should be used.
            using (var httpClient = new HttpClient())
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
                //todo: log error
                return null;
            }

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
    }
}
