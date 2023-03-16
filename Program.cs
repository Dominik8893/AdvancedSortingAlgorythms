using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;

class Program
{
    static int[] NumberArray;
    static string[] StringArray;

    static void CombSort<T>(T[] Array) where T : IComparable<T>
    {
        int n = Array.Length;
        float ShrinkFactor = 1.3f;
        bool Sorted = false;

        while(Sorted==false)
        {
            n = (int)Math.Floor(n / ShrinkFactor);

            if(n<=1)
            {
                n = 1;
                Sorted = true;
            }
            int i = 0;
            while(i + n < Array.Length) 
            {
                if (Array[i].CompareTo(Array[i+n]) > 0)
                {
                    (Array[i], Array[i + n]) = (Array[i + n],Array[i]);
                    Sorted= false;
                }

                i++;          
            }
        }

    }

    static void ShellSort<T>(T[] Array) where T : IComparable<T>
    {
        int N = Array.Length;
      
        int Gap = N / 3;

        while (Gap >= 1)
        {
            for (int i = Gap; i < N; i++)
            {
                T Temp = Array[i];
                int j = i;

                while (j >= Gap && (Array[j - Gap].CompareTo(Temp) > 0))
                {
                    Array[j] = Array[j - Gap];
                    j -= Gap;
                }

                Array[j] = Temp;
            }

            Gap /= 3;
        }
    }




    static void GenerateNumbers(int NumberAmount)
    {
        Random rnd = new Random();

        using (StreamWriter Writer = new StreamWriter("RandomNumbers.txt"))
        {
            for (int i = 0; i < NumberAmount; i++)
            {
                Writer.WriteLine(rnd.Next(0, 1000000));
            }
        }
    }

    static void ReadNumbersToArray()
    {
        string[] Lines = File.ReadAllLines("RandomNumbers.txt");

        // Create an array to store the integers
        NumberArray = new int[Lines.Length];

        // Parse each line as an integer and store it in the array
        for (int i = 0; i < Lines.Length; i++)
        {
            NumberArray[i] = int.Parse(Lines[i]);
        }
    }

    static bool CheckArray<T>(T[] Array) where T : IComparable<T>
    {
        T LastIndex = Array[0];
        for (int i = 0; i < Array.Length; i++)
        {
            if (LastIndex.CompareTo(Array[i]) <= 0)
            {
                LastIndex = Array[i];
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    static void ReadStringsToArray(int Amount)
    {
        StringArray = File.ReadAllLines("Words.txt").Take(Amount).ToArray();
    }


    static void Main()
    {
        //Integer Sorting
        Console.WriteLine("\nCombSort");
        //CombSort
        Stopwatch Stopwatch = new Stopwatch();
        int j = 1;
        for (int i = 0; i < 5; i++)
        {

            GenerateNumbers(j * 8000);
            ReadNumbersToArray();
            Stopwatch.Restart();
            CombSort(NumberArray);
            Stopwatch.Stop();

            if (CheckArray(NumberArray) == false)
            {
                Console.WriteLine("Sorted Incorrectly");
            }

            Console.WriteLine("{0} integers Elapsed time: {1}", j * 8000, Stopwatch.Elapsed);
            j *= 2;
        }

        Console.WriteLine("\nShellSort");

        j = 1;
        for (int i = 0; i < 5; i++)
        {

            GenerateNumbers(j * 8000);
            ReadNumbersToArray();
            Stopwatch.Restart();
            ShellSort(NumberArray);
            Stopwatch.Stop();

            if (CheckArray(NumberArray) == false)
            {
                Console.WriteLine("Sorted Incorrectly");

                using (StreamWriter writer = new StreamWriter("output.txt"))
                {
                    foreach (int num in NumberArray)
                    {
                        writer.WriteLine(num);
                    }
                }

                break;
            }

            Console.WriteLine("{0} integers Elapsed time: {1}", j * 8000, Stopwatch.Elapsed);
            j *= 2;
        }
        



        //String Sorting
        Console.WriteLine();
        Console.WriteLine("\nCombSort  Strings");

        for (int i = 0; i < 5; i++)
        {

            ReadStringsToArray((i + 1) * 4000);
            Stopwatch.Restart();

            CombSort(StringArray);
            Stopwatch.Stop();

            if (CheckArray(StringArray) == false)
            {
                Console.WriteLine("Sorted Incorrectly");
            }

            Console.WriteLine("{0} Strings Elapsed time: {1}", (i + 1) * 4000, Stopwatch.Elapsed);
        }

        Console.WriteLine();
        Console.WriteLine("\nShellSort  Strings");

        for (int i = 0; i < 5; i++)
        {

            ReadStringsToArray((i + 1) * 4000);
            Stopwatch.Restart();

            ShellSort(StringArray);
            Stopwatch.Stop();

            if (CheckArray(StringArray) == false)
            {
                Console.WriteLine("Sorted Incorrectly");
            }

            Console.WriteLine("{0} Strings Elapsed time: {1}", (i + 1) * 4000, Stopwatch.Elapsed);
        }
    }
}
