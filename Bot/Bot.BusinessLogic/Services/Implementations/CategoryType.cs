using System;
using AutoMapper;
using Bot.BusinessLogic.Services.Interfaces;
using Bot.Common.Dto;
using Bot.Common.Enums;
using Bot.Models.Data;
using Bot.Models.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.BusinessLogic.Services.Implementations
{
	public class CategoryType: ICategoryType
	{
		private readonly ApplicationContext _context = new ApplicationContext();
        public int PageCount { get; set; }
        private int _pageNumber { get; set; } = 1;
        public IMapper Mapper { get; set; }

        public List<CategoryDto> Get(int type)
        {
            IQueryable<Category> query = _context.Categories;
            query = query.Where(c => c.Type == (OperationType)type);
            var categories = query.ToList();
            var categoriesDto = Mapper.Map<List<CategoryDto>>(categories);
            return categoriesDto!;
        }

        public async Task NextPage(ITelegramBotClient bot, CallbackQuery callbackQuery,List<CategoryDto> list, List<InlineKeyboardButton> buttons)
        {
            _pageNumber++;
            if (_pageNumber<=3)
            {
                List<List<InlineKeyboardButton>> categoryButtons = new List<List<InlineKeyboardButton>>();

                for (int i = 0; i < 3; i++)
                    categoryButtons.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(list[i].Name, list[i].Name) });
                categoryButtons.Add(buttons);
                InlineKeyboardMarkup keyboard = new(categoryButtons);
                await bot.EditMessageTextAsync(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId, "Здесь представлены все имеющиеся категории расходов. Если вы не нашли подходящую для себя категорию, то нажмите кнопку “Назад“ и затем нажмите “Добавить категорию“", replyMarkup: keyboard);
            }
            return;
        }
        public List<Operation> Get()
        {
            //var list = new List<Category> {
            //    new Category {  Name = "Еда вне дома", Type = OperationType.Discharge },
            //    new Category {  Name = "Продукты и хоз товары", Type = OperationType.Discharge },
            //    new Category {  Name = "Здоровье и красота", Type = OperationType.Discharge },
            //    new Category {  Name = "Транспорт", Type = OperationType.Discharge },
            //    new Category { Name = "Одежда, товары", Type = OperationType.Discharge },
            //    new Category {  Name = "Коммунальные", Type = OperationType.Discharge },
            //    new Category { Name = "Автомобиль", Type = OperationType.Discharge },
            //    new Category {  Name = "Интернет и связь", Type = OperationType.Discharge },
            //    new Category {  Name = "Образование", Type = OperationType.Discharge },
            //    new Category {  Name = "Дети", Type = OperationType.Discharge },
            //    new Category {  Name = "Путешествия", Type = OperationType.Discharge },
            //    new Category {  Name = "Аренда жилья", Type = OperationType.Discharge },
            //    new Category { Name = "Подписки", Type = OperationType.Discharge },
            //    new Category { Name = "Помощь родителям", Type = OperationType.Discharge },
            //    new Category {Name = "Непредвиденное", Type = OperationType.Discharge },
            //    new Category { Name = "Дом, ремонт", Type = OperationType.Discharge },
            //    new Category { Name = "Страховка", Type = OperationType.Discharge },
            //    new Category {  Name = "Крипта", Type = OperationType.Discharge },
            //    new Category {  Name = "Развлечения", Type = OperationType.Discharge },
            //    new Category {  Name = "Личное", Type = OperationType.Discharge },
            //    new Category {  Name = "Финансовые", Type = OperationType.Discharge },
            //    new Category { Name = "Прочие", Type = OperationType.Discharge },
            //    new Category {  Name = "Зарплата", Type = OperationType.Income },
            //    new Category {  Name = "Фриланс", Type = OperationType.Income },
            //    new Category { Name = "Дивиденды", Type = OperationType.Income },
            //    new Category { Name = "Депозиты", Type = OperationType.Income },
            //    new Category { Name = "Аренда", Type = OperationType.Income },
            //    new Category { Name = "Крипта", Type = OperationType.Income },
            //    new Category { Name = "Бизнес", Type = OperationType.Income },
            //    new Category {  Name = "Услуги", Type = OperationType.Income }
            //    };
            //foreach (var item in list)
            //{
            //    _context.Categories.Add(item);
            //}
            //_context.SaveChanges();
            var list1 = _context.Categories.ToList();
            return null;
        }
    }
}

