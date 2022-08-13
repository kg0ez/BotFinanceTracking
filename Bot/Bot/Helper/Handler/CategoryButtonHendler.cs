using System;
using Bot.Common.Dto;
using Bot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Helper.Handler
{
	public class CategoryButtonHendler
	{
        public int PageCount { get; set; }
        private int _pageNumber { get; set; } = 1;

        public async Task NextPage(ITelegramBotClient bot, CallbackQuery callbackQuery, List<CategoryDto> list, IButtonService buttonService)
        {
            _pageNumber++;
            if (_pageNumber > 3)
            {
                _pageNumber = 3;
                return;
            }
            List<List<InlineKeyboardButton>> categoryButtons = new List<List<InlineKeyboardButton>>();

            for (int i = (3 * _pageNumber) - 3; i < 3 * _pageNumber; i++)
                if (i < list.Count)
                    categoryButtons.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(list[i].Name, list[i].Name) });
            categoryButtons.Add(buttonService.CategoryButtons());
            InlineKeyboardMarkup keyboard = new(categoryButtons);
            await bot.EditMessageTextAsync(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId, "Здесь представлены все имеющиеся категории расходов. Если вы не нашли подходящую для себя категорию, то нажмите кнопку “Назад“ и затем нажмите “Добавить категорию“", replyMarkup: keyboard);
            return;
        }
        public async Task BackPage(ITelegramBotClient bot, CallbackQuery callbackQuery, List<CategoryDto> list, IButtonService buttonService)
        {
            _pageNumber--;
            if (_pageNumber < 1)
            {
                _pageNumber = 1;
                return;
            }
            List<List<InlineKeyboardButton>> categoryButtons = new List<List<InlineKeyboardButton>>();

            for (int i = (3 * _pageNumber) - 3; i < 3 * _pageNumber; i++)
                if (i < list.Count)
                    categoryButtons.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(list[i].Name, list[i].Name) });
            categoryButtons.Add(buttonService.CategoryButtons());
            InlineKeyboardMarkup keyboard = new(categoryButtons);
            await bot.EditMessageTextAsync(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId, "Здесь представлены все имеющиеся категории расходов. Если вы не нашли подходящую для себя категорию, то нажмите кнопку “Назад“ и затем нажмите “Добавить категорию“", replyMarkup: keyboard);
            return;
        }
    }
}

