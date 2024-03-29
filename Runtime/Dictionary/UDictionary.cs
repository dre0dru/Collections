using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;

namespace Dre0Dru.Collections
{
    [Serializable]
    [DebuggerDisplay("Count = {Count}")]
    public class UDictionary<TKey, TValue, TKvp> : IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>,
        ISerializationCallbackReceiver, IDeserializationCallback, ISerializable
        where TKvp: IKvp<TKey, TValue>, new()
    {
        protected class SerializableDictionary : Dictionary<TKey, TValue>
        {
            public SerializableDictionary()
            {
            }

            public SerializableDictionary(IDictionary<TKey, TValue> dict) : base(dict)
            {
            }
            
            public SerializableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> enumerable) : base(enumerable)
            {
            }

            public SerializableDictionary(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }

        [SerializeField]
        private List<TKvp> _serialized;
        
        private SerializableDictionary _runtimeDictionary;

        private SerializableDictionary Dictionary => _runtimeDictionary;

        public TValue this[TKey key]
        {
            get => Dictionary[key];
            set
            {
                Dictionary[key] = value;

                #if UNITY_EDITOR
                AddOrUpdateList(key, value);
                #endif
            }
        }

        public Dictionary<TKey, TValue>.KeyCollection Keys => Dictionary.Keys;
        public Dictionary<TKey, TValue>.ValueCollection Values => Dictionary.Values;

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => Dictionary.Keys;
        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => Dictionary.Values;
        ICollection<TKey> IDictionary<TKey, TValue>.Keys => Dictionary.Keys;
        ICollection<TValue> IDictionary<TKey, TValue>.Values => Dictionary.Values;

        public int Count => Dictionary.Count;

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public UDictionary()
        {
            _runtimeDictionary = new SerializableDictionary();
            
            #if UNITY_EDITOR
            _serialized = new List<TKvp>();
            #endif
        }

        public UDictionary(IDictionary<TKey, TValue> dictionary)
        {
            _runtimeDictionary = new SerializableDictionary(dictionary);
            
            #if UNITY_EDITOR
            SerializedFromRuntime();
            #endif
        }
        
        public UDictionary(IEnumerable<KeyValuePair<TKey, TValue>> enumerable)
        {
            _runtimeDictionary = new SerializableDictionary(enumerable);
            
            #if UNITY_EDITOR
            SerializedFromRuntime();
            #endif
        }

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        protected UDictionary(SerializationInfo info, StreamingContext context)
        {
            _runtimeDictionary = new SerializableDictionary(info, context);
            
            #if UNITY_EDITOR
            SerializedFromRuntime();
            #endif
        }

        public void Add(TKey key, TValue value)
        {
            Dictionary.Add(key, value);
            
            #if UNITY_EDITOR
            AddOrUpdateList(key, value);
            #endif
        }

        public bool Remove(TKey key)
        {
            #if UNITY_EDITOR
            if (TryFindListIndexByKey(key, out var index))
            {
                _serialized.RemoveAt(index);
            }
            #endif

            return Dictionary.Remove(key);
        }

        public bool ContainsKey(TKey key) => Dictionary.ContainsKey(key);

        public bool TryGetValue(TKey key, out TValue value) => Dictionary.TryGetValue(key, out value);

        public void Clear()
        {
            Dictionary.Clear();
            
            #if UNITY_EDITOR
            _serialized.Clear();
            #endif
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly =>
            ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).IsReadOnly;

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).Add(item);
            
            #if UNITY_EDITOR
            AddOrUpdateList(item.Key, item.Value);
            #endif
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) =>
            ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).Contains(item);

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) =>
            ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).CopyTo(array, arrayIndex);

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            #if UNITY_EDITOR
            if (TryFindListIndexByKey(item.Key, out var index))
            {
                _serialized.RemoveAt(index);
            }
            #endif
            
            return ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).Remove(item);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() =>
            ((IEnumerable<KeyValuePair<TKey, TValue>>)Dictionary).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Dictionary).GetEnumerator();

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            #if !UNITY_EDITOR
            OnBeforeSerialize();
            #endif
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            #if UNITY_EDITOR
            CheckForCollisions();
            OnAfterDeserializeEditor();
            #endif

            #if !UNITY_EDITOR
            OnAfterDeserializePlayer();
            #endif
        }

        void IDeserializationCallback.OnDeserialization(object sender)
        {
            ((IDeserializationCallback)Dictionary).OnDeserialization(sender);
            
            #if UNITY_EDITOR
            OnBeforeSerialize();
            CheckForCollisions();
            #endif
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable)Dictionary).GetObjectData(info, context);
        }

        private void OnBeforeSerialize()
        {
            _serialized = new List<TKvp>(Dictionary.Count);

            foreach (var kvp in Dictionary)
            {
                _serialized.Add(new TKvp()
                {
                    Key = kvp.Key,
                    Value = kvp.Value
                });
            }
        }
        
        #if !UNITY_EDITOR
        private void OnAfterDeserializePlayer()
        {
            Dictionary.Clear();
            foreach (var kvp in _serialized)
            {
                Dictionary.Add(kvp.Key, kvp.Value);
            }

            _serialized = null;
        }
        #endif

        #if UNITY_EDITOR
        #pragma warning disable CS0414
        private bool _hasCollisions;
        #pragma warning restore CS0414
        
        private void SerializedFromRuntime()
        {
            _serialized = new List<TKvp>(_runtimeDictionary.Select(pair => new TKvp()
            {
                Key = pair.Key,
                Value = pair.Value
            }));
        }
        
        private void CheckForCollisions()
        {
            var hashSet = new HashSet<TKey>();

            foreach (var kvp in _serialized)
            {
                if (!hashSet.Add(kvp.Key))
                {
                    _hasCollisions = true;
                    return;
                }
            }

            _hasCollisions = false;
        }

        private void OnAfterDeserializeEditor()
        {
            Dictionary.Clear();
            foreach (var kvp in _serialized)
            {
                Dictionary.TryAdd(kvp.Key, kvp.Value);
            }
        }

        private void AddOrUpdateList(TKey key, TValue value)
        {
            if (TryFindListIndexByKey(key, out var index))
            {
                _serialized[index] = new TKvp
                {
                    Key = key,
                    Value = value
                };
            }
            else
            {
                _serialized.Add(new TKvp
                {
                    Key = key,
                    Value = value
                });
            }
        }

        private bool TryFindListIndexByKey(TKey key, out int index)
        {
            index = _serialized.FindIndex(kvp => kvp.Key.Equals(key));

            return index != -1;
        }
        #endif
    }

    [Serializable]
    [DebuggerDisplay("Count = {Count}")]
    public class UDictionary<TKey, TValue> : UDictionary<TKey, TValue, Kvp<TKey, TValue>>
    {
        public UDictionary()
        {
        }

        public UDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary)
        {
        }

        public UDictionary(IEnumerable<KeyValuePair<TKey, TValue>> enumerable) : base(enumerable)
        {
        }

        protected UDictionary(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
