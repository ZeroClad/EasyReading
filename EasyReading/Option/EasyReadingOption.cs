using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyReading.Option
{
    class EasyReadingOption : BaseOption<EasyReadingOption>
    {
        [Category("Overall setting")]
        [DisplayName("Reading Type")]
        [Description("Select reading type.")]
        [DefaultValue(ReadingType.ErrorList)]
        public ReadingType Type { get; set; } = ReadingType.ErrorList;

        [Category("Overall setting")]
        [DisplayName("File Path")]
        [Description("Input full txt file path.")]
        public string TxtFilePath { get; set; } = "";

        [Category("Statusbar setting")]
        [DisplayName("Current Page")]
        [Description("Current page of the book in statusbar mode.")]
        [DefaultValue(0)]
        public int StatusbarCurrentPage { get; set; } = 0;

        [Category("Statusbar setting")]
        [DisplayName("Length")]
        [Description("The length of each line.")]
        [DefaultValue(50)]
        public int StatusbarTextLength { get; set; } = 50;

        [Category("Statusbar setting")]
        [DisplayName("Paragraph Separator")]
        [Description("the separator between paragraphs.")]
        [DefaultValue(" ■■ ")]
        public string StatusbarSeparator { get; set; } = " ■■ ";

        [Category("Statusbar setting")]
        [DisplayName("Current Page")]
        [Description("Current page of the book in error list mode.")]
        [DefaultValue(0)]
        public int ErrorListCurrentPage { get; set; } = 0;

        [Category("Error list setting")]
        [DisplayName("Paragraph Count")]
        [Description("The number of paragraphs to show.")]
        [DefaultValue(3)]
        public int ParagraphCount { get; set; } = 3;

        [Category("Error list setting")]
        [DisplayName("Paragraph Maximum Length")]
        [Description("The length of each paragraph.")]
        [DefaultValue(100)]
        public int ParagraphLength { get; set; } = 100;

        [Category("Error list setting")]
        [DisplayName("Disguise Count")]
        [Description("The number of disguised messages.")]
        [DefaultValue(0)]
        public int DisguiseCount { get; set; } = 0;

        [Category("Error list setting")]
        [DisplayName("Complex Disguise")]
        [Description("Whether mix disguised message with paragraphs.")]
        [DefaultValue(false)]
        public bool ComplexDisguise { get; set; } = false;

        [Category("Error list setting")]
        [DisplayName("Error Level")]
        [Description("Whether mix disguised message with paragraphs.")]
        [DefaultValue(false)]
        public TaskErrorCategory ErrorLevel { get; set; } = TaskErrorCategory.Error;

    }

    public enum ReadingType
    {
        StatusBar,
        ErrorList
    }
}
