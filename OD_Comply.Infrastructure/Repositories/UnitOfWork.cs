using OD_Comply.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OD_Comply.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRoleRepository Roles { get; }

        public IAdminUserRepository AdminUsers { get; }

        public UnitOfWork(IRoleRepository roleRepository , IAdminUserRepository adminUserRepository)
        {
            Roles = roleRepository;
            AdminUsers= adminUserRepository;
        }
    }
}
