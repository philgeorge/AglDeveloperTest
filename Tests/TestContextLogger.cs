using AglDeveloperTest.Output;
using NUnit.Framework;

namespace AglDeveloperTest.Tests
{
    public class TestContextLogger : ILogger
    {
        public void WriteLine(string message)
        {
            TestContext.WriteLine(message); // this will write to the VS Test Explorer "Test Detail Summary" frame
        }

        public void Error(string message)
        {
            Assert.Fail(message);
        }
    }
}
