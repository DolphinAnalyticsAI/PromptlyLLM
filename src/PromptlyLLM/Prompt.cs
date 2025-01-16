namespace PromptlyLLM
{
    /// <summary>
    /// Represents a prompt containing user and system input for processing by a model provider.
    /// </summary>
    /// <remarks>
    /// This record is used to encapsulate the input data required for generating responses from a model provider.
    /// </remarks>
    public record Prompt(string UserPrompt, string SystemPrompt);
} 