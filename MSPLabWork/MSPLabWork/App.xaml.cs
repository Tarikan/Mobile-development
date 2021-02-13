using MSPLabWork.Services;
using Xamarin.Forms;

namespace MSPLabWork
{
    public partial class App : Application
    {
        private static MovieStore dataService;
        
        public static MovieStore DataStore
        {
            get
            {
                if (dataService == null)
                {
                    dataService = new MovieStore();
                }
                return dataService;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
