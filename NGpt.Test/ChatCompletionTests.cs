using NGpt.ChatCompletion;

namespace NGpt.Test;

public class ChatCompletionTests
{
    Chat _chat;

    public ChatCompletionTests()
    {
        _chat = new Chat();
    }

    [Fact]
    public async Task ShouldComplete()
    {
        var completionRequest = new ChatRequest()
        {
            Messages = new ChatMessage[]
            {
                new ChatMessage()
                {
                    Role = Role.User,
                    Content = "Say this is a test!",
                }
            },
            Temperature = 0.7f,
            Model = ChatModel.GPT3_5Turbo
        };

        var response = await _chat.CompleteAsync(completionRequest);
        var content = response.Choices[0].Message.Content;

        Assert.Equal("This is a test!", content);
    }

    [Fact]
    public async void ShouldAnswerAskedAboutName()
    {
        var completionRequest = new ChatRequest()
        {
            Messages = new ChatMessage[]
            {
                new ChatMessage()
                {
                    Role = Role.User,
                    Content = "What is your name?",
                }
            },
            Temperature = 0.7f,
            Model = ChatModel.GPT3_5Turbo
        };

        var response = await _chat.CompleteAsync(completionRequest);
        var content = response.Choices[0].Message.Content;

        Assert.True(content.Length > 0);
    }

    [Fact]
    public async void ShouldCompleteFor3ChatMessages()
    {
        var completionRequest = new ChatRequest()
        {
            Messages = new ChatMessage[]
            {
                new ChatMessage()
                {
                    Role = Role.User,
                    Content = "Create C# program that checks if a given number is prime number",
                }
            },
            Temperature = 0f,
            Model = ChatModel.GPT3_5Turbo,
            N = 3
        };

        var response = await _chat.CompleteAsync(completionRequest);

        Assert.Equal(3, response.Choices.Count);
        foreach (var choice in response.Choices)
        {
            var content = choice.Message.Content;
            Assert.True(content.Length > 0);
        }
    }

    [Fact]
    public async void ShouldCompleteFor2ChatMessages_whenNIs2()
    {
        var completionRequest = new ChatRequest()
        {
            Messages = new ChatMessage[]
            {
                new ChatMessage()
                {
                    Role = Role.User,
                    Content = "Create C# program that checks if a given number is prime number",
                }
            },
            Temperature = 0f,
            Model = ChatModel.GPT3_5Turbo,
            N = 2
        };

        var response = await _chat.CompleteAsync(completionRequest);

        Assert.Equal(2, response.Choices.Count);
        foreach (var choice in response.Choices)
        {
            var content = choice.Message.Content;
            Assert.True(content.Length > 0);
        }
    }

    [Fact]
    public async void ShouldCompleteForSimpleText()
    {
        var response = await _chat.CompleteAsync("Say hello");

        Assert.True(response.Contains("Hello"));
    }
}