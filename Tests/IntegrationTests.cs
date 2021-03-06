﻿using System.Threading.Tasks;
using AglDeveloperTest.Output;
using NUnit.Framework;

namespace AglDeveloperTest.Tests
{
    [Category("Integration")]
    public class IntegrationTests
    {
        [Test]
        public async Task FetchJson_Convert_WriteOutput()
        {
            var logger = new TestContextLogger();
            var reader = new PeopleJsonReader(logger);
            var jsonModel = await reader.GetModel();
            Assert.IsNotNull(jsonModel);

            var converter = new PeopleConverter();
            var model = converter.Convert(jsonModel);
            var writer = new ConsoleOutputWriter(logger);
            writer.Write(model);
        }
    }
}
