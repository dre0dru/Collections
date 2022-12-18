using System;
using System.Collections.Generic;

namespace Dre0Dru.Collections
{
    public static partial class CollectionsExtensions
    {
        public static void Add<TConstraint, TType>(this IDictionary<Type, TConstraint> dictionary,
            TType value)
            where TType : TConstraint =>
            dictionary.Add(typeof(TType), value);

        public static void Get<TConstraint, TType>(this IDictionary<Type, TConstraint> dictionary,
            out TType value)
            where TType : class, TConstraint =>
            value = dictionary[typeof(TType)] as TType;

        public static bool TryGet<TConstraint, TType>(this IDictionary<Type, TConstraint> dictionary,
            out TType value)
            where TType : class, TConstraint
        {
            if (dictionary.TryGetValue(typeof(TType), out var constrainedValue))
            {
                value = constrainedValue as TType;
                return true;
            }

            value = default;
            return false;
        }

        public static bool Remove<TConstraint, TType>(this IDictionary<Type, TConstraint> dictionary)
            where TType : TConstraint =>
            dictionary.Remove(typeof(TType));

        public static bool Contains<TConstraint, TType>(this IDictionary<Type, TConstraint> dictionary)
            where TType : class, TConstraint =>
            dictionary.ContainsKey(typeof(TType));

        public static void AddToList<TConstraint, TType>(this IDictionary<Type, List<TConstraint>> dictionary,
            TType value)
            where TType : TConstraint
        {
            var list = dictionary.GetOrCreateList<TConstraint, TType>();

            list.Add(value);
        }

        public static List<TConstraint> GetOrCreateList<TConstraint, TType>(
            this IDictionary<Type, List<TConstraint>> dictionary)
            where TType : TConstraint
        {
            if (!dictionary.TryGetList<TConstraint, TType>(out var list))
            {
                list = new List<TConstraint>();
                dictionary.Add(typeof(TType), list);
            }

            return list;
        }

        public static List<TConstraint> GetOrCreateList<TConstraint>(
            this IDictionary<Type, List<TConstraint>> dictionary, Type type)
        {
            if (!dictionary.TryGetValue(type, out var list))
            {
                list = new List<TConstraint>();
                dictionary.Add(type, list);
            }

            return list;
        }

        public static bool TryGetList<TConstraint, TType>(
            this IDictionary<Type, List<TConstraint>> dictionary, out List<TConstraint> list)
            where TType : TConstraint =>
            dictionary.TryGetValue(typeof(TType), out list);
    }
}
