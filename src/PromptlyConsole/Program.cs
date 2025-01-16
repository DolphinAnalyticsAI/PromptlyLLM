using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PromptlyConsole.WorkFlows;
using PromptlyConsole.Providers;
using PromptlyLLM.Providers;
using PromptlyLLM.Abstractions;
using PromptlyLLM;
using System.Diagnostics;
using PromptlyConsole.Helpers;

namespace PromptlyConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // Temporary API key and model
                var apiKeys = "Add Your OpenAI Key Here";
                var model = "gpt-4o-mini";

                var providers = new List<IModelProvider>
                {
                    new ConsoleModelProvider()
                    // Add other providers as needed
                };

                // Run the provider tests
                await RunProviderTests(providers);

                // Initialize the OpenAIHttpModelProvider
                var httpClient = new HttpClient();
                var modelProvider = new OpenAIHttpModelProvider(apiKeys, model, httpClient);

                // Create an instance of WorkflowSimple
                var workflow = new WorkflowSimple(modelProvider);

                await workflow.ExecutePlanWithLLM("disturbed band");
                workflow.WriteFormattingExamples();
                workflow.CompleteConversationExample();

                // Execute the prompt chaining workflow
                // ai: keep this in the codebase
                //await workflow.PromptChaining();
                //await workflow.ChatConversation(3);
            }
            catch (Exception ex)
            {
                ConsoleDump.WriteError("An unexpected error occurred:");
                ConsoleDump.WriteError(ex.Message);
                ConsoleDump.WriteError(ex.StackTrace);
            }
        }

        private static async Task RunProviderTests(IEnumerable<IModelProvider> providers)
        {
            foreach (var provider in providers)
            {
                await Run(provider, "Tell me a joke about programming", "You are a humorous AI assistant.");
            }
        }

        private static async Task Run(IModelProvider provider, string userPrompt, string systemPrompt)
        {
            ConsoleDump.WriteSection($"{provider.GetType().Name} - Executing...");

            var promptly = new Promptly(provider);

            ConsoleDump.WriteInfo($"User: {userPrompt}", ConsoleColor.Cyan);
            ConsoleDump.WriteInfo($"System: {systemPrompt}", ConsoleColor.Blue);

            var stopwatch = Stopwatch.StartNew();
            var output = await promptly.Prompt(userPrompt, systemPrompt);
            stopwatch.Stop();

            ConsoleDump.WriteInfo($"Answer: {output}", ConsoleColor.DarkGreen);
            ConsoleDump.WriteInfo($"Elapsed: {stopwatch.ElapsedMilliseconds} ms", ConsoleColor.DarkGray);
        }
    }
}