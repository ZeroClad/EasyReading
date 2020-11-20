using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyReading
{
    class StatusbarHelper
    {
        private readonly AsyncPackage package;
        private IVsStatusbar statusBar;

        public StatusbarHelper(AsyncPackage p)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            package = p ?? throw new ArgumentNullException(nameof(package));
            statusBar = package.GetService<SVsStatusbar, IVsStatusbar>();
        }

        public void ShowMessage(string m)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            int frozen;
            statusBar.IsFrozen(out frozen);

            if (frozen != 0)
                return;
            
            // Set the status bar text and make its display static.
            statusBar.SetText(m);

            //statusBar.Clear();
            ErrorHelper eh = new ErrorHelper();
            eh.Write(TaskCategory.All, TaskErrorCategory.Error, "context", "text", "document", 123, 321);
        }
    }
}
