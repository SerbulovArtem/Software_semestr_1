using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp1;

using System;
using System.Collections.Generic;
using System.Linq;

// not-oriented graph
public class Graph : IDisposable
{
    private bool _disposed;

    private int[,] _incidenceMatrix;
    private int _numEdges;
    private int _numVertices;
    
    public Graph(int[,] incidenceMatrix)
    {
        _incidenceMatrix = incidenceMatrix;
        _numVertices = incidenceMatrix.GetLength(0);
        _numEdges = incidenceMatrix.GetLength(1);
    }
    
    public Graph(int numVertices, int numEdges)
    {
        _numVertices = numVertices;
        _numEdges = numEdges;
        _incidenceMatrix = new int[numVertices, numEdges];
    }

    public void AddEdge(int edgeIndex, int vertex1Index, int vertex2Index)
    {
        _incidenceMatrix[vertex1Index, edgeIndex] = 1;
        _incidenceMatrix[vertex2Index, edgeIndex] = 1;
    }
    
    public void AddEdge()
    {
        int[,] newIncidenceMatrix = new int[_numVertices, _numEdges + 1];
        for (int i = 0; i < _numVertices; i++)
        {
            for (int j = 0; j < _numEdges; j++)
            {
                newIncidenceMatrix[i, j] = _incidenceMatrix[i, j];
            }
        }
        _incidenceMatrix = newIncidenceMatrix;
        _numEdges++;
    }
    
    // Fix subgraph
    public Graph Subgraph(int startEdgeIndex, int endEdgeIndex)
    {
        List<int> tempVertexIndexes = new List<int>();
        List<int> EdgesIndexes = new List<int>();
        int vertex1Index = 0, vertex2Index = 0, count = 0;

        for (int i = 0; i < _numEdges; ++i)
        {
            if (i < startEdgeIndex || i > endEdgeIndex)
            {
                EdgesIndexes.Add(i);
            }
        }

        for (int i = startEdgeIndex; i <= endEdgeIndex; ++i)
        {
            for (int j = 0; j < _numVertices; j++)
            {
                if (_incidenceMatrix[j, i] == 1 && count == 0)
                {
                    vertex1Index = j;
                    count++;
                }
                else if (_incidenceMatrix[j, i] == 1 && count == 1)
                {
                    vertex2Index = j;
                    count = 0;
                    tempVertexIndexes.Add(vertex1Index);
                    tempVertexIndexes.Add(vertex2Index);
                    break;
                }
            }
        }

        HashSet<int> VertexIndexes = new HashSet<int>();
        foreach (var vertices in tempVertexIndexes)
        {
            VertexIndexes.Add(vertices);
        }

        int subgraphNumVertices = VertexIndexes.Count;
        int subgraphNumEdges = endEdgeIndex - startEdgeIndex + 1;

        // Deleting of rows and columns

        int[,] matrix = _incidenceMatrix;

        tempVertexIndexes.Clear();
        for (int i = 0; i < _numVertices; ++i)
        {
            if (!VertexIndexes.Contains(i))
            {
                tempVertexIndexes.Add(i);
            }
        }
        
        int[] rowsToDelete = tempVertexIndexes.ToArray(); // Indices of rows to delete (0-based)
        int[] colsToDelete = EdgesIndexes.ToArray();;    // Indices of columns to delete (1-based)
        
        // Create a new matrix with the deleted rows and columns
        int[,] newMatrix = new int[matrix.GetLength(0) - rowsToDelete.Length, matrix.GetLength(1) - colsToDelete.Length];
        for (int i = 0, j = 0; i < matrix.GetLength(0); i++)
        {
            if (!rowsToDelete.Contains(i))
            {
                for (int k = 0, l = 0; k < matrix.GetLength(1); k++)
                {
                    if (!colsToDelete.Contains(k))
                    {
                        newMatrix[j, l] = matrix[i, k];
                        l++;
                    }
                }
                j++;
            }
        }
        
        matrix = newMatrix;
        
        Graph subgraph = new Graph(matrix);
        
        return subgraph;
    }
    
    public void Print()
    {
        for (int i = 0; i < _numVertices; ++i)
        {
            Console.Write("Vertex " + i + ": ");
            for (int j = 0; j < _numEdges; j++)
            {
                Console.Write(_incidenceMatrix[i, j] + " ");
            }

            Console.WriteLine();
        }
    }

    public static Graph operator +(Graph graph, (int vertex1Index, int vertex2Index) vertexIndexes)
    {
        bool flag = true;
        for (int i = 0; i < graph._numEdges; i++)
        {
            if (graph._incidenceMatrix[vertexIndexes.vertex1Index, i] == 1 &&
                graph._incidenceMatrix[vertexIndexes.vertex2Index, i] == 1)
                flag = false;
        }

        if (flag)
        {
            Graph newGraph = new Graph(graph._numVertices, graph._numEdges);
            newGraph._incidenceMatrix = graph._incidenceMatrix;
            newGraph.AddEdge();
            newGraph.AddEdge(newGraph._numEdges - 1, vertexIndexes.vertex1Index,
                vertexIndexes.vertex2Index);
            return newGraph;
        }
        return graph;
    }

