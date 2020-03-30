using GestionFC.SqLite.DBModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFC.SqLite
{
    public class GestionFCDataBase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public GestionFCDataBase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(GestionFCModel).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(GestionFCModel)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<GestionFCModel>> GetGestionFCItemAsync()
        {
            return Database.Table<GestionFCModel>().ToListAsync();
        }

        //public Task<List<TodoItem>> GetItemNotDoneAsync()
        //{
        //    // SQL queries are also possible
        //    return Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        //}

        //public Task<TodoItem> GetItemAsync(int id)
        //{
        //    return Database.Table<TodoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        //}

        //public Task<int> SaveItemAsync(GestionFCModel item)
        //{
        //    if (item.UserSaved != 0)
        //    {
        //        return Database.UpdateAsync(item);
        //    }
        //    else
        //    {
        //        return Database.InsertAsync(item);
        //    }
        //}

        //public Task<int> DeleteItemAsync(GestionFCModel item)
        //{
        //    return Database.DeleteAsync(item);
        //}

        public Task<int> SaveGestionFCItemAsync(GestionFCModel item)
        {
            Database.DeleteAllAsync<GestionFCModel>();
            return Database.InsertAsync(item);
        }

        public Task<int> DeleteAllAsync()
        {
            return Database.DeleteAllAsync<GestionFCModel>();
        }
    }
}
