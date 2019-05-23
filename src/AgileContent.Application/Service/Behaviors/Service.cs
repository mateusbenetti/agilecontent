using AgileContent.Application.Interface.Behaviors;
using FluentValidation.Results;
using System.Collections.Generic;

namespace AgileContent.Application.Service.Behaviors
{
    public class Service : IService
    {
        public Service()
        {
            Errors = new List<ValidationFailure>();
        }

        public IList<ValidationFailure> Errors { get;  set; }

        public bool HasErrors => Errors.Count > 0;
    }
}
