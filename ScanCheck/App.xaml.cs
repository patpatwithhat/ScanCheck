using System.Windows;

namespace ScanCheck
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private AppBootstrapper? _bootstrapper;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _bootstrapper = new AppBootstrapper();
        }
    }


}
