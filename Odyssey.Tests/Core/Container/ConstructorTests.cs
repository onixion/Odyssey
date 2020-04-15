using Microsoft.VisualStudio.TestTools.UnitTesting;
using Odyssey.Contracts;
using Odyssey.Core;
using System.Collections.Generic;

namespace Odyssey.Tests.Core.Container
{
    /// <summary>
    /// Constructor tests.
    /// </summary>
    [TestClass]
    public class ConstructorTests
    {
        /// <summary>
        /// Test the constructor.
        /// </summary>
        [TestMethod]
        public void ConstructorEmpty()
        {
            new OdysseyContainer(new List<Registration> { });
        }

        /// <summary>
        /// Test the constructor.
        /// </summary>
        [TestMethod]
        public void Constructor()
        {
            new OdysseyContainer(new List<Registration>
            {
                new Registration(typeof(IInterface), typeof(Implementation)),
            });
        }

        interface IInterface
        {
        }

        class Implementation : IInterface
        {
        }
    }
}
