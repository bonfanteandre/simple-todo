using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Business.Entities;

namespace Todo.Business.Repositories
{
    public interface ITodoItemRepository
    {
        Task AddAsync(TodoItem item);
        void Update(TodoItem item);
        void Remove(TodoItem item);
        Task<TodoItem> FindAsync(int id);
        Task<IEnumerable<TodoItem>> ByStatusAsync(TodoItemStatus status);
    }
}
