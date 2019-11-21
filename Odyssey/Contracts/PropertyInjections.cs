using System.Collections.Generic;
using System.Linq;

namespace Odyssey.Contracts
{
    /// <summary>
    /// Parameter injections.
    /// </summary>
    public class PropertyInjections
    {
        /// <summary>
        /// Injections.
        /// </summary>
        public IEnumerable<PropertyInjections> Injections { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="injections"></param>
        public PropertyInjections(IEnumerable<PropertyInjections> injections)
        {
            Injections = injections.ToArray();
        }
    }
}
