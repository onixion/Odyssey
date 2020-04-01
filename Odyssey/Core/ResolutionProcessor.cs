﻿using Odyssey.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Odyssey.Core
{
    /// <summary>
    /// Resolution processor.
    /// </summary>
    /// <remarks>
    /// Processes resolutions.
    /// </remarks>
    public class ResolutionProcessor
    {
        readonly RegistrationProcessRegistry registrationProcessRegistry;

        readonly ServiceCreator serviceCreator;

        readonly bool enableDebugMode;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResolutionProcessor(RegistrationProcessRegistry registrationProcessRegistry, ServiceCreator serviceCreator, bool enableDebugMode)
        {
            this.registrationProcessRegistry = registrationProcessRegistry;
            this.serviceCreator = serviceCreator;
            this.enableDebugMode = enableDebugMode;
        }

        /// <summary>
        /// Process the resolution.
        /// </summary>
        /// <param name="resolution">Resolution to be processed.</param>
        /// <returns>Service instance.</returns>
        public object Process(Resolution resolution)
        {
            if (enableDebugMode)
            {
                Debug.WriteLine($"[{nameof(ResolutionProcessor)}] Processing resolution ...");
                Debug.WriteLine($"[{nameof(ResolutionProcessor)}] - Resolution={resolution}");
            }

            var registrationProcess = registrationProcessRegistry.GetProcess(
                resolution.InterfaceType, 
                resolution.Name.HasValue ? resolution.Name.Value : "");

            // When the process has an instance, return it.
            if (registrationProcess.Instance.HasValue)
                return registrationProcess.Instance.Value;

            Injections injections = null;
            
            if (registrationProcess.Registration.Injections.HasValue)
                injections = registrationProcess.Registration.Injections.Value;

            if (resolution.Injections.HasValue)
                injections = resolution.Injections.Value;

            return serviceCreator.CreateService(
                registrationProcess.Registration.ImplementationType.Value, 
                injections);
        }
    }
}
