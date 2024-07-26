using APICompras.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaCompras.Models;

namespace APICompras.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly Contexto _context;
        public ClientesController(Contexto context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            var cliente = _context.Clientes.AsNoTracking().ToList();

            if (cliente is null)
            {
                return NotFound("Clientes não encontrados...");
            }
            return cliente;
        }

        [HttpGet("{id:int}", Name = "ObterCliente")]
        public ActionResult<Cliente> Get(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(p => p.Id == id);

            if (cliente is null)
            {
                return NotFound("Cliente não encontrado...");
            }
            return cliente;
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(cliente);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.Id == id);

            if (cliente is null)
            {
                return NotFound("Cliente não localizado...");
            }
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();

            return Ok(cliente);
        }

        [HttpPost]
        public ActionResult Post(Cliente cliente)
        {

            if (cliente is null)
            {
                return BadRequest();
            }

            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCliente",
                new { id = cliente.Id }, cliente);
        }
    }
}
