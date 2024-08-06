using ClassLibrary.DTOs.UserFoodDTOs;
using FluentValidation;

namespace TrainingTrackAPI.Application.Services.UserFoodServices
{
    public class UpdateUserFoodValidationService : AbstractValidator<UpdateUserFoodDTO>
    {
        public UpdateUserFoodValidationService()
        {
            RuleFor(x => x.FoodName)
               .NotEmpty().WithMessage("Nazwa jedzenia nie może być pusta!")
               .Length(1, 150).WithMessage("Nazwa jedzenia musi zawierać między 1, a 150 znaków");

            RuleFor(x => x.ProteinPer100g)
                .GreaterThanOrEqualTo(0.10m).WithMessage("Ilość białka musi wynosić między 0.1, a 1000 gramów!")
               .LessThanOrEqualTo(1000).WithMessage("Ilość białka musi wynosić między 0.1, a 1000 gramów!");

            RuleFor(x => x.CarbsPer100g)
                .GreaterThanOrEqualTo(0.10m).WithMessage("Ilość węglowodanów musi wynosić między 0.1, a 1000 gramów!")
               .LessThanOrEqualTo(1000).WithMessage("Ilość węglowodanów musi wynosić między 0.1, a 1000 gramów!");

            RuleFor(x => x.FatPer100g)
                .GreaterThanOrEqualTo(0.10m).WithMessage("Ilość tłuszczu musi wynosić między 0.1, a 1000 gramów!")
               .LessThanOrEqualTo(1000).WithMessage("Ilość tłuszczu musi wynosić między 0.1, a 1000 gramów!");
        }
    }
}
