# Promptly - Your Easy-to-Use LLM Wrapper 🚀

Welcome to **`Promptly`**, a lightweight and efficient wrapper designed to streamline the management of model providers and prompt processing. Implemented following the Microsoft Provider Model—often referred to as the *Strategy* or *Plugin Model*—`Promptly` allows you to seamlessly integrate one of the built-in providers or extend functionality by implementing your own provider through the `IModelProvider` interface.

This class serves as a thin abstraction over popular LLM providers like **OpenAI**, **Anthropic**, and **Genesis**, making it an ideal choice for projects that require quick setup and execution without the overhead of more complex frameworks like **Semantic Kernel**. Its simplicity and flexibility make it perfect for developers who need to get up and running swiftly.

### Quick Usage Example

First, ensure you have the necessary dependencies installed. Then, add `Promptly` to your project:

```bash
dotnet add package PromptlyLLM
```

Create an instance and send a request:

```csharp
Promptly promptly = new Promptly(new OpenAIHttpModelProvider("your-api-key", "your-model"));
string output = promptly.Prompt("What is the capital of France?");
```

or if you want to use a typed data prompt:

```csharp
Promptly promptly = new Promptly(new OpenAIHttpModelProvider("your-api-key", "your-model"));
IMyClass instance = promptly.Prompt<MyClass>("What is the capital of France?");
```

You can also use the `Prompt` object and include a system prompt:

```csharp
Promptly promptly = new Promptly(new OpenAIHttpModelProvider("your-api-key", "your-model"));
string output = promptly.Prompt(new Prompt
{
    UserPrompt = "What is the capital of France?",
    SystemPrompt = "You are a knowledgeable assistant."
});
```

With `Promptly`, you can focus on delivering results efficiently, leveraging its streamlined architecture to handle both simple and complex prompt scenarios with minimal setup.

## Why Use Promptly?

- **Effortless Integration**: Add model providers and start processing prompts in just a few lines of code.
- **Versatile**: Handles both text and typed data prompts seamlessly.
- **Developer-Friendly**: Designed with simplicity and efficiency in mind.

## Getting Started in 10 Seconds ⏱️

Here's how you can get up and running with `Promptly` in no time:

### Installation

First, ensure you have the necessary dependencies installed. Then, add `Promptly` to your project:

### Quick Start Guide

1. **Initialize Promptly**: Create an instance of `Promptly` with your model provider.

    ```csharp
    var modelProvider = new OpenAIHttpModelProvider("your-api-key", "your-model");
    var promptly = new Promptly(modelProvider);
    ```

2. **Process a Simple Text Prompt**:

    ```csharp
    var response = await promptly.Prompt("Hello, world!", "You are a friendly assistant.");
    Console.WriteLine(response);
    ```

3. **Process a Typed Data Prompt**:

    ```csharp
    var typedResponse = await promptly.Prompt<MyResponseType>("Get data", "You are a data provider.");
    Console.WriteLine(typedResponse?.Property);
    ```

## Code Samples

Here's a practical example to illustrate how easy it is to use `Promptly`:

## Next Steps

- **Explore More**: Dive into the full documentation to explore advanced features and customization options.
- **Join the Community**: Connect with other developers using `Promptly` and share your experiences.
- **Contribute**: Found a bug or have a feature request? Open an issue or contribute to the project on GitHub.

Promptly's Tip: "Documentation is like a map—without it, you're just wandering in the code wilderness. Keep it clear and concise!"

Happy coding! If you have any questions or need further assistance, feel free to reach out. Let's make prompt processing a breeze! 🌟



## NuGet Package Information

- **Package Name**: `PromptlyLLM`
- **Author**: Dolphin Analytics AI
- **Description**: A lightweight and efficient wrapper for managing model providers and processing prompts, designed with simplicity and flexibility in mind.
- **Tags**: `LLM`, `Prompt`, `Promptly`, `OpenAI`, `Anthropic`, `Genesis`, `Semantic Kernel`
- **Project Site**: https://github.com/DolphinAnalyticsAI
- **Repository**: https://github.com/DolphinAnalyticsAI/PromptlyLLM
- **License**: [MIT License](https://github.com/DolphinAnalyticsAI/PromptlyLLM/blob/main/LICENSE)

## Release Notes

Initial release of `PromptlyLLM`, a lightweight and efficient wrapper for managing model providers and processing prompts. This release includes support for popular LLM providers like OpenAI, Anthropic, and Genesis, as well as the ability to handle both text and typed data prompts seamlessly.