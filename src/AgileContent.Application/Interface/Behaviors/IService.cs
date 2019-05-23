using FluentValidation.Results;
using System.Collections.Generic;

namespace AgileContent.Application.Interface.Behaviors
{
    public interface IService
    {
        IList<ValidationFailure> Errors { get; set; }

        bool HasErrors { get; }
    }
}
