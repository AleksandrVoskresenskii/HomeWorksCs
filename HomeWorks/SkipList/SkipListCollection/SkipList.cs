//-----------------------------------------------------------------------
// <copyright file="SkipList.cs" company="SPbSU">
//     Copyright (c) SPbSU. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#nullable disable
namespace SkipListCollection
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Generic-коллекция на базе структуры skip-list,
    /// упорядоченная по значению T.
    /// </summary>
    /// <typeparam name="T">Тип элементов, реализует IComparable&lt;T&gt;.</typeparam>
    public sealed class SkipList<T> : IList<T>
        where T : IComparable<T>
    {
        // Константы

        private const int DefaultMaxLevel = 32;
        private const double DefaultProbability = 0.5;

        // Поля

        private readonly SkipListNode<T> head;
        private readonly int maxLevel;
        private readonly double probability;
        private readonly Random rnd;
        private int level;
        private int count;
        private int version;

        // Конструкторы

        /// <summary>
        /// Создаёт skip-list с дефолтными параметрами.
        /// </summary>
        public SkipList()
            : this(DefaultMaxLevel, DefaultProbability)
        {
        }

        /// <summary>
        /// Создаёт skip-list с кастомными параметрами.
        /// </summary>
        /// <param name="maxLevel">Максимальное число уровней (>0).</param>
        /// <param name="probability">
        /// Вероятность повышения уровня (0..1).
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public SkipList(int maxLevel, double probability)
        {
            if (maxLevel < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(maxLevel),
                    "maxLevel must be > 0");
            }

            if (probability <= 0
                || probability >= 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(probability),
                    "probability must be in (0,1)");
            }

            this.maxLevel = maxLevel;
            this.probability = probability;
            this.rnd = new Random();
            this.level = 0;
            this.count = 0;
            this.version = 0;
            this.head = new SkipListNode<T>(maxLevel, default!);
        }

        // Свойства

        /// <inheritdoc/>
        public int Count
        {
            get { return this.count; }
        }

        /// <inheritdoc/>
        public bool IsReadOnly
        {
            get { return false; }
        }

        // Индексатор

        /// <inheritdoc/>
        public T this[int index]
        {
            get
            {
                if (index < 0
                    || index >= this.count)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(index),
                        "Index must be in [0..Count-1]");
                }

                var node = this.head.Forward[0];
                for (int i = 0; i < index; i++)
                {
                    if (node == null)
                    {
                        throw new InvalidOperationException();
                    }

                    node = node.Forward[0];
                }

                if (node == null)
                {
                    throw new InvalidOperationException();
                }

                return node.Value;
            }

            set
            {
                throw new NotSupportedException(
                    "Setter by index is not supported");
            }
        }

        // Публичные методы IList<T>

        /// <inheritdoc/>
        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var update = new SkipListNode<T>?[this.maxLevel + 1];
            var current = this.head;

            for (int i = this.level; i >= 0; i--)
            {
                while (current.Forward[i] != null
                    && current.Forward[i]!.Value.CompareTo(item) < 0)
                {
                    current = current.Forward[i]!;
                }

                update[i] = current;
            }

            int newLevel = this.RandomLevel();

            if (newLevel > this.level)
            {
                for (int i = this.level + 1; i <= newLevel; i++)
                {
                    update[i] = this.head;
                }

                this.level = newLevel;
            }

            var newNode = new SkipListNode<T>(newLevel, item);

            for (int i = 0; i <= newLevel; i++)
            {
                newNode.Forward[i] = update[i]!.Forward[i];
                update[i]!.Forward[i] = newNode;
            }

            this.count++;
            this.version++;
        }

        /// <inheritdoc/>
        public bool Remove(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var update = new SkipListNode<T>?[this.maxLevel + 1];
            var current = this.head;
            bool found = false;

            for (int i = this.level; i >= 0; i--)
            {
                while (current.Forward[i] != null
                    && current.Forward[i]!.Value.CompareTo(item) < 0)
                {
                    current = current.Forward[i]!;
                }

                update[i] = current;
            }

            var candidate = current.Forward[0];

            if (candidate != null
                && candidate.Value.CompareTo(item) == 0)
            {
                found = true;

                for (int i = 0; i <= this.level; i++)
                {
                    if (update[i]!.Forward[i] != candidate)
                    {
                        break;
                    }

                    update[i]!.Forward[i] = candidate.Forward[i];
                }

                while (this.level > 0
                    && this.head.Forward[this.level] == null)
                {
                    this.level--;
                }

                this.count--;
                this.version++;
            }

            return found;
        }

        /// <inheritdoc/>
        public void Clear()
        {
            for (int i = 0; i <= this.level; i++)
            {
                this.head.Forward[i] = null;
            }

            this.count = 0;
            this.level = 0;
            this.version++;
        }

        /// <inheritdoc/>
        public bool Contains(T item)
        {
            return this.IndexOf(item) >= 0;
        }

        /// <inheritdoc/>
        public int IndexOf(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            int idx = 0;
            foreach (var v in this)
            {
                if (v.CompareTo(item) == 0)
                {
                    return idx;
                }

                idx++;
            }

            return -1;
        }

        /// <inheritdoc/>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0
                || arrayIndex > array.Length)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(arrayIndex));
            }

            if (array.Length - arrayIndex < this.count)
            {
                throw new ArgumentException(
                    "Not enough space in target array");
            }

            int i = arrayIndex;
            foreach (var v in this)
            {
                array[i++] = v;
            }
        }

        /// <inheritdoc/>
        public void Insert(int index, T item)
        {
            throw new NotSupportedException(
                "Insert by index is not supported");
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            if (index < 0
                || index >= this.count)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(index));
            }

            T value = this[index];
            this.Remove(value);
        }

        // Перечислитель

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        // Вспомогательные методы

        private int RandomLevel()
        {
            int lvl = 0;

            while (this.rnd.NextDouble() < this.probability
                && lvl < this.maxLevel)
            {
                lvl++;
            }

            return lvl;
        }

        // Вложенный класс – перечислитель

        private sealed class Enumerator : IEnumerator<T>
        {
            private readonly SkipList<T> list;
            private readonly int initialVersion;
            private SkipListNode<T>? current;
            private bool started;

            public Enumerator(SkipList<T> list)
            {
                this.list = list;
                this.initialVersion = list.version;
                this.current = list.head;
                this.started = false;
            }

            public T Current
            {
                get
                {
                    if (!this.started
                        || this.current == this.list.head)
                    {
                        throw new InvalidOperationException();
                    }

                    return this.current.Value;
                }
            }

            object IEnumerator.Current
            {
                get { return this.Current; }
            }

            public bool MoveNext()
            {
                if (this.initialVersion != this.list.version)
                {
                    throw new InvalidOperationException(
                        "Collection was modified during enumeration");
                }

                this.current = this.current!.Forward[0];
                this.started = true;
                return this.current != null;
            }

            public void Reset()
            {
                throw new NotSupportedException();
            }

            public void Dispose()
            {
            }
        }
    }
}
