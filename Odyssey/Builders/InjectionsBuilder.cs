using Odyssey.Contracts;

namespace Odyssey.Builders
{
    /// <summary>
    /// Injections builder.
    /// </summary>
    public class InjectionsBuilder
    {
        /// <summary>
        /// Constructor injections.
        /// </summary>
        public ConstructorInjection ConstructorInjection { get; set; }

        /// <summary>
        /// Decorator injection.
        /// </summary>
        public DecoratorRegistration DecoratorInjection { get; set; }

        /// <summary>
        /// Build.
        /// </summary>
        /// <returns>Injections.</returns>
        public Injections Build()
        {
            return new Injections(ConstructorInjection, DecoratorInjection);
        }
    }
}
