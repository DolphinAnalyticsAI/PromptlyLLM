using PromptlyLLM;
using PromptlyLLM.Abstractions;

namespace PromptlyConsole.Providers;

public class ConsoleModelProvider : IModelProvider
{
    public async Task<string> Prompt(Prompt prompt)
    {
        // Simulate network latency or processing time
        await Task.Delay(500);

        return $"This is a simulated response to: {prompt.UserPrompt}";
    }

    public async Task<T?> Prompt<T>(Prompt prompt) where T : class, new()
    {
        // Simulate network latency or processing time
        await Task.Delay(500);

        // Create a sample instance with some demo data
        var result = new T();
        var properties = typeof(T).GetProperties();

        foreach (var prop in properties)
        {
            if (prop.PropertyType == typeof(string))
                prop.SetValue(result, $"Sample {prop.Name}");
            else if (prop.PropertyType == typeof(int))
                prop.SetValue(result, 42);
        }

        return result;
    }

    private static Prompt BuildPrompt(string userPrompt, string systemPrompt)
    {
        return new Prompt(userPrompt, systemPrompt);
    }
} 