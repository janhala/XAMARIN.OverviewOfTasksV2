using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XAMARIN.OverviewOfTasksV2.Entity
{
    public class TodoItemDatabase
    {
        private SQLiteAsyncConnection database;

        public TodoItemDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<SeznamPredmetu>().Wait();
            database.CreateTableAsync<SeznamUkolu>().Wait();
            database.CreateTableAsync<PredmetyVRozvrhu>().Wait();
        }

        // seznam predmetu

        public Task<List<SeznamPredmetu>> GetItemsAsync()
        {
            return database.Table<SeznamPredmetu>().ToListAsync();
        }

        public Task<List<SeznamPredmetu>> GetItemsNotDoneAsyncVolnaHodina()
        {
            return database.QueryAsync<SeznamPredmetu>("SELECT NazevPredmetu FROM SeznamPredmetu WHERE NazevPredmetu = '--volná hodina--'");
        }

        public Task<List<SeznamPredmetu>> GetItemsNotDoneAsync(int id)
        {
            return database.QueryAsync<SeznamPredmetu>("SELECT NazevPredmetu FROM SeznamPredmetu WHERE ID = " + id);
        }

        public Task<List<SeznamPredmetu>> GetItemsNotDoneAsyncForCount()
        {
            return database.QueryAsync<SeznamPredmetu>("SELECT NazevPredmetu FROM SeznamPredmetu");
        }

        public Task<SeznamPredmetu> GetItemAsync(int id)
        {
            return database.Table<SeznamPredmetu>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(SeznamPredmetu item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(SeznamPredmetu item)
        {
            return database.DeleteAsync(item);
        }

        // predmety v rozvrhu
        public Task<int> SaveItemAsync(PredmetyVRozvrhu item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<List<PredmetyVRozvrhu>> GetItemsNotDoneAsyncPredmetyVRozvrhu(int den)
        {
            return database.QueryAsync<PredmetyVRozvrhu>("SELECT * FROM PredmetyVRozvrhu WHERE Den = " + den + " ORDER BY Hodina");
        }

        public Task<List<PredmetyVRozvrhu>> GetItemsNotDoneAsyncPredmetyVRozvrhuDen(int id)
        {
            return database.QueryAsync<PredmetyVRozvrhu>("SELECT Den FROM PredmetyVRozvrhu WHERE ID = " + id + "");
        }

        public Task<List<PredmetyVRozvrhu>> GetItemsAsyncPredmetyVRozvrhu()
        {
            return database.Table<PredmetyVRozvrhu>().ToListAsync();
        }

        // seznam ukolu
        public Task<int> SaveItemAsync(SeznamUkolu item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<List<SeznamUkolu>> GetItemsAsyncSeznamUkolu()
        {
            return database.Table<SeznamUkolu>().ToListAsync();
        }
    }
}
