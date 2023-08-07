using System.Collections;

namespace ConsoleApp1.Help.Ivan;

public class IntArray : IEnumerable<int>, IEnumerator<int>
{
    private ArrayList _array;
    private int _currentIndex;

    public IntArray(int n)
    {
        _array = new ArrayList(n);
        _currentIndex = -1;
    }
    
    public IntArray(IntArray obj)
    {
        _array = obj._array;
        _currentIndex = obj._currentIndex;
    }
    
    public IntArray(int n, int rand_min, int rand_max)
    {
        _array = new ArrayList(n);
        
        var rand = new Random();
        int num;
        for (int i = 0; i < n; ++i)
        {
            num = rand.Next(rand_min, rand_max);
            _array.Add(num);
        }

        _currentIndex = -1;
    }
    
    public int this[int index]
    {
        get { return (int)_array[index]!; }
        set { _array[index] = value;  }
    }

    public int Count()
    {
        return _array.Count;
    }

    public static IntArray operator /(IntArray array1, IntArray array2)
    {
        var div_num_min = Math.Min(array1.Count(), array2.Count());
        var div_num_max = Math.Max(array1.Count(), array2.Count());

        IntArray resultIntArray = new IntArray(div_num_max);
        
        for (int i = 0; i < div_num_min; ++i)
        {
            resultIntArray._array.Add(array1[i] % array2[i]);
        }


        if (array1.Count() > array2.Count())
        {
            for (int i = div_num_min; i < div_num_max; ++i)
            {
                resultIntArray._array.Add(array1[i]);
            }
        }
        else if (array1.Count() < array2.Count())
        {
            for (int i = div_num_min; i < div_num_max; ++i)
            {
                resultIntArray._array.Add(array2[i]);
            }
        }

        return resultIntArray;
    }
    
    public IEnumerator<int> GetEnumerator()
    {
        return this;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public bool MoveNext()
    {
        _currentIndex++;
        return _currentIndex < _array.Count;
    }

    public void Reset()
    {
        _currentIndex = -1;
    }

    int IEnumerator<int>.Current => (int)_array[_currentIndex];

    public object Current => _array[_currentIndex];
    
    public void Dispose()
    { }

    public void Print()
    {
        foreach (var elem in _array)
        {
            Console.Write(elem + " ");
        }
        Console.WriteLine();
    }
}

public class Task_5
{
    public void Demo()
    {
        IntArray intArray1 = new IntArray(10, 1, 10);
        IntArray intArray2 = new IntArray(4, 1, 10);
        
        Console.WriteLine("Array1:");
        intArray1.Print();
        Console.WriteLine("Array2:");
        intArray2.Print();

        var DivArray = intArray1 / intArray2;

        Console.WriteLine("DivArray by print function:");
        DivArray.Print();
        
        Console.WriteLine("DivArray by foreach:");
        foreach (var elem in DivArray)
        {
            Console.Write(elem + " ");
        }
        Console.WriteLine();
    }
    
    /*static void Main()
    {
        Task_5 task5 = new Task_5();
        task5.Demo();
    }*/
}