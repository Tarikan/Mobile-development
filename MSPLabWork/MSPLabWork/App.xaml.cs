using MSPLabWork.Data;
using MSPLabWork.Services;
using System;
using System.IO;
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

        private static AppDB _db;

        public static AppDB DataBase
        {
            get
            {
                if (_db == null)
                {
                    _db = new AppDB(Path
                        .Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DB.db3"));
                }
                return _db;
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
