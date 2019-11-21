using Microsoft.VisualStudio.TestTools.UnitTesting;
using Odyssey.Contracts;
using Odyssey.Core;
using System.Collections.Generic;

namespace Odyssey.Tests.Core
{
    /// <summary>
    /// Test class for <see cref="Container"/>.
    /// </summary>
    [TestClass]
    public class ContainerTests
    {
        /// <summary>
        /// Tests constructor and destructor.
        /// </summary>
        [TestMethod]
        public void TestConstructorDestructor()
        {
            IList<Registration> registrations = new List<Registration>
            {
                new Registration(typeof(ICar), typeof(Bmw)),
            };

            using (IContainer container = new DefaultContainer(registrations))
            {
            }
        }

        /// <summary>
        /// Test simple "Lifetime.CreateOnce" resolve.
        /// </summary>
        [TestMethod]
        public void TestSimpleCreateOnceResolve()
        {
            IList<Registration> registrations = new List<Registration>
            {
                new Registration(typeof(ICar), typeof(Bmw)),
            };

            using (IContainer container = new DefaultContainer(registrations))
            {
                ICar car = (ICar)container.Resolve(new Resolution(typeof(ICar)));
                Assert.IsNotNull(car);
                Assert.IsTrue(car.GetType() == typeof(Bmw));

                Assert.IsTrue(ReferenceEquals(car, container.Resolve(new Resolution(typeof(ICar)))),
                    "Expected same references.");
            }
        }

        /// <summary>
        /// Test simple lifetime 'CreateOnResolve' resolve.
        /// </summary>
        [TestMethod]
        public void TestSimpleCreateOnResolve()
        {
            IList<Registration> registrations = new List<Registration>
            {
                new Registration(typeof(ICar), typeof(Bmw), true),
            };

            using (IContainer container = new DefaultContainer(registrations))
            {
                ICar car1 = (ICar)container.Resolve(new Resolution(typeof(ICar)));
                Assert.IsNotNull(car1);
                Assert.IsTrue(car1.GetType() == typeof(Bmw));

                ICar car2 = (ICar)container.Resolve(new Resolution(typeof(ICar)));
                Assert.IsNotNull(car2);
                Assert.IsTrue(car2.GetType() == typeof(Bmw));

                Assert.IsFalse(ReferenceEquals(car1, car2), "References should be different.");
            }
        }

        /// <summary>
        /// Test resolving the same interface with different names.
        /// </summary>
        [TestMethod]
        public void TestNameResolve()
        {
            IList<Registration> registrations = new List<Registration>
            {
                new Registration(typeof(IAnimal), typeof(Cat), name: "Cat"),
                new Registration(typeof(IAnimal), typeof(Dog), name: "Dog"),
            };

            using (IContainer container = new DefaultContainer(registrations))
            {
                IAnimal cat = (IAnimal)container.Resolve(new Resolution(typeof(IAnimal), "Cat"));
                Assert.IsNotNull(cat);
                Assert.IsTrue(cat.GetType() == typeof(Cat));

                IAnimal dog = (IAnimal)container.Resolve(new Resolution(typeof(IAnimal), "Dog"));
                Assert.IsNotNull(dog);
                Assert.IsTrue(dog.GetType() == typeof(Dog));

                Assert.IsTrue(ReferenceEquals(cat, container.Resolve(new Resolution(typeof(IAnimal), "Cat"))),
                    "Expected same references.");

                Assert.IsTrue(ReferenceEquals(dog, container.Resolve(new Resolution(typeof(IAnimal), "Dog"))),
                    "Expected same references.");

                Assert.IsFalse(ReferenceEquals(cat, dog), "References should be different.");
            }
        }

        /// <summary>
        /// Test parameter injection register.
        /// </summary>
        [TestMethod]
        public void TestParameterInjectionOnRegister()
        {
            IList<Registration> registrations = new List<Registration>
            {
                new Registration(
                    typeof(IComputer), 
                    typeof(LenovoT420),
                    parameterInjections: new ParameterInjection[] { new ParameterInjection(new LithiumIonBattery()) }),
            };

            using (IContainer container = new DefaultContainer(registrations))
            {
                IComputer computer = (IComputer)container.Resolve(new Resolution(typeof(IComputer)));

                Assert.IsNotNull(computer);
                Assert.IsTrue(computer.GetType() == typeof(LenovoT420));

                Assert.IsNotNull(computer.Battery);
                Assert.IsTrue(computer.Battery.GetType() == typeof(LithiumIonBattery));

                Assert.IsTrue(ReferenceEquals(computer, container.Resolve(new Resolution(typeof(IComputer)))),
                    "Expected same references.");
            }
        }

        /// <summary>
        /// Test parameter injection register.
        /// </summary>
        [TestMethod]
        public void TestParameterInjectionOnResolve()
        {
            IList<Registration> registrations = new List<Registration>
            {
                new Registration(
                    typeof(IComputer),
                    typeof(LenovoT420),
                    true),
            };

            using (IContainer container = new DefaultContainer(registrations))
            {
                IComputer computer = (IComputer)container.Resolve(
                    new Resolution(typeof(IComputer), parameterInjections: new ParameterInjection[] { new ParameterInjection(new LithiumIonBattery()) }));

                Assert.IsNotNull(computer);
                Assert.IsTrue(computer.GetType() == typeof(LenovoT420));

                Assert.IsNotNull(computer.Battery);
                Assert.IsTrue(computer.Battery.GetType() == typeof(LithiumIonBattery));
            }
        }

        /// <summary>
        /// Test resolve from parent container.
        /// </summary>
        [TestMethod]
        public void TestParentResolve()
        {
            var bmw = new Bmw();

            IList<Registration> registrations = new List<Registration>
            {
                new Registration(
                    typeof(ICar),
                    typeof(Bmw),
                    instance: bmw),
            };

            using (IContainer parentContainer = new DefaultContainer(registrations))
            {
                using (IContainer container = new DefaultContainer(new List<Registration>(), parentContainer))
                {
                    ICar car = (ICar)container.Resolve( new Resolution(typeof(ICar)));

                    Assert.IsNotNull(car);
                    Assert.IsTrue(car.GetType() == typeof(Bmw));
                    // FIX THIS
                    //Assert.IsTrue(car == bmw);
                }
            }
        }

        #region Test interfaces and classes

        // <summary>
        /// Car interface.
        /// </summary>
        interface ICar
        {
        }

        /// <summary>
        /// Bmw.
        /// </summary>
        class Bmw : ICar
        {
        }

        /// <summary>
        /// Animal.
        /// </summary>
        interface IAnimal
        {
        }

        /// <summary>
        /// Cat.
        /// </summary>
        class Cat : IAnimal
        {
        }

        /// <summary>
        /// Dog.
        /// </summary>
        class Dog : IAnimal
        {
        }

        /// <summary>
        /// Computer interface.
        /// </summary>
        interface IComputer
        {
            /// <summary>
            /// Battery.
            /// </summary>
            IBattery Battery { get; }
        }

        /// <summary>
        /// Lenovo T420.
        /// </summary>
        class LenovoT420 : IComputer
        {
            /// <summary>
            /// Battery.
            /// </summary>
            public IBattery Battery { get; }

            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="battery"></param>
            public LenovoT420(IBattery battery)
            {
                Battery = battery;
            }
        }

        /// <summary>
        /// Battery interface.
        /// </summary>
        interface IBattery
        {
        }

        /// <summary>
        /// Lithium ion battery.
        /// </summary>
        class LithiumIonBattery : IBattery
        {
        }

        #endregion
    }
}
