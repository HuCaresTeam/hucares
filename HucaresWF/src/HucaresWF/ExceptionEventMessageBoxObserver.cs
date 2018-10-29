using System.Windows.Forms;

namespace HucaresWF
{
    public class ExceptionEventMessageBoxObserver : IExceptionEventObserver
    {
        public void HandleExceptionEvent(object sender, ExceptionEventArgs args)
        {
            MessageBox.Show(args.ExceptionMessage);
        }
    }
}
