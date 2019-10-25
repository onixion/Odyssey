using System;

namespace SmartContainer.Contracts
{
    /// <summary>
    /// Resolution.
    /// </summary>
    public class Resolution
    {
        /// <summary>
        /// Interface type.
        /// </summary>
        public Type InterfaceType { get; }

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
        /// <param name="name"></param>
        /// <param name="parameterInjections"></param>
        /// <param name="propertyInjections"></param>
        public Resolution(Type interfaceType, string name, ParameterInjection[] parameterInjections, PropertyInjection[] propertyInjections)
        {
            InterfaceType = interfaceType;
            Name = name;
            ParameterInjections = (ParameterInjection[]) parameterInjections.Clone();
            PropertyInjections = (PropertyInjection[]) propertyInjections.Clone();
        }
    }
}
