using System;
using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace ExpenseTrackerApi;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Expense, ExpenseDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => (ExpenseCategoryDto)src.Category));

        CreateMap<ExpenseCreationDto, Expense>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => (ExpenseCategory)src.Category));

        CreateMap<ExpenseUpdationDto, Expense>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => (ExpenseCategory)src.Category));

        CreateMap<UserForRegistrationDto, User>();
    }
}
