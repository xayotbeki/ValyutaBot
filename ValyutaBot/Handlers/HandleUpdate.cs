using Telegram.Bot;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using ValyutaBot.Models;
using Telegram.Bot.Types;
using System.Threading.Tasks;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace ValyutaBot.Handlers;

public class HandleUpdate
{
    public async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;
        if (message == null) return;

        var text = message.Text;
        var chatId = message.Chat.Id;
        var messageId = message.MessageId;

        if (text == "/start")
        {
            await client.SendMessage(
                chatId: chatId,
                text: "salom xush kelibsiz✅" +
                "\nbotda valyutalar kursi taqdim etiladi!",
                replyParameters: messageId,
                replyMarkup: MainButton()
                );
        }
        if (text == "valyuta")
        {
            string url = @"https://cbu.uz/uz/arkhiv-kursov-valyut/json/";

            using var c = new HttpClient();
            var response = await c.GetAsync(url);
            string m = "";

            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var valyutas = JsonConvert.DeserializeObject<List<Valyuta>>(str);
                m += $"Bugungi kun: {valyutas[0].Date}\n";
                Dictionary<string, string> ccy = new Dictionary<string, string>()
                {
                    { "USD", "🇺🇸 Dollar" },
                    { "EUR", "🇪🇺 Yevro" },
                    { "RUB", "🇷🇺 Rubl" }
                };

                foreach (var valyuta in valyutas)
                {
                    if (valyuta.Ccy == "USD" ||
                        valyuta.Ccy == "EUR" ||
                        valyuta.Ccy == "RUB")
                    {

                        m += $"\n1 {ccy[valyuta.Ccy]} = {valyuta.Rate} UZS";
                    }
                }
            }

            await client.SendMessage(
                chatId: chatId,
                text: m
                );
        }
    }

    private ReplyKeyboardMarkup MainButton()
    {
        KeyboardButton[][] b =
        {
            new[] { new KeyboardButton("valyuta") }
        };

        return new ReplyKeyboardMarkup(b)
        {
            ResizeKeyboard = true
        };
    }
}
