using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XAMARIN.OverviewOfTasksV2.Entity;
using XAMARIN.OverviewOfTasksV2.View;

namespace XAMARIN.OverviewOfTasksV2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new XAMARIN.OverviewOfTasksV2.MainPage();

            MainPage = new XAMARIN.OverviewOfTasksV2.View.EnterSubjects();
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

        private static TodoItemDatabase _database;

        public static TodoItemDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new TodoItemDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("SQLiteDatabase9.db3"));
                }
                return _database;
            }
        }
    }
}
