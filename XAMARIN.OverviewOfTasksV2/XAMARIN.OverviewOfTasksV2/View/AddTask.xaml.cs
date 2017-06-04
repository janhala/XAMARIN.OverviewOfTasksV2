using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XAMARIN.OverviewOfTasksV2.Controls;
using XAMARIN.OverviewOfTasksV2.Entity;

namespace XAMARIN.OverviewOfTasksV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTask : ContentPage
    {
        public IEnumerable SubjectList { get; set; }
        public DateTime date { get; set; }
        public BindablePicker picker = new BindablePicker();
        public int UmisteniUkolu_ID_fromPicker { get; set; }
        public AddTask()
        {
            InitializeComponent();

            UmisteniUkolu_ID_fromPicker = 0;
        }

        private void SaveTask(object sender, EventArgs e)
        {
            SeznamUkolu seznamUkolu = new SeznamUkolu();
            
            if (nazevUkolu.Text == null || nazevUkolu.Text == " " || nazevUkolu.Text == "" || UmisteniUkolu_ID_fromPicker == 0)
            {
                warningText.Text = "Nejprve zadejte název předmětu, vyberte datum úkolu a přiřaďte ho k příslušné hodině!";
                warningText.IsVisible = true;
            } else
            {
                seznamUkolu.Name = nazevUkolu.Text;
                seznamUkolu.UmisteniUkolu_ID = UmisteniUkolu_ID_fromPicker;
                seznamUkolu.Comment = popisUkolu.Text;
                App.Database.SaveItemAsync(seznamUkolu);

                ViewSchoolTimetableFunction();
            }
            
        }

        private void datepicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            date = e.NewDate;
            int dayInWeekNumber = (int)date.DayOfWeek;
            if (dayInWeekNumber == 0)
            {
                dayInWeekNumber = 7;
            }
            refreshPicker(dayInWeekNumber);
        }

        private void refreshPicker(int denVtydnu)
        {
            if (picker != null)
            {
                addTaskLayout.Children.Remove(picker);
            }
            var subjectsFromDb = App.Database.GetItemsNotDoneAsyncPredmetyVRozvrhu(denVtydnu).Result;
            if (subjectsFromDb != null)
            {
                SubjectList = subjectsFromDb;
                picker.ItemsSource = SubjectList;
                picker.ItemSelected += PickerSelected;
                addTaskLayout.Children.Add(picker);
            }

        }

        private void PickerSelected(object sender, EventArgs e)
        {
            UmisteniUkolu_ID_fromPicker = ((sender as BindablePicker).SelectedItem as PredmetyVRozvrhu).ID;
        }

        async void ViewSchoolTimetableFunction()
        {
            await Navigation.PushModalAsync(new ViewSchoolTimetable());
        }
    }
}