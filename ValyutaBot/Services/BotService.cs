using Telegram.Bot;
using System.Threading;
using ValyutaBot.Handlers;
using System.Threading.Tasks;

namespace ValyutaBot.Services;

public class BotService
{
    private readonly TelegramBotClient _token;
    private readonly HandleUpdate _handleUpdate;
    public BotService(string token, HandleUpdate handleUpdate)
    {
        _token = new TelegramBotClient(token);
        _handleUpdate = handleUpdate;
    }

    public async Task StartReceiving(CancellationToken cancellationToken)
    {
        _token.StartReceiving(
            _handleUpdate.HandleUpdateAsync,
            HandleError.HandleErrorAsync,
            cancellationToken: cancellationToken
            );
    }
}
