using Odyssey.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Odyssey.Core
{
    /// <summary>
    /// Internal registration.
    /// </summary>
    public class InternalRegistration
    {
        /// <summary>
        /// Registration.
        /// </summary>
        public Registration Registration { get; }



        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="registration"></param>
        public InternalRegistration(Registration registration, object instance = null)
        {
            Registration = registration;
        }
    }
}
