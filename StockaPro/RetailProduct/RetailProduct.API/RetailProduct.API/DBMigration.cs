using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RetailProduct.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailProduct.API
{
    public static class DBMigration
    {
        public static IWebHost Migrate(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                        var context = scope.ServiceProvider.GetRequiredService<StockContext>();
                        context.Database.Migrate();
                    
                }
                catch (Exception ex)
                {
                }
            }
            return host;
        }
    }
}
