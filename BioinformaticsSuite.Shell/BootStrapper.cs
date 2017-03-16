using System.Windows;
using BioinformaticsSuite.Module;
using Prism.Modularity;
using Prism.Unity;

namespace BioinformaticsSuite.Shell
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            var moduleCatalog = (ModuleCatalog) ModuleCatalog;
            moduleCatalog.AddModule(typeof(ModuleInit));
        }

        protected override DependencyObject CreateShell()
        {
            var view = Container.TryResolve<Views.Shell>();
            return view;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window) Shell;
            Application.Current.MainWindow.Show();
        }
    }
}