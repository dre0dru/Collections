using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dre0Dru.Collections
{
    public static partial class CollectionsExtensions
    {
        public static IList<T> Shuffle<T>(this IList<T> collection)
        {
            int n = collection.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                (collection[k], collection[n]) = (collection[n], collection[k]);
            }

            return collection;
        }

        //TODO code duplication: https://stackoverflow.com/questions/12838122/ilistt-and-ireadonlylistt
        public static bool TryMoveNextCircular<T>(this IList<T> list, ref int index, out T next)
        {
            if (list.Count == 0)
            {
                next = default;
                return false;
            }

            index = (index + 1) % list.Count;

            next = list[index];
            return true;
        }

        public static bool TryMovePrevCircular<T>(this IList<T> list, ref int index, out T next)
        {
            if (list.Count == 0)
            {
                next = default;
                return false;
            }

            index -= 1;
            if (index < 0)
            {
                index = list.Count - 1;
            }

            index %= list.Count;

            next = list[index];
            return true;
        }

        public static bool TryMoveNextCircular<T>(this IReadOnlyList<T> list, ref int index, out T next)
        {
            if (list.Count == 0)
            {
                next = default;
                return false;
            }

            index = (index + 1) % list.Count;

            next = list[index];
            return true;
        }

        public static bool TryMovePrevCircular<T>(this IReadOnlyList<T> list, ref int index, out T next)
        {
            if (list.Count == 0)
            {
                next = default;
                return false;
            }

            index -= 1;
            if (index < 0)
            {
                index = list.Count - 1;
            }

            index %= list.Count;

            next = list[index];
            return true;
        }

        public static bool TryPopLast<T>(this IList<T> list, out T element)
        {
            if (list.Count > 0)
            {
                var lastIndex = list.LastIndex();

                element = list[lastIndex];
                list.RemoveAt(lastIndex);

                return true;
            }

            element = default;
            return false;
        }

        public static T Last<T>(this IList<T> list) => 
            list[list.LastIndex()];
    }
}
