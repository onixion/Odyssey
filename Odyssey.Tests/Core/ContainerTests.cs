using Microsoft.VisualStudio.TestTools.UnitTesting;
using Odyssey.Contracts;
using Odyssey.Core;
using System.Collections.Generic;
using System.Linq;

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

        #region Parameter injection tests

        #region On registration

        /// <summary>
        /// Test resolve parameters on registration using attributes.
        /// </summary>
        [TestMethod]
        public void TestParameterInjectionOnRegistrationWithAttributes()
        {
            IList<Registration> registrations = new List<Registration>
            {
                new Registration(
                    typeof(IDisplay),
                    typeof(BigDisplay)),

                new Registration(
                    typeof(IBattery),
                    typeof(LithiumIonBattery)),

                new Registration(
                    typeof(ISpeaker),
                    typeof(LoudSpeaker)),

                new Registration(
                    typeof(ILaptop),
                    typeof(RedLaptop)),
            };

            using (IContainer container = new DefaultContainer(registrations))
            {
                ILaptop laptop = (ILaptop)container.Resolve(new Resolution(typeof(ILaptop)));
                IDisplay display = (IDisplay)container.Resolve(new Resolution(typeof(IDisplay)));
                IBattery battery = (IBattery)container.Resolve(new Resolution(typeof(IBattery)));
                ISpeaker speaker = (ISpeaker)container.Resolve(new Resolution(typeof(ISpeaker)));

                Assert.IsNotNull(laptop);
                Assert.AreEqual(typeof(RedLaptop), laptop.GetType());

                Assert.IsNotNull(laptop.Display);
                Assert.AreSame(display, laptop.Display);
                Assert.AreEqual(typeof(BigDisplay), laptop.Display.GetType());

                Assert.IsNotNull(laptop.Battery);
                Assert.AreSame(battery, laptop.Battery);
                Assert.AreEqual(typeof(LithiumIonBattery), laptop.Battery.GetType());

                Assert.IsNotNull(laptop.Speaker);
                Assert.AreSame(speaker, laptop.Speaker);
                Assert.AreEqual(typeof(LoudSpeaker), laptop.Speaker.GetType());
            }
        }

        /// <summary>
        /// Test resolve parameters on registration using names.
        /// </summary>
        [TestMethod]
        public void TestParameterInjectionOnRegistrationWithNamedParameters()
        {
            var display = new BigDisplay();
            var battery = new LithiumIonBattery();
            var speaker = new LoudSpeaker();

            IList<Registration> registrations = new List<Registration>
            {
                new Registration(
                    typeof(ILaptop),
                    typeof(BlueLaptop),
                    parameterInjections: new ParameterInjection[]
                    {
                        new ParameterInjection(display, "display"),
                        new ParameterInjection(speaker, "speaker"),
                        new ParameterInjection(battery, "battery"),
                    }),
            };

            using (IContainer container = new DefaultContainer(registrations))
            {
                ILaptop laptop = (ILaptop)container.Resolve(new Resolution(typeof(ILaptop)));

                Assert.IsNotNull(laptop);
                Assert.AreEqual(typeof(BlueLaptop), laptop.GetType());

                Assert.IsNotNull(laptop.Display);
                Assert.AreSame(display, laptop.Display);
                Assert.AreEqual(typeof(BigDisplay), laptop.Display.GetType());

                Assert.IsNotNull(laptop.Battery);
                Assert.AreSame(battery, laptop.Battery);
                Assert.AreEqual(typeof(LithiumIonBattery), laptop.Battery.GetType());

                Assert.IsNotNull(laptop.Speaker);
                Assert.AreSame(speaker, laptop.Speaker);
                Assert.AreEqual(typeof(LoudSpeaker), laptop.Speaker.GetType());
            }
        }

        /// <summary>
        /// Test resolve parameters on registration using no names.
        /// </summary>
        [TestMethod]
        public void TestParameterInjectionOnRegistrationWithUnnamedParameters()
        {
            var display = new BigDisplay();
            var battery = new LithiumIonBattery();
            var speaker = new LoudSpeaker();

            IList<Registration> registrations = new List<Registration>
            {
                new Registration(
                    typeof(ILaptop),
                    typeof(BlueLaptop),
                    parameterInjections: new ParameterInjection[]
                    {
                        new ParameterInjection(speaker),
                        new ParameterInjection(battery),
                        new ParameterInjection(display),
                    }),
            };

            using (IContainer container = new DefaultContainer(registrations))
            {
                ILaptop laptop = (ILaptop)container.Resolve(new Resolution(typeof(ILaptop)));

                Assert.IsNotNull(laptop);
                Assert.AreEqual(typeof(BlueLaptop), laptop.GetType());

                Assert.IsNotNull(laptop.Display);
                Assert.AreSame(display, laptop.Display);
                Assert.AreEqual(typeof(BigDisplay), laptop.Display.GetType());

                Assert.IsNotNull(laptop.Battery);
                Assert.AreSame(battery, laptop.Battery);
                Assert.AreEqual(typeof(LithiumIonBattery), laptop.Battery.GetType());

                Assert.IsNotNull(laptop.Speaker);
                Assert.AreSame(speaker, laptop.Speaker);
                Assert.AreEqual(typeof(LoudSpeaker), laptop.Speaker.GetType());
            }
        }

        /// <summary>
        /// Test resolve parameters on registration using mixed methods.
        /// </summary>
        [TestMethod]
        public void TestParameterInjectionOnRegistrationWithMixedParameters()
        {
            var battery = new LithiumIonBattery();
            var speaker = new HighQualitySpeaker();

            IList<Registration> registrations = new List<Registration>
            {
                 new Registration(
                    typeof(IDisplay),
                    typeof(BigDisplay)),

                new Registration(
                    typeof(ILaptop),
                    typeof(GreenLaptop),
                    parameterInjections: new ParameterInjection[]
                    {
                        new ParameterInjection(speaker),
                        new ParameterInjection(battery, "battery"),
                    }),
            };

            using (IContainer container = new DefaultContainer(registrations))
            {
                ILaptop laptop = (ILaptop)container.Resolve(new Resolution(typeof(ILaptop)));
                IDisplay display = (IDisplay)container.Resolve(new Resolution(typeof(IDisplay)));

                Assert.IsNotNull(laptop);
                Assert.AreEqual(typeof(GreenLaptop), laptop.GetType());

                Assert.IsNotNull(laptop.Display);
                Assert.AreSame(display, laptop.Display);
                Assert.AreEqual(typeof(BigDisplay), laptop.Display.GetType());

                Assert.IsNotNull(laptop.Battery);
                Assert.AreSame(battery, laptop.Battery);
                Assert.AreEqual(typeof(LithiumIonBattery), laptop.Battery.GetType());

                Assert.IsNotNull(laptop.Speaker);
                Assert.AreSame(speaker, laptop.Speaker);
                Assert.AreEqual(typeof(HighQualitySpeaker), laptop.Speaker.GetType());
            }
        }

        #endregion

        #region On resolution

        #endregion

        #endregion

        #region Property injection tests

        #region On registration

        #endregion

        #region On resolution

        #endregion

        #endregion

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

        interface ILaptop
        {
            IBattery Battery { get; }

            IDisplay Display { get; }

            ISpeaker Speaker { get; }
        }

        interface IDisplay
        {
        }

        class BigDisplay : IDisplay
        {
        }

        interface ISpeaker
        {
        }

        class LoudSpeaker : ISpeaker
        {
        }

        class HighQualitySpeaker : ISpeaker
        {
        }

        class RedLaptop : ILaptop
        {
            public IBattery Battery { get; }

            public IDisplay Display { get; }

            public ISpeaker Speaker { get; }

            public RedLaptop([Resolve]IBattery battery, [Resolve]IDisplay display, [Resolve]ISpeaker speaker)
            {
                Battery = battery;
                Display = display;
                Speaker = speaker;
            }
        }

        class GreenLaptop : ILaptop
        {
            public IBattery Battery { get; }

            public IDisplay Display { get; }

            public ISpeaker Speaker { get; }

            public GreenLaptop([Resolve]IBattery battery, ISpeaker speaker, [Resolve]IDisplay display)
            {
                Battery = battery;
                Display = display;
                Speaker = speaker;
            }
        }

        class BlueLaptop : ILaptop
        {
            public IBattery Battery { get; }

            public IDisplay Display { get; }

            public ISpeaker Speaker { get; }

            public BlueLaptop(ISpeaker speaker, IBattery battery, IDisplay display)
            {
                Battery = battery;
                Display = display;
                Speaker = speaker;
            }
        }

        #endregion
    }
}
