using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dre0Dru.Collections
{
    [Serializable]
    public class FlattenedArray<T> : IEnumerable<T>
    {
        [SerializeField]
        private T[] _array2D;

        private readonly int _yDimension;

        public T this[int x, int y]
        {
            get => GetElementAtIndex(x, y);
            set => SetElementAtIndex(value, x, y);
        }

        public T this[Vector2Int index]
        {
            get => GetElementAtIndex(index.x, index.y);
            set => SetElementAtIndex(value, index.x, index.y);
        }

        public T this[int i]
        {
            get => _array2D[i];
            set => _array2D[i] = value;
        }

        public int Length => _array2D.Length;
        
        public FlattenedArray(int x, int y)
        {
            _yDimension = y;
            _array2D = new T[x * y];
        }

        public bool IsIndexValid(int x, int y)
        {
            return x >= 0 && y >= 0 && x < _array2D.Length / _yDimension && y < _yDimension;
        }

        private T GetElementAtIndex(int x, int y)
        {
            return _array2D[x * _yDimension + y];
        }

        private void SetElementAtIndex(T element, int x, int y)
        {
            _array2D[x * _yDimension + y] = element;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _array2D.GetEnumerator() as IEnumerator<T> ?? throw new NullReferenceException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}