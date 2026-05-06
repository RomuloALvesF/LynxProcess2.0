using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using TesteASPNET.Application.Services;
using TesteASPNET.Controllers;
using TesteASPNET.Infrastructure.Context;
using TesteASPNET.Infrastructure.Repositories;

namespace TesteASPNET.Infrastructure.InjecaoDependencia
{
    public sealed class ResolvedorDependenciasSimples : IDependencyResolver
    {
        private const string ChaveDataContext = "__DataContext";

        public object GetService(Type serviceType)
        {
            if (serviceType == null) return null;

            if (serviceType == typeof(DataContext))
                return ObterOuCriarDataContext();

            if (serviceType == typeof(ProdutoRepository))
                return new ProdutoRepository(ObterOuCriarDataContext());

            if (serviceType == typeof(ProdutoService))
                return new ProdutoService(new ProdutoRepository(ObterOuCriarDataContext()));

            if (serviceType == typeof(ProdutoController))
                return new ProdutoController((ProdutoService)GetService(typeof(ProdutoService)));

            if (!serviceType.IsAbstract && !serviceType.IsInterface)
                return Activator.CreateInstance(serviceType);

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var service = GetService(serviceType);
            if (service == null) yield break;
            yield return service;
        }

        public static void LiberarServicosDoRequest()
        {
            var items = HttpContext.Current != null ? HttpContext.Current.Items : null;
            if (items == null) return;

            var ctx = items[ChaveDataContext] as DataContext;
            if (ctx != null)
            {
                ctx.Dispose();
                items.Remove(ChaveDataContext);
            }
        }

        private static DataContext ObterOuCriarDataContext()
        {
            var items = HttpContext.Current != null ? HttpContext.Current.Items : null;
            if (items == null) return new DataContext();

            var existing = items[ChaveDataContext] as DataContext;
            if (existing != null) return existing;

            var created = new DataContext();
            items[ChaveDataContext] = created;
            return created;
        }
    }
}

