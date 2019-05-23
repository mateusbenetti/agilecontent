using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace AgileContent.Domain.Behaviors.Interface
{
    public interface ICommandHandler<TDto, TValidator, out TResult> where TValidator : IValidator<TDto>
    {
        void SetDto(TDto dto);
        void Execute();
        ValidationResult Valid();
        TResult GetResult();
    }
}
