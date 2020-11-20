using EasyReading.Option;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyReading
{
    class Book
    {
        public EasyReadingOption currentSetting, userSetting;

        // config related variables
        private string filePath = "";
        public ReadingType Type { get; set; } = ReadingType.ErrorList;

        public int StatusbarCurrentPage { get; set; } = 0;

        public int ErrorListCurrentPage { get; set; } = 0;

        public int ParagraphCount { get; set; } = 3;

        public int DisguiseCount { get; set; } = 0;

        public bool ComplexDisguise { get; set; } = false;
        public TaskErrorCategory ErrorLevel = TaskErrorCategory.Error;

        int StatusbarTextLength;
        int ParagraphLength;
        string StatusbarSeparator;

        public Book()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            currentSetting = new EasyReadingOption();
            userSetting = EasyReadingOption.Instance;

        }

        public void GotoPage(int p)
        {
            CheckBookStatus();

        }

        private void CheckBookStatus()
        {

        }

        private void UpdateSettings()
        {

        }


    }
}
