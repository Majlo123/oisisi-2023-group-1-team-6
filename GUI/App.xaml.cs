using GUI.Localization;
using System.Windows;

namespace GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void ChangeLanguage(string lang)
        {
            TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo(lang);
        }
    }
}
