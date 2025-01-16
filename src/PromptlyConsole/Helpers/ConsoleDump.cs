using System;
using System.Collections.Generic;
using System.Text.Json;

namespace PromptlyConsole.Helpers
{
    public static class ConsoleDump
    {
        public static void WriteHeader(string header)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========================================");
            Console.WriteLine(header);
            Console.WriteLine("========================================");
            Console.ResetColor();
        }

        public static void WriteSectionHeader(string sectionName)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========================================");
            Console.WriteLine(sectionName);
            Console.WriteLine("========================================");
            Console.ResetColor();
        }

        public static void WriteInfo(string info, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(info);
            Console.ResetColor();
        }

        public static void WriteWarning(string warning)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Warning: {warning}");
            Console.ResetColor();
        }

        public static void WriteError(string? error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string displayError = error ?? string.Empty;
            Console.WriteLine($"Error: {displayError}");
            Console.ResetColor();
        }

        public static void WriteSuccess(string success)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Success: {success}");
            Console.ResetColor();
        }

        public static void WriteLine(string line, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(line);
            Console.ResetColor();
        }

        public static void WriteSection(string sectionName)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========================================");
            Console.WriteLine(sectionName);
            Console.WriteLine("========================================");
            Console.ResetColor();
        }

        public static void WriteDivider()
        {
            Console.WriteLine("----------------------------------------");
        }

        public static void WriteColoredHeader(string header)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(header);
            Console.ResetColor();
        }

        public static void WriteColoredResponse(int iteration, string response)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("----------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Iteration {iteration} Response:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(response);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("----------------------------------------");
            Console.ResetColor();
        }

        public static void WriteJson(string json)
        {
            try
            {
                var jsonElement = JsonSerializer.Deserialize<JsonElement>(json);
                string prettyJson = JsonSerializer.Serialize(jsonElement, new JsonSerializerOptions { WriteIndented = true });

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("{");
                Console.ResetColor();

                foreach (var line in prettyJson.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                {
                    if (line.Trim().StartsWith("\""))
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan; // Keys
                        Console.Write(line.Substring(0, line.IndexOf(':') + 1));
                        Console.ResetColor();

                        Console.ForegroundColor = ConsoleColor.White; // Values
                        Console.WriteLine(line.Substring(line.IndexOf(':') + 1));
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(line);
                    }
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("}");
                Console.ResetColor();
            }
            catch (JsonException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid JSON format.");
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        public static void WritePrompt(string prompt, string defaultValue = "")
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(prompt);
            if (!string.IsNullOrEmpty(defaultValue))
            {
                Console.Write($" (default: {defaultValue})");
            }
            Console.Write(": ");
            Console.ResetColor();
        }

        public static void WriteConversation(string userMessage, string systemMessage)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("User: " + userMessage);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("System: " + systemMessage);
            Console.ResetColor();
        }

        public static void WriteQuestionAnswerPair(string question, string answer)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Q: " + question);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("A: " + answer);
            Console.ResetColor();
        }

        public static void WriteList(IEnumerable<string> items, bool numbered = false)
        {
            int index = 1;
            foreach (var item in items)
            {
                if (numbered)
                {
                    Console.WriteLine($"{index}. {item}");
                }
                else
                {
                    Console.WriteLine($"- {item}");
                }
                index++;
            }
        }

        public static void WriteTable(string[] headers, string[,] data)
        {
            int columns = headers.Length;
            int rows = data.GetLength(0);

            // Print headers
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < columns; i++)
            {
                Console.Write(headers[i].PadRight(20));
            }
            Console.WriteLine();
            Console.ResetColor();

            // Print separator
            Console.WriteLine(new string('-', columns * 20));

            // Print data
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(data[i, j].PadRight(20));
                }
                Console.WriteLine();
            }
        }

        public static void ProgressStart(string startMessage, int dotCount = 3, int delay = 500)
        {
            Console.WriteLine(startMessage);
            Console.Write("Progress: ");
            for (int i = 0; i < dotCount; i++)
            {
                Console.Write(".");
                System.Threading.Thread.Sleep(delay);
            }
        }

        public static void ProgressComplete(string completionMessage)
        {
            Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r"); // Clear the line
            Console.WriteLine(completionMessage);
        }
    }
} 