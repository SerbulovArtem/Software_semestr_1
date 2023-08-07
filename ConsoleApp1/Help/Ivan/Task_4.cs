namespace ConsoleApp1.Help.Ivan;

class Vector
{
    private int[] _array;

    private int _length;
    private int _capacity;

    private int _currentIndex;
    
    public Vector()
    {
    }

    public Vector(Vector obj)
    {
        _array = obj._array;
        _length = obj._length;
        _capacity = obj._capacity;

        _currentIndex = -1;
    }

    public Vector(int n, int rand_min, int rand_max)
    {
        _capacity = 2 * n;
        _array = new int[_capacity];

        _length = n;
        var rand = new Random();
        int num;
        for (int i = 0; i < _length; i++)
        {
            num = rand.Next(rand_min, rand_max);
            _array[i] = num;
        }

        _currentIndex = -1;
    }
    
    public int Length()
    {
        return _length;
    }

    public int Capacity()
    {
        return _capacity;
    }

    public static Vector operator -(Vector array, int number)
    {
        Vector resultVector = new Vector(array);

        int index = -1;
        for (int i = 0; i < resultVector._length; i++)
        {
            if (resultVector._array[i] == number) index = i;
        }

        if (index != -1)
        {
            if ((resultVector._length - 1) == resultVector._capacity / 2)
            {
                resultVector._capacity /= 2;
                int[] newArray = new int[resultVector._capacity];

                for (int i = 0, j = 0; i < resultVector._length; ++i, ++j)
                {
                    if (i == index)
                    {
                        j--;
                        continue;
                    }
                    newArray[j] = array._array[i];
                }
            
                resultVector._array = newArray;
                resultVector._length--;
            }
            else
            {
                int[] newArray = new int[resultVector._capacity];
                for (int i = 0; i < resultVector._length; ++i)
                {
                    newArray[i] = array._array[i];
                }
            
                resultVector._array = newArray;
                resultVector._length--;
            }
        }
        
        return resultVector;
    }
    
    public static Vector operator +(Vector array, int number)
    {
        Vector resultVector = new Vector(array);

        if (resultVector._length == resultVector._capacity)
        {
            resultVector._capacity *= 2;
            int[] newArray = new int[resultVector._capacity];

            for (int i = 0; i < resultVector._length; ++i)
            {
                newArray[i] = array._array[i];
            }

            newArray[resultVector._length] = number;

            resultVector._array = newArray;
            resultVector._length++;
        }
        else
        {
            resultVector._array[resultVector._length] = number;
            resultVector._length++;
        }

        return resultVector;
    }

    public void Print()
    {
        foreach (var elem in _array)
        {
            Console.Write(elem + " ");
        }

        Console.WriteLine();
    }
}

public class Task_4
{
    public void Demo()
    {
        Vector vector = new Vector(1, 5, 5);

        vector.Print();

        vector = vector + 1;

        vector.Print();

        vector = vector + 2;

        vector.Print();

        vector = vector - 1;
        
        vector.Print();
        
        vector = vector - 5;
        
        vector.Print();
        
        vector = vector + 1;

        vector.Print();

        vector = vector + 2;

        vector.Print();

        Console.WriteLine();
    }

    /*static void Main()
    {
        Task_4 task4 = new Task_4();
        task4.Demo();
    }*/
}