    public static Graph operator -(Graph graph, int edgeIndex)
    {
        int[,] newMatrix = new int[graph._numVertices, graph._numEdges - 1];
        for (int i = 0; i < graph._numVertices; i++)
        {
            for (int k = 0, l = 0; k < graph._numEdges; k++)
            {
                if (k != edgeIndex)
                {
                    newMatrix[i, l] = graph._incidenceMatrix[i, k];
                    l++;
                }
            }
        }

        Graph newGraph = new Graph(newMatrix);

        return newGraph;
    }

    public static Graph operator +(Graph graph1, Graph graph2)
    {
        int numVertices = graph1._numVertices + graph2._numVertices;
        int numEdges = graph1._numEdges + graph2._numEdges;

        int[,] matrix1 = graph1._incidenceMatrix;
        int[,] matrix2 = graph2._incidenceMatrix;
        int[,] Newmatrix = new int[numVertices, numEdges];

        for (int i = 0; i < graph1._numVertices; i++)
        {
            for (int j = 0; j < graph1._numEdges; j++)
            {
                Newmatrix[i, j] = matrix1[i, j];
            }
        }
        
        for (int i = graph1._numVertices, k = 0; i < numVertices; i++, k++)
        {
            for (int j = graph1._numEdges, l = 0; j < numEdges; j++, l++)
            {
                Newmatrix[i, j] = matrix2[k, l];
            }
        }
        
        Graph newGraph = new Graph(Newmatrix);
        
        return newGraph;
    }

    public static Graph operator -(Graph graph1, Graph graph2)
    {
        Graph newGraph = graph1.Subgraph(graph1._numEdges - graph2._numEdges, graph1._numEdges - 1);
        
        return newGraph;
    }

    public static int[] GetEdge(int[,] incidenceMatrix, int edgeIndex)
    {
        List<int> edge = new List<int>();
        for (int i = 0; i < incidenceMatrix.GetLength(0); i++)
        {
            if (incidenceMatrix[i, edgeIndex] == 1)
            {
                edge.Add(i);
            }
        }

        return edge.ToArray();
    }

    
    // BFS algorithm
    public List<int> ShortestPath(int startVertexIndex, int endVertexIndex)
    {
        int numVertices = _numVertices;
        int numEdges = _numEdges;
        int[] distances = Enumerable.Repeat(Int32.MaxValue, numVertices).ToArray();
        distances[startVertexIndex] = 0;
        Queue<int> vertexQueue = new Queue<int>();
        vertexQueue.Enqueue(startVertexIndex);
        int[] predecessors = new int[numVertices];
        Array.Fill(predecessors, -1);
        int currentVertexIndex;
        while (vertexQueue.Count > 0)
        {
            currentVertexIndex = vertexQueue.Dequeue();
            for (int i = 0; i < numEdges; i++)
            {
                if (_incidenceMatrix[currentVertexIndex, i] == 1)
                {
                    int[] edgeVertices = GetEdge(_incidenceMatrix, i);
                    int adjacentVertexIndex =
                        (edgeVertices[0] == currentVertexIndex) ? edgeVertices[1] : edgeVertices[0];
                    int newDistance = distances[currentVertexIndex] + 1;
                    if (newDistance < distances[adjacentVertexIndex])
                    {
                        distances[adjacentVertexIndex] = newDistance;
                        predecessors[adjacentVertexIndex] = currentVertexIndex;
                        vertexQueue.Enqueue(adjacentVertexIndex);
                    }
                }
            }
        }

        List<int> path = new List<int>();
        currentVertexIndex = endVertexIndex;
        while (currentVertexIndex != startVertexIndex)
        {
            path.Insert(0, currentVertexIndex);
            currentVertexIndex = predecessors[currentVertexIndex];
            if (currentVertexIndex == -1)
            {
                return null;
            }
        }

        path.Insert(0, startVertexIndex);
        return path;
    }

    public void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~Graph()
    {
        Dispose(false);
    }
}

public class Task_4
{
    public void Demo()
    {
        Graph graph = new Graph(5, 6);
        graph.AddEdge(0, 1, 0);
        graph.AddEdge(1, 1, 2);
        graph.AddEdge(2, 1, 3);
        graph.AddEdge(3, 0, 4);
        graph.AddEdge(4, 1, 4);
        graph.AddEdge(5, 3, 4);
        Console.WriteLine("Initial graph:");
        graph.Print();
        
        Graph graphUnionEdge = graph + (2, 3);
        Console.WriteLine("Graph union edge:");
        graphUnionEdge.Print();
        
        Graph graphDifferenceEdge = graph - 2;
        Console.WriteLine("Graph difference edge:");
        graphDifferenceEdge.Print();
        
        Graph subgraph = graph.Subgraph(0, 2);
        Console.WriteLine("Subgraph:");
        subgraph.Print();
        
        Graph graphUnion = graph + subgraph;
        Console.WriteLine("Graph union:");
        graphUnion.Print();

        Graph graphDifference = graph - subgraph;
        Console.WriteLine("Graph difference:");
        graphDifference.Print();

        int vertex1 = 2, vertex2 = 4;
        List<int> shortestPath = graph.ShortestPath(vertex1, vertex2);
        Console.WriteLine($"Shortest path from vertex {vertex1} to vertex {vertex2}:");
        foreach (int vertexIndex in shortestPath)
        {
            Console.Write(vertexIndex + " ");
        }

        Console.WriteLine();

        graph.Dispose();
    }

    /*static void Main()
    {
        Task_4 task4 = new Task_4();
        task4.Demo();
    }*/
}