using APICompras.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaCompras.Models;

namespace APICompras.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly Contexto _context;
        public UsuariosController(Contexto context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> Get()
        {
            var usuarios = _context.Usuarios.AsNoTracking().ToList();

            if (usuarios is null)
            {
                return NotFound("Usuarios não encontrados...");
            }
            return usuarios;
        }

        [HttpGet("{id:int}", Name = "ObterUsuario")]
        public ActionResult<Usuario> Get(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(p => p.Id == id);

            if (usuario is null)
            {
                return NotFound("Usuario não encontrado...");
            }
            return usuario;
        }

        [HttpPost("autenticar")]
        public ActionResult<bool> Autenticar(Usuario usuarioLogin)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Login == usuarioLogin.Login && u.Senha == usuarioLogin.Senha);

            if (usuario != null)
            {
                return Ok(new { message = "Usuário autenticado com sucesso." });
            }
            else
            {
                return Ok(new { message = "Usuário não autenticado." });
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(usuario);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(c => c.Id == id);

            if (usuario is null)
            {
                return NotFound("Usuario não localizado...");
            }
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            return Ok(usuario);
        }

        [HttpPost]
        public ActionResult Post(Usuario usuario)
        {

            if (usuario is null)
            {
                return BadRequest();
            }

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterUsuario",
                new { id = usuario.Id }, usuario);
        }
    }
}
