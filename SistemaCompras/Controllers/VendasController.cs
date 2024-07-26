using APICompras.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaCompras.Models;

namespace APICompras.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly Contexto _context;
        public VendasController(Contexto context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Venda>> Get()
        {
            var venda = _context.Vendas.AsNoTracking().ToList();

            if (venda is null)
            {
                return NotFound("Vendas não encontrados...");
            }
            return venda;
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Venda venda)
        {
            if (id != venda.Id)
            {
                return BadRequest();
            }

            _context.Entry(venda).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(venda);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var venda = _context.Vendas.FirstOrDefault(c => c.Id == id);

            if (venda is null)
            {
                return NotFound("Venda não localizado...");
            }
            _context.Vendas.Remove(venda);
            _context.SaveChanges();

            return Ok(venda);
        }

        [HttpPost]
        public ActionResult Post(int id, ItensVenda item)
        {
            var cliente = _context.Clientes.FirstOrDefault(p => p.Id == id);
            //desnecessario

            if (item is null || id == 0)
            {
                return BadRequest();
            }

            var venda = new Venda
            {
                Status = "Aberta",
                ClienteId = id
            };

            _context.Vendas.Add(venda);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterVenda",
                new { id = venda.Id }, venda);
        }

        [HttpGet("{id:int}", Name = "AdicionarItens")]
        public ActionResult<Venda> Get(int id)
        {
            var venda = _context.Vendas.FirstOrDefault(p => p.Id == id);

            if (venda is null)
            {
                return NotFound("Venda não encontrado...");
            }
            return venda;
        }

        [HttpPost]
        public ActionResult Post()
        {
            var venda = new Venda();

            if (venda is null)
            {
                return BadRequest();
            }

            _context.Vendas.Add(venda);
            _context.SaveChanges();

            return new CreatedAtRouteResult("AdicionarCliente",
                new { id = venda.Id }, venda);
        }

    }
}
