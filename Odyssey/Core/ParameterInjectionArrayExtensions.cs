using Odyssey.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Odyssey.Core
{
    /// <summary>
    /// Parameter injection array extensions.
    /// </summary>
    public static class ParameterInjectionEnumerableExtensions
    {
        /// <summary>
        /// Try to get the parameter injection.
        /// </summary>
        /// <param name="parameterInjections"></param>
        /// <param name="parameterInfo"></param>
        /// <param name="parameterInjection"></param>
        /// <returns></returns>
        public static bool TryGetParameterInjection(this IEnumerable<ParameterInjection> parameterInjections, ParameterInfo parameterInfo, out ParameterInjection parameterInjection)
        {
            parameterInjection = parameterInjections
                .Where(injection => injection.Name != null)
                .FirstOrDefault(injection => injection.Name == parameterInfo.Name);

            return parameterInjection != null;
        }

        /// <summary>
        /// Returns all parameter injections with no name.
        /// </summary>
        /// <param name="parameterInjections"></param>
        /// <returns></returns>
        public static IEnumerable<ParameterInjection> UnnameParameterInjections(this IEnumerable<ParameterInjection> parameterInjections)
        {
            return parameterInjections.Where(parameterInjection => parameterInjection.Name == null);
        }
    }
}
