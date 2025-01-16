using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PromptlyLLM.Abstractions;
using System.Diagnostics;

namespace PromptlyLLM.Providers
{
    public class OpenAIHttpModelProvider : IModelProvider  
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _model;
        private const string ApiUrl = "https://api.openai.com/v1/chat/completions";

        public OpenAIHttpModelProvider(string apiKey, string model, HttpClient? httpClient = null)
        {
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _httpClient = httpClient ?? new HttpClient();
            ConfigureHttpClient();
        }

        private void ConfigureHttpClient()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            _httpClient.BaseAddress = new Uri(ApiUrl);
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

      
        public async Task<string> Prompt(Prompt prompt)
        {
            var result = await SendHttpRequest(prompt);

            if (result != null && result.Choices != null && result.Choices.Length > 0)
            {
                var choice = result.Choices[0];
                if (choice != null && choice.Message != null)
                {
                    var content = choice.Message.Content;
                    Debug.WriteLine($"Deserialized Choices Count: {result.Choices.Length}");
                    Debug.WriteLine($"Response: {content}");

                    return content ?? string.Empty;
                }
                else
                {
                    Debug.WriteLine("Choice or Message is null.");
                    return string.Empty;
                }
            }
            else
            {
                Debug.WriteLine("No choices available or result is null.");
                return string.Empty;
            }
        }

        private async Task<OpenAIResponse?> SendHttpRequest(Prompt prompt)
        {
            var requestBody = new
            {
                model = _model,
                messages = new[]
                {
                    new { role = "system", content = prompt.SystemPrompt ?? string.Empty },
                    new { role = "user", content = prompt.UserPrompt ?? string.Empty }
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync(ApiUrl, content);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Raw JSON Response: {responseContent}");

                var result = JsonSerializer.Deserialize<OpenAIResponse>(responseContent, options);
                return result;
            }
            catch (JsonException jsonEx)
            {
                Trace.TraceError($"JSON Deserialization Error: {jsonEx.Message}");
                Debug.WriteLine($"JSON Deserialization Error: {jsonEx}");
            }
            catch (HttpRequestException httpEx)
            {
                Trace.TraceError($"HTTP Request Error: {httpEx.Message}");
                Debug.WriteLine($"HTTP Request Error: {httpEx}");
            }
            catch (Exception ex)
            {
                Trace.TraceError($"General Error: {ex.Message}");
                Debug.WriteLine($"General Error: {ex}");
            }

            return null;
        }

        public async Task<T?> Prompt<T>(Prompt prompt) where T : class, new()
        {
            var responseString = await Prompt(prompt);
            if (string.IsNullOrEmpty(responseString))
            {
                // Return a new instance of T if the response string is null or empty
                return new T();
            }

            var result = JsonSerializer.Deserialize<T>(responseString);
            if (result != null)
            {
                return result;
            }
            else
            {
                // Return a new instance of T if deserialization returns null
                return new T();
            }
        }

        #region OpenAIResponse Helper Classes
        private class OpenAIResponse
        {
            public string? Id { get; set; }
            public string? Object { get; set; }
            public long Created { get; set; }
            public string? Model { get; set; }
            public Choice[]? Choices { get; set; }
            public Usage? Usage { get; set; }
            public string? ServiceTier { get; set; }
            public string? SystemFingerprint { get; set; }
        }

        private class Choice
        {
            public int Index { get; set; }
            public Message? Message { get; set; }
            public object? Logprobs { get; set; }
            public string? FinishReason { get; set; }
        }

        private class Message
        {
            public string? Role { get; set; }
            public string? Content { get; set; }
            public object? Refusal { get; set; }
        }

        private class Usage
        {
            public int PromptTokens { get; set; }
            public int CompletionTokens { get; set; }
            public int TotalTokens { get; set; }
            public TokenDetails? PromptTokensDetails { get; set; }
            public TokenDetails? CompletionTokensDetails { get; set; }
        }

        private class TokenDetails
        {
            public int CachedTokens { get; set; }
            public int AudioTokens { get; set; }
            public int ReasoningTokens { get; set; }
            public int AcceptedPredictionTokens { get; set; }
            public int RejectedPredictionTokens { get; set; }
        }
        #endregion
    }


}