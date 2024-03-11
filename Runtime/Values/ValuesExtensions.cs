using System;
using System.Collections.Generic;

namespace Dre0Dru.Collections
{
    public static class ValuesExtensions
    {
        public static bool TryToConsume<T>(this ConsumableValue<T> consumableValue, out T value)
        {
            value = default;

            if (!consumableValue.IsConsumed)
            {
                consumableValue.IsConsumed = true;
                value = consumableValue;
                return true;
            }

            return false;
        }

        public static bool HasValueChanged<T>(this ValueChange<T> valueChange)
            where T : IEquatable<T>
        {
            return !EqualityComparer<T>.Default.Equals(valueChange.PreviousValue, valueChange.CurrentValue);
        }
    }
}
