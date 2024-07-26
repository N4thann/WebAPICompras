using APICompras.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaCompras.Models;

namespace APICompras.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItensVendasController : ControllerBase
    {
        private readonly Contexto _context;
        public ItensVendasController(Contexto context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ItensVenda>> Get()
        {
            var item = _context.ItensVendas.AsNoTracking().ToList();

            if (item is null)
            {
                return NotFound("Itens não encontrados...");
            }
            return item;
        }

        [HttpGet("{id:int}", Name = "ObterItem")]
        public ActionResult<ItensVenda> Get(int id)
        {
            var item = _context.ItensVendas.FirstOrDefault(p => p.Id == id);

            if (item is null)
            {
                return NotFound("Item não encontrado...");
            }
            return item;
        }

        [HttpPost]
        public ActionResult ItemPrecoFinal(int produtoId, int quant)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == produtoId);

            if (produto is null)
            {
                return NotFound("Produto não encontrado...");
            }

            var item = new ItensVenda
            {
                Quantidade = quant,
                ProdutoId = produto.Id,
                Total = quant >= 3 ? quant * produto.Preco * 0.7 : quant * produto.Preco
            };

            _context.ItensVendas.Add(item);
            _context.SaveChanges();

            return Ok(item);
        }      

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var item = _context.ItensVendas.FirstOrDefault(c => c.Id == id);

            if (item is null)
            {
                return NotFound("Item não localizado...");
            }
            _context.ItensVendas.Remove(item);
            _context.SaveChanges();

            return Ok(item);
        }
    }
}
