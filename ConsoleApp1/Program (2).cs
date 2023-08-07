/*using System.Diagnostics;
using HDF5CSharp;
using System;
using System.IO;



class ThreeDimensionalArrayRectangularObject
{
    private uint _size;
    private TimeSpan _timeSpan;

    private int[,,] _ints_rectangular;

    public ThreeDimensionalArrayRectangular()
    {
    }

    public ThreeDimensionalArrayRectangular(uint size)
    {
        _size = size;
        CreateRandomThreeDimensionalArrayRectangular(size);
    }

    public int[,,] Ints_Rectangular
    {
        get { return _ints_rectangular; }
        set { _ints_rectangular = value; }
    }

    public uint Size
    {
        get { return _size; }
        set { _size = value; }
    }

    public void CreateRandomThreeDimensionalArrayRectangular(uint size)
    {
        _ints_rectangular = new int[size, size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    _ints_rectangular[i, j, k] = (i*10+j)*10+k;
                }
            }
        }
    }

    public int[][][] CreateJaggedArrayFromRectungular()
    {
        int[][][] _ints_ragged = new int[_size][][];

        for (int i = 0; i < _size; i++)
        {
            _ints_ragged[i] = new int[_size][];
            for (int j = 0; j < _size; j++)
            {
                _ints_ragged[i][j] = new int[_size];
            }
        }

        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                for (int k = 0; k < _size; k++)
                {
                    _ints_ragged[i][j][k] = _ints_rectangular[i, j, k];
                }
            }
        }

        return _ints_ragged;
    }

    public void PrintArray()
    {
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                for (int k = 0; k < _size; k++)
                {
                    // j k i - implementation in HDF5
                    Console.Write(_ints_rectangular[j, k, i] + " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
    

    public long InnerProduct()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        long inner_product = 0;
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                for (int k = 0; k < _size; k++)
                {
                    // j k i - implementation in HDF5
                    inner_product += _ints_rectangular[i, j, k]*_ints_rectangular[i, j, k];
                }
            }
        }

        stopwatch.Stop();
        _timeSpan = stopwatch.Elapsed;

        return inner_product;
    }

    public void PrintElapsedTime()
    {
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00}:{4:00}:{5:00}",
            _timeSpan.Hours, _timeSpan.Minutes, _timeSpan.Seconds,
            _timeSpan.Milliseconds, _timeSpan.Microseconds, _timeSpan.Nanoseconds);
        Console.WriteLine("RunTime " + elapsedTime);
    }
}

class ThreeDimensionalArrayJagged
{
    private uint _size;
    private int[][][] _ints_jagged;
    
    private TimeSpan _timeSpan;
    
    public int[][][] Ints_Jagged
    {
        get { return _ints_jagged; }
        set { _ints_jagged = value; }
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
                for (int k = 0; k < _size; k++)
                {
                    // j k i - implementation in HDF5
                    Console.Write(_ints_jagged[j][k][i] + " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
    
    public long InnerProduct()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        long inner_product = 0;
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                for (int k = 0; k < _size; k++)
                {
                    // j k i - implementation in HDF5
                    inner_product += _ints_jagged[i][j][k]*_ints_jagged[i][j][k];
                }
            }
        }
        
        stopwatch.Stop();
        _timeSpan = stopwatch.Elapsed;

        return inner_product;
    }

    public void PrintElapsedTime()
    {
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00}:{4:00}:{5:00}",
            _timeSpan.Hours, _timeSpan.Minutes, _timeSpan.Seconds,
            _timeSpan.Milliseconds, _timeSpan.Microseconds, _timeSpan.Nanoseconds);
        Console.WriteLine("RunTime " + elapsedTime);
    }
}

class Task_3
{
    public void Demo()
    {
        Console.Write("Enter size: ");
        uint size = Convert.ToUInt32(Console.ReadLine());

        var ThreeDimensionalArrayClass = new ThreeDimensionalArrayRectangular(size);

        long fileId = Hdf5.CreateFile("RectangularFile.H5");

        Hdf5.WriteObject(fileId, ThreeDimensionalArrayClass, "ThreeDimensionalArrayRectangularObject");

        ThreeDimensionalArrayRectangular readObject = new ThreeDimensionalArrayRectangular();

        readObject = Hdf5.ReadObject(fileId, readObject, "ThreeDimensionalArrayRectangularObject");

        Hdf5.CloseFile(fileId);

        //readObject.PrintArray();
        
        
        Console.WriteLine("Inner product of rectangular array: " + readObject.InnerProduct());
        readObject.PrintElapsedTime();

        var ThreeDimensionalArrayClass1 = new ThreeDimensionalArrayJagged();
        ThreeDimensionalArrayClass1.Ints_Jagged = readObject.CreateJaggedArrayFromRectungular();
        ThreeDimensionalArrayClass1.Size = readObject.Size;
        
        Console.WriteLine("Inner product of jagged array: " + ThreeDimensionalArrayClass1.InnerProduct());
        ThreeDimensionalArrayClass1.PrintElapsedTime();
    }
    
    /*static void Main()
    {
        Task_3 task3 = new Task_3();
        task3.Demo();
    }#1#
}*/