using B4.PE3.VanLookManu.Domain.Services;
using B4.PE3.VanLookManu.Domain.Services.Local;
using B4.PE3.VanLookManu.Domain.Services.Validator;
using B4.PE3.VanLookManu.ViewModels;
using FreshMvvm;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace B4.PE3.VanLookManu
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //register freshioc depend
            FreshIOC.Container.Register<IUserDataService>(new UserMockDataJson());
            FreshIOC.Container.Register<ILocationUserDataService>(new LocationMockDataJson());
            FreshIOC.Container.Register<Validator>(new Validator());

            //MainPage = new StartPage();
            MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<StartViewModel>());

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
