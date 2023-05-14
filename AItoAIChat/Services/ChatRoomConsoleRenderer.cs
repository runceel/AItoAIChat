using Microsoft.SemanticKernel.AI.ChatCompletion;

namespace AItoAIChat.Services;
public class ChatRoomConsoleRenderer : IChatRoomRenderer
{
    public ValueTask RenderAsync(ChatRoom chatRoom)
    {
        var latestMessage = chatRoom.CurrentTurnUser
            .ChatHistory
            .Messages
            .Last(x => x.AuthorRole != ChatHistory.AuthorRoles.System);
        var messageOwner = latestMessage.AuthorRole == ChatHistory.AuthorRoles.Assistant ?
            chatRoom.CurrentTurnUser :
            chatRoom.NextTurnUser;
        Console.WriteLine($"{messageOwner.User.Name}> {latestMessage.Content}");
        return ValueTask.CompletedTask;
    }
}
