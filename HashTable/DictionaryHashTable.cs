using System;
using System.Collections.Generic;
using System.Text;

namespace HashTables
{
    public class DictionaryHashTable<TValue> where TValue : IComparable<TValue>
    {
		SimpleBinaryTree<TValue>[] hashArray; // Массив ячеек хеш-таблицы
        int arraySize;
		SimpleBinaryTree<TValue> nonItem = null; // Для удаленных элементов
        public DictionaryHashTable(int size) // Конструктор
        {
            arraySize = size;
            hashArray = new SimpleBinaryTree<TValue>[arraySize];
			//for (int i = 0; i < size; i++)
			//{
			//	hashArray[i] = new SimpleBinaryTree<TValue>();
			//}
        }
        public void displayTable()
        {
            Console.WriteLine("Table: ");
            for (int j = 0; j < arraySize; j++)
            {
				if (hashArray[j] != null)
				{
					hashArray[j].Tree_preorder();
					Console.WriteLine();
				}
				else
					Console.WriteLine("** ");
            }
            Console.WriteLine("#################################################################");
        }

		public int hashFuncBig(string key="dima")
		{
			int hashVal = 0;
			int pow27 = 1; // 1, 27, 27*27 и т. д.
			char[] c = key.ToCharArray();
			for (int j = c.Length - 1; j >= 0; j--) // Справа налево
			{
				int letter = Convert.ToInt32(c[j]); // Получение кода символа
				hashVal += pow27 * letter; // Умножение на степень 27
				pow27 *= 27; // Следующая степень 27
			}
			return hashVal % arraySize;
		}
		public int hashFuncLittle(string key)
		{
			ulong HASH_MULL = 31;
			ulong hashVal = 0;
			char[] c = key.ToCharArray();
			for (int i = 0, length = c.Length; i < length; i++)
			{
				hashVal += hashVal * HASH_MULL + (ulong)Convert.ToInt32(c[i]);
			}
			ulong ret = hashVal % (ulong)arraySize;
			return Convert.ToInt32(ret);
		}

		public bool Add(string key, TValue value)
		{
			//int hashVal = hashFuncBig(key);
			int hashVal = hashFuncLittle(key);
			
			if (hashArray[hashVal] != null)
			{
				if (hashArray[hashVal].Find(key) != null)
				{
					return false;
				}
			}
			else
			{
				hashArray[hashVal] = new SimpleBinaryTree<TValue>();
			}
			hashArray[hashVal].Put(key, value);
			return true;
		}

		public bool Find(string key, out TValue value)
		{
			int hashVal = hashFuncLittle(key);
			value = default(TValue);
			if (hashArray[hashVal] != null)
			{
				if (hashArray[hashVal].Find(key) != null)
				{
					value = hashArray[hashVal].Find(key)._value;
					return true;
				}
			}
			return false;
		}

		public bool Delete(string key)
		{
			int hashVal = hashFuncLittle(key);
			bool ret = hashArray[hashVal].Remove(key);
			if(hashArray[hashVal].count == 0)
			{
				hashArray[hashVal] = null;
			}
			return ret;
			//if (hashArray[hashVal] != null)
			//{
			//	if (hashArray[hashVal].Find(key) != null)
			//	{
			//		hashArray[hashVal].Remove(key);
			//		return true;
			//	}
			//}
			//return false;
		}

		//public int hashFunc1(int key)
		//{
		//    return key % arraySize;
		//}
		//public int hashFunc2(int key)
		//{
		//    // Возвращаемое значение отлично от нуля, меньше размера массива,
		//    // функция отлична от хеш-функции 1
		//    // Размер массива должен быть простым по отношению к 5, 4, 3 и 2
		//    return 5 - key % 5;
		//}
		//public int hashFunc3(String key)
		//{
		//    int hashVal = 0;
		//    int pow27 = 1; // 1, 27, 27*27 и т. д.
		//    char[] c = key.ToCharArray();
		//    for (int j = c.Length - 1; j >= 0; j--) // Справа налево
		//    {
		//        int letter = Convert.ToInt32(c[j]) - 96; // Получение кода символа
		//        hashVal += pow27 * letter; // Умножение на степень 27
		//        pow27 *= 27; // Следующая степень 27
		//    }
		//    return hashVal % arraySize;
		//}
		//public void insert(int key, KeyValueData item)
		//// (Метод предполагает, что таблица не заполнена)
		//{
		//    int hashVal = hashFunc1(key); // Хеширование ключа
		//    int stepSize = hashFunc2(key); // Вычисление смещения
		//                                   // Пока не будет найдена

