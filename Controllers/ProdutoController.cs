using System.Web.Mvc;
using TesteASPNET.Domain.Entities;

namespace TesteASPNET.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly Application.Services.ProdutoService _service;

        public ProdutoController(Application.Services.ProdutoService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            return View(_service.ListarTodos());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _service.Criar(produto);
                return RedirectToAction("Index");
            }
            return View(produto);
        }

        public ActionResult Edit(int id)
        {
            var produto = _service.ObterPorId(id);
            if (produto == null) return HttpNotFound();
            return View(produto);
        }

        [HttpPost]
        public ActionResult Edit(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _service.Atualizar(produto);
                return RedirectToAction("Index");
            }
            return View(produto);
        }

        public ActionResult Delete(int id)
        {
            _service.Remover(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _service.Remover(id);
            return RedirectToAction("Index");
        }
    }
}