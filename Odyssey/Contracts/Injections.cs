namespace Odyssey.Contracts
{
    /// <summary>
    /// Injections.
    /// </summary>
    public class Injections
    {
        /// <summary>
        /// Parameter injections.
        /// </summary>
        public ParameterInjections ParameterInjections { get; }

        /// <summary>
        /// Property injections
        /// </summary>
        public PropertyInjections PropertyInjections { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parameterInjections"></param>
        /// <param name="propertyInjections"></param>
        public Injections(ParameterInjections parameterInjections, PropertyInjections propertyInjections)
        {
            ParameterInjections = parameterInjections;
            PropertyInjections = propertyInjections;
        }
    }
}
