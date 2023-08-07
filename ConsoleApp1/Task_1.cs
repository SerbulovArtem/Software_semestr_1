namespace ConsoleApp1;

class Task_1
{
    static int CountBits(int n)
    {
        int count = 0;
        if (n >= 0)
        {
            while (n > 0)
            {
                count += n & 1;
                n >>= 1;
            }
        }
        else
        {
            int bits = 32;
            while (n != -1)
            {
                bits--;
                count += n & 1;
                n >>= 1;
            }

            count += bits;
        }

        return count;
    }
    
    /*public static void Main()
    {
        int n = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("From program: " + Task_1.CountBits(n));
        Console.WriteLine("In binary representation: " + Convert.ToString(n, 2));
    }*/
}