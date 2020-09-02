using System;

namespace AglDeveloperTest.Output
{
    public interface ILogger
    {
        void WriteLine(string message);
    }
    public class ConsoleLogger : ILogger
    {
        public void WriteLine(string message) => Console.WriteLine(message);
    }
}
