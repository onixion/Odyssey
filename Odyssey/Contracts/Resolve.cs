using System;

namespace Odyssey.Contracts
{
    /// <summary>
    /// Resolve attribute.
    /// </summary>
    /// <remarks>
    /// Marks the property or parameter in order to be resolved.
    /// Also adds additional information for the resolve operation.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Parameter)]
    public class Resolve : Attribute
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }
    }
}
