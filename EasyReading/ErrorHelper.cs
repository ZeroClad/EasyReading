using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace EasyReading
{
    class ErrorHelper : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            return Package.GetGlobalService(serviceType);
        }

        public ErrorListProvider GetErrorListProvider()
        {
            ErrorListProvider provider = new ErrorListProvider(this);//this implementing IServiceProvider
            provider.ProviderName = "EasyReading";
            provider.ProviderGuid = FirstCommand.CommandSet;
            return provider;
        }

        public void Write(
            TaskCategory category,
            TaskErrorCategory errorCategory,
            string context, //used as an indicator when removing
            string text,
            string document,
            int line,
            int column)
        {
            ErrorTask task = new ErrorTask();
            task.Text = text;
            task.ErrorCategory = errorCategory;
            //The task list does +1 before showing this numbers
            task.Line = line - 1;
            task.Column = column - 1;
            task.Document = document;
            task.Category = category;

            GetErrorListProvider().Tasks.Add(task);//add it to the errorlistprovider
        }
    }

}
