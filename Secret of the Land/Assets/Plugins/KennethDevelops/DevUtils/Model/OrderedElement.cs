using System;

namespace KennethDevelops.DevUtils.Model {    
    /// <summary>
    /// Represents an ordered element within an ordered list. 
    /// </summary>
    /// <typeparam name="T">The type of the element.</typeparam>
    /// <remarks>
    /// For more detailed information, check the full documentation here: https://github.com/kennethdevelops/DevUtilsDocs/wiki/OrderedElement
    /// </remarks>
    [Serializable]
    public class OrderedElement<T> {

        /// <summary>
        /// Represents the order of the element.
        /// </summary>
        public int order;
    
        /// <summary>
        /// Represents the element.
        /// </summary>
        public T element;

        /// <summary>
        /// Creates a new instance of the OrderedElement class with a specified order and element.
        /// </summary>
        /// <param name="order">The order of the element.</param>
        /// <param name="element">The element.</param>
        public OrderedElement(int order, T element){
            this.order   = order;
            this.element = element;
        }
    }

}