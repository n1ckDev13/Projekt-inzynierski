using ClassLibrary.DTOs.UserDTOs;
using FluentValidation;

namespace TrainingTrackAPI.Application.Services.UserServices
{
    public class UpdateUserValidationService : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserValidationService() 
        {

            RuleFor(x => x.UserName)
    .NotEmpty().WithMessage("Nowa nazwa użytkownika jest wymagana!")
    .MinimumLength(5).WithMessage("Nowa nazwa użytkownika musi zawierać od 5 do 20 znaków!")
    .MaximumLength(20).WithMessage("Nowa nazwa użytkownika musi zawierać od 5 do 20 znaków!")
    .Must(userName => !userName.Contains(" ")).WithMessage("Nowa nazwa użytkownika nie może zawierać spacji!");

RuleFor(x => x.UserEmail)
    .NotEmpty().WithMessage("Nowy email jest wymagany!")
    .EmailAddress().WithMessage("Nieprawidłowy nowy email!")
    .Must(userMail => !userMail.Contains(" ")).WithMessage("Nowy email nie może zawierać spacji!");

// zmiana tylko zwykłych danych
When(x => string.IsNullOrEmpty(x.NewPassword), () =>
{
    RuleFor(x => x.UserPassword)
        .NotEmpty().WithMessage("Pole hasło nie może być puste!");

    RuleFor(x => x.ConfirmUserPassword)
        .Equal(x => x.UserPassword).WithMessage("Hasła nie są zgodne!");
});

// zmiana hasła
When(x => !string.IsNullOrEmpty(x.NewPassword), () =>
{
    RuleFor(x => x.UserPassword)
        .NotEmpty().WithMessage("Pole hasło nie może być puste!");

    RuleFor(x => x.ConfirmUserPassword)
        .Equal(x => x.UserPassword).WithMessage("Obecne hasła nie są zgodne!");

    RuleFor(x => x.NewPassword)
        .NotEmpty().WithMessage("Nowe hasło jest wymagane!")
        .MinimumLength(8).WithMessage("Nowe hasło musi zawierać od 8 do 32 znaków!")
        .MaximumLength(32).WithMessage("Nowe hasło musi zawierać od 8 do 32 znaków!")
        .Matches("[A-Z]").WithMessage("Nowe hasło musi zawierać przynajmniej jedną wielką literę!")
        .Matches("[a-z]").WithMessage("Nowe hasło musi zawierać przynajmniej jedną małą literę!")
        .Matches("[0-9]").WithMessage("Nowe hasło musi zawierać przynajmniej jedną cyfrę!")
        .Matches("[!@#$%^&*(),.?\":{}|<>]").WithMessage("Nowe hasło musi zawierać przynajmniej jeden znak specjalny z " +
            "!@#$%^&*(),.?\\\":{}|<>.")
        .Must(newPassword => !newPassword.Contains(" ")).WithMessage("Nowe hasło nie może zawierać spacji!");
});


        }
    }
}
