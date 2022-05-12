using Microsoft.Extensions.DependencyInjection;
using OD_Comply.Application.Interfaces;
using OD_Comply.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OD_Comply.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IAdminUserRepository, AdminUserRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
