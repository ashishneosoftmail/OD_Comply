using OD_Comply.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OD_Comply.Application.Interfaces
{
    public interface IAdminUserRepository : IGenericRepository<AdminUser>
    {
        Task<Tuple<string, bool, AdminUserDetail, JwtTokenDetails>> Login(string email, string password);
    }
}
