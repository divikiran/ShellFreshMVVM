using System;
using FreshMvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ShellFreshh
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MyShellPage();

            //var page = FreshPageModelResolver.ResolvePageModel<MyShellViewModel>();
            //var basicNavContainer = new FreshNavigationContainer(page);
            //MainPage = basicNavContainer;


            var page = new FreshShellPage();
            page.AddShell<MainViewModel>("Home", "a");
            page.AddShell<AboutViewModel>("About", "b");
            MainPage = page;
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
