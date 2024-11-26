using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task.Models;

namespace task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private static List<ModelTask> ModelTasks = new List<ModelTask>();

        [HttpGet]
        public ActionResult<List<ModelTask>> SearchTasks()
        {
            return Ok(ModelTasks);
        }

        [HttpPost]
        public ActionResult<ModelTask> AddTask([FromBody] ModelTask newTask)
        {
            if (newTask == null)
            {
                return BadRequest("A tarefa não pode ser nula");
            }

            ModelTasks.Add(newTask);

            return CreatedAtAction(nameof(SearchTasks), new { id = newTask.Id }, newTask);
        }

        [HttpDelete("{Id}")]
        public ActionResult<ModelTask> RemoverTask(int Id)
        {
            var pesquisa = ModelTasks.Find(x => x.Id == Id);

            if (pesquisa == null)
                return NotFound("Tarefa não encontrada");

            ModelTasks.Remove(pesquisa);

            return Ok(pesquisa); // Retorna a tarefa removida
        }
    }
}
