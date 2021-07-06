using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Business.Entities;
using Todo.Business.Repositories;
using Todo.Data.Context;

namespace Todo.Data.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoContext _context;

        public TodoItemRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TodoItem item)
        {
            await _context.TodoItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public void Update(TodoItem item)
        {
            _context.TodoItems.Update(item);
            _context.SaveChanges();
        }

        public void Remove(TodoItem item)
        {
            _context.TodoItems.Remove(item);
            _context.SaveChanges();
        }

        public async Task<TodoItem> FindAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<IEnumerable<TodoItem>> ByStatusAsync(TodoItemStatus status)
        {
            return await _context.TodoItems
                .AsNoTracking()
                .Where(i => i.Status == status)
                .OrderByDescending(i => i.Id)
                .ToListAsync();
        }
    }
}