		//    while (hashArray[hashVal] != null && hashArray[hashVal].getKey() != -1)// пустая ячейка или -1
		//    {
		//        hashVal += stepSize; // Прибавление смещения
		//        hashVal %= arraySize; // Возврат к началу
		//    }
		//    hashArray[hashVal] = item; // Вставка элемента
		//}
		//public KeyValueData delete(int key) // Удаление элемента данных
		//{
		//    int hashVal = hashFunc1(key); // Хеширование ключа
		//    int stepSize = hashFunc2(key); // Вычисление смещения
		//    while (hashArray[hashVal] != null) // Пока не найдена пустая ячейка
		//    { // Ключ найден?
		//        if (hashArray[hashVal].getKey() == key)
		//        {
		//            KeyValueData temp = hashArray[hashVal]; // Временное сохранение
		//            hashArray[hashVal] = nonItem; // Удаление элемента
		//            return temp; // Метод возвращает элемент
		//        }
		//        hashVal += stepSize; // Прибавление смещения
		//        hashVal %= arraySize; // Возврат к началу
		//    }
		//    return null; // Элемент не найден
		//}
		//public KeyValueData find(int key) // Поиск элемента с заданным ключом
		//                              // (Метод предполагает, что таблица не заполнена)
		//{
		//    int hashVal = hashFunc1(key); // Хеширование ключа
		//    int stepSize = hashFunc2(key); // Вычисление смещения
		//    while (hashArray[hashVal] != null) // Пока не найдена пустая ячейка
		//    { // Ключ найден?
		//        if (hashArray[hashVal].getKey() == key)
		//            return hashArray[hashVal]; // Да, метод возвращает элемент
		//        hashVal += stepSize; // Прибавление смещения
		//        hashVal %= arraySize; // Возврат к началу
		//    }
		//    return null; // Элемент не найден
		//}
	}

	public class SimpleBinaryTree<TValue> where TValue : IComparable<TValue>
	{
		public SimpleBinaryTree_node<TValue> root;                //корень
		public SimpleBinaryTree_node<TValue> current;        //текущий эл-т
		public int count { get; set; }                   //количество эл-тов
		private bool added = false;             // добавлен ли узел-логическая переменная
		internal Queue<SimpleBinaryTree_node<TValue>> queue;    //очередь для обхода

		// Put
		//функция добавления эл-та
		public void Put(string key, TValue value)
		{
			if (root == null)
			{
				root = new SimpleBinaryTree_node<TValue>(key, value);
				count++;
				current = root;
				return;
			}
			else
			{
				if (current.parent == null)
					current.parent = root;

				//если ключ у того, который добавляем, больше
				//чем у текущего, то он становится справа

				if (key.CompareTo(current._key) > 0)
				{
					if (current.right == null)
					{
						current.right = new SimpleBinaryTree_node<TValue>(key, value);

						current.right.parent = current;
						added = true;
						current = root;
						count++;
						return;
					}
					//если правый ребенок уже имеется,
					//перебрасываем указатель на правого ребенка
					//текущего и проверяем снова
					else
					{
						current = current.right;
						Put(key, value);
					}
					return;
				}
				if (key.CompareTo(current._key) < 0)
				{
					if (current.left == null)
					{
						current.left = new SimpleBinaryTree_node<TValue>(key, value);
						current.left.parent = current;

						added = true;
						current = root;
						count++;
						return;
					}
					else
					{
						current = current.left;
						Put(key, value);
					}
					return;
				}
			}
		}

		//функция для поиска узла по ключу
		//в результате выдает его значение(value)
		public TValue getValue(string key)
		{
			//начинаем проверку от корня
			//присваиваем текущему эл-ту его значение
			current = root;
			if (root == null)
			{
				current = root;
				return default(TValue);//лучше добавить message
			}

			while (current != null)
			{
				if (key.CompareTo(current._key) == 0)
				{
					return current._value;
				}
				else
				{
					//если ключ узла, который ищем больше, чем ключ текущего элемента
					//двигаемся по правой ветке
					if (key.CompareTo(current._key) > 0)
					{
						if (current.right != null)
						{
							current = current.right;
						}
						else
						{
							return default(TValue);
						}
					}
					//если ключ узла, который ищем меньше, чем ключ текущего элемента
					//двигаемся по левой ветке
					else if (key.CompareTo(current._key) < 0)
					{
						if (current.left != null)
						{
							current = current.left;
						}
						else
						{
							return default(TValue);
						}

					}
				}
			}
			return default(TValue);
		}

		//функция поиска, которая отличается от предыдущей тем, что возвращает узел
		//она понадобится для удаления узла
		public SimpleBinaryTree_node<TValue> Find(string key)
		{
			current = root;
			if (root == null)
			{
				current = root;
				return null;
			}
			while (current != null)
			{
				if (key.CompareTo(current._key) == 0)
				{
					return current;
				}
				else
				{
					if (key.CompareTo(current._key) > 0)
					{
						if (current.right != null)
						{
							current = current.right;
						}
						else
						{
							return null;
						}
					}
					else if (key.CompareTo(current._key) < 0)
					{
						if (current.left != null)
						{
							current = current.left;
						}
						else
						{
							return null;
						}
					}
				}
			}
			return null;
		}


