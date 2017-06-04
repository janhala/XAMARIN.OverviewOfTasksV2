using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XAMARIN.OverviewOfTasksV2.Entity
{
    public class PredmetyVRozvrhu
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int NazevPredmetu_ID { get; set; }
        public int Den { get; set; }
        public int Hodina { get; set; }
        public string denText { get; set; }

        public override string ToString()
        {
            var subjectNameFromDB = App.Database.GetItemsNotDoneAsync(NazevPredmetu_ID).Result;
            if (Den == 1)
            {
                denText = "pondělí";
            }
            if (Den == 2)
            {
                denText = "úterý";
            }
            if (Den == 3)
            {
                denText = "středa";
            }
            if (Den == 4)
            {
                denText = "čtvrtek";
            }
            if (Den == 5)
            {
                denText = "pátek";
            }
            return "ID - " + ID + " Název předmětu - " + subjectNameFromDB[0].NazevPredmetu + ", " + denText + " " + Hodina + ".hodina";
        }
    }
}
