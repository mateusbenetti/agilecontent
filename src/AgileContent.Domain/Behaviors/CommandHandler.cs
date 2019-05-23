using AgileContent.Domain.Behaviors.Interface;
using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace AgileContent.Domain.Behaviors
{
    public abstract class CommandHandler<TDto, TValidator, TResult> : ICommandHandler<TDto, TValidator, TResult>
        where TValidator : IValidator<TDto>, new()
    {
        readonly TValidator _validator;
        public CommandHandler()
        {
            _validator = new TValidator();
        }
        public void SetDto(TDto dto)
        {
            Dto = dto;
        }
        public TDto Dto { get; private set; }
        protected TResult Result { get; set; }
        public  TResult GetResult()
        {
            return Result;
        }
        public ValidationResult Valid()
        {
            var result = _validator.Validate(Dto);
            return result;
        }

        public abstract void Execute();
    }
}
