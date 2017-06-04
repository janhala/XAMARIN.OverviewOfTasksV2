using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XAMARIN.OverviewOfTasksV2.Entity;

namespace XAMARIN.OverviewOfTasksV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnterSubjects : ContentPage
    {
        public int firstSubjectSaved { get; set; }
        public EnterSubjects()
        {
            InitializeComponent();

            firstSubjectSaved = 0;

            var volnaHodina = App.Database.GetItemsNotDoneAsyncVolnaHodina().Result;
            if (volnaHodina.Count == 0)
            {
                SeznamPredmetu hodina = new SeznamPredmetu();
                hodina.NazevPredmetu = "--volná hodina--";
                App.Database.SaveItemAsync(hodina);
            }

            var seznamPredmetu = App.Database.GetItemsNotDoneAsyncForCount().Result;
            if (seznamPredmetu.Count > 1)
            {
                NextPage();
            }

            AddHour();
        }

        public void AddHour()
        {
            warningText.IsVisible = false;

            Entry entry = new Entry();
            entry.Placeholder = "Zadejte název a poté klikněte na Enter";
            entry.Completed += SaveHours;
            StackLayoutMap.Children.Add(entry);
        }

        void SaveHours(object sender, EventArgs e)
        {
            var text = ((Entry)sender).Text;
            if (text == null || text == "" || text == " ")
            {
                warningText.Text = "Nejprve zadejte název předmětu!";
                warningText.IsVisible = true;
            } else
            {
                Label label = new Label();
                label.Text = "Předmět " + text + " byl úspěšně uložen";
                StackLayoutMap.Children.Add(label);

                SeznamPredmetu hodina = new SeznamPredmetu();
                hodina.NazevPredmetu = text;
                App.Database.SaveItemAsync(hodina);

                firstSubjectSaved = 1;

                ((Entry)sender).IsEnabled = false;

                AddHour();
            }
        }

        async void NextPage(object sender, EventArgs e)
        {
            if (firstSubjectSaved == 1)
            {
                //Navigation.PushAsync(new EnterSchoolTimetable());

                //new NavigationPage(new EnterSchoolTimetable());

                await Navigation.PushModalAsync(new EnterSchoolTimetable());

            }
            else
            {
                warningText.Text = "Neuložili jste ani jeden předmět!! Zadejte všechny předměty ve Vašem rozvrhu a teprve poté klikněte na POKRAČOVAT!";
                warningText.IsVisible = true;
            }
        }

        async void NextPage()
        {
            await Navigation.PushModalAsync(new EnterSchoolTimetable());
        }
    }
}