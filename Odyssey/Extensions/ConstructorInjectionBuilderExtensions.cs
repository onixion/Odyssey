using Odyssey.Builders;
using Odyssey.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Odyssey.Extensions
{
    /// <summary>
    /// Constructor injection builder extensions.
    /// </summary>
    public static class ConstructorInjectionBuilderExtensions
    {
        /// <summary>
        /// Add parameter injections.
        /// </summary>
        /// <param name="parameterInjections">Parameter injections.</param>
        /// <returns>Constructor injection builder.</returns>
        public static ConstructorInjectionBuilder AddParameterInjections(
            this ConstructorInjectionBuilder constructorInjectionBuilder, 
            IEnumerable<ParameterInjection> parameterInjections)
        {
            foreach (var parameterInjection in parameterInjections)
                constructorInjectionBuilder.AddParameterInjection(parameterInjection);

            return constructorInjectionBuilder;
        }
    }
}
