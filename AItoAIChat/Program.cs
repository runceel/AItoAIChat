using AItoAIChat;
using AItoAIChat.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.AI.ChatCompletion;

var c = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var options = c.GetSection(nameof(AItoAIChatOptions)).Get<AItoAIChatOptions>()!;

var kernel = Kernel.Builder
    .Configure(config =>
    {
        config.AddAzureChatCompletionService(
            options.ModelName,
            options.Endpoint,
            options.APIKey);
    })
    .Build();

var chatManager = new ChatManager(
    kernel.GetService<IChatCompletion>(),
    new ChatRoomConsoleRenderer());

await chatManager.CreateChatRoomAsync(
    "こんにちは。お久しぶりです。あなたって17歳と言っていますが50代ですよね？",
    new User(
        "Kazuaki",
        """
        Windows Phone とか好きです
        """,
        new[]
        {
            "0時です。GW終了をお知らせします",
            "明日からの勤務に備えてリモートしてる最中、寿司画像を連投している \r\n@od_10z\r\n さんを許すな",
            "またリモートをしてしまった",
            "今年度もよろしくお願いいたします",
            "テレビつけたらサヨナラ勝ちしてた",
            "Cloud Gaming のソウルハッカーズ2が日本語に対応してた",
            "マインスイーパーかな",
            "コメチキにレモンを絞った",
            "アクセル全開じゃないですか\r\n僕も出勤しよ",
            "17歳です",
            "免許更新忘れなかった",
            "やはり天才じゃったか",
            "困った時は自販機も探してみよう",
            "日曜夕方の時報みたいになってきました",
            "調べものをしようとしてたはずなのに Google のハロウィンゲームを無限にやってる",
        }),
    new User(
        "Shinji",
        """
        Chrome拡張「Kindle Search」とかつくってます
        C#,TypeScript,React,Vue Salesforce 
        認定資格
        ・Service Cloud Consultant
        ・Sales Cloud Consultant
        ・http://Force.com Developer
        """,
        new[]
        {
            "ララクリスティーヌ14着か。\r\n負けたとは言え勝ちに等しい内容だった",
            "先々週の天皇賞春は不戦勝、先週のNHKマイルは本命に挙げた馬が4着と絶好調の馬券神によるヴィクトリアマイル予想。\r\nこの勢いのまま春全勝と行きたいと思います。",
            "まあ本命は馬群に沈みましたが他に挙げた馬が1着2着なのでこれはもう実質的に勝ちと言ってよいでしょう",
            "東京めちゃ雨降ってるな",
            "馬券神が15時ちょっと前をお知らせしました",
            "このアプリ自体は乗っ取りアプリっぽいんだけど\r\n過剰に権限を要求してくるのはtwitterの仕様なので「こんなに権限を要求してくるから怪しい」は違います",
            "3つの祠のうち2つクリアしたからもうすぐラスダンかな",
            "かずきさん狂気の更新",
            "VIVE発売の頃に組んだパソコンなのでさすがにそろそろ組み直したい思いつつなかなか気力がわかな",
            "前作の続きか\r\n体力も最初からMAXだしマスタード持ってるし姫さまもいるし今回は楽勝だな",
            "どうも、17歳です",
            "メガドライブミニ2は今年出たおもちゃなので17歳がRTしても良いと思います！",
            "あんまり17歳17歳言ってると年齢がバレてしまうな、気をつけないと",
            "10年経っても変わらず17歳の人をフォローしておくと安心ですよ",
            "TLにはろくな17歳がいないけど\r\n17歳も捨てたもんじゃないな",
        }));

for (int i = 0; i < 10; i++)
{
    await chatManager.NextTurnAscyn();
}

var judge = new Judge(kernel);
var result = await judge.EvalAsync(chatManager.GetChatHistorySnapshot());
Console.WriteLine();
Console.WriteLine("============= 判決 =================");
Console.WriteLine(result);