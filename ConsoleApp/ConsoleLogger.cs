using System;

namespace AglDeveloperTest.Output
{
    // this abstraction is useful in case the output should be switched to something other than the Console
    public interface ILogger
    {
        void WriteLine(string message);
        void Error(string message);
    }

    public class ConsoleLogger : ILogger
    {
        public void WriteLine(string message) => Console.WriteLine(message);
        public void Error(string message) => Console.WriteLine(message);
    }
}
