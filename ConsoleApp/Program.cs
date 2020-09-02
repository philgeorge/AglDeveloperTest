using AglDeveloperTest.Output;
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
                var converter = new PeopleConverter();
                var model = converter.Convert(jsonModel);
                var writer = new ConsoleOutputWriter(logger);
                writer.Write(model);
            }
            
            // todo: DI for JSON web request and Console output

            // HttpClientFactory for web request - note where to add auth, retries, etc.

            // end-to-end integration tests
        }
    }
}
