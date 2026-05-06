using System;
using System.Collections.Generic;
using System.Linq;
using TesteASPNET.Domain.Entities;
using TesteASPNET.Infrastructure.Context;

namespace TesteASPNET.Infrastructure.Repositories
{
    public class ProdutoRepository 
    {
        private readonly DataContext _context;

        public ProdutoRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
        }

        public void Update(Produto produto)
        {
            _context.Entry(produto).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var prod = GetById(id);
            if (prod != null)
            {
                prod.DataDelecao = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public Produto GetById(int id)
        {
            var produto = _context.Produtos.Find(id);
            if (produto == null) return null;
            return produto.DataDelecao == null ? produto : null;
        }

        public IEnumerable<Produto> GetAll() => _context.Produtos.Where(p => p.DataDelecao == null).ToList();
    }
}