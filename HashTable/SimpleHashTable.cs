using System;
using System.Collections.Generic;
using System.Text;

namespace HashTables
{
	class SimpleHashTable
	{
		DataItem[] hashArray; // Массив ячеек хеш-таблицы
		int arraySize;
		DataItem nonItem = null; // Для удаленных элементов
		public SimpleHashTable(int size)// Конструктор
		{
			arraySize = size;
			hashArray = new DataItem[arraySize];
		}
		public void displayTable()
		{
			Console.WriteLine("Table: ");
			for (int j = 0; j < arraySize; j++)
			{
				if (hashArray[j] != null)
					Console.WriteLine(hashArray[j].getKey() + " ");
				else
					Console.WriteLine("** ");
			}
			Console.WriteLine("");
		}
		// -------------------------------------------------------------
		public int hashFunc1(int key)
		{
			return key % arraySize;
		}
		public int hashFunc2(int key)
		{
			// Возвращаемое значение отлично от нуля, меньше размера массива,
			// функция отлична от хеш-функции 1
			// Размер массива должен быть простым по отношению к 5, 4, 3 и 2
			return 5 - key % 5;
		}
		/// <summary>
		/// ///////////////////
		/// 
		public int hashFunc3(String key)
		{
			int hashVal = 0;
			int pow27 = 1; // 1, 27, 27*27 и т. д.
			char[] c = key.ToCharArray();
			for (int j = c.Length - 1; j >= 0; j--) // Справа налево
			{
				int letter = Convert.ToInt32(c[j]) - 96; // Получение кода символа
				hashVal += pow27 * letter; // Умножение на степень 27
				pow27 *= 27; // Следующая степень 27
			}
			return hashVal % arraySize;
		}
		public void insert(int key, DataItem item)
		// (Метод предполагает, что таблица не заполнена)
		{
			int hashVal = hashFunc1(key); // Хеширование ключа
			int stepSize = hashFunc2(key); // Вычисление смещения
										   // Пока не будет найдена

			while (hashArray[hashVal] != null && hashArray[hashVal].getKey() != -1)// пустая ячейка или -1
			{
				hashVal += stepSize; // Прибавление смещения
				hashVal %= arraySize; // Возврат к началу
			}
			hashArray[hashVal] = item; // Вставка элемента
		}
		public DataItem delete(int key) // Удаление элемента данных
		{
			int hashVal = hashFunc1(key); // Хеширование ключа
			int stepSize = hashFunc2(key); // Вычисление смещения
			while (hashArray[hashVal] != null) // Пока не найдена пустая ячейка
			{ // Ключ найден?
				if (hashArray[hashVal].getKey() == key)
				{
					DataItem temp = hashArray[hashVal]; // Временное сохранение
					hashArray[hashVal] = nonItem; // Удаление элемента
					return temp; // Метод возвращает элемент
				}
				hashVal += stepSize; // Прибавление смещения
				hashVal %= arraySize; // Возврат к началу
			}
			return null; // Элемент не найден
		}
		public DataItem find(int key) // Поиск элемента с заданным ключом
									  // (Метод предполагает, что таблица не заполнена)
		{
			int hashVal = hashFunc1(key); // Хеширование ключа
			int stepSize = hashFunc2(key); // Вычисление смещения
			while (hashArray[hashVal] != null) // Пока не найдена пустая ячейка
			{ // Ключ найден?
				if (hashArray[hashVal].getKey() == key)
					return hashArray[hashVal]; // Да, метод возвращает элемент
				hashVal += stepSize; // Прибавление смещения
				hashVal %= arraySize; // Возврат к началу
			}
			return null; // Элемент не найден
		}
	}

	class DataItem
	{
		public int iData { get; set; } // Данные (ключ)
									   // public int Value { get; set; }
		public DataItem(int Key)
		{
			this.iData = Key;
		}
		public int getKey()
		{ return iData; }

		/* public DataItem(int Key, int Value)
         {
             this.iData = Key;
             this.Value = Value;
         }*/
		//--------------------------------------------------------------

	}
}
