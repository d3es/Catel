﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BoxingCache.cs" company="Catel development team">
//   Copyright (c) 2008 - 2016 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Catel.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// Caches boxed objects to minimize the memory footprint for boxed value types.
    /// </summary>
    public class BoxingCache<T>
        where T : struct
    {
        private readonly Dictionary<T, object> _boxedValues = new Dictionary<T, object>();
        private readonly Dictionary<object, T> _unboxedValues = new Dictionary<object, T>();

        /// <summary>
        /// Adds the value to the cache.
        /// </summary>
        /// <param name="value">The value to add to the cache.</param>
        protected object AddUnboxedValue(T value)
        {
            var boxedValue = (object)value;

            lock (_boxedValues)
            {
                _boxedValues[value] = boxedValue;
            }

            lock (_unboxedValues)
            {
                _unboxedValues[boxedValue] = value;
            }

            return boxedValue;
        }

        /// <summary>
        /// Adds the value to the cache.
        /// </summary>
        /// <param name="boxedValue">The value to add to the cache.</param>
        protected T AddBoxedValue(object boxedValue)
        {
            var unboxedValue = (T)boxedValue;

            lock (_boxedValues)
            {
                _boxedValues[unboxedValue] = boxedValue;
            }

            lock (_unboxedValues)
            {
                _unboxedValues[boxedValue] = unboxedValue;
            }

            return unboxedValue;
        }

        /// <summary>
        /// Gets the boxed value representing the specified value.
        /// </summary>
        /// <param name="value">The value to box.</param>
        /// <returns>The boxed value.</returns>
        public object GetBoxedValue(T value)
        {
            lock (_boxedValues)
            {
                object boxedValue;

                if (!_boxedValues.TryGetValue(value, out boxedValue))
                {
                    boxedValue = AddUnboxedValue(value);
                }

                return boxedValue;
            }
        }

        /// <summary>
        /// Gets the unboxed value representing the specified value.
        /// </summary>
        /// <param name="boxedValue">The value to unbox.</param>
        /// <returns>The unboxed value.</returns>
        public T GetUnboxedValue(object boxedValue)
        {
            lock (_unboxedValues)
            {
                T unboxedValue;

                if (!_unboxedValues.TryGetValue(boxedValue, out unboxedValue))
                {
                    unboxedValue = AddBoxedValue(boxedValue);
                }

                return unboxedValue;
            }
        }
    }
}