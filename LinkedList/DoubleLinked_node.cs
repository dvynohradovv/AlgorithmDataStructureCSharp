using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// dvinogradov
/// </summary>

namespace LinkedLists
{
    public class DoubleLinked_node<T>
    {
        private T value;
        private DoubleLinked_node<T> next;
        private DoubleLinked_node<T> prev;

       public DoubleLinked_node(T _data, DoubleLinked_node<T> _next=null, DoubleLinked_node<T> _prev = null)
        {
           value = _data;
           next = _next;
           prev = _next;
        }

        /// <summary>
        /// ссылка на следующий элемент
        /// </summary>
        public DoubleLinked_node<T> Next
        {
            get => next;
            set => next = value;
        }

        /// <summary>
        /// ссылка на предыдущий элемент
        /// </summary>
        public DoubleLinked_node<T> Prev
        {
            get => prev;
            set => prev = value;
        }

        /// <summary>
        /// данные узла
        /// </summary>
        public T Data
        {
            get => value;
            set => this.value = value;
        }
     
    }
}
