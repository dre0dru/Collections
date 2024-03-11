using System;
using System.Diagnostics;

namespace Dre0Dru.Collections
{
    //TODO custom inspector? so it can be synced with enum? or just manually set bits
    [DebuggerTypeProxy(typeof(Bitmask32DebugView))]
    public struct Bitmask32 : IEquatable<Bitmask32>
    {
        private uint _value;

        public uint Value => _value;

        public Bitmask32(uint initialValue = 0u)
        {
            _value = initialValue;
        }
        
        public Bitmask32 With(uint value)
        {
            _value |= value;

            return this;
        }

        public Bitmask32 Without(uint value)
        {
            _value &= ~value;

            return this;
        }
        
        public bool Equals(Bitmask32 other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            return obj is Bitmask32 other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (int)_value;
        }
    }
    
    [DebuggerTypeProxy(typeof(Bitmask64DebugView))]
    public struct Bitmask64 : IEquatable<Bitmask64>
    {
        private ulong _value;

        public ulong Value => _value;

        public Bitmask64(ulong initialValue = 0ul)
        {
            _value = initialValue;
        }
        
        public Bitmask64 With(ulong value)
        {
            _value |= value;

            return this;
        }
        
        public Bitmask64 Without(ulong value)
        {
            _value &= ~value;

            return this;
        }
        
        public bool Equals(Bitmask64 other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            return obj is Bitmask64 other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (int)_value;
        }
    }
    
    internal sealed class Bitmask32DebugView
    {
        private Bitmask32 _data;

        public Bitmask32DebugView(Bitmask32 data)
        {
            _data = data;
        }

        public bool[] Bits
        {
            get
            {
                var array = new bool[32];
                for (int i = 0; i < 32; ++i)
                {
                    array[i] = IsSet(i);
                }
                return array;
            }
        }

        private bool IsSet(int pos)
        {
            var mask =  0xffffffffu >> (31);
            var tmp0 = _data.Value >> pos;
            return 0u != (tmp0 & mask);
        }
    }
    
    internal sealed class Bitmask64DebugView
    {
        private Bitmask64 _data;

        public Bitmask64DebugView(Bitmask64 data)
        {
            _data = data;
        }

        public bool[] Bits
        {
            get
            {
                var array = new bool[64];
                for (int i = 0; i < 64; ++i)
                {
                    array[i] = IsSet(i);
                }
                return array;
            }
        }

        private bool IsSet(int pos)
        {
            var mask = 0xfffffffffffffffful >> (63);
            var tmp0 = _data.Value >> pos;
            return 0ul != (tmp0 & mask);
        }
    }
}
