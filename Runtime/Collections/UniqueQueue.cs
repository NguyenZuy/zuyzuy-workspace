using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zuy.Workspace
{
    /// <summary>
    /// A custom queue implementation that ensures unique elements.
    /// Only allows an element to be added once, preventing duplicates.
    /// </summary>
    [Serializable]
    public sealed class UniqueQueue<T> : IEnumerable<T>
    {
        // Internal queue to store elements
        [SerializeField]
        private List<T> _items = new List<T>();

        // HashSet to track unique elements for efficient lookup
        [NonSerialized]
        private HashSet<T> _uniqueCheck = new HashSet<T>();

        /// <summary>
        /// Gets the number of elements in the UniqueQueue.
        /// </summary>
        public int Count => _items.Count;

        /// <summary>
        /// Determines whether an element is already in the queue.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>True if the item exists in the queue, otherwise false.</returns>
        public bool Contains(T item)
        {
            return _uniqueCheck.Contains(item);
        }

        /// <summary>
        /// Attempts to add a unique element to the queue.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>True if the item was added, false if it already exists.</returns>
        public bool Enqueue(T item)
        {
            // Check if the item is already in the queue
            if (_uniqueCheck.Contains(item))
            {
                return false;
            }

            // Add the item to both the list and the hashset
            _items.Add(item);
            _uniqueCheck.Add(item);
            return true;
        }

        /// <summary>
        /// Removes and returns the first element in the queue.
        /// </summary>
        /// <returns>The first element in the queue.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the queue is empty.</exception>
        public T Dequeue()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("The UniqueQueue is empty.");
            }

            // Get the first item
            T item = _items[0];

            // Remove from both list and hashset
            _items.RemoveAt(0);
            _uniqueCheck.Remove(item);

            return item;
        }

        /// <summary>
        /// Returns the first element in the queue without removing it.
        /// </summary>
        /// <returns>The first element in the queue.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the queue is empty.</exception>
        public T Peek()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("The UniqueQueue is empty.");
            }

            return _items[0];
        }

        /// <summary>
        /// Removes all elements from the UniqueQueue.
        /// </summary>
        public void Clear()
        {
            _items.Clear();
            _uniqueCheck.Clear();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the UniqueQueue.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the UniqueQueue.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Tries to dequeue an item if the queue is not empty.
        /// </summary>
        /// <param name="result">The dequeued item, if successful.</param>
        /// <returns>True if an item was dequeued, false otherwise.</returns>
        public bool TryDequeue(out T result)
        {
            if (_items.Count > 0)
            {
                result = Dequeue();
                return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Tries to peek at the first item without removing it.
        /// </summary>
        /// <param name="result">The first item, if available.</param>
        /// <returns>True if an item was peeked, false otherwise.</returns>
        public bool TryPeek(out T result)
        {
            if (_items.Count > 0)
            {
                result = _items[0];
                return true;
            }

            result = default;
            return false;
        }
    }
}