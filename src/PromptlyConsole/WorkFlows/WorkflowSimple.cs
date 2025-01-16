using System;
using System.Threading.Tasks;
using PromptlyConsole.Helpers;
using PromptlyLLM.Providers;
using PromptlyLLM.Abstractions;
using PromptlyLLM;

namespace PromptlyConsole.WorkFlows
{
    public class WorkflowSimple
    {
        private readonly OpenAIHttpModelProvider _modelProvider;

        public WorkflowSimple(OpenAIHttpModelProvider modelProvider)
        {
            _modelProvider = modelProvider ?? throw new ArgumentNullException(nameof(modelProvider));
        }

        public async Task PromptChaining(int iterations)
        {
            ConsoleDump.WriteHeader("Prompt Chaining Workflow");
            ConsoleDump.WriteLine(""); // Add space after header

            string userPrompt = "Describe the process of photosynthesis.";
            string systemPrompt = "You are a knowledgeable assistant.";

            for (int i = 0; i < iterations; i++)
            {
                ConsoleDump.WriteSectionHeader($"Iteration {i + 1}");
                ConsoleDump.WriteLine(""); // Add space after section header

                ConsoleDump.WriteInfo($"User Prompt: {userPrompt}", ConsoleColor.Gray);
                ConsoleDump.WriteInfo($"System Prompt: {systemPrompt}", ConsoleColor.Gray);
                ConsoleDump.WriteLine(""); // Add space after prompts

                ConsoleDump.ProgressStart("Starting the call to LLM");
                
                // Simulate the call to LLM
                var response = await _modelProvider.Prompt(new Prompt(userPrompt, systemPrompt));
                
                ConsoleDump.ProgressComplete("Call to LLM complete.");
                ConsoleDump.WriteLine(""); // Add space after progress complete

                ConsoleDump.WriteColoredResponse(i + 1, response);
                ConsoleDump.WriteLine(""); // Add space after response

                ConsoleDump.WriteQuestionAnswerPair(userPrompt, response);
                ConsoleDump.WriteInfo("Response Time: 150ms", ConsoleColor.Gray);
                ConsoleDump.WriteLine(""); // Add space after question-answer pair

                // Update the user prompt for the next iteration
                userPrompt = response;
            }
        }
        
        public async Task ChatConversation(int iterations)
        {
            ConsoleDump.WriteHeader("Prompt Chaining Workflow");

            string userPrompt = "Tell me a fun fact about space.";
            string systemPrompt = "You are a knowledgeable assistant.";

            for (int i = 0; i < iterations; i++)
            {
                ConsoleDump.WriteInfo($"Iteration {i + 1}", ConsoleColor.Gray);
                ConsoleDump.WriteInfo($"User Prompt: {userPrompt}", ConsoleColor.Gray);
                ConsoleDump.WriteInfo($"System Prompt: {systemPrompt}", ConsoleColor.Gray);

                ConsoleDump.ProgressStart("Starting the call to LLM");
                
                // Simulate the call to LLM
                var response = await _modelProvider.Prompt(new Prompt(userPrompt, systemPrompt));

                ConsoleDump.ProgressComplete("Call to LLM complete.");

                ConsoleDump.WriteColoredResponse(i + 1, response);

                ConsoleDump.WriteQuestionAnswerPair(userPrompt, response);
                ConsoleDump.WriteInfo("Response Time: 150ms", ConsoleColor.Gray);

                // Update the user prompt for the next iteration
                userPrompt = response;
            }
        }

