using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartContainer.Contracts;
using SmartContainer.Extensions;
using System;
using System.Collections.Generic;

namespace Odyssey.Tests.Extensions
{
    /// <summary>
    /// Test class for <see cref="ResolveExtensions"/>
    /// </summary>
    [TestClass]
    public class ResolveExtensionsTests
    {
        /// <summary>
        /// Test resolve.
        /// </summary>
        public void TestResolve()
        {
            Bmw bmw = new Bmw();

            FakeContainer fakeContainer = new FakeContainer(resolveFunc: (resolution) =>
            {
                Assert.AreEqual(typeof(Bmw), resolution.InterfaceType);

                return bmw;
            });

            var service = fakeContainer.Resolve<ICar>();

            Assert.IsNotNull(service);
            Assert.AreEqual(typeof(Bmw), service.GetType());
        }

        /// <summary>
        /// Car interface.
        /// </summary>
        interface ICar
        {
        }

        /// <summary>
        /// Car implementation.
        /// </summary>
        class Bmw : ICar
        {

        }

        /// <summary>
        /// Fake container.
        /// </summary>
        class FakeContainer : IContainer
        {
            /// <summary>
            /// Resolve func.
            /// </summary>
            readonly Func<Resolution, object> resolveFunc;

            /// <summary>
            /// Dispose action.
            /// </summary>
            readonly Action disposeAction;

            /// <summary>
            /// Create container func.
            /// </summary>
            readonly Func<IEnumerable<Registration>, IContainer, IContainer> createContainerFunc;

            public FakeContainer(
                Func<Resolution,object> resolveFunc = null, 
                Func<IEnumerable<Registration>, IContainer, IContainer> createContainerFunc = null,
                Action disposeAction =null)
            {
                this.resolveFunc = resolveFunc;
                this.disposeAction = disposeAction;
                this.createContainerFunc = createContainerFunc;
            }

            public IContainer CreateContainer(IEnumerable<Registration> registrations, IContainer parentContainer = null)
            {
                return createContainerFunc?.Invoke(registrations, parentContainer);
            }

            public void Dispose()
            {
                disposeAction?.Invoke();
            }

            public object Resolve(Resolution resolution)
            {
                return resolveFunc?.Invoke(resolution);
            }
        }
    }
}
