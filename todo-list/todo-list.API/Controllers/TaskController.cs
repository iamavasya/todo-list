using Microsoft.AspNetCore.Mvc;
using todo_list.BusinessLogic.Interfaces;
using todo_list.Infrastructure.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace todo_list.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/Task
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDo>>> Get()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        // GET api/Task/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDo>> Get(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        // POST api/Task
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TaskDo task)
        {
            await _taskService.AddTaskAsync(task);
            return CreatedAtAction(nameof(Get), new { id = task.Id }, task);
        }

        // PUT api/Task/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TaskDo task)
        {
            task.Id = id;
            await _taskService.UpdateTaskAsync(task);
            return NoContent();
        }

        // DELETE api/Task/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}
