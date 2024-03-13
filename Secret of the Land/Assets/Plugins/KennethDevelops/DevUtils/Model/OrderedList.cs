using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KennethDevelops.DevUtils.Model {    /// <summary>
    /// Represents an ordered list of elements. 
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <remarks>
    /// For more detailed information, check the full documentation here: https://github.com/kennethdevelops/DevUtilsDocs/wiki/OrderedList
    /// </remarks>
    [Serializable]
    public class OrderedList<T> : IEnumerable<OrderedElement<T>> {

        [SerializeField] private List<OrderedElement<T>> _list        = new List<OrderedElement<T>>();
        public readonly          int                     defaultOrder = 100;

        /// <summary>
        /// Creates a new instance of the OrderedList class.
        /// </summary>
        public OrderedList() { }

        /// <summary>
        /// Creates a new instance of the OrderedList class with a specified default order.
        /// </summary>
        /// <param name="defaultOrder">The default order for the elements.</param>
        public OrderedList(int defaultOrder) {
            this.defaultOrder = defaultOrder;
        }

        /// <summary>
        /// Adds an element to the list with a specified order.
        /// </summary>
        /// <param name="order">The order of the element.</param>
        /// <param name="element">The element to add to the list.</param>
        /// <returns>Returns the updated list.</returns>
        public OrderedList<T> Add(int order, T element) {
            return Add(new OrderedElement<T>(order, element));
        }

        /// <summary>
        /// Adds an ordered element to the list.
        /// </summary>
        /// <param name="element">The ordered element to add to the list.</param>
        /// <returns>Returns the updated list.</returns>
        public OrderedList<T> Add(OrderedElement<T> element) {
            _list.Add(element);
            return this;
        }

        /// <summary>
        /// Removes an ordered element from the list.
        /// </summary>
        /// <param name="element">The ordered element to remove from the list.</param>
        /// <returns>Returns the updated list.</returns>
        public OrderedList<T> Remove(OrderedElement<T> element) {
            _list.Remove(element);
            return this;
        }

        /// <summary>
        /// Removes an element from the list.
        /// </summary>
        /// <param name="element">The element to remove from the list.</param>
        /// <returns>Returns the updated list.</returns>
        public OrderedList<T> Remove(T element) {
            for (var i = 0; i < _list.Count; i++) {
                if (!_list[i].element.Equals(element)) continue;

                _list.RemoveAt(i);
                return this;
            }

            return this;
        }

        /// <summary>
        /// Removes the element at a specific order from the list.
        /// </summary>
        /// <param name="order">The order of the element to remove.</param>
        /// <returns>Returns the updated list.</returns>
        public OrderedList<T> RemoveAt(int order) {
            for (var i = 0; i < _list.Count; i++) {
                if (_list[i].order != order) continue;

                _list.RemoveAt(i);
                return this;
            }

            return this;
        }

        /// <summary>
        /// Removes all instances of a specific element from the list.
        /// </summary>
        /// <param name="element">The element to remove.</param>
        /// <returns>Returns the updated list.</returns>
        public OrderedList<T> RemoveAll(T element) {
            for (var i = 0; i < _list.Count; i++) {
                if (!_list[i].element.Equals(element)) continue;

                _list.RemoveAt(i);
            }

            return this;
        }

        /// <summary>
        /// Removes all elements that match the conditions defined by the specified predicate.
        /// </summary>
        /// <param name="condition">The predicate delegate that defines the conditions of the elements to remove.</param>
        /// <returns>Returns the updated list.</returns>
        public OrderedList<T> RemoveAll(Func<T, bool> condition) {
            for (var i = 0; i < _list.Count; i++) {
                if (!condition(_list[i].element)) continue;

                _list.RemoveAt(i);
            }

            return this;
        }

        /// <summary>
        /// Returns the list of ordered elements.
        /// </summary>
        /// <returns>Returns the list of ordered elements.</returns>
        public List<OrderedElement<T>> GetOrderedElements() {
            return _list;
        }

        /// <summary>
        /// Returns the number of elements in the list.
        /// </summary>
        public int Count => _list.Count;

        // Plus and minus operators need to be documented as well. The use of these operators is unclear, so you may need to adjust the descriptions.
        public static OrderedList<T> operator -(OrderedList<T> list, OrderedElement<T> element) {
            return list == null ? list = new OrderedList<T>() : list.Remove(element);
        }

        public static OrderedList<T> operator -(OrderedList<T> list, T element) {
            return list == null ? list = new OrderedList<T>() : list.Remove(element);
        }

        public static OrderedList<T> operator +(OrderedList<T> list, OrderedElement<T> element) {
            if (list == null) list = new OrderedList<T>();
            return list.Add(element);
        }

        public static OrderedList<T> operator +(OrderedList<T> list, T element) {
            if (list == null) list = new OrderedList<T>();
            return list.Add(new OrderedElement<T>(list.defaultOrder, element));
        }

        /// <summary>
        /// Returns an enumerator that iterates through the list.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the list.</returns>
        IEnumerator<OrderedElement<T>> IEnumerable<OrderedElement<T>>.GetEnumerator() {
            return _list.OrderBy(n => n.order).GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the list.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the list.</returns>
        public IEnumerator GetEnumerator() {
            return _list.OrderBy(n => n.order).GetEnumerator();
        }
    }

}