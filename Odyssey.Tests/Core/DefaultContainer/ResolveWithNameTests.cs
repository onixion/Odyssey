using Microsoft.VisualStudio.TestTools.UnitTesting;
using Odyssey.Contracts;
using System.Collections.Generic;

namespace Odyssey.Tests.Core.DefaultContainer
{
    /// <summary>
    /// Resolve with name tests.
    /// </summary>
    [TestClass]
    public class ResolveWithNameTests
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
                new Registration(typeof(IInterface), typeof(Implementation2), name: "This"),
            });

            IInterface service1 = (IInterface)container.Resolve(new Resolution(typeof(IInterface), name: "This"));
            IInterface service2 = (IInterface)container.Resolve(new Resolution(typeof(IInterface), name: "This"));

            Assert.IsNotNull(service1);
            Assert.IsNotNull(service2);

            Assert.IsTrue(ReferenceEquals(service1, service2));
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
                new Registration(typeof(IInterface), typeof(Implementation2), createOnResolve: true, name: "This"),
            });

            IInterface service1 = (IInterface)container.Resolve(new Resolution(typeof(IInterface), name: "This"));
            IInterface service2 = (IInterface)container.Resolve(new Resolution(typeof(IInterface), name: "This"));
            IInterface service3 = (IInterface)container.Resolve(new Resolution(typeof(IInterface)));

            Assert.IsNotNull(service1);
            Assert.IsNotNull(service2);
            Assert.IsNotNull(service3);

            Assert.IsFalse(ReferenceEquals(service1, service2));
            Assert.IsFalse(ReferenceEquals(service1, service3));
            Assert.IsFalse(ReferenceEquals(service2, service3));
        }

        /// <summary>
        /// Minimal instance.
        /// </summary>
        [TestMethod]
        public void MinimalInstance()
        {
            IInterface originalService1 = new Implementation();
            IInterface originalService2 = new Implementation2();

            IContainer container = new Odyssey.Core.DefaultContainer(new List<Registration>
            {
                new Registration(typeof(IInterface), typeof(Implementation), instance: originalService1),
                new Registration(typeof(IInterface), typeof(Implementation), instance: originalService2, name: "This"),
            });

            IInterface service1 = (IInterface)container.Resolve(new Resolution(typeof(IInterface), name: "This"));
            IInterface service2 = (IInterface)container.Resolve(new Resolution(typeof(IInterface), name: "This"));
            IInterface service3 = (IInterface)container.Resolve(new Resolution(typeof(IInterface)));

            Assert.IsNotNull(service1);
            Assert.IsNotNull(service2);
            Assert.IsNotNull(service3);

            Assert.IsTrue (ReferenceEquals(service1, originalService2));
            Assert.IsTrue (ReferenceEquals(service2, originalService2));
            Assert.IsFalse(ReferenceEquals(service3, originalService2));
            Assert.IsTrue (ReferenceEquals(service3, originalService1));

            Assert.IsTrue (ReferenceEquals(service1, service2));
            Assert.IsFalse(ReferenceEquals(service1, service3));
        }

        interface IInterface
        {
        }

        class Implementation : IInterface
        {
        }

        class Implementation2 : IInterface
        {
        }
    }
}
