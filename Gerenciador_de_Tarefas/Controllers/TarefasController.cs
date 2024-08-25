using Microsoft.AspNetCore.Mvc;
using Gerenciador_de_Tarefas.httpRequest;
using Gerenciador_de_Tarefas.httpResponse;
using Microsoft.EntityFrameworkCore;


namespace Gerenciador_de_tarefas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {

        private readonly gerenciadorDbContext _contexto;

        public TarefasController(gerenciadorDbContext contexto)
        {
            _contexto = contexto;
        }

        // Rota para adicionar tarefa
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TarefaRequest request)
        {
            if (request == null)
            {
                return BadRequest("Você não pode adicionar uma tarefa vazia");

            }
            var novaTarefa = new Tarefa
            {
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                DataCriacao = DateTime.Now,
                Status = StatusTarefa.Pendente
            };

            // Validando se o titulo é nulo ou não e não permitindo prosseguir caso seja nulo
            if (string.IsNullOrWhiteSpace(novaTarefa.Titulo))
            {

                return BadRequest("Você não pode adicionar uma tarefa vazia");

            }
            else
            {
                _contexto.Tarefas.Add(novaTarefa);
                await _contexto.SaveChangesAsync();
            }

            var response = new TarefaResponse
            {
                Id = novaTarefa.Id,
                Titulo = novaTarefa.Titulo,
                Descricao = novaTarefa.Descricao,
                Status = novaTarefa.Status,
                DataCriacao = novaTarefa.DataCriacao
            };



            return CreatedAtAction(nameof(Create), new { id = response.Id }, response);
        }
        // Fim da rota de adicionar tarefa

        // Rota para trazer todas as tarefas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tarefas = await _contexto.Tarefas.ToListAsync();

            var response = tarefas.Select(t => new TarefaResponse
            {
            
            Id = t.Id,
            Titulo = t.Titulo,
            Status = t.Status,
            DataCriacao = t.DataCriacao,
            DataConclusao = t.DataConclusao
            }).ToList();
            return Ok(response);
        }
        // Fim da rota para trazer todas as tarefas

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] TarefaRequest)
        {
            // Verificar se a tarefa existe, senão retornar não encontrada
            var tarefaExistente = await _contexto.Tarefas.FindAsync(id);



        }
    }
}