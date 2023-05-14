using Microsoft.SemanticKernel.AI.ChatCompletion;

namespace AItoAIChat.Services;
public record ChatHistorySnapshot(User User, User Assistant, IEnumerable<ChatHistory.Message> Messages)
{
    public IEnumerable<string> GetMessagesAsText()
    {
        return Messages
            .Where(x => x.AuthorRole is ChatHistory.AuthorRoles.User or ChatHistory.AuthorRoles.Assistant)
            .Select(x => $"{(x.AuthorRole == ChatHistory.AuthorRoles.Assistant ? Assistant.Name : User.Name)}: {x.Content}");
    }
}