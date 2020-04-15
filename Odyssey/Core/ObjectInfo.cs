using Odyssey.Contracts;
using System;
using System.Linq;
using System.Reflection;

namespace Odyssey.Core
{
    /// <summary>
    /// Object info.
    /// </summary>
    public class ObjectInfo
    {
        /// <summary>
        /// Constructor info.
        /// </summary>
        /// <remarks>
        /// We only support one constructor. Services must not have more than one constructor.
        /// </remarks>
        public ConstructorInfo ConstructorInfo { get; }

        /// <summary>
        /// Property details.
        /// </summary>
        public PropertyDetail[] Properties { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ObjectInfo(Type type)
        {
            ConstructorInfo = type
                .GetConstructors()
                .First();

            Properties = GetPropertyDetails(type);
        }

        /// <summary>
        /// Retrieves the property infos and resolve attributes.
        /// </summary>
        /// <param name="type">Type to retrieve property infos from.</param>
        /// <returns>Property details.</returns>
        PropertyDetail[] GetPropertyDetails(Type type)
        {
            return type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty)

                // Map property infos to null or property details.
                .Select(propertyInfo =>
                {
                    var resolveAttribute = propertyInfo.GetCustomAttribute<ResolveInfo>();
                    if (resolveAttribute == null)
                        return null;

                    return new PropertyDetail(
                        propertyInfo,
                        resolveAttribute.Name);
                })

                // Remove nulls.
                .Where(resolveAttribute => resolveAttribute != null)

                .ToArray();
        }
    }
}
