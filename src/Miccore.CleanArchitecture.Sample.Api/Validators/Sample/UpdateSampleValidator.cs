using FluentValidation;
using Miccore.CleanArchitecture.Sample.Application.Commands.Sample;

namespace Miccore.CleanArchitecture.Sample.Api.Validators.Sample
{
    /// <summary>
    /// validator of update
    /// </summary>
    public class UpdateSampleValidator : AbstractValidator<UpdateSampleCommand>
    {
        public UpdateSampleValidator(){
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();

        }
    }
}