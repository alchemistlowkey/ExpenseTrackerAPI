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
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => (ExpenseCategory)src.Category))
            // Let the entity default handle CreatedAt & UpdatedAt — don't try to parse the formatted strings
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<ExpenseUpdationDto, Expense>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => (ExpenseCategory)src.Category))
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<UserForRegistrationDto, User>();
    }
}
