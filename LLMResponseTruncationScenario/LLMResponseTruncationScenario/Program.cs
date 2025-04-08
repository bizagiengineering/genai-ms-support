using OpenAI.Chat;
using Azure.AI.OpenAI;
using System.ClientModel;
using Microsoft.Extensions.Configuration;

// Load configuration from secrets.json
var configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

// Retrieve secrets
var openAIUri = configuration["OpenAI:Uri"];
var openAIKey = configuration["OpenAI:Key"];

// Path to the file containing the context to be sent to the model
var contextFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "context.txt");

// Model to be used (GPT-4o in this case)
var model = "GPT4o";

// Read the context from the file
var context = File.ReadAllText(contextFilePath);

// User's question
var question = "I want you to be Cixin Liu and answer any question with accuracy, describing the guzheng operation regarding the book in an exact and explicit manner without ignoring anything";

// Initialize the Azure OpenAI client
var _client = new AzureOpenAIClient(new Uri(openAIUri), new ApiKeyCredential(openAIKey));

// Configure chat completion options
var chatCompletionsOptions = new ChatCompletionOptions()
{
    Temperature = 0,
    FrequencyPenalty = 0,
    PresencePenalty = 0,
    ResponseFormat = ChatResponseFormat.CreateTextFormat(),
    MaxOutputTokenCount = 16384
};

// Prepare the chat messages to be sent to the model
var chatMessages = new List<ChatMessage>
{
    new SystemChatMessage("Consider the following relevant information to answer: "),
    new SystemChatMessage(context),
    new UserChatMessage(question)
};

// Create a chat client for the specified model
ChatClient client = _client.GetChatClient(model);

// Send the chat messages and get the response
var result = await client.CompleteChatAsync(chatMessages, chatCompletionsOptions);

// Extract the last message from the response
var chatCompletion = result.Value;
var index = chatCompletion.Content.Count - 1;
var Answer = chatCompletion.Content[index].Text;

// Log token usage for debugging purposes
Console.WriteLine($"OutputTokenCount = {result.Value.Usage.OutputTokenCount}");
Console.WriteLine($"InputTokenCount = {result.Value.Usage.InputTokenCount}");
Console.WriteLine($"TotalTokenCount = {result.Value.Usage.TotalTokenCount}");

// Print the model's response
Console.WriteLine($"Answer = {Answer}");

Console.WriteLine("End");


