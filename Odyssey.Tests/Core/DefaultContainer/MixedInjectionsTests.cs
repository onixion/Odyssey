using Microsoft.VisualStudio.TestTools.UnitTesting;
using Odyssey.Contracts;
using System.Collections.Generic;
using System.IO;

namespace Odyssey.Tests.Core.DefaultContainer
{
    /// <summary>
    /// Mixed injections tests.
    /// </summary>
    /// <remarks>
    /// There are so many ways to combine injections that
    /// it is impossible to test all combinations. This test
    /// class has many very different mixed injection unit
    /// tests, which will cover the most common ways injections
    /// will be used. Obviously, there are many ways to mix them.
    /// </remarks>
    [TestClass]
    public class MixedInjectionsTests
    {
        /// <summary>
        /// This test will create register a logger implementation and wrap
        /// it in a decorator. It then executes the log method of the logger
        /// and checks if the decorator does its job.
        /// </summary>
        [TestMethod]
        public void MixedTest1()
        {
            MemoryStream memoryStream = new MemoryStream();

            IContainer container = new Odyssey.Core.DefaultContainer(new List<Registration>
            {
                new Registration(
                    typeof(ILogger),
                    typeof(Logger),
                    injections: new Injections(
                        constructorInjection: new ConstructorInjection(new List<ParameterInjection>()
                        {
                            new ParameterInjection("memoryStream", memoryStream),
                        }),
                        decoratorInjection: new DecoratorRegistration(
                            typeof(LoggerDecorator),
                            new Injections(
                                new ConstructorInjection(new List<ParameterInjection>()
                                {
                                    new ParameterInjection("text", "[Test]")
                                })))))
            });

            ILogger service = (ILogger)container.Resolve(new Resolution(typeof(ILogger)));

            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(LoggerDecorator));

            service.Log("Hello");

            memoryStream.Seek(0, SeekOrigin.Begin);
            TextReader textReader = new StreamReader(memoryStream);
            Assert.AreEqual("[Test]Hello", textReader.ReadToEnd());
        }

        interface ILogger
        {
            void Log(string message);
        }

        class Logger : ILogger
        {
            readonly TextWriter textWriter;

            public Logger(MemoryStream memoryStream)
            {
                textWriter = new StreamWriter(memoryStream);
            }

            public void Log(string message)
            {
                textWriter.Write(message);
                textWriter.Flush();
            }
        }

        class LoggerDecorator : ILogger
        {
            readonly ILogger logger;

            readonly string text;

            public LoggerDecorator(ILogger logger, string text)
            {
                this.logger = logger;
                this.text = text;
            }

            public void Log(string message)
            {
                logger.Log(text + message);
            }
        }

    }
}
