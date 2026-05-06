using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using TesteASPNET.Infrastructure.Context;
using TesteASPNET.Infrastructure.InjecaoDependencia;

namespace TesteASPNET
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, TesteASPNET.Migrations.Configuration>());

            DependencyResolver.SetResolver(new ResolvedorDependenciasSimples());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_EndRequest()
        {
            ResolvedorDependenciasSimples.LiberarServicosDoRequest();
        }
    }
}
