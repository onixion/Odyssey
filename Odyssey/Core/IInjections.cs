using GroundWork.Core;
using Odyssey.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Odyssey.Core
{
    /// <summary>
    /// Injections interface.
    /// </summary>
    public interface IInjections
    {
        /// <summary>
        /// Constructor injection.
        /// </summary>
        Optional<ConstructorInjection> ConstructorInjection { get; }

        /// <summary>
        /// Deocrator injection.
        /// </summary>
        Optional<DecoratorRegistration> DecoratorInjection { get; }
    }
}
