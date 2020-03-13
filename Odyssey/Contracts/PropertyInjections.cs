﻿using System.Collections.Generic;
using System.Linq;
using Utilinator.Core;

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
        public IEnumerable<PropertyInjection> Injections { get; }

        /// <summary>
        /// Injection behaviour.
        /// </summary>
        public InjectionBehaviour InjectionBehaviour { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="injections"></param>
        /// <param name="injectionBehaviour"></param>
        public PropertyInjections(IEnumerable<PropertyInjection> injections, InjectionBehaviour injectionBehaviour = InjectionBehaviour.Default)
        {
            Argument.NotNull(nameof(injections), injections);

            Injections = injections.ToArray();
            InjectionBehaviour = injectionBehaviour;
        }
    }
}
