using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("/primeiro")]
        public ActionResult<Produto> GetPrimeiro()
        {
            return _context.Produtos.FirstOrDefault();
        }
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            return _context.Produtos.ToList();
        }
        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            return produto != null ? Ok(produto) : NotFound("Produto não encontrado");
        }
        [HttpPost]
        public ActionResult Post([FromBody] Produto produto)
        {
            if (produto is null) return BadRequest();
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId) return BadRequest();
            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(produto);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if (produto is null) return NotFound("Produto não encontrado");
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return Ok(produto);
        }
    }  
}
