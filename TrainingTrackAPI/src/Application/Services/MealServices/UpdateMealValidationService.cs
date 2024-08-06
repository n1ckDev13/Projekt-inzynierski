using ClassLibrary.DTOs.MealDTO;
using FluentValidation;

namespace TrainingTrackAPI.Application.Services.MealServices
{
    public class UpdateMealValidationService : AbstractValidator<UpdateMealDTO>
    {
        public UpdateMealValidationService()
        {
            RuleFor(x => x.MealName)
                .NotEmpty().WithMessage("Nazwa posiłku nie może być pusta!")
                .Length(1, 150).WithMessage("Nazwa posiłku musi wynosić od 1 do 150 znaków!");
        }
    }
}
