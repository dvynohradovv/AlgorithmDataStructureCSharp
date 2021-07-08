using System;

namespace HashTables
{
    class Program
    {
        static void Main(string[] args)
        {
            DictionaryHashTable<int> dictionaryHashTable = new DictionaryHashTable<int>(10);

            dictionaryHashTable.Add("shut", 1);
            dictionaryHashTable.Add("shut", 1);

            dictionaryHashTable.Add("carriage", 2);
            dictionaryHashTable.Add("humor", 3);
            dictionaryHashTable.Add("grape", 4);
            dictionaryHashTable.Add("large", 5);
            dictionaryHashTable.Add("dam", 6);
            dictionaryHashTable.Add("love", 7);
            dictionaryHashTable.Add("floor", 8);
            dictionaryHashTable.Add("prefer", 9);
            dictionaryHashTable.Add("bump", 10);
            dictionaryHashTable.Add("squeeze", 11);

            dictionaryHashTable.displayTable();

            dictionaryHashTable.Delete("shut");
            dictionaryHashTable.Delete("shut");

            dictionaryHashTable.displayTable();

            dictionaryHashTable.Delete("floor");

            dictionaryHashTable.displayTable();
        }
    }
}
