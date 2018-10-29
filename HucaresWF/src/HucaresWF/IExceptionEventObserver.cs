using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HucaresWF
{
    public interface IExceptionEventObserver
    {
        void HandleExceptionEvent(object sender, ExceptionEventArgs args);
    }
}
