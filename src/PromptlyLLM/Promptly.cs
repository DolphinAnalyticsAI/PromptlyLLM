using PromptlyLLM.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PromptlyLLM
{
    /// <summary>
    /// Manages model providers and facilitates the processing of prompts.
    /// </summary>
    /// <remarks>
    /// This class allows for the addition of model providers and provides methods to process both simple text and typed data prompts.
    /// </remarks>
    public class Promptly
    {
        private readonly IModelProvider? _provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="Promptly"/> class.
        /// </summary>
        public Promptly() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Promptly"/> class with a specified model provider.
        /// </summary>
        /// <param name="provider">The model provider to be added.</param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="provider"/> is null.</exception>
        public Promptly(IModelProvider provider)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            _provider = provider;
        }

    
        /// <summary>
        /// Processes a simple text prompt and returns a response as a string.
        /// </summary>
        /// <param name="userPrompt">The user's input prompt.</param>
        /// <param name="systemPrompt">The system's contextual input or instructions.</param>
        /// <returns>A task representing the asynchronous operation, with a string result containing the response.</returns>
        public Task<string> Prompt(string userPrompt, string systemPrompt = "")
        {
            var prompt = BuildPrompt(userPrompt, systemPrompt);
            return Prompt(prompt);
        }

        /// <summary>
        /// Processes a prompt and returns a response as a string.
        /// </summary>
        /// <param name="prompt">The prompt to be processed.</param>
        /// <returns>A task representing the asynchronous operation, with a string result containing the response.</returns>
        public async Task<string> Prompt(Prompt prompt)
        {
            if (_provider == null)
            {
                throw new InvalidOperationException("Model provider is not initialized.");
            }

            if (prompt == null)
            {
                throw new ArgumentNullException(nameof(prompt), "The prompt cannot be null.");
            }

            try
            {
                return await _provider.Prompt(prompt);
            }
            catch (Exception ex)
            {
                // Log the exception details here using your logging framework
                // For example, using Console.WriteLine for simplicity
                Debug.WriteLine($"An error occurred while processing the prompt: {ex.Message}");

                // Depending on how you want to handle errors, you can either:
                // a) Rethrow the exception to let the caller handle it
                throw;

                // b) Or return a default value or error message
                // return "An error occurred while processing your request.";
            }
        }

        /// <summary>
        /// Processes a typed data prompt and returns a response as an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the response object, which must be a class with a parameterless constructor.</typeparam>
        /// <param name="prompt">The prompt to be processed.</param>
        /// <returns>A task representing the asynchronous operation, with a result of type <typeparamref name="T"/> containing the response.</returns>
        public Task<T?> Prompt<T>(Prompt prompt) where T : class, new()
        {
            throw new NotImplementedException();
        }

        private static Prompt BuildPrompt(string userPrompt, string systemPrompt)
        {
            Prompt prompt = new Prompt(userPrompt, systemPrompt);
            return prompt;            
        }
              
    }
}