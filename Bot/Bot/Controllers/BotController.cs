using Bot.BusinessLogic.Helper;
using Bot.BusinessLogic.Services.Interfaces;
using Bot.Common.Dto;
using Bot.Helper.Handler;
using Bot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot.Controllers
{
    public class BotController
    {
        private readonly IButtonService _buttonService;
        private readonly ICategoryType _categoryType;

        private MessageHendler messageHendler;

        public BotController(IButtonService buttonService,ICategoryType categoryType)
        {
            _categoryType = categoryType;
            _buttonService = buttonService;
            messageHendler = new MessageHendler(_buttonService,_categoryType);
        }
        public async Task HandleUpdatesAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            if (update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                await messageHendler.HandleMessage(botClient, update.Message);
                return;
            }

            if (update.Type == UpdateType.CallbackQuery)
            {
                await HandleCallbackQuery(botClient, update.CallbackQuery);
                return;
            }
        }
        
        public async Task HandleCallbackQuery(ITelegramBotClient botClient,
            CallbackQuery callbackQuery)
        {
            
            if (callbackQuery.Data.StartsWith("category_next"))
            {
                List<CategoryDto> list = _categoryType.Get(1);
                await _buttonHendler.NextPage(botClient, callbackQuery, list, _buttonService);
                return;
            }
            if (callbackQuery.Data.StartsWith("category_back"))
            {
                List<CategoryDto> list = _categoryType.Get(1);
                await _buttonHendler.BackPage(botClient, callbackQuery, list, _buttonService);
                return;
            }
            
            
            await botClient.SendTextMessageAsync(
                callbackQuery.Message.Chat.Id,
                $"You choose with data: {callbackQuery.Data}");
            return;
        }
        private CategoryButtonHendler _buttonHendler = new CategoryButtonHendler();
    }
}

