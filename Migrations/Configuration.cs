using System.Data.Entity.Migrations;
using TesteASPNET.Infrastructure.Context;

namespace TesteASPNET.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataContext context)
        {
        }
    }
}
