using ClassLibrary.DTOs.MealFoodsDTOs;
using FluentValidation;

namespace TrainingTrackAPI.Application.Services.MealFoodServices
{
    public class CreateMealFoodValidationService : AbstractValidator<CreateMealFoodDTO>
    {
        public CreateMealFoodValidationService()
        {
            RuleFor(x => x.QuantityInGrams)
               .GreaterThanOrEqualTo(10).WithMessage("Ilość musi wynosić między 10, a 10000 gramów!")
               .LessThanOrEqualTo(10000).WithMessage("Ilość musi wynosić między 10, a 10000 gramów!");
        }
    }
}
