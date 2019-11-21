using Odyssey.Contracts;

namespace Odyssey.Core.Builders
{
    /// <summary>
    /// Injections builder.
    /// </summary>
    public class InjectionsBuilder
    {
        /// <summary>
        /// Parameter injections.
        /// </summary>
        ParameterInjections parameterInjections;

        /// <summary>
        /// Set parameter injections.
        /// </summary>
        /// <param name="parameterInjections"></param>
        /// <returns>Injections builder.</returns>
        public InjectionsBuilder SetParameterInjections(ParameterInjections parameterInjections)
        {
            this.parameterInjections = parameterInjections;
            return this;
        }

        /// <summary>
        /// Property injections.
        /// </summary>
        PropertyInjections propertyInjections;

        /// <summary>
        /// Set property injections.
        /// </summary>
        /// <param name="propertyInjections"></param>
        /// <returns>Injections builder.</returns>
        public InjectionsBuilder SetPropertyInjections(PropertyInjections propertyInjections)
        {
            this.propertyInjections = propertyInjections;
            return this;
        }

        /// <summary>
        /// Build.
        /// </summary>
        /// <returns>Injections.</returns>
        public Injections Build()
        {
            return new Injections(parameterInjections, propertyInjections);
        }
    }
}
