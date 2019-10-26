using System;
using System.Collections.Generic;
using System.Linq;

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
        public IEnumerable<ParameterInjection> ParameterInjections { get; }

        /// <summary>
        /// Property injections.
        /// </summary>
        public IEnumerable<PropertyInjection> PropertyInjections { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <param name="name"></param>
        /// <param name="parameterInjections"></param>
        /// <param name="propertyInjections"></param>
        public Resolution(
            Type interfaceType, 
            string name = null, 
            IEnumerable<ParameterInjection> parameterInjections = null,
            IEnumerable<PropertyInjection> propertyInjections = null)
        {
            InterfaceType = interfaceType;
            Name = name;

            if (parameterInjections != null)
                ParameterInjections = parameterInjections.ToArray();

            if (propertyInjections != null)
                PropertyInjections = propertyInjections.ToArray();
        }
    }
}
