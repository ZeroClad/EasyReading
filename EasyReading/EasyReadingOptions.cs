using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyReading
{
    class EasyReadingOptions : DialogPage
    {
        [Category("Text settings")]
        [DisplayName("Font size")]
        [Description("Size of the greeting text")]
        public int TextSize { get; set; } = 32;

        [Category("Text settings")]
        [DisplayName("Color")]
        [Description("Color of the greeting text")]
        public string TextColor { get; set; } = "Red";
    }
}
