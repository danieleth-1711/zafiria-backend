using FluentValidation;
using Zafiria.Application.DTOs;

namespace Zafiria.Application.Validators;

public class CrearJoyaDtoValidator : AbstractValidator<CrearJoyaDto>
{
    public CrearJoyaDtoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .MaximumLength(100).WithMessage("El nombre no puede tener mas de 100 caracteres");

        RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("La descripcion es obligatoria");

        RuleFor(x => x.Precio)
            .GreaterThan(0).WithMessage("El precio debe ser mayor a 0");

        RuleFor(x => x.Material)
            .NotEmpty().WithMessage("El material es obligatorio");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo");

        RuleFor(x => x.CategoriaId)
            .GreaterThan(0).WithMessage("Debe seleccionar una categoria valida");

        RuleFor(x => x.OcasionId)
            .GreaterThan(0).WithMessage("Debe seleccionar una ocasion valida");
    }
}