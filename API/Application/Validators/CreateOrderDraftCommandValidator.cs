using API.Application.Commands;
using FluentValidation;

namespace API.Application.Validators
{
    public class CreateOrderDraftCommandValidator : AbstractValidator<CreateOrderDraftCommand>
    {
        public CreateOrderDraftCommandValidator()
        {
            RuleFor(c => c.CustomerId).NotNull();
            RuleFor(c => c.IsRoundTrip).NotNull();
            RuleFor(c => c.Items).NotNull().WithMessage("No Booking Information Found.");
        }
    }
}
