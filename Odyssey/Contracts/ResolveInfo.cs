using System;

namespace Odyssey.Contracts
{
    /// <summary>
    /// Resolve info attribute.
    /// </summary>
    /// <remarks>
    /// Adds additional information for the injection system for the resolving process.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Parameter)]
    public class ResolveInfo : Attribute
    {
        /// <summary>
        /// Name.
        /// </summary>
        /// <remarks>
        /// Hint for the name.
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResolveInfo()
        {
        }

        /// <summary>
        /// Resolve.
        /// </summary>
        /// <param name="name">Name to resolve.</param>
        public ResolveInfo(string name)
        {
            Name = name;
        }
    }
}
