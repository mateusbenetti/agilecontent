using AgileContent.BussinessLogic.Interface.Behaviors;
using FluentValidation.Results;
using System.Collections.Generic;

namespace AgileContent.BussinessLogic.Behaviors
{
    public class BussinessLogic : IBussinessLogic
    {
        public BussinessLogic()
        {
            Errors = new List<ValidationFailure>();
        }

        public IList<ValidationFailure> Errors { get;  set; }

        public bool HasErrors => Errors.Count > 0;
    }
}
