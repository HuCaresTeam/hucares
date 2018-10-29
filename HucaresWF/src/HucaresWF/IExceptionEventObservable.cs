using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HucaresWF
{
    public interface IExceptionEventObservable
    {
        event EventHandler<ExceptionEventArgs> ExceptionEventHandler;

        void InvokeHandler(ExceptionEventArgs eventArgs);
    }
}
