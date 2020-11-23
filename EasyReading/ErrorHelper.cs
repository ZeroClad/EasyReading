using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;

namespace EasyReading
{
    class ErrorHelper : IServiceProvider
    {
        private List<ErrorTask> currentTask, newTask;
        private ErrorListProvider currentProvider;

        public ErrorHelper()
        {
            currentTask = new List<ErrorTask>();
            newTask = new List<ErrorTask>();
            currentProvider = GetErrorListProvider();
        }

        public object GetService(Type serviceType)
        {
            return Package.GetGlobalService(serviceType);
        }

        public ErrorListProvider GetErrorListProvider()
        {
            ErrorListProvider provider = new ErrorListProvider(this);//this implementing IServiceProvider
            provider.ProviderName = "EasyReading";
            provider.ProviderGuid = new Guid(EasyReadingPackage.PackageGuidString);
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
            task.ErrorCategory = TaskErrorCategory.Error;
            task.Text = text;
            task.ErrorCategory = errorCategory;
            //The task list does +1 before showing this numbers
            task.Line = line - 1;
            task.Column = column - 1;
            task.Document = document;
            task.Category = category;

            currentProvider.Tasks.Add(task);  //add it to the errorlistprovider
        }

        public void Add(ErrorTask t)
        {
            newTask.Add(t);
        }

        public void Refresh()
        {
            foreach (ErrorTask t in currentTask)
                currentProvider.Tasks.Remove(t);
            currentTask.Clear();
            foreach (ErrorTask t in newTask)
            {
                currentProvider.Tasks.Add(t);
                currentTask.Add(t);
            }
            newTask.Clear();
        }
    }

}
