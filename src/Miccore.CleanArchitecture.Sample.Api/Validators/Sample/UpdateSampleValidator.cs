using FluentValidation;
using Miccore.CleanArchitecture.Sample.Application.Commands.Sample;
using Miccore.CleanArchitecture.Sample.Core.Enumerations;

namespace Miccore.CleanArchitecture.Sample.Api.Validators.Sample
{
    /// <summary>
    /// validator of update
    /// </summary>
    public class UpdateSampleValidator : AbstractValidator<UpdateSampleCommand>
    {
        public UpdateSampleValidator(){
            
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("NAME_"+ValidatorEnum.NOT_EMPTY.ToString())
                .NotNull().WithMessage("NAME_"+ValidatorEnum.NOT_NULL.ToString());

        }
    }
}