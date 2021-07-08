using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// dvinogradov
/// </summary>

namespace LinkedLists
{
    public class DoubleLinkedList<T> : IEnumerable<T>  // односвязный список
    {
        DoubleLinked_node<T> head; // головной/первый элемент
        DoubleLinked_node<T> tail; // последний/хвостовой элемент
        int count;  // количество элементов в списке

        // добавление элемента в конец
        public void AddEnd(T data)
        {
            DoubleLinked_node<T> node = new DoubleLinked_node<T>(data);
            //Node<T> current = head;
            if (head == null)
            {
                head = node;
            }
            else
            {
                tail.Next = node;
                node.Prev = tail;
            }
            tail = node;
            count++;
        }

        // добавление элемента в начало
        public void AddFront(T data)
        {
            DoubleLinked_node<T> node = new DoubleLinked_node<T>(data);
            DoubleLinked_node<T> temp = head;
            node.Next = temp;
            head = node;
            if (count == 0)
                tail = head;
            else
                temp.Prev = node;
            count++;
        }

        // удаление элемента
        public bool Remove(T data)
        {
            DoubleLinked_node<T> current = head;

            // поиск удаляемого узла
            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    break; 
                }
                current = current.Next;
            }
            if (current != null)
            {
                // если узел не последний
                if (current.Next != null)
                {
                    current.Next.Prev = current.Prev;
                }
                else
                {
                    // если последний, переустанавливаем tail
                    tail = current.Prev;
                }

                // если узел не первый
                if (current.Prev != null)
                {
                    current.Prev.Next = current.Next;
                }
                else
                {
                    // если первый, переустанавливаем head
                    head = current.Next;
                }
                count--;
                return true;
            }
            return false;
        }
        public bool Remove(DoubleLinked_node<T> node)
        {
            //Node<T> current = node;

            if (node != null)
            {
                // если узел не последний
                if (node.Next != null)
                {
                    node.Next.Prev = node.Prev;
                }
                else
                {
                    // если последний, переустанавливаем tail
                    tail = node.Prev;
                }

                // если узел не первый
                if (node.Prev != null)
                {
                    node.Prev.Next = node.Next;
                }
                else
                {
                    // если первый, переустанавливаем head
                    head = node.Next;
                }
                count--;
                return true;
            }
            return false;
        }

        public int Count { get => count; }
        public bool IsEmpty { get => count == 0; }
        //
        public DoubleLinked_node<T> Last 
        {
            get => tail;
        }

        // очистка списка
        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }

        //Содержится ли
        public bool Contains(T data)
        {
            DoubleLinked_node<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                    return true;
                current = current.Next;
            }
            return false;
        }


        // реализация интерфейса IEnumerable


        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            DoubleLinked_node<T> current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
        public IEnumerable<T> BackEnumerator()
        {
            DoubleLinked_node<T> current = tail;
            while (current != null)
            {
                yield return current.Data;
                current = current.Prev;
            }
        }
    }
}


