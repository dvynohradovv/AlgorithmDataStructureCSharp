using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTrees
{
    public class DictionaryBinaryTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        public DictionaryBinaryTree<TKey, TValue> root;// корень
        public DictionaryBinaryTree<TKey, TValue> current;// текущий эл-т
        public int count { get; set; }// количество эл-тов
        private bool added = false;        		// добавлен ли узел-логическая переменная
        internal Queue<DictionaryBinaryTree<TKey, TValue>> queue;    // очередь для обхода

        // Put
        //функция добавления эл-та
        public void Put(string value, TKey key)
        {

            if (root == null)
            {
                root = new DictionaryBinary_node<TKey>(value, key);
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
                        current.right = new DictionaryBinary_node<TKey>(value, key);

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
                        current.left = new DictionaryBinary_node<TKey>(value, key);
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
        public string Find_node(TKey targeObj)
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

        public DictionaryBinary_node<TKey> Find(TKey key1)
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

        public TKey Minimum()
        {
            //создаем узел и присваиваем ему значение нашего корня

            DictionaryBinary_node<TKey> c = root;
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

        public bool Remove(TKey key)
        {
            DictionaryBinary_node<TKey> remove_node = Find(key);
            return Remove(remove_node);
        }
        public bool Remove(DictionaryBinary_node<TKey> remove_node)
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
                DictionaryBinary_node<TKey> node = remove_node.right;
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
            queue = new Queue<DictionaryBinary_node<TKey>>();
            //	обход начинаем с корневого узла
            visit_node(root);
        }

        //за основу берем алгоритм поиска  в глубину типа Pre-order
        //мы проверяем корни перед тем как проверить их детей
        public void visit_node(DictionaryBinary_node<TKey> node)
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
    public class DictionaryBinary_node<T> where T : IComparable<T>
    {
        public T key;                                          //ключ
        public string value = "";		       //значение
        public DictionaryBinary_node<T> left = null;    //левый ребенок
        public DictionaryBinary_node<T> right = null; //правый ребенок
        public DictionaryBinary_node<T> parent;       //родитель

        public DictionaryBinary_node()
        { }

        public DictionaryBinary_node(string value, T key)
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












	public class DictionaryBinaryTree<TKey, TValue> where TKey : IComparable
	{
		//Поля
		public DictionaryBinaryTree<TKey, TValue> Parent { get; set; }
		public DictionaryBinaryTree<TKey, TValue> Left { get; set; }
		public DictionaryBinaryTree<TKey, TValue> Right { get; set; }
		public TKey Key { get; set; }
		public TValue Value { get; set; }

		//Конструктор
		public DictionaryBinaryTree()
		{
			//this.Parent = this;
		}
		public DictionaryBinaryTree(TKey key, TValue value, DictionaryBinaryTree<TKey, TValue> parent)
		{
			this.Key = key;
			this.Value = value;
			this.Parent = parent;
		}
		public DictionaryBinaryTree((TKey, TValue)[] KeyValuePair)
		{
			Parent = null;
			foreach (var item in KeyValuePair)
			{
				bstree_add(item.Item1, item.Item2);
			}
		}

		//Добавить эллемент
		public void bstree_add(TKey key, TValue value)
		{
			if (key.CompareTo(this.Key) < 0)
			{
				if (this.Left == null)
				{
					this.Left = new DictionaryBinaryTree<TKey, TValue>(key, value, this);
				}
				else if (this.Left != null)
					this.Left.bstree_add(key, value);
			}
			else
			{
				if (this.Right == null)
				{
					this.Right = new DictionaryBinaryTree<TKey, TValue>(key, value, this);
				}
				else if (this.Right != null)
					this.Right.bstree_add(key, value);
			}
		}

		//public void bstree_add(TKey key, TValue value)
		//{
		//    if (key.CompareTo(this.Key) < 0)
		//    {
		//        if (this.Left == null)
		//        {
		//            this.Left = new BSTDictionary<TKey, TValue>(key, value, this);
		//        }
		//        else if (this.Left != null)
		//            this.Left.bstree_add(key, value);
		//    }
		//    else
		//    {
		//        if (this.Right == null)
		//        {
		//            this.Right = new BSTDictionary<TKey, TValue>(key, value, this);
		//        }
		//        else if (this.Right != null)
		//            this.Right.bstree_add(key, value);
		//    }
		//}

		//Поиск по дереву
		public DictionaryBinaryTree<TKey, TValue> bstree_lookup(TKey val)
		{
			return _bstree_lookup(this, val);
		}
		private DictionaryBinaryTree<TKey, TValue> _bstree_lookup(DictionaryBinaryTree<TKey, TValue> tree, TKey val)
		{
			if (tree == null) return null;
			switch (val.CompareTo(tree.Key))
			{
				case 1: return _bstree_lookup(tree.Right, val);
				case -1: return _bstree_lookup(tree.Left, val);
				case 0: return tree;
				default: return null;
			}
		}

		//Поиск максимального эллемента
		public DictionaryBinaryTree<TKey, TValue> bstree_max()
		{
			return _bstree_max(GoToRoot(this));
		}
		private DictionaryBinaryTree<TKey, TValue> _bstree_max(DictionaryBinaryTree<TKey, TValue> right_nod)
		{
			if (right_nod.Right != null)
			{
				return _bstree_max(right_nod.Right);
			}
			return right_nod;
		}

		//Поиск минимального эллемента
		public DictionaryBinaryTree<TKey, TValue> bstree_min()
		{
			return _bstree_min(GoToRoot(this));
		}
		private DictionaryBinaryTree<TKey, TValue> _bstree_min(DictionaryBinaryTree<TKey, TValue> left_nod)
		{
			if (left_nod.Left != null)
			{
				return _bstree_max(left_nod.Left);
			}
			return left_nod;
		}

		//Поднятие к корню
		private DictionaryBinaryTree<TKey, TValue> GoToRoot(DictionaryBinaryTree<TKey, TValue> parent)
		{
			if (parent.Parent != null)
			{
				return parent.Parent;
			}
			return parent;
		}

		//Удаление эллемента
		public bool bstree_delete(TKey key)
		{
			//Проверяем, существует ли данный узел
			DictionaryBinaryTree<TKey, TValue> tree = bstree_lookup(key);
			if (tree == null)
			{
				//Если узла не существует, вернем false
				return false;
			}
			DictionaryBinaryTree<TKey, TValue> curTree;

			//Если удаляем корень
			if (tree == this)
			{
				if (tree.Right != null)
				{
					curTree = tree.Right;
				}
				else curTree = tree.Left;

				while (curTree.Left != null)
				{
					curTree = curTree.Left;
				}
				TKey temp = curTree.Key;

				this.bstree_delete(temp);
				tree.Key = temp;

				return true;
			}

			//Удаление листьев
			if (tree.Left == null && tree.Right == null && tree.Parent != null)
			{
				if (tree == tree.Parent.Left)
					tree.Parent.Left = null;
				else
				{
					tree.Parent.Right = null;
				}
				return true;
			}

			//Удаление узла, имеющего левое поддерево, но не имеющее правого поддерева
			if (tree.Left != null && tree.Right == null)
			{
				//Меняем родителя
				tree.Left.Parent = tree.Parent;
				if (tree == tree.Parent.Left)
				{
					tree.Parent.Left = tree.Left;
				}
				else if (tree == tree.Parent.Right)
				{
					tree.Parent.Right = tree.Left;
				}
				return true;
			}

			//Удаление узла, имеющего правое поддерево, но не имеющее левого поддерева
			if (tree.Left == null && tree.Right != null)
			{
				//Меняем родителя
				tree.Right.Parent = tree.Parent;
				if (tree == tree.Parent.Left)
				{
					tree.Parent.Left = tree.Right;
				}
				else if (tree == tree.Parent.Right)
				{
					tree.Parent.Right = tree.Right;
				}
				return true;
			}

			//Удаляем узел, имеющий поддеревья с обеих сторон
			if (tree.Right != null && tree.Left != null)
			{
				curTree = tree.Right;

				while (curTree.Left != null)
				{
					curTree = curTree.Left;
				}

				//Если самый левый элемент является первым потомком
				if (curTree.Parent == tree)
				{
					curTree.Left = tree.Left;
					tree.Left.Parent = curTree;
					curTree.Parent = tree.Parent;
					if (tree == tree.Parent.Left)
					{
						tree.Parent.Left = curTree;
					}
					else if (tree == tree.Parent.Right)
					{
						tree.Parent.Right = curTree;
					}
					return true;
				}
				//Если самый левый элемент НЕ является первым потомком
				else
				{
					if (curTree.Right != null)
					{
						curTree.Right.Parent = curTree.Parent;
					}
					curTree.Parent.Left = curTree.Right;
					curTree.Right = tree.Right;
					curTree.Left = tree.Left;
					tree.Left.Parent = curTree;
					tree.Right.Parent = curTree;
					curTree.Parent = tree.Parent;
					if (tree == tree.Parent.Left)
					{
						tree.Parent.Left = curTree;
					}
					else if (tree == tree.Parent.Right)
					{
						tree.Parent.Right = curTree;
					}

					return true;
				}
			}
			return false;
		}

		//Перегрузка преобразования в строку
		public override string ToString()
		{
			return Value.ToString();
		}
	}
	public static class BTreePrinter
	{
		class NodeInfo
		{
			public DictionaryBinaryTree<int, string> Node;
			public string Text;
			public int StartPos;
			public int Size { get { return Text.Length; } }
			public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
			public NodeInfo Parent, Left, Right;
		}

		public static void PrintTree(this DictionaryBinaryTree<int, string> root, int topMargin = 2, int leftMargin = 2)
		{
			if (root == null) return;
			int rootTop = Console.CursorTop + topMargin;
			var last = new List<NodeInfo>();
			var next = root;
			for (int level = 0; next != null; level++)
			{
				var item = new NodeInfo { Node = next, Text = next.Key.ToString(" 0 ") };
				if (level < last.Count)
				{
					item.StartPos = last[level].EndPos + 1;
					last[level] = item;
				}
				else
				{
					item.StartPos = leftMargin;
					last.Add(item);
				}
				if (level > 0)
				{
					item.Parent = last[level - 1];
					if (next == item.Parent.Node.Left)
					{
						item.Parent.Left = item;
						item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos);
					}
					else
					{
						item.Parent.Right = item;
						item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos);
					}
				}
				next = next.Left ?? next.Right;
				for (; next == null; item = item.Parent)
				{
					Print(item, rootTop + 2 * level);
					if (--level < 0) break;
					if (item == item.Parent.Left)
					{
						item.Parent.StartPos = item.EndPos;
						next = item.Parent.Node.Right;
					}
					else
					{
						if (item.Parent.Left == null)
							item.Parent.EndPos = item.StartPos;
						else
							item.Parent.StartPos += (item.StartPos - item.Parent.EndPos) / 2;
					}
				}
			}
			Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
		}

		private static void Print(NodeInfo item, int top)
		{
			SwapColors();
			Print(item.Text, top, item.StartPos);
			SwapColors();
			if (item.Left != null)
				PrintLink(top + 1, "┌", "┘", item.Left.StartPos + item.Left.Size / 2, item.StartPos);
			if (item.Right != null)
				PrintLink(top + 1, "└", "┐", item.EndPos - 1, item.Right.StartPos + item.Right.Size / 2);
		}

		private static void PrintLink(int top, string start, string end, int startPos, int endPos)
		{
			Print(start, top, startPos);
			Print("─", top, startPos + 1, endPos);
			Print(end, top, endPos);
		}

		private static void Print(string s, int top, int left, int right = -1)
		{
			Console.SetCursorPosition(left, top);
			if (right < 0) right = left + s.Length;
			while (Console.CursorLeft < right) Console.Write(s);
		}

		private static void SwapColors()
		{
			var color = Console.ForegroundColor;
			Console.ForegroundColor = Console.BackgroundColor;
			Console.BackgroundColor = color;
		}
	}
}
