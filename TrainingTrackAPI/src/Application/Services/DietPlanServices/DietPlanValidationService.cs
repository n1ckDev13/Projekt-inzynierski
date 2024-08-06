using ClassLibrary.DTOs.DietPlanDTO;
using FluentValidation;

namespace TrainingTrackAPI.Application.Services.DietPlanServices
{
    public class DietPlanValidationService : AbstractValidator<CreateDietPlanDTO>
    {
        public DietPlanValidationService()
        {
            RuleFor(x => x.PlanName)
                .NotEmpty().WithMessage("Nazwa planu nie może być pusta!")
                .Length(1, 150).WithMessage("Nazwa planu musi mynosić od 1 do 150 znaków!");

            RuleFor(x => x.Calories)
                .GreaterThanOrEqualTo(500).WithMessage("Liczba kalorii musi wynosić między 500, a 20000!")
                .LessThanOrEqualTo(20000).WithMessage("Liczba kalorii musi wynosić między 500, a 20000!");
               
        }
    }
}
