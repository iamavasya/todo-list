using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo_list.BusinessLogic.Interfaces;
using todo_list.Infrastructure.Interfaces;
using todo_list.Infrastructure.Models;
using todo_list.BusinessLogic.Dtos;
using Microsoft.VisualBasic;

namespace todo_list.BusinessLogic.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICategoryService _categoryService;

        public TaskService(ITaskRepository taskRepository, ICategoryService categoryService)
        {
            _taskRepository = taskRepository;
            _categoryService = categoryService;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllTasksAsync();
            return tasks.Select(MapTaskToDto).ToList();
        }

        public async Task<TaskDo> GetTaskByIdAsync(int id)
        {
            return await _taskRepository.GetTaskByIdAsync(id);
        }

        public async Task<TaskDto> GetTaskDtoByIdAsync(int id)
        {
            var task = await GetTaskByIdAsync(id);
            return MapTaskToDto(task);
        }

        public async Task<TaskDo> AddTaskAsync(TaskDto task)
        {
            var taskEntity = MapDtoToTask(task);
            taskEntity.Category = await _categoryService.GetCategoryByIdAsync(taskEntity.CategoryId);
            await _taskRepository.AddTaskAsync(taskEntity);
            return taskEntity;
        }

        public async Task UpdateTaskAsync(TaskDto task, int id)
        {
            var oldTaskEntity = await GetTaskByIdAsync(id);
            var taskEntity = MapDtoToTask(task, oldTaskEntity);
            taskEntity.Category = await _categoryService.GetCategoryByIdAsync(taskEntity.CategoryId);
            await _taskRepository.UpdateTaskAsync(taskEntity);
        }

        public async Task DeleteTaskAsync(int id)
        {
            await _taskRepository.DeleteTaskAsync(id);
        }

        public TaskDto MapTaskToDto(TaskDo task)
        {
            return new TaskDto { Title = task.Title, 
                                 Description = task.Description,
                                 DueDate = task.DueDate,
                                 IsCompleted = task.IsCompleted,
                                 CategoryId = task.CategoryId,
                                 CategoryName = task.Category.Name};
        }

        public TaskDo MapDtoToTask(TaskDto taskDto, TaskDo? task = null)
        {
            if (task != null)
            {
                task.Title = taskDto.Title;
                task.Description = taskDto.Description;
                task.DueDate = taskDto.DueDate;
                task.IsCompleted = taskDto.IsCompleted;
                task.CategoryId = taskDto.CategoryId;
                return task;
            }
            else
            {
                return new TaskDo
                {
                    Title = taskDto.Title,
                    Description = taskDto.Description,
                    DueDate = taskDto.DueDate,
                    IsCompleted = taskDto.IsCompleted,
                    CategoryId = taskDto.CategoryId
                };
            }
        }
    }
}
