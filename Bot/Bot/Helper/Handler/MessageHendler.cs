using Bot.BusinessLogic.Helper;
using Bot.BusinessLogic.Services.Interfaces;
using Bot.Common.Dto;
using Bot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
namespace Bot.Helper.Handler
{
	public class MessageHendler
	{
        private readonly IButtonService _buttonService;
        private readonly ICategoryType _categoryType;

        public MessageHendler(IButtonService buttonService,ICategoryType categoryType)
        {
            _buttonService = buttonService;
            _categoryType = categoryType;
        }

        public async Task HandleMessage(ITelegramBotClient botClient, Message message)
        {
            if (message.Text == "/start" || message.Text =="На главную")
            {
                ReplyKeyboardMarkup keyboard = _buttonService.MenuButton(
                    new KeyboardButton[] { "💸 Добавить расходы", "💰 Добавить доходы" },
                new KeyboardButton[] { "📄 Моя таблица", "👥 Совместный учет" },
                new KeyboardButton[] { "⚙️ Настройки" });
                if (message.Text == "/start")
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Добро пожаловать! Я буду вести учёт ваших доходов и расходов! ",
                    replyMarkup: keyboard);
                else
                    await botClient.SendTextMessageAsync(message.Chat.Id,"Вы вернулись на главную страницу", replyMarkup: keyboard);
                    return;
            }
            if(message.Text == "💰 Добавить доходы" || message.Text == "Назад")
            {
                ReplyKeyboardMarkup keyboard = _buttonService.MenuButton(
                    new KeyboardButton[] {"Выбрать категорию", "Добавить категорию"},
                    new KeyboardButton[] {"На главную"}
                    );
                await botClient.SendTextMessageAsync(message.Chat.Id, "Для ввода расходов, пожалуйста, выберите категорию расходов или создайте свою", replyMarkup: keyboard);
                return;
            }
            if(message.Text == "Выбрать категорию")
            {
                List<CategoryDto> list = _categoryType.Get(1);

                List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();
                
                for (int i = 0; i < 3; i++)
                    buttons.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(list[i].Name, list[i].Name) });

                buttons.Add(_buttonService.CategoryButtons());
                InlineKeyboardMarkup keyboard = new(buttons);

                _categoryType.PageCount = Convert.ToInt32(Math.Round((double)list.Count / 3));

                await botClient.SendTextMessageAsync(message.Chat.Id, "Здесь представлены все имеющиеся категории расходов. Если вы не нашли подходящую для себя категорию, то нажмите кнопку “Назад“ и затем нажмите “Добавить категорию“", replyMarkup: keyboard);
                return;
            }
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Команда: "+message.Text+" не найдена");
        }
        
    }
}

