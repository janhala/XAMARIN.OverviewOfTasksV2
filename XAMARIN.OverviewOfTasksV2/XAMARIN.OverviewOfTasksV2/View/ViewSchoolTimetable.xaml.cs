using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XAMARIN.OverviewOfTasksV2.Entity;

namespace XAMARIN.OverviewOfTasksV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewSchoolTimetable : ContentPage
    {
        public DateTime selectedDate { get; set; }
        public DateTime endDate { get; set; }
        private ObservableCollection<GroupedViewModelUkolu> grouped { get; set; }
        public ViewSchoolTimetable()
        {
            InitializeComponent();



            System.DateTime datimeToday = DateTime.Now;
            selectedDate = datimeToday.Date;
            int dayOfWeek = ((int)selectedDate.DayOfWeek == 0) ? 7 : (int)selectedDate.DayOfWeek;
            selectedDate = selectedDate.AddDays((-dayOfWeek) + 1);

            RefreshView(selectedDate);

        }

        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
            return GetFirstDayOfWeek(dayInWeek, defaultCultureInfo);
        }

        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            firstDayInWeek = firstDayInWeek.AddDays(1);
            return firstDayInWeek;
        }

        private void PredchoziTyden(object sender, EventArgs e)
        {
            selectedDate = selectedDate.AddDays(-7);
            RefreshView(selectedDate);
        }

        private void DalsiTyden(object sender, EventArgs e)
        {
            selectedDate = selectedDate.AddDays(7);
            RefreshView(selectedDate);
        }

        public void RefreshView(DateTime selectedDate)
        {
            endDate = selectedDate.AddDays(6);
            string startDateString = selectedDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            selectedDateLabel.Text = selectedDate.ToString("d.M.yyyy") + " - " + endDate.ToString("d.M.yyyy");

            grouped = new ObservableCollection<GroupedViewModelUkolu>();
            var pondeliGroup = new GroupedViewModelUkolu() { LongName = "Pondělí", ShortName = "Pondělí" };
            var uteryGroup = new GroupedViewModelUkolu() { LongName = "Úterý", ShortName = "Úterý" };
            var stredaGroup = new GroupedViewModelUkolu() { LongName = "Středa", ShortName = "Středa" };
            var ctvrtekGroup = new GroupedViewModelUkolu() { LongName = "Čtvrtek", ShortName = "Čtvrtek" };
            var patekGroup = new GroupedViewModelUkolu() { LongName = "Pátek", ShortName = "Pátek" };

            var seznamUkolu = App.Database.GetItemsNotDoneAsyncSeznamUkolu(startDateString, endDateString).Result;
            foreach (SeznamUkolu objUkol in seznamUkolu)
            {
                var dayFromSubjectsList = App.Database.GetItemsNotDoneAsyncPredmetyVRozvrhuDen(objUkol.UmisteniUkolu_ID).Result;

                var hourFromDB = App.Database.GetHourFromPredmetyVRozvrhu(objUkol.UmisteniUkolu_ID).Result;
                var subjectFromDB = App.Database.GetItemsNotDoneAsync(hourFromDB[0].NazevPredmetu_ID).Result;

                if (dayFromSubjectsList.Count > 0)
                {
                    if (dayFromSubjectsList[0].Den == 1)
                    {
                        pondeliGroup.Add(new SeznamUkolu() { Name = objUkol.Name + " | " + subjectFromDB[0].NazevPredmetu + ", " + hourFromDB[0].Hodina + ".hodina", Comment = objUkol.Comment, ID = objUkol.ID, date = objUkol.date, UmisteniUkolu_ID = objUkol.UmisteniUkolu_ID });
                    }
                    if (dayFromSubjectsList[0].Den == 2)
                    {
                        uteryGroup.Add(new SeznamUkolu() { Name = objUkol.Name + " | " + subjectFromDB[0].NazevPredmetu + ", " + hourFromDB[0].Hodina + ".hodina", Comment = objUkol.Comment, ID = objUkol.ID, date = objUkol.date, UmisteniUkolu_ID = objUkol.UmisteniUkolu_ID });
                    }
                    if (dayFromSubjectsList[0].Den == 3)
                    {
                        stredaGroup.Add(new SeznamUkolu() { Name = objUkol.Name + " | " + subjectFromDB[0].NazevPredmetu + ", " + hourFromDB[0].Hodina + ".hodina", Comment = objUkol.Comment, ID = objUkol.ID, date = objUkol.date, UmisteniUkolu_ID = objUkol.UmisteniUkolu_ID });
                    }
                    if (dayFromSubjectsList[0].Den == 4)
                    {
                        ctvrtekGroup.Add(new SeznamUkolu() { Name = objUkol.Name + " | " + subjectFromDB[0].NazevPredmetu + ", " + hourFromDB[0].Hodina + ".hodina", Comment = objUkol.Comment, ID = objUkol.ID, date = objUkol.date, UmisteniUkolu_ID = objUkol.UmisteniUkolu_ID });
                    }
                    if (dayFromSubjectsList[0].Den == 5)
                    {
                        patekGroup.Add(new SeznamUkolu() { Name = objUkol.Name + " | " + subjectFromDB[0].NazevPredmetu + ", " + hourFromDB[0].Hodina + ".hodina", Comment = objUkol.Comment, ID = objUkol.ID, date = objUkol.date, UmisteniUkolu_ID = objUkol.UmisteniUkolu_ID });
                    }
                }
            }

            grouped.Add(pondeliGroup); grouped.Add(uteryGroup); grouped.Add(stredaGroup); grouped.Add(ctvrtekGroup); grouped.Add(patekGroup);
            lstView.ItemsSource = grouped;
        }

        async void AddTaskFunction(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddTask());
        }

        private void lstView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var SelectedItem = e.SelectedItem;

            SeznamUkolu ukol = e.SelectedItem as SeznamUkolu;
            var tst = ukol.ID;

            ViewTaskFunction(ukol);
        }

        async void ViewTaskFunction(SeznamUkolu ukol)
        {
            await Navigation.PushModalAsync(new ViewTask(ukol));
        }
    }
}