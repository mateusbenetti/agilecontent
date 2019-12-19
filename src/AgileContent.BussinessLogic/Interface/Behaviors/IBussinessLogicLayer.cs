using FluentValidation.Results;
using System.Collections.Generic;

namespace AgileContent.BussinessLogic.Interface.Behaviors
{
    public interface IBussinessLogic
    {
        IList<ValidationFailure> Errors { get; set; }

        bool HasErrors { get; }
    }
}
