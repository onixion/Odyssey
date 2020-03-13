using Microsoft.VisualStudio.TestTools.UnitTesting;
using Odyssey.Contracts;
using System.Collections.Generic;

namespace Odyssey.Tests.Core.DefaultContainer
{
    /// <summary>
    /// Property injection tests.
    /// </summary>
    [TestClass]
    public class PropertyInjectionTests
    {
        /// <summary>
        /// Minimal constructor injection test.
        /// </summary>
        [TestMethod]
        public void Minimal()
        {
            IContainer container = new Odyssey.Core.DefaultContainer(new List<Registration>
            {
                new Registration(
                    typeof(IInterface),
                    typeof(Implementation),
                    injections: new Injections(propertyInjections: new PropertyInjections(new List<PropertyInjection>()
                    {
                        new PropertyInjection("Number", 5),
                    }))),
            });

            IInterface service = (IInterface)container.Resolve(new Resolution(typeof(IInterface)));

            Assert.IsNotNull(service);
            Assert.AreEqual(service.Number, 5);
        }

        interface IInterface
        {
            int Number { get; set; }
        }

        class Implementation : IInterface
        {
            [PropertyInject]
            public int Number { get; set; }
        }
    }
}
