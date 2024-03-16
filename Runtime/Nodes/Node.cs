using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dre0Dru.Collections
{
    [Serializable]
    public class Node<TNode> : INode<TNode>
        where TNode : INode<TNode>
    {
        [SerializeField]
        private NodeId _id;

        [SerializeReference]
        private List<TNode> _children;

        protected IList<TNode> Children => _children;

        public NodeId Id => _id;

        public Node() : this(NodeId.Default, new List<TNode>())
        {
        }

        public Node(NodeId id) : this(id, new List<TNode>())
        {
        }

        public Node(List<TNode> children) : this(NodeId.Default, children)
        {
        }
        
        public Node(params TNode[] children) : this(NodeId.Default, new List<TNode>(children))
        {
        }

        public Node(NodeId id, params TNode[] children) : this(id, new List<TNode>(children))
        {
        }
        
        public Node(NodeId id, List<TNode> children)
        {
            _id = id;
            _children = children;
        }

        public void SetId(NodeId id)
        {
            _id = id;

            var childrenDepth = _id.Depth + 1;

            for (var i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                child.SetId(new NodeId()
                {
                    Depth = childrenDepth,
                    Index = i
                });
            }
        }

        public void AddChild(TNode child)
        {
            _children.Add(child);
            child.SetId(new NodeId()
            {
                Depth = _id.Depth + 1,
                Index = _children.Count - 1,
            });
        }

        public IEnumerator<TNode> GetEnumerator()
        {
            return Children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
