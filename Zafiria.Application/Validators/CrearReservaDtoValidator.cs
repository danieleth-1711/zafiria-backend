using FluentValidation;
using Zafiria.Application.DTOs;

namespace Zafiria.Application.Validators;

public class CrearReservaDtoValidator : AbstractValidator<CrearReservaDto>
{
    public CrearReservaDtoValidator()
    {
        RuleFor(x => x.NombreCliente)
            .NotEmpty().WithMessage("El nombre del cliente es obligatorio")
            .MaximumLength(100).WithMessage("El nombre no puede tener mas de 100 caracteres");

        RuleFor(x => x.Telefono)
            .NotEmpty().WithMessage("El telefono es obligatorio")
            .Matches(@"^\+?[0-9]{7,15}$").WithMessage("El telefono no es valido");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es obligatorio")
            .EmailAddress().WithMessage("El email no es valido");

        RuleFor(x => x.JoyaId)
            .GreaterThan(0).WithMessage("Debe seleccionar una joya valida");
    }
}