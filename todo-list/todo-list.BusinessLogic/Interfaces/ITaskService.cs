using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo_list.Infrastructure.Models;
using todo_list.BusinessLogic.Dtos;

namespace todo_list.BusinessLogic.Interfaces
{
    public interface ITaskService 
    {
        Task<IEnumerable<TaskDto>> GetAllTasksAsync();
        Task<TaskDo> GetTaskByIdAsync(int id);
        Task<TaskDto> GetTaskDtoByIdAsync(int id);
        Task<TaskDo> AddTaskAsync(TaskDto task);
        Task UpdateTaskAsync(TaskDto task, int id);
        Task DeleteTaskAsync(int id);
        TaskDto MapTaskToDto(TaskDo task);
        TaskDo MapDtoToTask(TaskDto taskDto, TaskDo? task = null);
    }
}
