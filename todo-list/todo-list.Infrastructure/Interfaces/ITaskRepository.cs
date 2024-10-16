﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo_list.Infrastructure.Models;

namespace todo_list.Infrastructure.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskDo>> GetAllTasksAsync();
        Task<TaskDo> GetTaskByIdAsync(int id);
        Task AddTaskAsync(TaskDo task);
        Task UpdateTaskAsync(TaskDo task);
        Task DeleteTaskAsync(int id);
    }
}
