using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OD_Comply.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IRoleRepository Roles { get; }
        IAdminUserRepository AdminUsers { get; }
    }
}
