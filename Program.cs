sing System;

namespace TextSearch
{
    public class Program
    {
        public static int CountOccurrences(string text, string word)
        {
            if (string.IsNullOrEmpty(word)) return 0;

            int count = 0, index = 0;
            while ((index = text.IndexOf(word, index, StringComparison.Ordinal)) != -1)
            {
                count++;
                index += word.Length;
            }
            return count;
        }

        public static int Main(string[] args)
        {
            try
            {
                // Перевірка наявності аргументу -word
                if (args.Length < 2 || args[0] != "-word")
                {
                    Console.Error.WriteLine("Usage: -word <searchWord>");
                    return 1;
                }

                string searchWord = args[1];

                // Читання тексту з stdin
                string? inputText = Console.In.ReadToEnd();
                if (string.IsNullOrWhiteSpace(inputText))
                {
                    Console.Error.WriteLine("No input text provided.");
                    return 2;
                }

                int count = CountOccurrences(inputText, searchWord);
                Console.WriteLine(count);
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
                return 99;
            }
        }
    }
}

