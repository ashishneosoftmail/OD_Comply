using Dapper;
using Microsoft.Extensions.Configuration;
using OD_Comply.Application.Interfaces;
using OD_Comply.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OD_Comply.Infrastructure.Repositories
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public RoleRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public Task<Tuple<string, bool>> AddAsync(Role entity)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<string, bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Tuple<string, bool,IReadOnlyList<Role>>> GetAllAsync()
        {
            string msg = "";
            bool isSuccess = false;
            try
            {
                List<Role> roles = new List<Role>();
               var query = "GetAllRoles";
              
                using (var connection = CreateConnection())
                {                   
                    var data = (await connection.QueryAsync<Role>(query));

                    foreach (var entity in data)
                    {
                        roles.Add(entity);
                    }
                    if (data.Count() > 0)
                    {
                        msg = "Success";
                        isSuccess = true;
                        return new Tuple<string, bool,IReadOnlyList<Role>>( msg, isSuccess, roles);
                    }
                    else
                    {
                        isSuccess = false;
                        msg = "No Data Found.";
                        return new Tuple<string, bool,IReadOnlyList<Role>>( msg, isSuccess,null);
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                msg = "Something went wrong!";
                return new Tuple<string, bool,IReadOnlyList<Role>>( msg, isSuccess,null);
            }
        }

        public Task<Role> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<string, bool>> UpdateAsync(Role entity)
        {
            throw new NotImplementedException();
        }
    }
}
