using System.Diagnostics;
using HDF5CSharp;

class MatrixRectangularCalculations
{
    public static int[,] Transpose(int[,] matrix)
    {
        int size = matrix.GetLength(0);
        int[,] result = new int[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                result[j, i] = matrix[i, j];
            }
        }

        return result;
    }

    public static int[,] MultiplyMatrices(int[,] matrix1, int[,] matrix2)
    {
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

        return result;
    }

    public static long SumElements(int[,] matrix)
    {
        int size = matrix.GetLength(0);
        long sum = 0;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                sum += matrix[i, j];
            }
        }

        return sum;
    }
}

class MatrixJaggedCalculations
{
    public static int[][] Transpose(int[][] matrix)
    {
        int size = matrix[0].GetLength(0);
        int[][] result = new int[size][];

        for (int i = 0; i < size; i++)
        {
            result[i] = new int[size];
        }
        
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                result[j][i] = matrix[i][j];
            }
        }

        return result;
    }

    public static int[][] MultiplyMatrices(int[][] matrix1, int[][] matrix2)
    {
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

        return result;
    }

    public static long SumElements(int[][] matrix)
    {
        int size = matrix[0].GetLength(0);
        long sum = 0;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                sum += matrix[i][j];
            }
        }

        return sum;
    }
}

class ThreeDimensionalArrayRectangular
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
        var rand = new Random();

        _ints_rectangular = new int[size, size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    _ints_rectangular[i, j, k] = rand.Next(1, 50);
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

    public int[,] GetMatrix(int index)
    {
        int[,] result = new int[_size, _size];
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                // i j index - 1 - correct implementation
                result[i, j] = _ints_rectangular[i, j, index - 1];
            }
        }

        return result;
    }

    public long InnerProduct(string[] indexes)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        long inner_product;
        int[,] matrixA = GetMatrix(Convert.ToInt32(indexes[0]));
        int[,] matrixB = GetMatrix(Convert.ToInt32(indexes[1]));

        var transposed_matrixA = MatrixRectangularCalculations.Transpose(matrixA);

        var product_matrixA_matrixB = MatrixRectangularCalculations.MultiplyMatrices(transposed_matrixA, matrixB);

        inner_product = MatrixRectangularCalculations.SumElements(product_matrixA_matrixB);

        stopwatch.Stop();
        _timeSpan = stopwatch.Elapsed;

        return inner_product;
    }

    public void PrintElapsedTime()
    {
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            _timeSpan.Seconds, _timeSpan.Milliseconds, _timeSpan.Microseconds,
            _timeSpan.Nanoseconds);
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
    
    public int[][] GetMatrix(int index)
    {
        int[][] result = new int[_size][];
        for (int i = 0; i < _size; i++)
        {
            result[i] = new int[_size];
            for (int j = 0; j < _size; j++)
            {
                // i j index - 1 - correct implementation
                result[i][j] = _ints_jagged[i][j][index - 1];
            }
        }

        return result;
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
    
    public long InnerProduct(string[] indexes)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        long inner_product;
        int[][] matrixA = GetMatrix(Convert.ToInt32(indexes[0]));
        int[][] matrixB = GetMatrix(Convert.ToInt32(indexes[1]));
        
        var transposed_matrixA = MatrixJaggedCalculations.Transpose(matrixA);

        var product_matrixA_matrixB = MatrixJaggedCalculations.MultiplyMatrices(transposed_matrixA, matrixB);

        inner_product = MatrixJaggedCalculations.SumElements(product_matrixA_matrixB);

        stopwatch.Stop();
        _timeSpan = stopwatch.Elapsed;

        return inner_product;
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

        var ThreeDimensionalArrayClass = new ThreeDimensionalArrayRectangular(size);

        long fileId = Hdf5.CreateFile("ThreeDimensionalArrayRectangularFile.H5");

        Hdf5.WriteObject(fileId, ThreeDimensionalArrayClass, "ThreeDimensionalArrayRectangularObject");

        ThreeDimensionalArrayRectangular readObject = new ThreeDimensionalArrayRectangular();

        readObject = Hdf5.ReadObject(fileId, readObject, "ThreeDimensionalArrayRectangularObject");

        Hdf5.CloseFile(fileId);

        //readObject.PrintArray();

        Console.Write("Please enter two numbers of matrices from three dimensional array for inner product: ");
        var indexes = Console.ReadLine()!.Split(" ");
        
        Console.WriteLine("Inner product of rectangular array: " + readObject.InnerProduct(indexes));
        readObject.PrintElapsedTime();

        var ThreeDimensionalArrayClass1 = new ThreeDimensionalArrayJagged();
        ThreeDimensionalArrayClass1.Ints_Jagged = readObject.CreateJaggedArrayFromRectungular();
        ThreeDimensionalArrayClass1.Size = readObject.Size;
        
        Console.WriteLine("Inner product of jagged array: " + ThreeDimensionalArrayClass1.InnerProduct(indexes));
        ThreeDimensionalArrayClass1.PrintElapsedTime();
    }
    
    /*static void Main()
    {
        Task_3 task3 = new Task_3();
        task3.Demo();
    }*/
}