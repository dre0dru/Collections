using System.Collections.Generic;

namespace Dre0Dru.Collections
{
    public class BitCounter32<T>
        where T : class
    {
        private readonly Dictionary<T, uint> _tagBits;
        private int _bitCount;

        public BitCounter32()
        {
            _tagBits = new Dictionary<T, uint>();
        }

        public uint GetBit(T value)
        {
            if (!_tagBits.TryGetValue(value, out var bit))
            {
                bit = 1U << _bitCount;
                _tagBits[value] = bit;
                _bitCount++;
            }

            return bit;
        }
    }
    
    public class BitCounter64<T>
        where T : class
    {
        private readonly Dictionary<T, ulong> _tagBits;
        private int _bitCount;

        public BitCounter64()
        {
            _tagBits = new Dictionary<T, ulong>();
        }

        public ulong GetBit(T value)
        {
            if (!_tagBits.TryGetValue(value, out var bit))
            {
                bit = 1UL << _bitCount;
                _tagBits[value] = bit;
                _bitCount++;
            }

            return bit;
        }
    }
}