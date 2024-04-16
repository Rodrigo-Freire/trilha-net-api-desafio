using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var tarefa = _context.Tarefas.Find(id); // Buscar a tarefa pelo ID no banco de dados

            if (tarefa == null) return NotFound(); // Se não encontrar a tarefa, retornar NotFound

            return Ok(tarefa); // Caso contrário, retornar OK com a tarefa encontrada
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var todasAsTarefas = _context.Tarefas.ToList(); // Buscar todas as tarefas no banco de dados

            return Ok(todasAsTarefas); // Retornar OK com a lista de tarefas encontradas
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            var tarefasComTitulo = _context.Tarefas
                .Where(t => t.Titulo.Contains(titulo)) // Filtrar tarefas pelo título
                .ToList();

            return Ok(tarefasComTitulo); // Retornar OK com a lista de tarefas encontradas
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            var tarefasComStatus = _context.Tarefas
                .Where(t => t.Status == status) // Filtrar tarefas pelo status
                .ToList();

            return Ok(tarefasComStatus); // Retornar OK com a lista de tarefas encontradas
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            _context.Tarefas.Add(tarefa); // Adicionar a tarefa recebida no EF
            _context.SaveChanges(); // Salvar as mudanças no banco de dados

            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            // Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            _context.Tarefas.Remove(tarefaBanco); // Remover a tarefa encontrada
            _context.SaveChanges(); // Salvar as mudanças no banco de dados

            return NoContent(); // Retornar NoContent (204) para indicar que a tarefa foi removida com sucesso
        }
    }
}
