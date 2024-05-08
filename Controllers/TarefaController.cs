using desafio_api_net.Context;
using desafio_api_net.Models;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaContext _context;

        public TarefaController(TarefaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult Create(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });
                
            _context.Add(tarefa);
            _context.SaveChanges();

            return CreatedAtAction(nameof(BuscarPorId), new { id = tarefa.Id}, tarefa);
        }

        [HttpGet("{id}")]
        public ActionResult BuscarPorId(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefaBanco.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia." });

            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();

            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
                return NotFound();

            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();

            return NoContent();

        }

        [HttpGet("ListarTodos")]
        public IActionResult List()
        {
            var tarefas = _context.Tarefas.ToList();

            if (tarefas == null)
                return NotFound();

            return Ok(tarefas);
        }

        [HttpGet("BuscarPorTitulo")]
        public IActionResult BuscarPorTitulo(string titulo)
        {
            var tituloTarefa = _context.Tarefas.Where(x => x.Titulo.Contains(titulo));
            return Ok(tituloTarefa);
        }

        [HttpGet("BuscarPorData")]
        public IActionResult BuscarPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);

            return Ok(tarefa);
        }

        [HttpGet("BuscarPorStatus")]
        public IActionResult BuscarPorStatus(EnumStatusTarefa status)
        {
            var statusTarefa = _context.Tarefas.Where(x => x.Status == status);

            return Ok(statusTarefa);
        }

    }
}
