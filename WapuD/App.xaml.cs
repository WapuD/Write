namespace WapuD
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            WapuD.ViewModelLocator.Init();
            base.OnStartup(e);
        }
    }
}
