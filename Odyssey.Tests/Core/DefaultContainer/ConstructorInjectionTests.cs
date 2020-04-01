using Microsoft.VisualStudio.TestTools.UnitTesting;
using Odyssey.Contracts;
using Odyssey.Core;
using System.Collections.Generic;

namespace Odyssey.Tests.Core.DefaultContainer
{
    /// <summary>
    /// Constructor injection tests.
    /// </summary>
    [TestClass]
    public class ConstructorInjectionTests
    {
        /// <summary>
        /// Minimal constructor injection test.
        /// </summary>
        [TestMethod]
        public void Minimal()
        {
            IContainer container = Container.New(new List<Registration>
            {
                new Registration(
                    typeof(IInterface), 
                    typeof(Implementation), 
                    injections: new Injections(new ConstructorInjection(new List<ParameterInjection>()
                    {
                        new ParameterInjection("number", 2),
                    }))),
            }, true);

            IInterface service = (IInterface)container.Resolve(new Resolution(typeof(IInterface)));

            Assert.IsNotNull(service);
            Assert.AreEqual(service.Number, 2);
        }

        interface IInterface
        {
            int Number { get; }
        }

        class Implementation : IInterface
        {
            public int Number { get; }

            public Implementation(int number)
            {
                Number = number;
            }
        }
    }
}
