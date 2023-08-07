namespace ConsoleApp1;

class Task_2
{
    static double MaxDiamondCount(double[,] arr)
    {
        int n = Convert.ToInt32(Math.Sqrt(arr.Length));

        double max = 0;

        if (n % 2 == 0)
        {
            max = arr[0, n / 2];

            for (int i = 0; i < n / 2; ++i)
            {
                if (arr[i, i + n / 2] > max)
                {
                    max = arr[i, i + n / 2];
                }
                if (arr[i + n / 2, i] > max)
                {
                    max = arr[i + n / 2, i];
                }
            }
            
            for (int i = 0, j = n / 2 - 1; i < n / 2; i++, j--)
            {
                if (arr[i, j] > max)
                {
                    max = arr[i, j];
                }
            }
            
            for (int i = n / 2, j = n - 1; i < n; i++, j--)
            {
                if (arr[i, j] > max)
                {
                    max = arr[i, j];
                }
            }
        }
        else
        {
            max = arr[0, n / 2];

            for (int i = 0; i < n / 2 + 1; ++i)
            {
                if (arr[i, i + n / 2] > max)
                {
                    max = arr[i, i + n / 2];
                }
                if (arr[i + n / 2, i] > max)
                {
                    max = arr[i + n / 2, i];
                }
            }
            
            for (int i = 0, j = n / 2; i < n / 2 + 1; i++, j--)
            {
                if (arr[i, j] > max)
                {
                    max = arr[i, j];
                }
            }
            
            for (int i = n / 2, j = n - 1; i < n; i++, j--)
            {
                if (arr[i, j] > max)
                {
                    max = arr[i, j];
                }
            }
        }

        return max;
    }

    static double[,] CreateRandomMatrix(int n)
    {
        var rand = new Random();

        double[,] arr = new double[n, n];

        for (int i = 0; i < n; ++i)
        {
            for (int j = 0; j < n; j++)
            {
                arr[i, j] = rand.Next(101);
            }
        }

        return arr;
    }

    static void PrintMatrix(double[,] arr)
    {
        int lenght = Convert.ToInt32(Math.Sqrt(arr.Length));

        Console.WriteLine("The Matrix:");
        for (int i = 0; i < lenght; ++i)
        {
            for (int j = 0; j < lenght; ++j)
            {
                Console.Write(arr[i, j] + "\t");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
    

    /*public static void Main()
    {
        Console.Write("Enter n: ");
        int n = Convert.ToInt32(Console.ReadLine());

        double[,] arr = CreateRandomMatrix(n);

        double max = MaxDiamondCount(arr);
        
        PrintMatrix(arr);
        
        Console.WriteLine("Here is the maximum: " + max);
    }*/
}