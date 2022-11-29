using System;

namespace Dre0Dru.Collections
{
    public struct BitmaskFilter32 : IEquatable<BitmaskFilter32>
    {
        private Bitmask32 _includeMask;
        private Bitmask32 _excludeMask;
        
        public BitmaskFilter32(uint includeMask = 0u, uint excludeMask = 0u)
        {
            _includeMask = new Bitmask32(includeMask);
            _excludeMask = new Bitmask32(excludeMask);
        }

        public BitmaskFilter32 Include(uint value)
        {
            _includeMask = _includeMask.With(value);

            return this;
        }

        public BitmaskFilter32 Exclude(uint value)
        {
            _excludeMask = _excludeMask.With(value);

            return this;
        }
        
        public bool Matches(Bitmask32 bitmask) =>
            (bitmask.Value & _includeMask.Value) == _includeMask.Value &&
            (bitmask.Value & _excludeMask.Value) == 0;

        public bool Equals(BitmaskFilter32 other) =>
            _includeMask.Equals(other._includeMask) && 
            _excludeMask.Equals(other._excludeMask);

        public override bool Equals(object obj) =>
            obj is BitmaskFilter32 other && Equals(other);

        public override int GetHashCode() =>
            HashCode.Combine(_includeMask, _excludeMask);
    }
    
    public struct BitmaskFilter64 : IEquatable<BitmaskFilter64>
    {
        private Bitmask64 _includeMask;
        private Bitmask64 _excludeMask;
        
        public BitmaskFilter64(ulong includeMask = 0ul, ulong excludeMask = 0ul)
        {
            _includeMask = new Bitmask64(includeMask);
            _excludeMask = new Bitmask64(excludeMask);
        }

        public BitmaskFilter64 Include(uint value)
        {
            _includeMask = _includeMask.With(value);

            return this;
        }

        public BitmaskFilter64 Exclude(uint value)
        {
            _excludeMask = _excludeMask.With(value);

            return this;
        }
        
        public bool Matches(Bitmask64 bitmask) =>
            (bitmask.Value & _includeMask.Value) == _includeMask.Value &&
            (bitmask.Value & _excludeMask.Value) == 0;

        public bool Equals(BitmaskFilter64 other) =>
            _includeMask.Equals(other._includeMask) && 
            _excludeMask.Equals(other._excludeMask);

        public override bool Equals(object obj) =>
            obj is BitmaskFilter64 other && Equals(other);

        public override int GetHashCode() =>
            HashCode.Combine(_includeMask, _excludeMask);
    }
}
