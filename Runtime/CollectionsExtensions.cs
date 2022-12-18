using System.Collections.Generic;

namespace Dre0Dru.Collections
{
    public static partial class CollectionsExtensions
    {
        public static int LastIndex<T>(this ICollection<T> collection) => 
            collection.Count - 1;
    }
}
