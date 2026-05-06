using System;
using System.Collections.Generic;
using TesteASPNET.Domain.Entities;

namespace TesteASPNET.Application.Services
{
    public class ProdutoService
    {
        private readonly Infrastructure.Repositories.ProdutoRepository _repo;

        public ProdutoService() : this(new Infrastructure.Repositories.ProdutoRepository(new Infrastructure.Context.DataContext())) { }

        public ProdutoService(Infrastructure.Repositories.ProdutoRepository repo)
        {
            _repo = repo;
        }

        public void Criar(Produto produto)
        {
            produto.DataAtualizacao = DateTime.Now;
            _repo.Add(produto);
        }

        public void Atualizar(Produto produto)
        {
            produto.DataAtualizacao = DateTime.Now;
            _repo.Update(produto);
        }
        public void Remover(int id) => _repo.Delete(id);
        public Produto ObterPorId(int id) => _repo.GetById(id);
        public IEnumerable<Produto> ListarTodos() => _repo.GetAll();
    }
}