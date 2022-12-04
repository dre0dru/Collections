using System;
using System.Collections;
using System.Collections.Generic;

namespace Dre0Dru.Collections
{
    public class TypeConstrainedDictionary<TConstraint> : IEnumerable<KeyValuePair<Type, TConstraint>>
        where TConstraint : class
    {
        private readonly Dictionary<Type, TConstraint> _dictionary;

        public int Count => _dictionary.Count;
        
        public Dictionary<Type, TConstraint>.KeyCollection Keys => _dictionary.Keys;
        public Dictionary<Type, TConstraint>.ValueCollection Values => _dictionary.Values;
        
        [UnityEngine.Scripting.RequiredMember]
        public TypeConstrainedDictionary()
        {
            _dictionary = new Dictionary<Type, TConstraint>();
        }
        
        public TypeConstrainedDictionary(IDictionary<Type, TConstraint> dictionary)
        {
            _dictionary = new Dictionary<Type, TConstraint>(dictionary);
        }
        
        public TypeConstrainedDictionary(IEnumerable<KeyValuePair<Type, TConstraint>> enumerable)
        {
            _dictionary = new Dictionary<Type, TConstraint>(enumerable);
        }

        public void Add<TValue>(TValue value)
            where TValue : TConstraint =>
            _dictionary.Add(typeof(TValue), value);

        public TValue Get<TValue>()
            where TValue : class, TConstraint =>
            _dictionary[typeof(TValue)] as TValue;

        public bool Remove<TValue>()
            where TValue : TConstraint =>
            _dictionary.Remove(typeof(TValue));

        public bool Contains<TValue>()
            where TValue : class, TConstraint =>
            _dictionary.ContainsKey(typeof(TValue));

        public bool TryGet<TValue>(out TValue value)
            where TValue : class, TConstraint
        {
            if (_dictionary.TryGetValue(typeof(TValue), out var constrainedValue))
            {
                value = constrainedValue as TValue;
                return true;
            }

            value = default;
            return false;
        }
        
        public void Clear() =>
            _dictionary.Clear();
        
        public IEnumerator<KeyValuePair<Type, TConstraint>> GetEnumerator() =>
            ((IEnumerable<KeyValuePair<Type, TConstraint>>)_dictionary).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_dictionary).GetEnumerator();
    }
}
