using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTrees
{
	public class SimpleBinaryTree<T> where T : IComparable<T>
	{
		public SimpleBinaryTree_node<T> root;                //корень
		public SimpleBinaryTree_node<T> current;        //текущий эл-т
		public int count { get; set; }                   //количество эл-тов
		private bool added = false;             // добавлен ли узел-логическая переменная
		internal Queue<SimpleBinaryTree_node<T>> queue;    //очередь для обхода

		// Put
		//функция добавления эл-та
		public void Put(string value, T key)
		{

			if (root == null)
			{
				root = new SimpleBinaryTree_node<T>(value, key);
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

				if (key.CompareTo(current.key) > 0)
				{
					if (current.right == null)
					{
						current.right = new SimpleBinaryTree_node<T>(value, key);

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
						Put(value, key);
					}
					return;
				}
				if (key.CompareTo(current.key) < 0)
				{
					if (current.left == null)
					{
						current.left = new SimpleBinaryTree_node<T>(value, key);
						current.left.parent = current;

						added = true;
						current = root;
						count++;
						return;
					}
					else
					{
						current = current.left;
						Put(value, key);
					}
					return;
				}
			}
		}


		// Find_node
		//функция для поиска узла по ключу
		//в результате выдает его значение(value)

		public string Find_node(T targeObj)
		{
			//начинаем проверку от корня
			//присваиваем текущему эл-ту его значение
			current = root;
			if (root == null)
			{
				current = root;
				return "такого узла нет";
			}

			while (current != null)
			{
				if (targeObj.CompareTo(current.key) == 0)
				{

					return current.value;
				}
				else
				{
					//если ключ узла, который ищем больше, чем ключ текущего элемента
					//двигаемся по правой ветке
					if (targeObj.CompareTo(current.key) > 0)
					{
						if (current.right != null)
						{

							current = current.right;

						}
						else
						{

							return "такого узла нет";
						}

					}
					//если ключ узла, который ищем меньше, чем ключ текущего элемента
					//двигаемся по левой ветке

					else if (targeObj.CompareTo(current.key) < 0)
					{
						if (current.left != null)
						{
							current = current.left;

						}
						else
						{

							return "такого узла нет";
						}

					}
				}
			}
			return null;
		}

		//  Find

		//функция поиска, которая отличается от предыдущей тем, что возвращает узел
		//она понадобится для удаления узла

		public SimpleBinaryTree_node<T> Find(T key1)
		{
			current = root;
			if (root == null)
			{

				current = root;
				return null;
			}

			while (current != null)
			{
				if (key1.CompareTo(current.key) == 0)
				{

					return current;
				}
				else
				{
					if (key1.CompareTo(current.key) > 0)
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
					else if (key1.CompareTo(current.key) < 0)
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

		//  Minimum

		//поиск минимального элемента(выдает его ключ)

		public T Minimum()
		{
			//создаем узел и присваиваем ему значение нашего корня

			SimpleBinaryTree_node<T> c = root;
			//известно, что в бинарном дереве крайний левый элемент от корня
			//является минимальным
			while (c.left != null)
			{
				c = c.left;
			}
			return c.key;
		}

		//Remove
		//функция удаления

		public bool Remove(T key)
		{
			SimpleBinaryTree_node<T> remove_node = Find(key);
			return Remove(remove_node);
		}
		public bool Remove(SimpleBinaryTree_node<T> remove_node)
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
				SimpleBinaryTree_node<T> node = remove_node.right;
				while (node.left != null)
				{
					node = node.left;
				}
				remove_node.key = node.key;
				remove_node.value = node.value;
				//запускаем проверку дальше,чтобы вернуть дерево в нормальное состояние
				Remove(node);
			}

			return true;
		}

		// Обход дерева
		public void Tree_preorder()
		{
			//создаем очередь в которую будем помещать наши элементы
			queue = new Queue<SimpleBinaryTree_node<T>>();
			//	обход начинаем с корневого узла
			visit_node(root);
		}

		//за основу берем алгоритм поиска  в глубину типа Pre-order
		//мы проверяем корни перед тем как проверить их детей
		public void visit_node(SimpleBinaryTree_node<T> node)
		{
			//идем по левой потом по правой ветке
			if (node == null)
			{
				return;
			}
			queue.Enqueue(node);
			Console.WriteLine(node.key);
			visit_node(node.left);
			visit_node(node.right);

		}

	}
	public class SimpleBinaryTree_node<T> where T : IComparable<T>
	{
		public T key;                                          //ключ
		public string value = "";              //значение
		public SimpleBinaryTree_node<T> left = null;    //левый ребенок
		public SimpleBinaryTree_node<T> right = null; //правый ребенок
		public SimpleBinaryTree_node<T> parent;       //родитель

		public SimpleBinaryTree_node()
		{ }

		public SimpleBinaryTree_node(string value, T key)
		{
			this.key = key;
			this.value = value;
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
