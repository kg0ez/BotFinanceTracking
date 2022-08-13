using System;
using AutoMapper;
using Bot.Common.Dto;
using Bot.Models.Models;

namespace Bot.BusinessLogic.Services.Interfaces
{
	public interface ICategoryType
	{
		List<Operation> Get();
		List<CategoryDto> Get(int type);

		IMapper Mapper { get; set; }
		int PageCount { get; set; }
	}
}

