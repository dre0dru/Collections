using System;

namespace Dre0Dru.Collections
{
    [Serializable]
    public struct NodeId : IEquatable<NodeId>
    {
        public static NodeId Default => new NodeId();
        
        public int Depth;
        public int Index;

        public override string ToString()
        {
            return $"{Depth}:{Index}";
        }

        public bool Equals(NodeId other)
        {
            return Index == other.Index && Depth == other.Depth;
        }

        public override bool Equals(object? obj)
        {
            return obj is NodeId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Index, Depth);
        }
    }
}
