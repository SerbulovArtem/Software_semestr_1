using System.Collections;

namespace ConsoleApp1;

public class IntArray : IEnumerable<int>, IEnumerator<int>
{
    private int[] _array;
    private int _count;
    private int _capacity;

    private int _currentIndex;
    
    private Hashtable _hashtable;

    public IntArray() { }
    
    public IntArray(IntArray obj)
    {
        _array = obj._array;
        _count = obj._count;
        _capacity = obj._capacity;

        _currentIndex = -1;
        
        _hashtable = obj._hashtable;
    }
    
    public IntArray(int n, int rand_min, int rand_max)
    {
        _capacity = 2 * n;
        _array = new int[_capacity];
        _hashtable = new Hashtable();
        
        _count = n;
        for (int i = 0; i < _count;)
        {
            var rand = new Random();
            int num = rand.Next(rand_min, rand_max);
        
            if (!_hashtable.Contains(num))
            {
                _hashtable.Add(num, num);
                _array[i] = num;
                ++i;
            }
            else
            {
                _count--;
            }
        }
        _currentIndex = -1;
    }
    
    public int this[int index]
    {
        get { return _array[index]; }
    }

    public int Count()
    {
        return _count;
    }

    public static IntArray operator +(IntArray array, int number)
    {
        IntArray resultIntArray = new IntArray(array);

        if (!resultIntArray._hashtable.Contains(number))
        {
            resultIntArray._hashtable.Add(number, number);
            if (resultIntArray._count == resultIntArray._capacity)
            {
                resultIntArray._capacity *= 2;
                int[] newArray = new int[resultIntArray._capacity];

                for (int i = 0; i < resultIntArray._count; ++i)
                {
                    newArray[i] = array._array[i];
                }

                newArray[resultIntArray._count] = number;

                resultIntArray._array = newArray;
                resultIntArray._count++;
            }
            else
            {
                resultIntArray._array[resultIntArray._count] = number;
                resultIntArray._count++;
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
        if (_currentIndex < _count) return true;
        Reset();
        return false;
    }

    public void Reset()
    {
        _currentIndex = -1;
    }

    int IEnumerator<int>.Current => _array[_currentIndex];

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
    
    public void Demo()
    {
        IntArray intArray = new IntArray(1, 3, 10);
        
        intArray.Print();
        
        intArray = intArray + 1;
        
        intArray.Print();
        
        intArray = intArray + 2;
        
        intArray.Print();
        
        foreach (var elem in intArray)
        {
            Console.Write(elem + " ");
        }
        Console.WriteLine();
        
        foreach (var elem in intArray)
        {
            Console.Write(elem + " ");
        }
        Console.WriteLine();
    }
}

public class Program6
{
    /*static void Main()
    {
        IntArray intArray = new IntArray();
        intArray.Demo();
    }*/
}