        public void WriteFormattingExamples()
        {
            ConsoleDump.WriteHeader("Header Example");
            ConsoleDump.WriteSection("Section Example");
            ConsoleDump.WriteInfo("Info Example", ConsoleColor.Green);
            ConsoleDump.WriteWarning("Warning Example");
            ConsoleDump.WriteError("Error Example");
            ConsoleDump.WriteSuccess("Success Example");
            ConsoleDump.WriteLine("Line Example");
            ConsoleDump.WriteDivider();
            ConsoleDump.WriteJson("{\"name\": \"John Doe\", \"age\": 30, \"city\": \"New York\"}");

            ConsoleDump.WritePrompt("Enter your name", "John Doe");
            ConsoleDump.WriteConversation("Hello, how are you?", "I'm a helpful assistant.");
            ConsoleDump.WriteQuestionAnswerPair("What is the capital of France?", "The capital of France is Paris.");

            ConsoleDump.WriteList(new[] { "Apple", "Banana", "Cherry" }, numbered: true);

            string[] headers = { "Name", "Age", "City" };
            string[,] data = {
                { "John Doe", "30", "New York" },
                { "Jane Smith", "25", "Los Angeles" },
                { "Sam Brown", "40", "Chicago" }
            };
            ConsoleDump.WriteTable(headers, data);
        }

        public void CompleteConversationExample()
        {
            ConsoleDump.WriteHeader("Conversation with OpenAI Provider");
            ConsoleDump.WriteInfo("Provider: OpenAI", ConsoleColor.Gray);
            ConsoleDump.WriteInfo("User Prompt: What is the capital of France?", ConsoleColor.Gray);
            ConsoleDump.WriteInfo("System Prompt: You are a helpful assistant.", ConsoleColor.Gray);

            ConsoleDump.ProgressStart("Starting the call to LLM");
            // Simulate the call to LLM
            System.Threading.Thread.Sleep(1500); // Simulate delay
            ConsoleDump.ProgressComplete("Call to LLM complete.");

            string response = "The capital of France is Paris.";
            ConsoleDump.WriteColoredResponse(1, response);

            ConsoleDump.WriteQuestionAnswerPair("What is the capital of France?", response);
            ConsoleDump.WriteInfo("Response Time: 150ms", ConsoleColor.Gray);
        }

        public async Task ExecutePlanWithLLM(string topic)
        {
            ConsoleDump.WriteHeader("Execute Plan with LLM");
            ConsoleDump.WriteLine(""); // Add space after header

            // Step 1: Generate a plan
            ConsoleDump.WriteInfo($"Generating plan for topic: {topic}", ConsoleColor.Gray);
            ConsoleDump.ProgressStart("Calling LLM to generate plan");
            
            
            var prompt = new Prompt($"Create a detailed plan for the topic: {topic}", 
                            "You are a strategic planner.");
            
            var planResponse = await _modelProvider.Prompt(prompt);
            
            ConsoleDump.ProgressComplete("Plan generation complete.");
            ConsoleDump.WriteColoredResponse(1, planResponse);
            ConsoleDump.WriteLine(""); // Add space after plan response

            // Step 2: Execute the plan with 3 asynchronous LLM calls
            ConsoleDump.WriteInfo("Executing plan with 3 asynchronous LLM calls", ConsoleColor.Gray);

            var tasks = new List<Task<string>>
            {
                _modelProvider.Prompt(new Prompt($"Step 1 of the plan: {planResponse}",
                    "You are an expert in this field.")),
                _modelProvider.Prompt(new Prompt($"Step 2 of the plan: {planResponse}",
                    "You are an expert in this field.")),
                _modelProvider.Prompt(new Prompt($"Step 3 of the plan: {planResponse}",
                    "You are an expert in this field."))
            };

            var results = await Task.WhenAll(tasks);

            // Step 3: Compile and compare responses
            ConsoleDump.WriteInfo("Compiling and comparing responses", ConsoleColor.Gray);
            ConsoleDump.WriteLine(""); // Add space before results

            for (int i = 0; i < results.Length; i++)
            {
                ConsoleDump.WriteColoredResponse(i + 2, results[i]);
                ConsoleDump.WriteLine(""); // Add space after each response
            }

            // Aggregate the results
            var aggregatedResponse = string.Join("\n", results);
            ConsoleDump.WriteInfo("Aggregated Response:", ConsoleColor.Gray);
            ConsoleDump.WriteLine(aggregatedResponse);
        }
    }
} 