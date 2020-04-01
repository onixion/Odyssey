using GroundWork.Core;
using System;

namespace Odyssey.Contracts
{
    /// <summary>
    /// Registration.
    /// </summary>
    public class Registration
    {
        /// <summary>
        /// Interface type.
        /// </summary>
        public Type InterfaceType { get; }

        /// <summary>
        /// Implementation type.
        /// </summary>
        public Optional<Type> ImplementationType { get; }

        /// <summary>
        /// Create on resolve.
        /// </summary>
        public bool CreateOnResolve { get; }
  
        /// <summary>
        /// Instance.
        /// </summary>
        public Optional<object> Instance { get; }

        /// <summary>
        /// Name.
        /// </summary>
        public Optional<string> Name { get; }

        /// <summary>
        /// Injections.
        /// </summary>
        public Optional<Injections> Injections { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <param name="implementationType"></param>
        /// <param name="createOnResolve"></param>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <param name="injections"></param>
        public Registration(
            Type interfaceType, 
            Type implementationType = null, 
            bool createOnResolve = false,
            object instance = null, 
            string name = null,
            Injections injections = null)
        {
            InterfaceType = interfaceType;
            ImplementationType = new Optional<Type>(implementationType);
            CreateOnResolve = createOnResolve;
            Instance = new Optional<object>(instance);
            Name = new Optional<string>(name);
            Injections = new Optional<Injections>(injections);
        }

        /// <summary>
        /// To string.
        /// </summary>
        public override string ToString()
        {
            return $"Registration[Interface={InterfaceType},Implementation={ImplementationType.ValueOrDefault},CreateOnResolve={CreateOnResolve}]";
        }
    }
}
