using System;

namespace SmartContainer.Contracts
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
        public Type ImplementationType { get; }

        /// <summary>
        /// Life time.
        /// </summary>
        public Lifetime? Lifetime { get; }

        /// <summary>
        /// Instance.
        /// </summary>
        public object Instance { get; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Parameter injections.
        /// </summary>
        public ParameterInjection[] ParameterInjections { get; }

        /// <summary>
        /// Property injections.
        /// </summary>
        public PropertyInjection[] PropertyInjections { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <param name="implementationType"></param>
        /// <param name="lifetime"></param>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <param name="parameterInjections"></param>
        /// <param name="propertyInjections"></param>
        public Registration(
            Type interfaceType, 
            Type implementationType, 
            Lifetime? lifetime, 
            object instance, 
            string name,
            ParameterInjection[] parameterInjections,
            PropertyInjection[] propertyInjections)
        {
            InterfaceType = interfaceType;
            ImplementationType = implementationType;
            Lifetime = lifetime;
            Instance = instance;
            Name = name;
            ParameterInjections = (ParameterInjection[]) parameterInjections.Clone();
            PropertyInjections = (PropertyInjection[]) propertyInjections.Clone();
        }
    }
}
