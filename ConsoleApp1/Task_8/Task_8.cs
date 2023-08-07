namespace ConsoleApp1.Task_8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WikipediaGeneralInfo
{
    public string Topic { get; set; }
    public string Language { get; set; }

    public string Info()
    {
        return ($"Wikipedia topic: {Topic}, language: {Language}.");
    }

    public WikipediaGeneralInfo(string topic = "", string language = "")
    {
        Topic = topic;
        Language = language;
    }

    public override string ToString()
    {
        return ($"{Topic}|{Language}");
    }
}

public class WikipediaReviewInfo
{
    public string ReviewedBy { get; set; }
    
    public string QualityLevel { get; set; }

    public string Info()
    {
        return ($"Reviewed by: {ReviewedBy}, quality level: {QualityLevel}.");
    }

    public WikipediaReviewInfo(string reviewedBy = "", string qualityLevel = "")
    {
        ReviewedBy = reviewedBy;
        QualityLevel = qualityLevel;
    }

    public override string ToString()
    {
        return ($"{ReviewedBy}|{QualityLevel}");
    }
}

public class WikipediaWebsite
{
    public WikipediaGeneralInfo wikipediaGeneralInfo;
    public WikipediaReviewInfo wikipediaReviewInfo;

    public string Info()
    {
        return ($"Wikipedia Website\n Wikipedia topic: {wikipediaGeneralInfo.Topic}, " +
                $"language: {wikipediaGeneralInfo.Language}, " +
                $"reviewed by: {wikipediaReviewInfo.ReviewedBy}, quality level: {wikipediaReviewInfo.QualityLevel}\n");
    }

    public WikipediaWebsite(WikipediaGeneralInfo wikipediaGeneralInfo, WikipediaReviewInfo wikipediaReviewInfo)
    {
        this.wikipediaGeneralInfo = new WikipediaGeneralInfo(wikipediaGeneralInfo.Topic, wikipediaGeneralInfo.Language);
        this.wikipediaReviewInfo =  new WikipediaReviewInfo(wikipediaReviewInfo.ReviewedBy, wikipediaReviewInfo.QualityLevel);
    }

    public override string ToString()
    {
        return ("<" + wikipediaGeneralInfo + "^" + wikipediaReviewInfo + ">");
    }
}

public abstract class AbstractFile
{
    protected string filePath;
    public List<WikipediaWebsite> DataWikipediaWebsites { get; } = new List<WikipediaWebsite>();

    public void AddWebsite(WikipediaWebsite item)
    {
        DataWikipediaWebsites.Add(item);
        WriteToFile();
    }

    public void RemoveWebsite(short ind)
    {
        DataWikipediaWebsites.RemoveAt(ind);
        WriteToFile();
    }

    protected abstract void WriteToFile();
    protected abstract void ReadFromFile();
}

public class TxtFile : AbstractFile
{
    string path = @"E:/Software_4_semester/ConsoleApp1/Task_8/Text.txt";

    public TxtFile()
    {
        ReadFromFile();
    }

    ~TxtFile()
    {
        WriteToFile();
    }

    protected override void ReadFromFile()
    {
        StreamReader reader = new StreamReader(path);

        List<string> objects = reader.ReadToEnd().Split(new char[] { '<', '>' }, StringSplitOptions.RemoveEmptyEntries)
            .ToList();

        reader.Close();
        ConvertToWikipediaWebsites(objects);
    }

    public void ConvertToWikipediaWebsites(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            string[] temp = list[i].Split('^');
            string[] temp1 = temp[0].Split('|');
            string[] temp2 = temp[1].Split('|');
            string topic = temp1[0];
            string language = temp1[1];
            string reviwedBy = temp2[0];
            string qualityLevel = temp2[1];

            AddWebsite(new WikipediaWebsite(new WikipediaGeneralInfo(topic, language),
                new WikipediaReviewInfo(reviwedBy, qualityLevel)));
        }
    }

    protected override void WriteToFile()
    {
        StreamWriter sw = new StreamWriter(path, false);
        {
            for (int i = 0; i < DataWikipediaWebsites.Count; i++)
            {
                string str = DataWikipediaWebsites[i].ToString();
                sw.Write(str);
            }
        }
        sw.Close();
    }

    public void Refresh()
    {
        DataWikipediaWebsites.Clear();
        ReadFromFile();
    }

    public void Rewrite()
    {
        WriteToFile();
    }
}

public class BinFile : AbstractFile
{
    string path = @"E:/Software_4_semester/ConsoleApp1/Task_8/Bin.dat";

    public BinFile()
    {
        ReadFromFile();
    }

    ~BinFile()
    {
        WriteToFile();
    }

