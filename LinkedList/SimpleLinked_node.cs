using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedLists
{
    public class SimpleLinked_node<T>
    {
        private T data;
        private SimpleLinked_node<T> next;

       public SimpleLinked_node(T data, SimpleLinked_node<T> next=null)
        {
           this.data = data;
           this.next = next;
        }

        /// <summary>
        /// ссылка на следующий элемент
        /// </summary>
        public SimpleLinked_node<T> Next
        {
            get { return this.next; }
            set { this.next = value; }
        }
        /// <summary>
        /// данные узла
        /// </summary>
        public T Data
        {
            get { return this.data; }
            set { this.data = value; }
        }
     
    }
}
