using Odyssey.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Odyssey.Core
{
    public static class PropertyInjectionsEnumerableExtensions
    {
        public static bool TryGetPropertyInjection(this IEnumerable<PropertyInjection> propertyInjections, PropertyInfo propertyInfo, out PropertyInjection propertyInjection)
        {
            propertyInjection = propertyInjections
               .Where(injection => injection.Name != null)
               .FirstOrDefault(injection => injection.Name == propertyInfo.Name);

            return propertyInjection != null;
        }
    }
}
