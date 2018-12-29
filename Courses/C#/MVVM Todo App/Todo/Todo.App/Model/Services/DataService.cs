using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.App.Model.DataEntities;

namespace Todo.App.Model.Services
{
    public class DataService
    {
        private StorageService _storage;

        private Dictionary<int, Category> _categories = new Dictionary<int, Category>();
        private Dictionary<int, TodoItem> _items = new Dictionary<int, DataEntities.TodoItem>();

        public DataService(StorageService storage)
        {
            _storage = storage;            
        }

        public async Task Initialize()
        {
            await _loadData();
        }

        private async Task _loadData()
        {
            var db = await _storage.Load<Database>() ?? new Database();
            _categories = db.Categories.ToDictionary(x => x.Uid);
            _items = db.Items.ToDictionary(x => x.Uid);
        }

        private async Task _saveData()
        {
            var db = new Database
            {
                Categories = _categories.Values.ToArray(), 
                Items = _items.Values.ToArray()
            };

            await _storage.Save(db);
        }


        private async Task _delete<T>(Dictionary<int, T> dict, int uid)
            where T : IEntity
        {
            dict.Remove(uid);
            await _saveData();
        }

        private async Task<T> _save<T>(Dictionary<int, T> dict, T item)
            where T : IEntity
        {
            if ((dict.ContainsKey(item.Uid) && (item.Uid > 0)))
            {
                // update
                dict[item.Uid] = item;
            }
            else
            {
                // new
                var newUid = dict.Keys.Any() ? dict.Keys.Max() + 1 : 1;
                item.Uid = newUid;
                dict.Add(newUid, item);
            }

            await _saveData();
            return item;
        }

        private Task<T> _get<T>(Dictionary<int, T> dict, int uid)
        {
            return Task.FromResult(dict[uid]);
        }

        private Task<IEnumerable<T>> _getAll<T>(Dictionary<int, T> dict, Func<T, bool> condition = null)
        {
            IEnumerable<T> res = dict.Values.ToArray();
            if (condition != null)
                res = res.Where(condition);


            return Task.FromResult(res);
        }


        public Task DeleteCategory(int uid) => _delete(_categories, uid);

        public Task DeleteItem(int uid) => _delete(_items, uid);

        public Task SaveCategory(Category c) => _save(_categories, c);

        public Task SaveItem(TodoItem i) => _save(_items, i);

        public Task<Category> GetCategory(int uid) => _get(_categories, uid);

        public Task<TodoItem> GetItem(int uid) => _get(_items, uid);

        public Task<IEnumerable<Category>> GetCategories() => _getAll(_categories);

        public Task<IEnumerable<TodoItem>> GetItems(int categoryUid) => _getAll(_items, i => i.CategoryUid == categoryUid);

        public async Task<int> GetItemsCount(int categoryUid)
        {
            var res = await GetItems(categoryUid);
            return res.Count();
        }


    }
}
