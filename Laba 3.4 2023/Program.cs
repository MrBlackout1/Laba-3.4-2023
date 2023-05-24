using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string[] filePaths = { "file1.txt", "file2.txt", "file3.txt" };

        // Делегат типу Func<string, IEnumerable<string>> для токенізації текстових файлів
        Func<string, IEnumerable<string>> tokenizeTextFile = (filePath) =>
        {
            string text = File.ReadAllText(filePath);
            return text.Split(new[] { ' ', '\t', '\n', '\r', '.', ',', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        };

        // Делегат типу Func<IEnumerable<string>, IDictionary<string, int>> для обчислення частоти слів
        Func<IEnumerable<string>, IDictionary<string, int>> calculateWordFrequency = (words) =>
        {
            var wordFrequency = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (wordFrequency.ContainsKey(word))
                    wordFrequency[word]++;
                else
                    wordFrequency[word] = 1;
            }
            return wordFrequency;
        };

        // Дія для відображення статистики частоти слів
        Action<IDictionary<string, int>> displayWordFrequency = (wordFrequency) =>
        {
            foreach (var kvp in wordFrequency)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        };

        var overallWordFrequency = new Dictionary<string, int>();

        foreach (var filePath in filePaths)
        {
            IEnumerable<string> words = tokenizeTextFile(filePath);
            IDictionary<string, int> wordFrequency = calculateWordFrequency(words);

            // Додавання статистики частоти слів до загального результату
            foreach (var kvp in wordFrequency)
            {
                string word = kvp.Key;
                int frequency = kvp.Value;
                if (overallWordFrequency.ContainsKey(word))
                    overallWordFrequency[word] += frequency;
                else
                    overallWordFrequency[word] = frequency;
            }
        }

        // Відображення загальної статистики частоти слів
        displayWordFrequency(overallWordFrequency);
    }
}
