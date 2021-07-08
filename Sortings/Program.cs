using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sortings
{
    class Program
    {
        public static void Main()
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("length=: ");
            int length = Convert.ToInt32(Console.ReadLine());
            int[] array = new int[length];
            Functions.FillInArray(ref array, -array.Length, array.Length);
            Console.WriteLine("Unsorted array is: ");
            Functions.WriteArray(ref array);

            Console.WriteLine("1 - SelectionSort\n" +
                "2 - QuickSort\n" +
                "3 - MergeSort\n");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    {
                        sw.Start();
                        Sortings.SelectionSort(ref array);
                        break;
                    }
                case 2:
                    {
                        sw.Start();
                        Sortings.QuickSort(ref array);
                        break;
                    }
                case 3:
                    {
                        sw.Start();
                        Sortings.MergeSort(ref array);
                        break;
                    }

                default: break;
            }
            sw.Stop();
            Console.WriteLine("Sorted array is: ");
            Functions.WriteArray(ref array);
            Console.WriteLine("Sorted time: {0} ", (sw.ElapsedMilliseconds / 1000.0).ToString());
            sw.Reset(); 
        }
    }
    static class Functions 
    {
        private static Random rnd = new Random();
        public static void FillInArray(ref int[] array, int from, int to) 
        {
            for (int i = 0, length = array.Length; i < length; i++)
            {
                array[i] = rnd.Next(from, to);
            }
        }
        public static void WriteArray(ref int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
            Console.WriteLine("\n");
        }
        public static void Swap(ref int x, ref int y)
        {
            var t = x;
            x = y;
            y = t;
        }
    }
    static class Sortings
    {
        public static void SelectionSort(ref int[] array)
        {
            int temp, smallest, length = array.Length;
            for (int i = 0; i < length - 1; i++)
            {
                smallest = i;
                for (int j = i + 1; j < length; j++)
                {
                    if (array[j] < array[smallest])
                    {
                        smallest = j;
                    }
                }
                temp = array[smallest];
                array[smallest] = array[i];
                array[i] = temp;
            }
        }
        public static int[] QuickSort(ref int[] array)
        {
            return QuickSort(array, 0, array.Length - 1);
        }
        public static int[] MergeSort(ref int[] array)
        {
            return MergeSort(array, 0, array.Length - 1);
        }

        //quick
        private static int Partition(int[] array, int minIndex, int maxIndex)
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (array[i] < array[maxIndex])
                {
                    pivot++;
                    Functions.Swap(ref array[pivot], ref array[i]);
                }
            }

            pivot++;
            Functions.Swap(ref array[pivot], ref array[maxIndex]);
            return pivot;
        }
        private static int[] QuickSort(int[] array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return array;
            }

            var pivotIndex = Partition(array, minIndex, maxIndex);
            QuickSort(array, minIndex, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, maxIndex);

            return array;
        }

        //merge
        private static void Merge(int[] array, int lowIndex, int middleIndex, int highIndex)
        {
            var left = lowIndex;
            var right = middleIndex + 1;
            var tempArray = new int[highIndex - lowIndex + 1];
            var index = 0;
            while ((left <= middleIndex) && (right <= highIndex)) {
                if (array[left] < array[right]) {
                    tempArray[index] = array[left];
                    left++;
                }
                else {
                    tempArray[index] = array[right];
                    right++;
                }
                index++;
            }
            for (var i = left; i <= middleIndex; i++) {
                tempArray[index] = array[i];
                index++;
            }
            for (var i = right; i <= highIndex; i++) {
                tempArray[index] = array[i];
                index++;
            }
            for (var i = 0; i < tempArray.Length; i++) {
                array[lowIndex + i] = tempArray[i];
            }
        }
        private static int[] MergeSort(int[] array, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex) {
                var middleIndex = (lowIndex + highIndex) / 2;
                MergeSort(array, lowIndex, middleIndex);
                MergeSort(array, middleIndex + 1, highIndex);
                Merge(array, lowIndex, middleIndex, highIndex);
            }
            return array;
        }

    }
}
