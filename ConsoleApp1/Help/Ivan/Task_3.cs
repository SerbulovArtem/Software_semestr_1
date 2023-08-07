/*using System.Diagnostics;
using HDF5CSharp;

class TwoDimensionalArrayRectangular
{
    private uint _size;
    private TimeSpan _timeSpan;

    private int[,] _ints_rectangular;
    
    public int[,] Ints_Rectangular
    {
        get { return _ints_rectangular; }
        set { _ints_rectangular = value; }
    }

    public uint Size
    {
        get { return _size; }
        set { _size = value; }
    }
    
    public void PrintArray()
    {
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                // j i - implementation in HDF5
                Console.Write(_ints_rectangular[j, i] + " ");
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
    
    public int[,] MultiplyMatrices(int[,] matrix1, int[,] matrix2)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        
        int size = matrix1.GetLength(0);
        int[,] result = new int[size, size];
        int temp = 0;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                temp = 0;
                for (int k = 0; k < size; k++)
                {
                    temp += matrix1[i, k] * matrix2[k, j];
                }

                result[i, j] = temp;
            }
        }
        
        stopwatch.Stop();
        _timeSpan = stopwatch.Elapsed;

        return result;
    }
    
    public void PrintElapsedTime()
    {
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            _timeSpan.Seconds, _timeSpan.Milliseconds, _timeSpan.Microseconds,
            _timeSpan.Nanoseconds);
        Console.WriteLine("RunTime " + elapsedTime);
    }
}

class TwoDimensionalArrayJagged
{
    private uint _size;
    private int[][] _ints_jagged;
    
    private TimeSpan _timeSpan;
    
    public int[][] Ints_Jagged
    {
        get { return _ints_jagged; }
        set { _ints_jagged = value; }
    }
    
    public uint Size
    {
        get { return _size; }
        set { _size = value; }
    }
    
    public TwoDimensionalArrayJagged()
    {
    }

    public TwoDimensionalArrayJagged(uint size)
    {
        _size = size;
        CreateRandomTwoDimensionalArrayJagged(size);
    }
    
    public void CreateRandomTwoDimensionalArrayJagged(uint size)
    {
        var rand = new Random();

        _ints_jagged = new int[size][];

        for (int i = 0; i < size; i++)
        {
            _ints_jagged[i] = new int[size];
            for (int j = 0; j < size; j++)
            {
                _ints_jagged[i][j] = rand.Next(1, 20);
            }
        }
    }
    
    public int[,] CreateRectungularArrayFromJagged()
    {
        int[,] _ints_rectangular = new int[_size,_size];
        
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                _ints_rectangular[i,j] = _ints_jagged[i][j];
            }
        }

        return _ints_rectangular;
    }
    
    public void PrintArray()
    {
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                // j i - implementation in HDF5
                Console.Write(_ints_jagged[j][i] + " ");
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
    
    public int[][] MultiplyMatrices(int[][] matrix1, int[][] matrix2)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        
        int size = matrix1[0].GetLength(0);
        int[][] result = new int[size][];
        int temp = 0;
        for (int i = 0; i < size; i++)
        {
            result[i] = new int[size];
            for (int j = 0; j < size; j++)
            {
                temp = 0;
                for (int k = 0; k < size; k++)
                {
                    temp += matrix1[i][k] * matrix2[k][j];
                }

                result[i][j] = temp;
            }
        }

        stopwatch.Stop();
        _timeSpan = stopwatch.Elapsed;
        
        return result;
    }
    
    public void PrintElapsedTime()
    {
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            _timeSpan.Seconds, _timeSpan.Milliseconds, _timeSpan.Microseconds,
            _timeSpan.Nanoseconds);
        Console.WriteLine("RunTime " + elapsedTime);
    }
}

class Task_3
{
    public void Demo()
    {
        Console.Write("Enter size: ");
        uint size = Convert.ToUInt32(Console.ReadLine());

        var TwoDimensionalArrayClass = new TwoDimensionalArrayJagged(size);

        long fileId = Hdf5.CreateFile("TwoDimensionalArrayJaggedFile.H5");

        Hdf5.WriteObject(fileId, TwoDimensionalArrayClass, "TwoDimensionalArrayJaggedObject");

        TwoDimensionalArrayJagged readObject = new TwoDimensionalArrayJagged();

        readObject = Hdf5.ReadObject(fileId, readObject, "TwoDimensionalArrayJaggedObject");

        Hdf5.CloseFile(fileId);

        //readObject.PrintArray();
        
        Console.WriteLine("Squared rectangular matrix time: ");
        readObject.MultiplyMatrices(readObject.Ints_Jagged, readObject.Ints_Jagged);
        readObject.PrintElapsedTime();


        var TwoDimensionalArrayClass1 = new TwoDimensionalArrayRectangular();
        TwoDimensionalArrayClass1.Ints_Rectangular = readObject.CreateRectungularArrayFromJagged();
        TwoDimensionalArrayClass1.Size = readObject.Size;
        
        //TwoDimensionalArrayClass1.PrintArray();
        
        Console.WriteLine("Squared jagged matrix time: ");
        TwoDimensionalArrayClass1.MultiplyMatrices(TwoDimensionalArrayClass1.Ints_Rectangular, TwoDimensionalArrayClass1.Ints_Rectangular);
        TwoDimensionalArrayClass1.PrintElapsedTime();
    }
    
    /*static void Main()
    {
        Task_3 task3 = new Task_3();
        task3.Demo();
    }#1#
}*/