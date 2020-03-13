using Microsoft.VisualStudio.TestTools.UnitTesting;
using Odyssey.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Odyssey.Tests.Core.DefaultContainer
{
    /// <summary>
    /// Constructor injection resolve tests.
    /// </summary>
    [TestClass]
    public class ConstructorInjectionResolveTests
    {
        /// <summary>
        /// Minimal.
        /// </summary>
        [TestMethod]
        public void Minimal()
        {
            IContainer container = new Odyssey.Core.DefaultContainer(new List<Registration>
            {
                new Registration(typeof(IInterface1), typeof(Implementation1)),
            });

            IInterface1 service = (IInterface1)container.Resolve(new Resolution(typeof(IInterface1)));

            Assert.IsNotNull(service);
            Assert.IsNotNull(service.Inter);
        }

        interface IInterface1
        {
            IInterface2 Inter { get; }
        }

        class Implementation1 : IInterface1
        {
            [Resolve]
            public IInterface2 Inter { get; set; }
        }

        interface IInterface2
        {
        }

        class Implementation2 : IInterface2
        {
        }
    }
}
