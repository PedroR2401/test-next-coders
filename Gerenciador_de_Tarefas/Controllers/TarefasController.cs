using Microsoft.AspNetCore.Mvc;

namespace Gerenciador_de_tarefas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Tarefa blablabla");
        }
    }
}