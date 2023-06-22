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
        public static int[] DeletePrime(int[] array)
        {
            int maxIndex, max;
            List<int> result = new List<int>();
            FindMax(array, out maxIndex, out max);
            for (int i = 0; i < maxIndex; i++)
            {
                if (!isPrime(array[i]))
                {
                    result.Add(array[i]);
                }
            }
            for(int i = maxIndex; i< array.Length; i++)
            {
                result.Add(array[i]);
            }
            return result.ToArray();
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
            int minIndex, min;
            FindMin(array, out minIndex, out min);
            int maxIndex, max;
            FindMax(array, out maxIndex, out max);
            array[minIndex] = max;
            array[maxIndex] = min;
            return array;
        }

        private static void FindMax(int[] array, out int maxIndex, out int max)
        {
            maxIndex = 0;
            max = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                    maxIndex = i;
                }

            }
        }
        private static void FindMax(int[] array, out int max)
        {
            max = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }

            }
        }
        private static void FindMin(int[] array, out int minIndex, out int min)
        {
            minIndex = 0;
            min = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < min)
                {
                    min = array[i];
                    minIndex = i;
                }

            }
        }
        private static void FindMin(int[] array, out int min)
        {
            min = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < min)
                {
                    min = array[i];
                }

            }
        }
        public static bool isPrime(int n)
        {
            if (n <= 1)
                return false;

            // Check if n=2 or n=3
            if (n == 2 || n == 3)
                return true;

            // Check whether n is divisible by 2 or 3
            if (n % 2 == 0 || n % 3 == 0)
                return false;

            // Check from 5 to square root of n
            // Iterate i by (i+6)
            for (int i = 5; i <= Math.Sqrt(n); i = i + 6)
                if (n % i == 0 || n % (i + 2) == 0)
                    return false;

            return true;
        }
    }
}

