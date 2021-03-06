﻿using GroundWork;
using GroundWork.Contracts;
using System;

namespace Odyssey.Contracts
{
    /// <summary>
    /// Decorator registration.
    /// </summary>
    public class DecoratorRegistration
    {
        /// <summary>
        /// Implementation type.
        /// </summary>
        public IOptional<Type> ImplementationType { get; }

        /// <summary>
        /// Injections.
        /// </summary>
        public IOptional<Injections> Injections { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="implementationType">Implementation type.</param>
        /// <param name="injections">Injections.</param>
        public DecoratorRegistration(Type implementationType = null, Injections injections = null)
        {
            ImplementationType = new Optional<Type>(implementationType);
            Injections = new Optional<Injections>(injections);
        }
    }
}
