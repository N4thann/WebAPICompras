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

        //Mostra a venda com a possibilidade de adicionar e remover os itens
        [HttpGet("{id:int}", Name = "ObterVenda")]
        public ActionResult<Venda> Get(int id)
        {
            var venda = _context.Vendas.Include(v => v.ListaDeItens).FirstOrDefault(v => v.Id == id);
           
            if (venda is null)
            {
                return NotFound("Venda não encontrado...");
            }
            return venda;
        }
        
        //Gerar uma venda nova
        [HttpPost]
        public ActionResult Post()
        {
            var venda = new Venda
            {
                Status = "Aberta",
                Total = 0
            };

            _context.Vendas.Add(venda);
            _context.SaveChanges();

            return new CreatedAtRouteResult("AdicionarCliente",
                new { id = venda.Id }, venda);
        }

        //Adicionar um cliente a venda que foi aberta
        [HttpPut("{vendaId}/AdicionarCliente")]
        public ActionResult AdicionarCliente(int vendaId, int clienteId)
        {
            var venda = _context.Vendas.Find(vendaId);
            if (venda == null)
            {
                return NotFound("Venda não encontrada.");
            }

            var cliente = _context.Clientes.Find(clienteId);
            if (cliente == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            venda.ClienteId = cliente.Id;

            _context.Vendas.Update(venda);
            _context.SaveChanges();

            return RedirectToAction("ObterVenda", new { idVenda = vendaId });
        }

        //Adicionar um item a venda
        [HttpPut]
        public ActionResult AdicionarItem(int idVenda, int produtoId, int quant)
        {
            var venda = _context.Vendas.Include(v => v.ListaDeItens).FirstOrDefault(v => v.Id == idVenda);
            if (venda == null)
            {
                return NotFound("Venda não encontrada.");
            }

            var produto = _context.Produtos.FirstOrDefault(p => p.Id == produtoId);
            if (produto == null)
            {
                return NotFound("Produto não encontrado...");
            }

            var item = new ItensVenda
            {
                VendaId = venda.Id,
                ProdutoId = produto.Id,
                Quantidade = quant,
                Total = quant >= 3 ? quant * produto.Preco * 0.9 : quant * produto.Preco
            };

            venda.ListaDeItens.Add(item);
            _context.Vendas.Update(venda);
            _context.SaveChanges();

            return Ok(new { message = "Item adicionado à venda com sucesso.", item });
        }

        //remover um item da venda
        [HttpDelete("{vendaId}/itens/{item}")]
        public IActionResult RemoverItemDaVenda(int vendaId, ItensVenda item)
        {
            var venda = _context.Vendas.Include(v => v.ListaDeItens).FirstOrDefault(v => v.Id == vendaId);
            if (venda == null)
            {
                return NotFound("Venda não encontrada.");
            }

            if (item == null)
            {
                return NotFound("Item não encontrado na venda.");
            }

            venda.ListaDeItens.Remove(item);
            _context.ItensVendas.Remove(item);//deleta o item da tabela
            _context.Vendas.Update(venda);
            _context.SaveChanges();

            return Ok(new { message = "Item removido da venda com sucesso."});
        }

        //Finalizar uma venda
        [HttpPut("{vendaId}/Finalizar")]
        public IActionResult FinalizarVenda(int vendaId)
        {
            var venda = _context.Vendas.Include(v => v.ListaDeItens).FirstOrDefault(v => v.Id == vendaId);
            if (venda == null)
            {
                return NotFound("Venda não encontrada.");
            }

            venda.Status = "Finalizada";
            venda.Total = venda.ListaDeItens.Sum(item => item.Total);

            _context.Vendas.Update(venda);
            _context.SaveChanges();

            return Ok(new { message = "Venda finalizada com sucesso.", venda });
        }

    }
}

