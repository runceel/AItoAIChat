namespace AItoAIChat.Services;
public class ChatRoom
{

    public ChatRoom(ChatUser user1, ChatUser user2)
    {
        User1 = user1;
        User2 = user2;
        CurrentTurnUser = User1;
    }

    public ChatUser User1 { get; }
    public ChatUser User2 { get; }

    public ChatUser CurrentTurnUser { get; private set; }
    public ChatUser NextTurnUser =>
        CurrentTurnUser == User1 ? User2 : User1;

    public void SwitchUser() => CurrentTurnUser = CurrentTurnUser == User1 ? User2 : User1;
}
