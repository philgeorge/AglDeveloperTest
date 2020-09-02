using AglDeveloperTest.OutputModel;

namespace AglDeveloperTest.Output
{
    public class ConsoleOutputWriter
    {
        private ILogger _logger;

        public ConsoleOutputWriter(ILogger logger)
        {
            _logger = logger;
        }

        public void Write(Model model)
        {
            foreach(var gender in model.Genders)
            {
                _logger.WriteLine(gender.Gender);
                foreach(var cat in gender.CatNames)
                {
                    _logger.WriteLine($"* {cat}");
                }
            }
        }
    }
}