    protected override void ReadFromFile()
    {
        BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open));
        
        while (reader.BaseStream.Position != reader.BaseStream.Length)
        {

            string topic = reader.ReadString();
            string language = reader.ReadString();
            string reviwedBy = reader.ReadString();
            string qualityLevel = reader.ReadString();

            AddWebsite(new WikipediaWebsite(new WikipediaGeneralInfo(topic, language),
                new WikipediaReviewInfo(reviwedBy, qualityLevel)));
        }
        reader.Close();
    }

    public void ConverttoObj(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            string[] temp = list[i].Split('^');
            string[] temp1 = temp[0].Split('|');
            string[] temp2 = temp[1].Split('|');
            string topic = temp1[0];
            string language = temp1[1];
            string reviwedBy = temp2[0];
            string qualityLevel = temp2[1];

            AddWebsite(new WikipediaWebsite(new WikipediaGeneralInfo(topic, language),
                new WikipediaReviewInfo(reviwedBy, qualityLevel)));
        }
    }

    protected override void WriteToFile()
    {
        BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate));

        {
            for (int i = 0; i < DataWikipediaWebsites.Count; i++)
            {
                string str = DataWikipediaWebsites[i].ToString();
                writer.Write(DataWikipediaWebsites[i].wikipediaGeneralInfo.Topic);
                writer.Write(DataWikipediaWebsites[i].wikipediaGeneralInfo.Language);
                writer.Write(DataWikipediaWebsites[i].wikipediaReviewInfo.ReviewedBy);
                writer.Write(DataWikipediaWebsites[i].wikipediaReviewInfo.QualityLevel);
            }
        }

        writer.Close();
    }

    public void Refresh()
    {
        DataWikipediaWebsites.Clear();
        ReadFromFile();
    }

    public void Rewrite()
    {
        WriteToFile();
    }
}

