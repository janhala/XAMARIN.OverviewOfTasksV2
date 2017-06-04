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
            //int dayOfWeek = (int)DateTime.Today.DayOfWeek;
            //System.DateTime datimeFirstDayInWeek = GetFirstDayOfWeek(datimeToday);
            //selectedDateLabel.Text = String.Format("{0:d.M.yyyy}", datimeFirstDayInWeek);
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
            //selectedDateLabel.Text = String.Format("{0:d.M.yyyy}", selectedDate);

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
                if (dayFromSubjectsList.Count > 0)
                {
                    var nazevPredmetuDb = App.Database.GetItemsNotDoneAsync(dayFromSubjectsList[0].NazevPredmetu_ID).Result;

                    if (dayFromSubjectsList[0].Den == 1)
                    {
                        pondeliGroup.Add(new SeznamUkolu() { Name = objUkol.Name + "( " + nazevPredmetuDb[0].NazevPredmetu + ", " + dayFromSubjectsList[0].Hodina + ".hodina )", Comment = objUkol.Comment });
                    }
                    if (dayFromSubjectsList[0].Den == 2)
                    {
                        uteryGroup.Add(new SeznamUkolu() { Name = objUkol.Name, Comment = objUkol.Comment });
                    }
                    if (dayFromSubjectsList[0].Den == 3)
                    {
                        stredaGroup.Add(new SeznamUkolu() { Name = objUkol.Name, Comment = objUkol.Comment });
                    }
                    if (dayFromSubjectsList[0].Den == 4)
                    {
                        ctvrtekGroup.Add(new SeznamUkolu() { Name = objUkol.Name, Comment = objUkol.Comment });
                    }
                    if (dayFromSubjectsList[0].Den == 5)
                    {
                        patekGroup.Add(new SeznamUkolu() { Name = objUkol.Name, Comment = objUkol.Comment });
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
            /*SeznamUkolu ukol = new SeznamUkolu();
            object ukolObj = e.SelectedItem;
            ukol = ukolObj;*/

            var firstSelectedItem = e.SelectedItem;

            /*SeznamUkolu ukol = new SeznamUkolu();
            ukol = ((ListView)sender).SelectedItem;*/
            //SeznamUkolu ukol = ((ListView)sender).SelectedItem;

            // nepodarilo se mi vyresit, pokud by byl termin na opravy tak dodelam

        }
    }
}