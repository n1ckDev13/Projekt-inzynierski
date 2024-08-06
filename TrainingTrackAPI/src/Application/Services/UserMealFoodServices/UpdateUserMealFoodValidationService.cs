using ClassLibrary.DTOs.UserMealFoodDTOs;
using FluentValidation;

namespace TrainingTrackAPI.Application.Services.UserMealFoodServices
{
    public class UpdateUserMealFoodValidationService : AbstractValidator<UpdateUserMealFoodDTO>
    {
        public UpdateUserMealFoodValidationService()
        {
            RuleFor(x => x.QuantityInGrams)
              .GreaterThanOrEqualTo(10).WithMessage("Ilość musi wynosić między 10, a 10000 gramów!")
              .LessThanOrEqualTo(10000).WithMessage("Ilość musi wynosić między 10, a 10000 gramów!");
        }
    }
}
