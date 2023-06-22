using System;
using System.Collections.Generic;

namespace Train1
{
    static class ArrayManipulator
    {
        public static int[] Manipulate(int[] array)
        {
            int place = 0;
            int[] resultArray = new int[array.Length];
            for (int i = 0; i< array.Length; i++)
            {
                place = (i+4)%(array.Length);
                resultArray[place] = array[i];
            }
            return RepaceMinAndMax(resultArray);
        }

        public static int[] ParseArray(string text) 
        {
            string[] array = text.Split(' ');
            List<int> result = new List<int>();
            int currentNumber = 0;
            for(int i = 0; i < array.Length; i++)
            {
                try
                {
                    currentNumber = Int32.Parse(array[i]);
                    result.Add(currentNumber);
                }
                catch
                {
                    Console.WriteLine($"Unable to parse '{currentNumber}'");
                }
                
            }
            return result.ToArray();
        }

        public static int[] RepaceMinAndMax(int[] array)
        {
            int min = array[0];
            int minIndex = 0;
            int maxIndex = 0;
            int max = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                    maxIndex = i;
                }
                if (array[i] < min)
                {
                    min = array[i];
                    minIndex = i;
                }
            }
            array[minIndex] = max;
            array[maxIndex] = min;
            return array;
            
        }
    }
}
