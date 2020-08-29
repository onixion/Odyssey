using GroundWork.Contracts;
using Odyssey.Contracts;

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
        IOptional<ConstructorInjection> ConstructorInjection { get; }

        /// <summary>
        /// Deocrator injection.
        /// </summary>
        IOptional<DecoratorRegistration> DecoratorInjection { get; }
    }
}
