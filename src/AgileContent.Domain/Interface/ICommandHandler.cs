using System;
using System.Collections.Generic;
using System.Text;

namespace AgileContent.Domain.Interface
{
    public interface ICommandHandler
    {
        void Execute();
    }
}
