using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace ZuyZuy.Workspace
{
    /// <summary>
    /// A thread-safe queue implementation that ensures unique elements.
    /// Only allows an element to be added once, preventing duplicates.
    /// Uses concurrent collections for multi-thread safety.
    /// </summary>
    [Serializable]
    public sealed class ConcurrentUniqueQueue<T> : IEnumerable<T>
    {
        // Internal concurrent queue to store elements
        [NonSerialized]
        private ConcurrentQueue<T> _items = new ConcurrentQueue<T>();

        // Concurrent dictionary to track unique elements for efficient lookup
        [NonSerialized]
        private ConcurrentDictionary<T, byte> _uniqueCheck = new ConcurrentDictionary<T, byte>();

        // Reader-writer lock for operations that need to modify both collections atomically
        [NonSerialized]
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        /// <summary>
        /// Gets the number of elements in the ConcurrentUniqueQueue.
        /// </summary>
        public int Count
        {
            get
            {
                _lock.EnterReadLock();
                try
                {
                    return _items.Count;
                }
                finally
                {
                    _lock.ExitReadLock();
                }
            }
        }

        /// <summary>
        /// Determines whether an element is already in the queue.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>True if the item exists in the queue, otherwise false.</returns>
        public bool Contains(T item)
        {
            return _uniqueCheck.ContainsKey(item);
        }

        /// <summary>
        /// Attempts to add a unique element to the queue.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>True if the item was added, false if it already exists.</returns>
        public bool Enqueue(T item)
        {
            // Try to add to the unique check dictionary first
            if (_uniqueCheck.TryAdd(item, 0))
            {
                // If successfully added to unique check, add to queue
                _items.Enqueue(item);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes and returns the first element in the queue.
        /// </summary>
        /// <returns>The first element in the queue.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the queue is empty.</exception>
        public T Dequeue()
        {
            _lock.EnterWriteLock();
            try
            {
                if (_items.IsEmpty)
                {
                    throw new InvalidOperationException("The ConcurrentUniqueQueue is empty.");
                }

                if (_items.TryDequeue(out T item))
                {
                    // Remove from unique check
                    _uniqueCheck.TryRemove(item, out _);
                    return item;
                }

                throw new InvalidOperationException("Failed to dequeue item from ConcurrentUniqueQueue.");
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Returns the first element in the queue without removing it.
        /// </summary>
        /// <returns>The first element in the queue.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the queue is empty.</exception>
        public T Peek()
        {
            _lock.EnterReadLock();
            try
            {
                if (_items.IsEmpty)
                {
                    throw new InvalidOperationException("The ConcurrentUniqueQueue is empty.");
                }

                if (_items.TryPeek(out T item))
                {
                    return item;
                }

                throw new InvalidOperationException("Failed to peek at item in ConcurrentUniqueQueue.");
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Removes all elements from the ConcurrentUniqueQueue.
        /// </summary>
        public void Clear()
        {
            _lock.EnterWriteLock();
            try
            {
                // Clear both collections
                while (!_items.IsEmpty)
                {
                    _items.TryDequeue(out _);
                }
                _uniqueCheck.Clear();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the ConcurrentUniqueQueue.
        /// Note: This creates a snapshot of the current state and may not reflect concurrent modifications.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            _lock.EnterReadLock();
            try
            {
                // Create a snapshot of the current queue
                return _items.ToArray().AsEnumerable().GetEnumerator();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the ConcurrentUniqueQueue.
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
            _lock.EnterWriteLock();
            try
            {
                if (_items.TryDequeue(out result))
                {
                    // Remove from unique check
                    _uniqueCheck.TryRemove(result, out _);
                    return true;
                }

                result = default;
                return false;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Tries to peek at the first item without removing it.
        /// </summary>
        /// <param name="result">The first item, if available.</param>
        /// <returns>True if an item was peeked, false otherwise.</returns>
        public bool TryPeek(out T result)
        {
            _lock.EnterReadLock();
            try
            {
                return _items.TryPeek(out result);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Disposes the reader-writer lock when the object is disposed.
        /// </summary>
        public void Dispose()
        {
            _lock?.Dispose();
        }
    }
}