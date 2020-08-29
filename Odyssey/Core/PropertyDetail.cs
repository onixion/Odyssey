using GroundWork;
using GroundWork.Contracts;
using System.Reflection;

namespace Odyssey.Core
{
    /// <summary>
    /// Property detail.
    /// </summary>
    public class PropertyDetail
    {
        /// <summary>
        /// Property info.
        /// </summary>
        public PropertyInfo PropertyInfo { get; }

        /// <summary>
        /// Name.
        /// </summary>
        public IOptional<string> Name { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PropertyDetail(PropertyInfo propertyInfo, string name = null)
        {
            PropertyInfo = propertyInfo;
            Name = new Optional<string>(name);
        }
    }
}