		//поиск минимального элемента(выдает его ключ)
		public string Min_key()
		{
			//создаем узел и присваиваем ему значение нашего корня

			SimpleBinaryTree_node<TValue> c = root;
			//известно, что в бинарном дереве крайний левый элемент от корня
			//является минимальным
			while (c.left != null)
			{
				c = c.left;
			}
			return c._key;
		}

		//поиск минимального элемента
		private SimpleBinaryTree_node<TValue> Min_nod()
		{
			//создаем узел и присваиваем ему значение нашего корня

			SimpleBinaryTree_node<TValue> tmp = root;
			//известно, что в бинарном дереве крайний левый элемент от корня
			//является минимальным
			while (tmp.left != null)
			{
				tmp = tmp.left;
			}
			return tmp;
		}

		//функция удаления
		public bool Remove(string key)
		{
			SimpleBinaryTree_node<TValue> remove_node = Find(key);
			return Remove(remove_node);
		}
		public bool Remove(SimpleBinaryTree_node<TValue> remove_node)
		{
			if (remove_node == null)
			{
				return false;
			}
			//если количество элементов узла=1,значит нам необходимо удалить корень
			if (count == 1)
			{
				root = null;
				count--;
			}
			//если у удаляемого узла нет детей

			else if (remove_node.left == null && remove_node.right == null)
			{
				// если удаляемый узел является левым ребенком      
				if (remove_node.IsLeftChild)
				{
					remove_node.parent.left = null;
				}
				// если удаляемый узел является правым ребенком                   
				else
				{
					remove_node.parent.right = null;
				}
				remove_node.parent = null;
				count--;
			}
			//один ребенок
			else if (remove_node.ChildCount == 1)
			{
				//если есть левый ребенок
				if (remove_node.left != null)
				{
					//то родителем левого ребенка станет родитель удаляемого узла
					remove_node.left.parent = remove_node.parent;
					if (remove_node == root)
					{
						root = remove_node.left;
					}
					if (remove_node.IsLeftChild)
					{

						remove_node.parent.left = remove_node.left;
						remove_node.left = null;
					}
					else if (remove_node.IsRightChild || remove_node.parent != null)
					{

						remove_node.parent.right = remove_node.left;
						remove_node.right = null;
					}

				}
				else if (remove_node.right != null)
				{
					remove_node.right.parent = remove_node.parent;
					if (remove_node == root)
					{
						root = remove_node.right;
					}
					if (remove_node.IsRightChild)
					{

						remove_node.parent.right = remove_node.right;
						remove_node.right = null;
					}
					else if (remove_node.IsLeftChild || remove_node.parent != null)
					{

						remove_node.parent.left = remove_node.right;
						remove_node.right = null;
					}
				}
				//убираем ссылку на родителя и уменьшаем кол-во элементов в дереве
				remove_node.parent = null;
				count--;
			}
			//если есть два ребенка
			//у левой ветки крайний правый становится вместо удаляемого узла
			else
			{
				SimpleBinaryTree_node<TValue> node = remove_node.right;
				while (node.left != null)
				{
					node = node.left;
				}
				remove_node._value = node._value;
				remove_node._key = node._key;
				//запускаем проверку дальше,чтобы вернуть дерево в нормальное состояние
				Remove(node);
			}

			return true;
		}

		// Обход дерева
		public void Tree_preorder()
		{
			//создаем очередь в которую будем помещать наши элементы
			queue = new Queue<SimpleBinaryTree_node<TValue>>();
			//	обход начинаем с корневого узла
			visit_node(root);
		}

		//за основу берем алгоритм поиска  в глубину типа Pre-order
		//мы проверяем корни перед тем как проверить их детей
		public void visit_node(SimpleBinaryTree_node<TValue> node)
		{
			//идем по левой потом по правой ветке
			if (node == null)
			{
				return;
			}
			queue.Enqueue(node);
			Console.Write("|key: {0}, value: {1}| ", node._key, node._value);
			visit_node(node.left);
			visit_node(node.right);

		}

	}
	public class SimpleBinaryTree_node<TValue> where TValue : IComparable<TValue>
	{
		public string _key;//ключ
		public TValue _value;//значение

		public SimpleBinaryTree_node<TValue> left = null;    //левый ребенок
		public SimpleBinaryTree_node<TValue> right = null; //правый ребенок
		public SimpleBinaryTree_node<TValue> parent;       //родитель

		public SimpleBinaryTree_node() { }

		public SimpleBinaryTree_node(string key, TValue value)
		{
			this._key = key;
			this._value = value;
			left = null;
			right = null;
		}

		//является ли узел левым ребенком
		public virtual bool IsLeftChild
		{
			get { return this.parent != null && this.parent.left == this; }
		}

		//является ли узел правым ребенком
		public virtual bool IsRightChild
		{
			get { return this.parent != null && this.parent.right == this; }
		}

		//определяем количество детей у узла
		public virtual int ChildCount
		{
			get
			{
				int count = 0;

				if (this.left != null)
					count++;

				if (this.right != null)
					count++;

				return count;
			}
		}
	}
}
