namespace Dre0Dru.Collections
{
    public static class NullableExtensions
    {
        public static bool TryGet<T>(T? nullable, out T value)
            where T: struct
        {
            value = default;
            
            if (nullable.HasValue)
            {
                value = nullable.Value;
                return true;
            }

            return false;
        }
    }
}
