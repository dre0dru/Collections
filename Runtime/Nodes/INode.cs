using System.Collections.Generic;

namespace Dre0Dru.Collections
{
    public interface INode<TNode> : IEnumerable<TNode>
    {
        NodeId Id { get; }
        
        void SetId(NodeId id);

        void AddChild(TNode child);
    }
}
