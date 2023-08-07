/*namespace ConsoleApp1;

using System.Text.RegularExpressions;

public class Task_6
{
    static void Main()
    {
        Console.WriteLine("Enter less than 253 characters:");
        string sentence = Console.ReadLine()!;
        while (sentence.Length > 253)
        {
            Console.WriteLine("Enter less than 253 characters:");
            sentence = Console.ReadLine()!;
        }
        
        List<string> words = Regex.Split(sentence, @"\s+").ToList();
        if (words[0].Length == 0)
            words.RemoveAt(0);

        int min_length = words[0].Length;
        int max_length = words[0].Length;
        
        foreach (var word in words)
        {
            if (word.Length > max_length)
                max_length = word.Length;
            if (word.Length < min_length)
                min_length = word.Length;
        }
        
        Console.WriteLine("The longest words:");
        foreach (var word in words)
        {
            if (word.Length == max_length) 
                Console.Write(word + " ");
        }
        Console.WriteLine();
        
        Console.WriteLine("The shortest words:");
        foreach (var word in words)
        {
            if (word.Length == min_length) 
                Console.Write(word + " ");
        }
        Console.WriteLine();
    }
}*/