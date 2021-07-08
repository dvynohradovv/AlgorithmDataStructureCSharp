using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTrees
{
	class Program
	{
        static void Main(string[] args)
        {
            //DictionaryBinaryTree<int, string> dict = new DictionaryBinaryTree<int, string>(5, "5", null);
            //dict.bstree_add(6, "6");
            //dict.bstree_add(4, "4");

            //dict.bstree_delete(5);

            //BTreePrinter.PrintTree(dict, 2, 2);

            //return 0;


            DictionaryBinaryTree<int, string> dictionary = new DictionaryBinaryTree<int, string>(new (int, string)[] 
            {
                (53, "1"), (13, "2"), (66, "3"), 
                (55, "4"), (63, "5"), (24, "6"), 
                (77, "7"), (82, "8"), (44, "9"), 
                (40, "10"), (18, "11"), (86, "12"), 
                (15, "13"), (68, "14"), (30, "15")
            } );

            BTreePrinter.PrintTree(dictionary, 2, 2);

            dictionary.bstree_lookup(82);
            dictionary.bstree_lookup(40);
            dictionary.bstree_lookup(30);

            dictionary.bstree_max();
            dictionary.bstree_min();

            dictionary.bstree_delete(53);
            dictionary.bstree_delete(66);
            dictionary.bstree_delete(13);

            dictionary.bstree_delete(100);

            BTreePrinter.PrintTree(dictionary, 2, 2);



            //Tree<int> q = new Tree<int>();

            //q.Put("Барсук", 46);
            //q.Put("Ёжик", 26);
            //q.Put("Хомячок", 2);
            //q.Put("Гусеничка", 79);
            //q.Put("Мартышка", 76);
            //q.Put("Конфета", 99);
            //q.Put("Шар", 41);
            //q.Put("Леденец", 37);
            //q.Put("Жук", 51);
            //q.Put("Камень", 6);
            //q.Put("Чайки", 97);
            //q.Put("Песок", 35);
            //q.Put("Море", 93);
            //q.Put("Облако", 10);
            //q.Put("Колпак", 21);
            //// Console.WriteLine("количество элементов={0}", q.Count());
            ////сделать обход дерева,чтобы его вывести!!!!!!!!!
            //Console.WriteLine("слово 1-{0}", q.Find_node(79));
            //Console.WriteLine("слово 2-{0}", q.Find_node(10));
            //Console.WriteLine("слово 3-{0}", q.Find_node(35));
            //Console.WriteLine("минимальный элемент={0}", q.Minimum());
            //q.Tree_preorder();
            //Console.WriteLine("______________");
            //q.Remove(46);
            //q.Remove(26);
            //q.Remove(99);
            //q.Tree_preorder();


        }
	}
}
