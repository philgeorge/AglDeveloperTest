using AglDeveloperTest.Output;
using System;
using System.Threading.Tasks;

namespace AglDeveloperTest
{
    class Program
    {
        static async Task Main(string[] _)
        {
            // see http://agl-developer-test.azurewebsites.net/

            var logger = new ConsoleLogger();

            var reader = new PeopleJsonReader(logger);
            var jsonModel = await reader.GetModel();

            if (jsonModel != null)
            {
                Console.WriteLine($"Number of people: {jsonModel.Owners.Count}");
            }

            //todo: method to transform
            
            //todo: unit tests            
            //todo: DI for JSON web request and Console output

            // HttpClientFactory for web request - note where to add auth, retries, etc.

            // end-to-end integration tests
        }
    }
}
