using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo_list.Infrastructure.Interfaces;
using todo_list.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using todo_list.Infrastructure.Data;

namespace todo_list.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TodoDbContext _context;

        public TaskRepository(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskDo>> GetAllTasksAsync()
        {
            return await _context.Tasks.Include(t => t.Category).ToListAsync();
        }

        public async Task<TaskDo> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTaskAsync(TaskDo task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(TaskDo task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
