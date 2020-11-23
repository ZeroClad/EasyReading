using EasyReading.Option;
using Microsoft.VisualStudio.Shell;
using System;
using System.IO;
using System.Text;

namespace EasyReading
{
    class Book
    {
        public EasyReadingOption currentSetting, userSetting;
        private string[] content;
        AsyncPackage package;
        StatusbarHelper statusbar;
        ErrorHelper errorlist;
        string statusbarText = "";
        string bookName = "";

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
            if (p != -1 && p != 1)
                return;
            if (!CheckBookStatus())
                return;
            if (userSetting.ParagraphCount >= content.Length)
                return;

            if (userSetting.Type == ReadingType.ErrorList)
            {
                ErrorListShow(p);
            }
            else
            {
                StatusbarShow(p);
            }
            
            //userSetting.CurrentPage++;
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
            bookName = Path.GetFileName(currentSetting.TxtFilePath);
            return true;
        }

        private bool ReadFile(string path)
        {
            if (!File.Exists(path))
            {
                statusbar.ShowMessage("File not exist!");
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
            // todo: sync user setting and current setting for further beautify.
            userSetting.Save();
        }

        private void StatusbarShow(int p)
        {
            // init errorlist

            if (p == 1)
            {
                while (statusbarText.Length < userSetting.StatusbarTextLength)
                {
                    statusbarText += content[userSetting.ParagraphCount++];
                    statusbarText += userSetting.StatusbarSeparator;
                }
                string showText = statusbarText.Substring(0, userSetting.StatusbarTextLength);
                statusbarText = statusbarText.Substring(userSetting.StatusbarTextLength);
            }
            else
            {
                while (statusbarText.Length < userSetting.StatusbarTextLength)
                {
                    statusbarText += content[userSetting.ParagraphCount++];
                    statusbarText += userSetting.StatusbarSeparator;
                }
                string showText = statusbarText.Substring(0, userSetting.StatusbarTextLength);
                statusbarText = statusbarText.Substring(userSetting.StatusbarTextLength);
            }
            statusbar.ShowMessage("");
        }

        private void ErrorListShow(int p)
        {
            statusbarText = "";
            if (p == -1)
                userSetting.CurrentPage -= 2 * userSetting.ParagraphCount;
            if (userSetting.CurrentPage < 0)
                userSetting.CurrentPage = 0;
            if (userSetting.CurrentPage > content.Length - userSetting.ParagraphCount)
                userSetting.CurrentPage = content.Length - userSetting.ParagraphCount;
            for (int i = 0; i < userSetting.ParagraphCount; i++)
            {
                errorlist.Add(GenerateTask(userSetting.CurrentPage, content[userSetting.CurrentPage++]));
            }
            errorlist.Refresh();
            UpdateSettings();
        }

        private ErrorTask GenerateTask(int line, string text)
        {
            return new ErrorTask
            {
                ErrorCategory = currentSetting.ErrorLevel,
                Text = text,
                Line = line - 1,
                Document = bookName,
                Category = TaskCategory.All
            };
        }
    }
}
