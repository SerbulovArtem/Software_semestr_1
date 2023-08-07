namespace ConsoleApp1;
using System;
using System.IO;

public class Program
{
    public static void Main(string[] args)
    {
        // Redirect standard input to read from Console.OpenStandardInput()
        Console.SetIn(new StreamReader(Console.OpenStandardInput()));

        // Redirect standard output to write to Console.OpenStandardOutput()
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });

        string line;
        
        do
        {
            // Read input from standard input
            line = Console.ReadLine();

            switch (line)
            {
                case "chown --help":
                    Console.WriteLine("man chown invoked");
                    break;
                
                case "man chown":
                    Console.WriteLine("man chown invoked");
                    break;
                
                case "chown":
                    Console.WriteLine("man chown invoked");
                    break;
            }
        } while (line != "exit");
    }
}