using Microsoft.AspNetCore.Mvc;
using Gerenciador_de_Tarefas.httpRequest;
using Gerenciador_de_Tarefas.httpResponse;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;


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
        [HttpPost("CriarTarefa")]
        public async Task<IActionResult> Criar([FromBody] TarefaRequest request)
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



            return CreatedAtAction(nameof(Criar), new { id = response.Id }, response);
        }
        // Fim da rota de adicionar tarefa

        // Rota para trazer todas as tarefas
        [HttpGet("MostrarTarefas")]
        public async Task<IActionResult> MostarTudo()
        {
            var tarefas = await _contexto.Tarefas.ToListAsync();

            var response = tarefas.Select(t => new TarefaResponse
            {

                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                Status = t.Status,
                DataCriacao = t.DataCriacao,
                DataConclusao = t.DataConclusao
            }).ToList();
            return Ok(response);
        }
        // Fim da rota para trazer todas as tarefas

        // Começo da rota de atualizar as tarefas
        [HttpPut("Atualizar/{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] TarefaRequest request)
        {
            // Verificar se a tarefa existe, senão retornar não encontrada
            var tarefaExistente = await _contexto.Tarefas.FindAsync(id);
            if (tarefaExistente == null)
            {
                return NotFound(new { mensagem = "Essa tarefa não foi encontrada" });

            }

            tarefaExistente.Titulo = request.Titulo;
            tarefaExistente.Descricao = request.Descricao;
            tarefaExistente.Status = request.Status;
            tarefaExistente.DataConclusao = request.DataConclusao;


            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao atualizar a tarefa.", detalhe = ex.Message });
            }
            return NoContent();
        }
        // Fim da rota de alterar

        // Rota para filtrar as tarefas por Status
        [HttpGet("Filtrar")]
        public async Task<ActionResult<IEnumerable<TarefaResponse>>> FiltrarTarefas(int status)
        {
            if (status > 1)
            {
                return BadRequest("O numero que você digitou não é valido");
            }

            var tarefas = await _contexto.Tarefas

            .Where(t => t.Status == (StatusTarefa)status)
            .Select(t => new TarefaResponse
            {

                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                Status = t.Status,
                DataCriacao = t.DataCriacao,
                DataConclusao = t.DataConclusao

            }).ToListAsync();

            if (tarefas.Count == 0)
            {
                return NotFound("Nenhuma tarefa encontrada com o status especificado. ");
            }

            return Ok(tarefas);

        }
        // Fim do filtrar


        [HttpDelete("Deletar/{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            // Verificar se a tarefa existe, senão retornar não encontrada
            var tarefa = await _contexto.Tarefas.FindAsync(id);

            if (tarefa == null)
            {
                return NotFound(new { mensagem = "Essa tarefa não foi encontrada" });

            }

            _contexto.Tarefas.Remove(tarefa);

            await _contexto.SaveChangesAsync();

            // Retornando que foi bem sucedido mas não há o que retornar
            return NoContent();

        }




    }
}