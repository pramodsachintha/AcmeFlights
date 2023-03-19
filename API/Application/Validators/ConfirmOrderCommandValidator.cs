using API.Application.Commands;
using FluentValidation;

namespace API.Application.Validators
{
    public class ConfirmOrderCommandValidator : AbstractValidator<ConfirmOrderCommand>
    {
        public ConfirmOrderCommandValidator()
        {
            RuleFor(order => order.OrderId)
                .NotNull()
                .NotEmpty()
                .WithMessage("No Order Id Found");
        }
    }
}
