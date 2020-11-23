using Microsoft.VisualStudio.Shell;

namespace EasyReading.Option
{
    internal class EasyReadingOptionPage<T> : DialogPage where T : BaseOption<T>, new()
    {
        private BaseOption<T> _model;

        public EasyReadingOptionPage()
        {
#pragma warning disable VSTHRD104 // Offer async methods
            _model = ThreadHelper.JoinableTaskFactory.Run(BaseOption<T>.CreateAsync);
#pragma warning restore VSTHRD104 // Offer async methods
        }

        public override object AutomationObject => _model;

        public override void LoadSettingsFromStorage()
        {
            _model.Load();
        }

        public override void SaveSettingsToStorage()
        {
            _model.Save();
        }
    }

}
