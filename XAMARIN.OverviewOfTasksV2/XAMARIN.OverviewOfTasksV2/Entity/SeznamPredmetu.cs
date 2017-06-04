using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XAMARIN.OverviewOfTasksV2.Entity
{
    public class SeznamPredmetu
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string NazevPredmetu { get; set; }
        public override string ToString()
        {
            return " Název předmětu: " + NazevPredmetu;
        }
    }
}
