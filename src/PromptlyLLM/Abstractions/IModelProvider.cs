namespace PromptlyLLM.Abstractions
{
    /// <summary>
    /// Defines the contract for a model provider capable of processing prompts and returning responses.
    /// </summary>
    /// <remarks>
    /// Implementations of this interface are expected to handle both simple text prompts and typed data prompts.
    /// </remarks>
    public interface IModelProvider
    {
        /// <summary>
        /// Processes a simple text prompt and returns a response as a string.
        /// </summary>
        /// <param name="prompt">The prompt containing user and system input to be processed.</param>
        /// <returns>A task representing the asynchronous operation, with a string result containing the response.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="prompt"/> is null.</exception>
        /// <example>
        /// <code>
        /// var response = await modelProvider.Prompt(new Prompt { UserPrompt = "Hello", SystemPrompt = "You are a friendly assistant." });
        /// Console.WriteLine(response);
        /// </code>
        /// </example>
        Task<string> Prompt(Prompt prompt);
        
        /// <summary>
        /// Processes a typed data prompt and returns a response as an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the response object, which must be a class with a parameterless constructor.</typeparam>
        /// <param name="prompt">The prompt containing user and system input to be processed.</param>
        /// <returns>A task representing the asynchronous operation, with a result of type <typeparamref name="T"/> containing the response.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="prompt"/> is null.</exception>
        /// <example>
        /// <code>
        /// var response = await modelProvider.Prompt<MyResponseType>(new Prompt { UserPrompt = "Get data", SystemPrompt = "You are a data provider." });
        /// Console.WriteLine(response.Property);
        /// </code>
        /// </example>
        Task<T?> Prompt<T>(Prompt prompt) where T : class, new();
    }
} 
