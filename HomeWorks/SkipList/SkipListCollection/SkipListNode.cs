//-----------------------------------------------------------------------
// <copyright file="SkipListNode.cs" company="SPbSU">
//     Copyright (c) SPbSU. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#nullable disable
namespace SkipListCollection
{
    using System;

    /// <summary>
    /// Узел skip-списка: хранит значение и ссылки вперед на разных уровнях.
    /// </summary>
    /// <typeparam name="T">Тип значений, реализующий IComparable&lt;T&gt;.</typeparam>
    internal sealed class SkipListNode<T>
        where T : IComparable<T>
    {
        // Поля

        /// <summary>
        /// Указатели на следующий узел на каждом уровне (0..Level).
        /// </summary>
        private readonly SkipListNode<T>?[] forward;

        /// <summary>
        /// Хранимое значение узла.
        /// </summary>
        private readonly T value;

        /// <summary>
        /// Максимальный уровень этого узла (>=0).
        /// </summary>
        private readonly int level;

        // Свойства

        /// <summary>
        /// Получает массив указателей на следующий узел.
        /// </summary>
        internal SkipListNode<T>?[] Forward
        {
            get { return this.forward; }
        }

        /// <summary>
        /// Получает значение узла.
        /// </summary>
        internal T Value
        {
            get { return this.value; }
        }

        /// <summary>
        /// Получает максимальный уровень узла.
        /// </summary>
        internal int Level
        {
            get { return this.level; }
        }

        // Конструкторы

        /// <summary>
        /// Создаёт новый узел заданного уровня и значения.
        /// </summary>
        /// <param name="level">Максимальный уровень (>=0).</param>
        /// <param name="value">Значение.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Если level &lt; 0.
        /// </exception>
        internal SkipListNode(int level, T value)
        {
            if (level < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(level), "Level must be non-negative");
            }

            this.level = level;
            this.value = value;
            this.forward = new SkipListNode<T>?[level + 1];
        }
    }
}
