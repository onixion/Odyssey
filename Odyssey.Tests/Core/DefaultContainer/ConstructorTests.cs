using Microsoft.VisualStudio.TestTools.UnitTesting;
using Odyssey.Contracts;
using System.Collections.Generic;

namespace Odyssey.Tests.Core.DefaultContainer
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
            new Odyssey.Core.DefaultContainer(new List<Registration> { });
        }

        /// <summary>
        /// Test the constructor.
        /// </summary>
        [TestMethod]
        public void Constructor()
        {
            new Odyssey.Core.DefaultContainer(new List<Registration>
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
