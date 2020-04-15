using Microsoft.VisualStudio.TestTools.UnitTesting;
using Odyssey.Contracts;
using System.Collections.Generic;

namespace Odyssey.Tests.Core.Container
{
    /// <summary>
    /// Decorator injection tests.
    /// </summary>
    [TestClass]
    public class DecoratorInjectionTests
    {
        /// <summary>
        /// Minimal.
        /// </summary>
        [TestMethod]
        public void Minimal()
        {
            IContainer container = new Odyssey.Core.OdysseyContainer(new List<Registration>
            {
                new Registration(
                    typeof(IInterface),
                    typeof(Implementation),
                    injections: new Injections(
                        decoratorInjection: new DecoratorRegistration(
                            typeof(Decorator)))),
            });

            IInterface service = (IInterface)container.Resolve(new Resolution(typeof(IInterface)));

            Assert.IsNotNull(service);
            Assert.AreEqual(service.Number, 10);
        }

        interface IInterface
        {
            int Number { get; }
        }

        class Implementation : IInterface
        {
            public int Number { get; } = 10;
        }

        class Decorator : IInterface
        {
            readonly IInterface instance;

            public int Number => instance.Number;

            public Decorator(IInterface instance)
            {
                this.instance = instance;
            }
        }
    }
}
