using FluentValidation;
using Miccore.CleanArchitecture.Sample.Application.Commands.Sample;
using Miccore.CleanArchitecture.Sample.Core.Enumerations;

namespace Miccore.CleanArchitecture.Sample.Api.Validators.Sample
{
    /// <summary>
    /// validator of creation
    /// </summary>
    public class CreateSampleValidator : AbstractValidator<CreateSampleCommand>
    {
        public CreateSampleValidator(){
            
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("NAME_"+ValidatorEnum.NOT_EMPTY.ToString())
                .NotNull().WithMessage("NAME_"+ValidatorEnum.NOT_NULL.ToString());

        }
    }
}