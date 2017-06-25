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
    public partial class ViewTask : ContentPage
    {
        public SeznamUkolu publicUkol = new SeznamUkolu();
        public IEnumerable SubjectList { get; set; }
        public DateTime date { get; set; }
        public BindablePicker picker = new BindablePicker();
        public int UmisteniUkolu_ID_fromPicker { get; set; }
        public ViewTask(SeznamUkolu ukol)
        {
            InitializeComponent();

            UmisteniUkolu_ID_fromPicker = 0;

            Name.Text = ukol.Name;
            Comment.Text = ukol.Comment;
            //Date.Text = "Aktuálně je úkol naplánován na datum " + ukol.date.ToString();
            date = DateTime.Parse(ukol.date);
            datepicker.Date = date;
            int dayInWeekNumber = (int)date.DayOfWeek;
            if (dayInWeekNumber == 0)
            {
                dayInWeekNumber = 7;
            }
            refreshPicker(dayInWeekNumber);

            publicUkol = ukol;
        }

        private void DeleteRecord(object sender, EventArgs e)
        {
            App.Database.DeleteItemAsync(publicUkol);

            ViewSchoolTimetableFunction();
        }

        async void ViewSchoolTimetableFunction()
        {
            await Navigation.PushModalAsync(new ViewSchoolTimetable());
        }

        private void EditRecord(object sender, EventArgs e)
        {
            if (Name.Text == null || Name.Text == " " || Name.Text == "" || UmisteniUkolu_ID_fromPicker == 0)
            {
                warningText.Text = "Nejprve zadejte název předmětu, vyberte datum úkolu a přiřaďte ho k příslušné hodině!";
                warningText.IsVisible = true;
            }
            else
            {
                SeznamUkolu editTask = new SeznamUkolu();
                editTask.ID = publicUkol.ID;
                editTask.UmisteniUkolu_ID = UmisteniUkolu_ID_fromPicker;
                editTask.Name = Name.Text;
                editTask.Comment = Name.Text;
                editTask.date = datepicker.Date.ToString("yyyy-MM-dd");

                App.Database.SaveItemAsyncSeznamUkolu(editTask);

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
                pickerLayout.Children.Remove(picker);
            }
            var subjectsFromDb = App.Database.GetItemsNotDoneAsyncPredmetyVRozvrhu(denVtydnu).Result;
            if (subjectsFromDb != null)
            {
                SubjectList = subjectsFromDb;
                picker.ItemsSource = SubjectList;
                picker.ItemSelected += PickerSelected;
                pickerLayout.Children.Add(picker);
            }

        }

        private void PickerSelected(object sender, EventArgs e)
        {
            UmisteniUkolu_ID_fromPicker = ((sender as BindablePicker).SelectedItem as PredmetyVRozvrhu).ID;
        }
    }
}