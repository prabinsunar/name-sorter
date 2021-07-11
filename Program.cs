using System;
using System.IO;

namespace NameSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            //Getting filename with directory from user
            Console.Write("Enter the file directory you want to sort: ");
            string userInput = Console.ReadLine();

            string[] arr = ReadFromFile(userInput);

            string[] result = QuickSort(arr, 0, arr.Length);
                       
            WriteToFile(result);

            foreach(string element in result)
            {
              Console.WriteLine($"{element} ");
            }
            Console.ReadLine();
        }

        //This method accepts a user input for a file directory and returns a array containing all the string contained in that file
        public static string[] ReadFromFile(string input)
        {
            string filePath = @$"{input}";
            string[] arr = { };
            try { 
                arr = File.ReadAllLines(filePath);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            return arr;
        }

        //This method is for writing collection of name to a new file with a specified path 
        public static void WriteToFile( string[] names)
        {
            string newFilePath = AppDomain.CurrentDomain.BaseDirectory + "sorted-names-list.txt"; //Getting the working directory and adding a new file with a specified name
            try
            {
                File.WriteAllLines(newFilePath, names);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        //A quicksort algorithm for comapring strings 
        public static string[] QuickSort(string[] arr, int start, int end)
        {
            if(start< end)
            {
                int pivotIndex = Partition(arr, start, end);
                QuickSort(arr, start, pivotIndex);
                QuickSort(arr, pivotIndex + 1, end);

            }
            return arr;
        }

        //Pivoting to a certain name with CompareTo function
        public static int Partition(string[] arr, int start, int end)
        {
            string pivot = Lastname(arr[start]);
            int swapIndex = start;
            for(int i = start + 1; i< end; i++)
            {
                
                if (Lastname(arr[i]).CompareTo(pivot) == -1)
                {
                    swapIndex++;
                    Swap(arr, i, swapIndex);
                }
            }
            Swap(arr, start, swapIndex);
            return swapIndex;
         
        }

        //Basic swapping method which swaps two elements in an array
        public static void Swap(string[] arr, int indexA, int indexB)
        {
            string temp = arr[indexA];
            arr[indexA] = arr[indexB];
            arr[indexB] = temp;
        }

        //This method gives out the lastname for a given name
        //If the name has a suffix like Jr., then the lastname is attached to that suffix
        public static string Lastname(string fullName)
        {
            string[] suffix = { "i", "ii", "iii", "iv", "v", "vi", "vii", "viii", "ix", "x", "Jr.", "sr" };
            string[] firstAndLast = fullName.Split(' ');
            string lastName = firstAndLast[firstAndLast.Length - 1];
            string penultimate = firstAndLast[firstAndLast.Length - 2];
            foreach(string element in suffix)
            {
                if(lastName == element)
                {
                    lastName = $"{penultimate} {lastName}";
                }
            }

            return lastName;
        }
    }
}
