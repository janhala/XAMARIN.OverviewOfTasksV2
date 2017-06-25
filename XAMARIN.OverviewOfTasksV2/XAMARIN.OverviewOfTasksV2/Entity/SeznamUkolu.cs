using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XAMARIN.OverviewOfTasksV2.Entity
{
    public class SeznamUkolu
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int UmisteniUkolu_ID { get; set; } //ID from PredmetyVRozvrhu
        public string date { get; set; }
        public SeznamUkolu()
        {
        }

        /*public override string ToString()
        {
            var hourFromDB = App.Database.GetHourFromPredmetyVRozvrhu(UmisteniUkolu_ID).Result;
            var subjectFromDB = App.Database.GetItemsNotDoneAsync(hourFromDB[0].NazevPredmetu_ID).Result;

            return subjectFromDB[0].NazevPredmetu + ", " + hourFromDB[0].Hodina + ".hodina";
        }*/
    }

    public class GroupedViewModelUkolu : ObservableCollection<SeznamUkolu>
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
    }
}
