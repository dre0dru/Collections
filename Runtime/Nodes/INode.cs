using System.Collections.Generic;

namespace Dre0Dru.Collections
{
    public interface INode<TNode> : IEnumerable<TNode>
    {
        void AddChild(TNode child);
    }
}
