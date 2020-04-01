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
                new Registration(typeof(IInterface), typeof(Implementation)),
                new Registration(typeof(IInterface2), typeof(Implementation2)),
            });

            IInterface2 service = (IInterface2)container.Resolve(new Resolution(typeof(IInterface2)));

            Assert.IsNotNull(service);
            Assert.IsNotNull(service.Inter);
            Assert.IsInstanceOfType(service.Inter, typeof(IInterface));
        }

        interface IInterface
        {
        }

        class Implementation : IInterface
        {
        }

        interface IInterface2
        {
            IInterface Inter { get; set; }
        }

        class Implementation2 : IInterface2
        {
            [Resolve]
            public IInterface Inter { get; set; }
        }
    }
}
