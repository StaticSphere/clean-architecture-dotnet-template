using FluentValidation;

namespace CleanArchTemplate.Application.Endpoints.People.Commands
{
    public class AddPersonCommandValidator : AbstractValidator<AddPersonCommand>
    {
        public AddPersonCommandValidator()
        {
            RuleFor(x => x.Id)
                .Null();

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Length(1, 100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .Length(1, 100);
        }
    }
}
