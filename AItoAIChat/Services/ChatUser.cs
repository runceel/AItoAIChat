using Microsoft.SemanticKernel.AI.ChatCompletion;

namespace AItoAIChat.Services;
public class ChatUser
{
    public User User { get; }
    public ChatHistory ChatHistory { get; }

    public ChatUser(User user, ChatHistory chatHistory)
    {
        User = user;
        ChatHistory = chatHistory;
    }
}
