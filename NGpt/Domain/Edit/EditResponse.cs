namespace NGpt.Domain.Edit
{
    public record EditResponse(int Created, List<Choice> Choices, Usage Usage);
    public record Choice(string Text, int Index);
    public record Usage(int PromptTokens, int CompletionTokens, int TotalTokens);
}
