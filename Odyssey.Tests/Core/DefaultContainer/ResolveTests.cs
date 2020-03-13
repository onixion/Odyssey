using Microsoft.VisualStudio.TestTools.UnitTesting;
using Odyssey.Contracts;
using System.Collections.Generic;

namespace Odyssey.Tests.Core.DefaultContainer
{
    /// <summary>
    /// Resolve tests.
    /// </summary>
    [TestClass]
    public class ResolveTests
    {
        /// <summary>
        /// Minimal resolve.
        /// </summary>
        [TestMethod]
        public void Minimal()
        {
            IContainer container = new Odyssey.Core.DefaultContainer(new List<Registration>
            {
                new Registration(typeof(IInterface), typeof(Implementation)),
            });

            IInterface service = (IInterface)container.Resolve(new Resolution(typeof(IInterface)));

            Assert.IsNotNull(service);
        }

        /// <summary>
        /// Minimal create on resolve.
        /// </summary>
        [TestMethod]
        public void MinimalCreateOnResolve()
        {
            IContainer container = new Odyssey.Core.DefaultContainer(new List<Registration>
            {
                new Registration(typeof(IInterface), typeof(Implementation), createOnResolve: true),
            });

            IInterface service1 = (IInterface)container.Resolve(new Resolution(typeof(IInterface)));
            IInterface service2 = (IInterface)container.Resolve(new Resolution(typeof(IInterface)));

            Assert.IsNotNull(service1);
            Assert.IsNotNull(service2);
            Assert.IsFalse(ReferenceEquals(service1, service2));
        }

        /// <summary>
        /// Minimal instance.
        /// </summary>
        [TestMethod]
        public void MinimalInstance()
        {
            IInterface originalService = new Implementation();

            IContainer container = new Odyssey.Core.DefaultContainer(new List<Registration>
            {
                new Registration(typeof(IInterface), typeof(Implementation), instance: originalService),
            });

            IInterface service1 = (IInterface)container.Resolve(new Resolution(typeof(IInterface)));
            IInterface service2 = (IInterface)container.Resolve(new Resolution(typeof(IInterface)));

            Assert.IsNotNull(service1);
            Assert.IsNotNull(service2);
            Assert.IsTrue(ReferenceEquals(service1, originalService));
            Assert.IsTrue(ReferenceEquals(service2, originalService));
            Assert.IsTrue(ReferenceEquals(service1, service2));
        }

        interface IInterface
        {
        }

        class Implementation : IInterface
        {
        }
    }
}
