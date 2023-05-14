using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.SkillDefinition;

namespace AItoAIChat.Services;
public class Judge
{
    private ISKFunction _judgeFunction;
    public Judge(IKernel kernel)
    {
        _judgeFunction = kernel.CreateSemanticFunction("""
            あなたは裁判官です。
            以下の2人の会話を評価して、どちらの主張が正しいか判断してください。
            どちらも正しい場合でも、何か理由をつけて勝者の名前を書いてください。

            <2人の会話履歴>
            {{$input}}
            </2人の会話履歴>
            <論点>
            どちらが本当に17歳であるか。17歳っぽいほうが勝者。
            </論点>

            判決は以下のフォーマットで記載してください。
            勝者: 勝者の名前
            コメント:
            この判断をした理由をここに記載してください。。


            判決:

            """,
            maxTokens: 3000);
    }

    public async ValueTask<string> EvalAsync(ChatHistorySnapshot chatHistory)
    {
        var input = string.Join("\n", chatHistory.GetMessagesAsText());
        return (await _judgeFunction.InvokeAsync(input)).Result;
    }
}
