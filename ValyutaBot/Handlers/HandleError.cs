using System;
using Telegram.Bot;
using System.Threading;
using System.Threading.Tasks;

namespace ValyutaBot.Handlers;

public static class HandleError
{
    public static async Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
    {
        var channelId = "-1002479894397";

        await client.SendMessage(
            chatId: channelId,
            text: $"{exception.Message}"
            );
    }
}
