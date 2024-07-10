using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

class ConnectionsGame
{
    private List<string> words;
    private Dictionary<int, List<string>> categories;
    private List<string> selectedWords;
    private Random random;

    public ConnectionsGame(string wordsFile, string categoriesFile)
    {
        InitializeGame(wordsFile, categoriesFile);
    }

    private void InitializeGame(string wordsFile, string categoriesFile)
    {
        LoadWords(wordsFile);
        LoadCategories(categoriesFile);
        selectedWords = new List<string>();
        random = new Random();
        ShuffleWords();
    }

    private void LoadWords(string filename)
    {
        words = File.ReadAllLines(filename).ToList();
    }

    private void LoadCategories(string filename)
    {
        categories = new Dictionary<int, List<string>>();
        var lines = File.ReadAllLines(filename);
        foreach (var line in lines)
        {
            var parts = line.Split(':');
            if (parts.Length == 2)
            {
                int categoryId = int.Parse(parts[0]);
                List<string> categoryWords = parts[1].Split(',').Select(w => w.Trim()).ToList();
                categories[categoryId] = categoryWords;
            }
        }
    }

    private void ShuffleWords()
    {
        for (int i = 0; i < words.Count; i++)
        {
            int r = random.Next(i, words.Count);
            string temp = words[i];
            words[i] = words[r];
            words[r] = temp;
        }
    }

    public void Play()
    {
        Console.WriteLine("Welcome to Connections!");
        Console.WriteLine("Find groups of four related words.");

        while (categories.Count > 0)
        {
            DisplayWords();
            SelectWords();
            CheckSelection();
        }

        Console.WriteLine("Congratulations! You've found all the connections!");
    }

    private void DisplayWords()
    {
        Console.WriteLine("\nAvailable words:");
        for (int i = 0; i < words.Count; i++)
        {
            Console.Write($"{words[i],-8}");
            if ((i + 1) % 4 == 0) Console.WriteLine();
        }
        Console.WriteLine();
    }

    private void SelectWords()
    {
        selectedWords.Clear();
        Console.WriteLine("Enter four words (one at a time):");
        for (int i = 0; i < 4; i++)
        {
            string word;
            do
            {
                word = Console.ReadLine().ToUpper();
                if (!words.Contains(word))
                {
                    Console.WriteLine("Invalid word. Try again.");
                }
            } while (!words.Contains(word));
            selectedWords.Add(word);
        }
    }

    private void CheckSelection()
    {
        foreach (var category in categories)
        {
            if (category.Value.All(word => selectedWords.Contains(word)))
            {
                Console.WriteLine($"Correct! You found category {category.Key}.");
                foreach (var word in category.Value)
                {
                    words.Remove(word);
                }
                categories.Remove(category.Key);
                ShuffleWords(); // 每次找到正确类别后重新打乱剩余单词
                return;
            }
        }
        Console.WriteLine("Incorrect. These words don't form a category. Try again.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        string wordsFile = "words.txt";
        string categoriesFile = "categories.txt";
        
        ConnectionsGame game = new ConnectionsGame(wordsFile, categoriesFile);
        game.Play();
    }
}