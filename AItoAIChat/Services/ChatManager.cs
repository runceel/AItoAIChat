using AItoAIChat.Commons;
using Microsoft.SemanticKernel.AI.ChatCompletion;

namespace AItoAIChat.Services;
public class ChatManager
{
    private readonly IChatCompletion _chatCompletion;
    private readonly IChatRoomRenderer _chatRoomRenderer;
    public ChatRoom? ChatRoom { get; set; }

    public ChatManager(IChatCompletion chatCompletion, IChatRoomRenderer chatRoomRenderer)
    {
        _chatCompletion = chatCompletion;
        _chatRoomRenderer = chatRoomRenderer;
    }

    public async ValueTask CreateChatRoomAsync(string initialPrompt, User user1, User user2)
    {
        var chatUser1 = new ChatUser(user1, _chatCompletion.CreateNewChat(CreateSystemMessage(user1, user2)));
        var chatUser2 = new ChatUser(user2, _chatCompletion.CreateNewChat(CreateSystemMessage(user2, user1)));

        ChatRoom = new ChatRoom(chatUser1, chatUser2);
        chatUser1.ChatHistory.AddMessage(ChatHistory.AuthorRoles.User, initialPrompt);
        chatUser2.ChatHistory.AddMessage(ChatHistory.AuthorRoles.Assistant, initialPrompt);
        await _chatRoomRenderer.RenderAsync(ChatRoom);
    }

    public async ValueTask NextTurnAscyn()
    {
        Verify.NotNull(ChatRoom);

        var (currentUser, nextUser) = (ChatRoom.CurrentTurnUser, ChatRoom.NextTurnUser);
        var nextUserMessage = await _chatCompletion.GenerateMessageAsync(currentUser.ChatHistory,
            requestSettings: new()
            {
                MaxTokens = 256,
                Temperature = 0.3,
            });
        currentUser.ChatHistory.AddMessage(ChatHistory.AuthorRoles.Assistant, nextUserMessage);
        nextUser.ChatHistory.AddMessage(ChatHistory.AuthorRoles.User, nextUserMessage);

        await _chatRoomRenderer.RenderAsync(ChatRoom);

        ChatRoom.SwitchUser();
    }

    public ChatHistorySnapshot GetChatHistorySnapshot()
    {
        Verify.NotNull(ChatRoom);

        return new ChatHistorySnapshot(
            ChatRoom.CurrentTurnUser.User,
            ChatRoom.NextTurnUser.User,
            ChatRoom.NextTurnUser.ChatHistory.Messages.ToArray());
    }


    private static string CreateSystemMessage(User assistant, User user)
    {
        return $$$""""
            あなたは自称17歳の50代男性です。
            会話は17歳として振舞って書いてください。決して50代であることを人に悟られてはいけません。
            ただし、5回に1回程度の割合で50代しか知らないことを発言してください。

            ### コンテキスト
            今日の日付: {{time.today}}

            ### あなたのプロフィール
            <プロフィール>
            <名前>{{{assistant.Name}}}</名前>
            <自己紹介>
            {{{assistant.Profile}}}
            </自己紹介>
            <あなたがロールプレイするユーザーの発言例>
            {{{ExampleMessagesAsSingleString(assistant)}}}
                        </あなたがロールプレイするユーザーの発言例>
            </プロフィール>
                        
            ### 相手のプロフィール
            <プロフィール>
            <名前>{{{user.Name}}}</名前>
            <自己紹介>
            {{{user.Profile}}}
            </自己紹介>
            <あなたがロールプレイするユーザーの発言例>
            {{{ExampleMessagesAsSingleString(user)}}}
            </あなたがロールプレイするユーザーの発言例>
            </プロフィール>

            ### 会話の題材
            相手が17歳であると嘘をついているのはプロフィールからも明らかです。会話の中から相手の嘘を暴いて17歳であることを認めさせてください。
            """";
    }

    private static string ExampleMessagesAsSingleString(User user) =>
        string.Join("\n", user.ExampleMessages);
}