class Program
{
    static void Main(string[] args)
    {
        bool menu = false;
        Console.WriteLine("Choose type of file");
        Console.WriteLine("1.TxT\n2.Binary");
        int o = Convert.ToInt32(Console.ReadLine());
        if (o == 1)
        {
            TxtFile factoryRepository = new TxtFile();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("\t\t\t\t\t\t\tSelect an action:");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("\t\t\t0-to exit!");
                Console.WriteLine("\t\t\t1-to see the websites");
                Console.WriteLine("\t\t\t2-to recreate the websites");

                Console.Write("input your number:");

                int InputUser;
                InputUser = int.Parse(Console.ReadLine());

                switch (InputUser)
                {
                    case 0:
                        menu = true;
                        break;
                    case 1:
                        for (int i = 0; i < factoryRepository.DataWikipediaWebsites.Count; i++)
                        {
                            Console.Write(factoryRepository.DataWikipediaWebsites[i].Info());
                        }

                        bool isBool = false;
                        while (isBool == false)
                        {
                            Console.WriteLine();
                            Console.WriteLine("\t\t\t1-add the website");
                            Console.WriteLine("\t\t\t2-sort the websites");
                            Console.WriteLine("\t\t\t0-exit");

                            int Input;
                            Console.Write("input your number:");
                            Input = int.Parse(Console.ReadLine());
                            if (Input == 1)
                            {
                                Console.WriteLine("input the website info");
                                string topic;
                                string language;
                                string reviewedBy;
                                string qualityLevel;
                                Console.Write("Topic: ");
                                topic = Console.ReadLine();
                                Console.Write("Language: ");
                                language = Console.ReadLine();
                                Console.Write("Reviewed by: ");
                                reviewedBy = Console.ReadLine();
                                Console.Write("Quality level: ");
                                qualityLevel = Console.ReadLine();
                                factoryRepository.AddWebsite(new WikipediaWebsite(new WikipediaGeneralInfo(topic, language),
                                    new WikipediaReviewInfo(reviewedBy, qualityLevel)));
                                factoryRepository.Rewrite();
                                factoryRepository.Refresh();
                            }

                            else if (Input == 2)
                            {
                                List<WikipediaWebsite> sortedOne = factoryRepository.DataWikipediaWebsites
                                    .OrderBy(wikipediaWebsite => wikipediaWebsite.wikipediaGeneralInfo.Topic).ToList();
                                for (int i = 0; i < sortedOne.Count; i++)
                                {
                                    Console.WriteLine(sortedOne[i].Info());
                                }
                            }
                            else if (Input == 0)
                            {
                                isBool = true;
                                break;
                            }
                        }

                        break;
                    case 2:
                        int k = 0;
                        for (int i = 0; i < factoryRepository.DataWikipediaWebsites.Count; i++)
                        {
                            Console.WriteLine(factoryRepository.DataWikipediaWebsites[i].Info());
                        }

                        int indexf;
                        indexf = int.Parse(Console.ReadLine());
                        string topic1;
                        topic1 = Console.ReadLine();
                        string language1;
                        language1 = Console.ReadLine();
                        string reviewedBy1;
                        reviewedBy1 = Console.ReadLine();
                        string qualityLevel1;
                        qualityLevel1 = Console.ReadLine();
                        for (int i = 0; i < factoryRepository.DataWikipediaWebsites.Count; i++)
                        {
                            if (indexf == i)
                            {
                                factoryRepository.DataWikipediaWebsites[i].wikipediaGeneralInfo.Topic = topic1;
                                factoryRepository.DataWikipediaWebsites[i].wikipediaGeneralInfo.Language = language1;
                                factoryRepository.DataWikipediaWebsites[i].wikipediaReviewInfo.ReviewedBy = reviewedBy1;
                                factoryRepository.DataWikipediaWebsites[i].wikipediaReviewInfo.QualityLevel = qualityLevel1;
                                k++;
                                break;
                            }
                        }

                        if (k == 3)
                        {
                            factoryRepository.DataWikipediaWebsites.RemoveAt(indexf);
                        }

                        factoryRepository.Rewrite();
                        factoryRepository.Refresh();

                        break;
                    default:
                        break;
                }

                if (menu)
                    break;
            }
        }
        else
        {
            BinFile factoryRepository = new BinFile();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("\t\t\t\t\t\t\tSelect an action:");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("\t\t\t0-to exit!");
                Console.WriteLine("\t\t\t1-to see the rides");
                Console.WriteLine("\t\t\t2-to recreate the rides");

                Console.Write("input your number:");

                int InputUser;
                InputUser = int.Parse(Console.ReadLine());

                switch (InputUser)
                {
                    case 0:
                        menu = true;
                        break;
                    case 1:
                        for (int i = 0; i < factoryRepository.DataWikipediaWebsites.Count; i++)
                        {
                            Console.Write(factoryRepository.DataWikipediaWebsites[i].Info());
                        }

                        bool isBool = false;
                        while (isBool == false)
                        {
                            Console.WriteLine();
                            Console.WriteLine("\t\t\t1-add the ride");
                            Console.WriteLine("\t\t\t2-sort the rides");
                            Console.WriteLine("\t\t\t0-exit");

                            int Input;
                            Console.Write("input your number:");
                            Input = int.Parse(Console.ReadLine());
                            if (Input == 1)
                            {
                                Console.WriteLine("input the website info");
                                string topic;
                                string language;
                                string reviewedBy;
                                string qualityLevel;
                                Console.Write("Topic: ");
                                topic = Console.ReadLine();
                                Console.Write("Language: ");
                                language = Console.ReadLine();
                                Console.Write("Reviewed by: ");
                                reviewedBy = Console.ReadLine();
                                Console.Write("Quality level: ");
                                qualityLevel = Console.ReadLine();
                                factoryRepository.AddWebsite(new WikipediaWebsite(new WikipediaGeneralInfo(topic, language),
                                    new WikipediaReviewInfo(reviewedBy, qualityLevel)));
                                factoryRepository.Rewrite();
                                factoryRepository.Refresh();
                            }

                            else if (Input == 2)
                            {
                                List<WikipediaWebsite> sortedOne = factoryRepository.DataWikipediaWebsites
                                    .OrderBy(WikipediaWebsite => WikipediaWebsite.wikipediaGeneralInfo.Topic).ToList();
                                for (int i = 0; i < sortedOne.Count; i++)   
                                {
                                    Console.WriteLine(sortedOne[i].Info());
                                }
                            }
                            else if (Input == 0)
                            {
                                isBool = true;
                                break;
                            }
                        }

                        break;
                    case 2:
                        int k = 0;
                        for (int i = 0; i < factoryRepository.DataWikipediaWebsites.Count; i++)
                        {
                            Console.WriteLine(factoryRepository.DataWikipediaWebsites[i].Info());
                        }

                        int indexf;
                        indexf = int.Parse(Console.ReadLine());
                        string topic1;
                        topic1 = Console.ReadLine();
                        string language1;
                        language1 = Console.ReadLine();
                        string reviewedBy1;
                        reviewedBy1 = Console.ReadLine();
                        string qualityLevel1;
                        qualityLevel1 = Console.ReadLine();
                        for (int i = 0; i < factoryRepository.DataWikipediaWebsites.Count; i++)
                        {
                            if (indexf == i)
                            {
                                factoryRepository.DataWikipediaWebsites[i].wikipediaGeneralInfo.Topic = topic1;
                                factoryRepository.DataWikipediaWebsites[i].wikipediaGeneralInfo.Language = language1;
                                factoryRepository.DataWikipediaWebsites[i].wikipediaReviewInfo.ReviewedBy = reviewedBy1;
                                factoryRepository.DataWikipediaWebsites[i].wikipediaReviewInfo.QualityLevel = qualityLevel1;
                                k++;
                                break;
                            }
                        }

                        if (k == 3)
                        {
                            factoryRepository.DataWikipediaWebsites.RemoveAt(indexf);
                        }

                        factoryRepository.Rewrite();
                        factoryRepository.Refresh();

                        break;
                    default:
                        break;
                }

                if (menu)
                    break;
            }
        }
    }
}