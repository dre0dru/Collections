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
        [SerializeReference]
        private List<TNode> _children;

        protected IList<TNode> Children => _children;

        public Node() : this(new List<TNode>())
        {
        }

        public Node(params TNode[] children) : this(new List<TNode>(children))
        {
        }

        public Node(List<TNode> children)
        {
            _children = children;
        }

        public void AddChild(TNode child)
        {
            _children.Add(child);
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
