namespace Dre0Dru.Collections
{
    public static partial class CollectionsExtensions
    {
        public static bool Matches(this Bitmask32 bitmask, BitmaskFilter32 bitmaskFilter) =>
            bitmaskFilter.Matches(bitmask);

        public static bool Matches(this Bitmask64 bitmask, BitmaskFilter64 bitmaskFilter) =>
            bitmaskFilter.Matches(bitmask);
    }
}
