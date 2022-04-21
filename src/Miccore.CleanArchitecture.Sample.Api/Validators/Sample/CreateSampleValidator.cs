using FluentValidation;
using Miccore.CleanArchitecture.Sample.Application.Commands.Sample;

namespace Miccore.CleanArchitecture.Sample.Api.Validators.Sample
{
    /// <summary>
    /// validator of creation
    /// </summary>
    public class CreateSampleValidator : AbstractValidator<CreateSampleCommand>
    {
        public CreateSampleValidator(){
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();

        }
    }
}