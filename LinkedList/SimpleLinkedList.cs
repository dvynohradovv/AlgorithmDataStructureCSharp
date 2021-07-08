using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace LinkedLists
{
    public class SimpleLinkedList<T> : IEnumerable<T>  // односвязный список
    {
        SimpleLinked_node<T> head; // головной/первый элемент
        SimpleLinked_node<T> tail; // последний/хвостовой элемент
        int count;  // количество элементов в списке

        // добавление элемента
        public void Add(T data)
        {
            SimpleLinked_node<T> node = new SimpleLinked_node<T>(data);
            SimpleLinked_node<T> current = head;
            if (head == null)
                head = node;
            else
            {
                while (current.Next != null)
                {
                    current = current.Next;
                }

               current.Next = node;
                tail = node;
            }
            count++;
        }
        
         // удаление элемента
        public bool Remove(T data)
        {
            SimpleLinked_node<T> current = head;
            SimpleLinked_node<T> previous = null;

            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    // Если узел в середине или в конце
                    if (previous != null)
                    {
                        // убираем узел current, теперь previous ссылается не на current, а на current.Next
                        previous.Next = current.Next;

                        // Если current.Next не установлен, значит узел последний,
                        // изменяем переменную tail
                        if (current.Next == null)
                            tail = previous;
                    }
                    else
                    {
                        // если удаляется первый элемент
                        // переустанавливаем значение head
                        head = head.Next;

                        // если после удаления список пуст, сбрасываем tail
                        if (head == null)
                            tail = null;
                    }
                    count--;
                    return true;
                }

                previous = current;
                current = current.Next;
            }
            return false;
        }

        public int Count { get { return count; } }
        public bool IsEmpty { get { return count == 0; } }
        // очистка списка
        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }
      
        // реализация интерфейса IEnumerable


        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            SimpleLinked_node<T> current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
    }
}


