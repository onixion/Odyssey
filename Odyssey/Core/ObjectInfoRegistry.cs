using Odyssey.Contracts;
using System;
using System.Collections.Generic;

namespace Odyssey.Core
{
    /// <summary>
    /// Object info registry.
    /// </summary>
    public class ObjectInfoRegistry
    {
        /// <summary>
        /// Type to object info.
        /// </summary>
        readonly IDictionary<Type, ObjectInfo> typeToObjectInfo = new Dictionary<Type, ObjectInfo>();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="registrations">Registrations.</param>
        public ObjectInfoRegistry(IEnumerable<Registration> registrations)
        {
            foreach(Registration registration in registrations)
            {
                if (registration.ImplementationType.HasValue)
                {
                    AddType(registration.ImplementationType.Value);
                }
            }
        }

        void AddType(Type type)
        {
            if (!typeToObjectInfo.ContainsKey(type))
                typeToObjectInfo.Add(type, new ObjectInfo(type));
        }

        /// <summary>
        /// Get object info.
        /// </summary>
        /// <param name="type">Type to get the object info.</param>
        /// <returns>Object info.</returns>
        public ObjectInfo GetObjectInfo(Type type)
        {
            if (!typeToObjectInfo.TryGetValue(type, out ObjectInfo objectInfo))
            {
                objectInfo = new ObjectInfo(type);
                typeToObjectInfo.Add(type, objectInfo);
            }

            return objectInfo;
        }
    }
}
