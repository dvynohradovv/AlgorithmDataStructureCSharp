using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


/// <summary>
/// dvinogradov
/// </summary>

namespace LinkedLists
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "";
            DoubleLinkedList<string> dLinkedList = new DoubleLinkedList<string>();
            using (StreamReader sr = new StreamReader("test4.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine();
                    string[] words = s.Split(' ', '.', ',');
                    foreach (string oword in words)
                    {
                        dLinkedList.AddEnd(oword);
                    }
                }
                sr.Close();
            }
            WriteList(dLinkedList);

            FormatV6(dLinkedList);

            WriteList(dLinkedList);
        }
        public static void WriteList<T>(DoubleLinkedList<T> list)
        {
            Console.WriteLine("************************************");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }

        public static void FormatV6(DoubleLinkedList<string> list)
        {
            //Форматируем текст
            DoubleLinked_node<string> tmp_node = list.Last;
            string word = "";
            while (tmp_node != null)
            {
                if (tmp_node.Data.Length == 2)
                {
                    word = tmp_node.Data;
                    tmp_node = tmp_node.Prev;
                    while (tmp_node != null)
                    {
                        if(tmp_node.Data == word)
                        {
                            list.Remove(tmp_node);
                        }
                        tmp_node = tmp_node.Prev;
                    }
                    break;
                }
                else
                {
                    tmp_node = tmp_node.Prev;
                }
            }
        }
    }
}