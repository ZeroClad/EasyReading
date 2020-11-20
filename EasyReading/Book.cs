using EasyReading.Option;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyReading
{
    class Book
    {
        public EasyReadingOption currentSetting, userSetting;
        private string[] content;
        AsyncPackage package;
        StatusbarHelper statusbar;
        ErrorHelper errorlist;

        public Book(AsyncPackage p)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            package = p;
            statusbar = new StatusbarHelper(p);
            errorlist = new ErrorHelper();
            userSetting = EasyReadingOption.Instance;
            //userSetting.TxtFilePath = "ghfjdskagfhuvkdsa";
            currentSetting = userSetting.DeepClone();
            //userSetting.Save();
        }

        public void GotoPage(int p)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (!CheckBookStatus())
                return;
            ErrorListShow(new string[] { content[userSetting.StatusbarCurrentPage] });
            userSetting.StatusbarCurrentPage++;
            userSetting.Save();
        }

        private bool CheckBookStatus()
        {
            if (content == null || content.Length == 0 || currentSetting.TxtFilePath != userSetting.TxtFilePath)
            {
                currentSetting.TxtFilePath = userSetting.TxtFilePath;
                if (!ReadFile(currentSetting.TxtFilePath))
                    return false;
            }
            return true;
        }

        private bool ReadFile(string path)
        {
            //path = "d:\\a.txt";
            if (!File.Exists(path))
            {
                StatusbarShow("File not exist!");
                return false;
            }
            try
            {
                content = File.ReadAllLines(path, Encoding.UTF8);
            }
            catch (Exception e)
            {
               System.Windows.Forms.MessageBox.Show(e.Message);
               return false;
            }
            return true;
        }

        private void UpdateSettings()
        {
        }

        private void StatusbarShow(string s)
        {
            statusbar.ShowMessage(s);
        }

        private void ErrorListShow(string[] s)
        {
            errorlist.Write(TaskCategory.All, TaskErrorCategory.Error, "a", s[0], "a", 1, 1);
        }
    }
}
