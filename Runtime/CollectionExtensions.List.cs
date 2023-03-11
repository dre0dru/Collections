﻿using System.Collections.Generic;
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
        public static int NextIndexCircular<T>(this IList<T> list, int currentIndex)
        {
            return ++currentIndex % list.Count;
        }

        public static int PrevIndexCircular<T>(this IList<T> list, int currentIndex)
        {
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = list.Count - 1;
            }

            currentIndex %= list.Count;

            return currentIndex;
        }

        public static int NextIndexCircular<T>(this IReadOnlyList<T> list, int currentIndex)
        {
            return ++currentIndex % list.Count;
        }

        public static int PrevIndexCircular<T>(this IReadOnlyList<T> list, int currentIndex)
        {
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = list.Count - 1;
            }

            currentIndex %= list.Count;

            return currentIndex;
        }

        public static T NextCircular<T>(this IList<T> list, int index) =>
            list[list.NextIndexCircular(index)];

        public static T PrevCircular<T>(this IList<T> list, int index) =>
            list[list.PrevIndexCircular(index)];

        public static bool TryNextCircular<T>(this IList<T> list, ref int index, out T next)
        {
            next = default;
            if (list.Count == 0)
            {
                return false;
            }

            index = list.NextIndexCircular(index);
            next = list[index];
            return true;
        }
        
        public static bool TryPrevCircular<T>(this IList<T> list, ref int index, out T next)
        {
            next = default;
            if (list.Count == 0)
            {
                return false;
            }

            index = list.PrevIndexCircular(index);
            next = list[index];
            return true;
        }

        public static T NextCircular<T>(this IReadOnlyList<T> list, int index) =>
            list[list.NextIndexCircular(index)];

        public static T PrevCircular<T>(this IReadOnlyList<T> list, int index) =>
            list[list.PrevIndexCircular(index)];

        public static bool TryNextCircular<T>(this IReadOnlyList<T> list, ref int index, out T next)
        {
            next = default;
            if (list.Count == 0)
            {
                return false;
            }

            index = list.NextIndexCircular(index);
            next = list[index];
            return true;
        }
        
        public static bool TryPrevCircular<T>(this IReadOnlyList<T> list, ref int index, out T next)
        {
            next = default;
            if (list.Count == 0)
            {
                return false;
            }

            index = list.PrevIndexCircular(index);
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
