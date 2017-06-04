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
    public partial class EnterSchoolTimetable : TabbedPage
    {
        public int HodinVPondeli { get; set; }
        public int HodinVUtery { get; set; }
        public int HodinVeStredu { get; set; }
        public int HodinVeCtvrtek { get; set; }
        public int HodinVPatek { get; set; }
        public IEnumerable HoursList { get; set; }

        public EnterSchoolTimetable()
        {
            InitializeComponent();

            HodinVPondeli = 0;
            HodinVUtery = 0;
            HodinVeStredu = 0;
            HodinVeCtvrtek = 0;
            HodinVPatek = 0;

            var hoursFromDb = App.Database.GetItemsAsync().Result;
            HoursList = hoursFromDb;

            var subjectInTimetableFromDb = App.Database.GetItemsAsyncPredmetyVRozvrhu().Result;
            if (subjectInTimetableFromDb.Count > 0)
            {
                NextPage();
            }
        }

        private void AddHourMonday(object sender, EventArgs e)
        {
            HodinVPondeli = HodinVPondeli + 1;
            PondeliStackLayout.Children.Add(new Label { Text = HodinVPondeli + ".hodina : " });
            PondeliStackLayout.Children.Add(new Controls.BindablePicker { ItemsSource = HoursList });
        }

        private void AddHourUtery(object sender, EventArgs e)
        {
            HodinVUtery = HodinVUtery + 1;
            UteryStackLayout.Children.Add(new Label { Text = HodinVUtery + ".hodina : " });
            UteryStackLayout.Children.Add(new Controls.BindablePicker { ItemsSource = HoursList });
        }

        private void AddHourStreda(object sender, EventArgs e)
        {
            HodinVeStredu = HodinVeStredu + 1;
            StredaStackLayout.Children.Add(new Label { Text = HodinVeStredu + ".hodina : " });
            StredaStackLayout.Children.Add(new Controls.BindablePicker { ItemsSource = HoursList });
        }

        private void AddHourCtvrtek(object sender, EventArgs e)
        {
            HodinVeCtvrtek = HodinVeCtvrtek + 1;
            CtvrtekStackLayout.Children.Add(new Label { Text = HodinVeCtvrtek + ".hodina : " });
            CtvrtekStackLayout.Children.Add(new Controls.BindablePicker { ItemsSource = HoursList });
        }

        private void AddHourPatek(object sender, EventArgs e)
        {
            HodinVPatek = HodinVPatek + 1;
            PatekStackLayout.Children.Add(new Label { Text = HodinVPatek + ".hodina : " });
            PatekStackLayout.Children.Add(new Controls.BindablePicker { ItemsSource = HoursList });
        }

        private void SaveAll(object sender, EventArgs e)
        {
            if (HodinVPondeli > 0 || HodinVUtery > 0 || HodinVeStredu > 0 || HodinVeCtvrtek > 0 || HodinVPatek > 0)
            {
                int den = 1;
                int hodina = 0;
                foreach (object child in PondeliStackLayout.Children)
                {
                    int pickerSelectedItem = 0;
                    if (child is BindablePicker && child != null)
                    {
                        hodina = hodina + 1;
                        if ((child as BindablePicker).SelectedItem != null)
                        {
                            pickerSelectedItem = ((SeznamPredmetu)((child as BindablePicker).SelectedItem)).ID;
                        }
                        else
                        {
                            pickerSelectedItem = 1;
                        }

                        PredmetyVRozvrhu predmetyVRozvrhu = new PredmetyVRozvrhu();
                        predmetyVRozvrhu.NazevPredmetu_ID = pickerSelectedItem;
                        predmetyVRozvrhu.Den = den;
                        predmetyVRozvrhu.Hodina = hodina;
                        App.Database.SaveItemAsync(predmetyVRozvrhu);
                    }
                }

                den = 2;
                hodina = 0;
                foreach (object child in UteryStackLayout.Children)
                {
                    int pickerSelectedItem = 0;
                    if (child is BindablePicker && child != null)
                    {
                        hodina = hodina + 1;
                        if ((child as BindablePicker).SelectedItem != null)
                        {
                            pickerSelectedItem = ((SeznamPredmetu)((child as BindablePicker).SelectedItem)).ID;
                        }
                        else
                        {
                            pickerSelectedItem = 1;
                        }

                        PredmetyVRozvrhu predmetyVRozvrhu = new PredmetyVRozvrhu();
                        predmetyVRozvrhu.NazevPredmetu_ID = pickerSelectedItem;
                        predmetyVRozvrhu.Den = den;
                        predmetyVRozvrhu.Hodina = hodina;
                        App.Database.SaveItemAsync(predmetyVRozvrhu);
                    }
                }

                den = 3;
                hodina = 0;
                foreach (object child in StredaStackLayout.Children)
                {
                    int pickerSelectedItem = 0;
                    if (child is BindablePicker && child != null)
                    {
                        hodina = hodina + 1;
                        if ((child as BindablePicker).SelectedItem != null)
                        {
                            pickerSelectedItem = ((SeznamPredmetu)((child as BindablePicker).SelectedItem)).ID;
                        }
                        else
                        {
                            pickerSelectedItem = 1;
                        }

                        PredmetyVRozvrhu predmetyVRozvrhu = new PredmetyVRozvrhu();
                        predmetyVRozvrhu.NazevPredmetu_ID = pickerSelectedItem;
                        predmetyVRozvrhu.Den = den;
                        predmetyVRozvrhu.Hodina = hodina;
                        App.Database.SaveItemAsync(predmetyVRozvrhu);
                    }
                }
                den = 4;
                hodina = 0;
                foreach (object child in CtvrtekStackLayout.Children)
                {
                    int pickerSelectedItem = 0;
                    if (child is BindablePicker && child != null)
                    {
                        hodina = hodina + 1;
                        if ((child as BindablePicker).SelectedItem != null)
                        {
                            pickerSelectedItem = ((SeznamPredmetu)((child as BindablePicker).SelectedItem)).ID;
                        }
                        else
                        {
                            pickerSelectedItem = 1;
                        }

                        PredmetyVRozvrhu predmetyVRozvrhu = new PredmetyVRozvrhu();
                        predmetyVRozvrhu.NazevPredmetu_ID = pickerSelectedItem;
                        predmetyVRozvrhu.Den = den;
                        predmetyVRozvrhu.Hodina = hodina;
                        App.Database.SaveItemAsync(predmetyVRozvrhu);
                    }
                }

                den = 5;
                hodina = 0;
                foreach (object child in PatekStackLayout.Children)
                {
                    int pickerSelectedItem = 0;
                    if (child is BindablePicker && child != null)
                    {
                        hodina = hodina + 1;
                        if ((child as BindablePicker).SelectedItem != null)
                        {
                            pickerSelectedItem = ((SeznamPredmetu)((child as BindablePicker).SelectedItem)).ID;
                        }
                        else
                        {
                            pickerSelectedItem = 1;
                        }

                        PredmetyVRozvrhu predmetyVRozvrhu = new PredmetyVRozvrhu();
                        predmetyVRozvrhu.NazevPredmetu_ID = pickerSelectedItem;
                        predmetyVRozvrhu.Den = den;
                        predmetyVRozvrhu.Hodina = hodina;
                        App.Database.SaveItemAsync(predmetyVRozvrhu);
                    }


                }

                NextPage();
            } else
            {
                warningText.Text = "Nejprve přidejte jednotlivé hodiny a k nim následně přiřaďte předměty!!";
                warningText.IsVisible = true;
            }
        }

        async void NextPage()
        {
            await Navigation.PushModalAsync(new ViewSchoolTimetable());
        }
    }
}