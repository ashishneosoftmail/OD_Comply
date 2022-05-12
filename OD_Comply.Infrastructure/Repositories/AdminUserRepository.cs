using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OD_Comply.Application.Interfaces;
using OD_Comply.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OD_Comply.Infrastructure.Repositories
{
    public class AdminUserRepository : BaseRepository, IAdminUserRepository
    {
        private readonly JwtSettings _jwtSettings;

        public AdminUserRepository(IConfiguration configuration, JwtSettings jwtSettings) : base(configuration)
        {
            _jwtSettings = jwtSettings;
        }

        public async Task<Tuple<string,bool>> AddAsync(AdminUser entity)
        {
            string msg = "";
            bool isSuccess = false;
            try
            {
                var query = "SP_OD_INSERT_UPDATE_ADMIN_USER";
                var parameters = new DynamicParameters();
                parameters.Add("Id",0, DbType.Int32);
                parameters.Add("Uuid", entity.Uuid, DbType.String);
                parameters.Add("FirstName", entity.FirstName, DbType.String);
                parameters.Add("LastName", entity.LastName, DbType.String);
                parameters.Add("Phone", entity.Phone, DbType.String);
                parameters.Add("Email", entity.Email, DbType.String);
                parameters.Add("SecondaryEmail", entity.SecondaryEmail, DbType.String);
                parameters.Add("Password", entity.Password, DbType.String);
                parameters.Add("RoleId", entity.RoleId, DbType.Int32);
                parameters.Add("IsActive", entity.IsActive, DbType.Boolean);
                parameters.Add("CreatedOn", entity.CreatedOn, DbType.DateTime);
                parameters.Add("CreatedBy", null, DbType.String);
                parameters.Add("UpdatedOn", null, DbType.DateTime);
                parameters.Add("UpdatedBy", null, DbType.String);
                parameters.Add("IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                parameters.Add("MSG", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);

                using (var connection = CreateConnection())
                {
                    var result = (await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure));
                    isSuccess = parameters.Get<bool>("IsSuccess");
                    msg = parameters.Get<string>("MSG");
                }

                return new Tuple<string, bool>(msg, isSuccess);
            }
            catch (Exception ex)
            {
                 msg = "Someting went wrong!";
                 isSuccess = false;
                return new Tuple<string, bool>(msg, isSuccess);

            }

           
        }

        public async Task<Tuple<string, bool>> DeleteAsync(int id)
        {
            string msg = "";
            bool isSuccess = false;
            try
            {
                var query = "SP_OD_DELETE_ADMIN_USER";
                var parameters = new DynamicParameters();
                parameters.Add("Id", 0, DbType.Int32);               
                parameters.Add("DeletedOn", DateTime.Now, DbType.DateTime);
                parameters.Add("DeletedBy", null, DbType.String);
                parameters.Add("IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                parameters.Add("MSG", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);

                using (var connection = CreateConnection())
                {
                    var result = (await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure));
                    isSuccess = parameters.Get<bool>("IsSuccess");
                    msg = parameters.Get<string>("MSG");
                }

                return new Tuple<string, bool>(msg, isSuccess);
            }
            catch (Exception ex)
            {
                msg = "Something went wrong!";
                isSuccess = false;
                return new Tuple<string, bool>(msg, isSuccess);

            }


        }

        public Task<Tuple<IReadOnlyList<AdminUser>, string, bool>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AdminUser> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Tuple<string, bool, AdminUserDetail, JwtTokenDetails>> Login(string email, string password)
        {
            string msg = "";
            bool isSuccess = false;
            try
            {
                AdminUserDetail admin = new AdminUserDetail();
                if (email == null)
                {
                    return new Tuple<string, bool, AdminUserDetail, JwtTokenDetails>(msg, isSuccess, null, null);
                }
                else
                {
                    var query = "SP_OD_ADMIN_USER_LOGIN";
                    var parameters = new DynamicParameters();
                    parameters.Add("Email", email, DbType.String);
                    parameters.Add("Password", password, DbType.String);
                    parameters.Add("IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    parameters.Add("MSG", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);

                    using (var connection = CreateConnection())
                    {
                        var data = (await connection.QueryAsync(query, parameters, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                        isSuccess = parameters.Get<bool>("IsSuccess");
                        msg = parameters.Get<string>("MSG");
                        admin.Id = data.Id;
                        admin.Uuid = data.Uuid;
                        admin.FirstName = data.FirstName;
                        admin.LastName = data.LastName;
                        admin.Email = data.Email;
                        admin.SecondaryEmail = data.SecondaryEmail;
                        admin.Phone = data.Phone;
                        admin.RoleId = data.RoleId;
                        admin.RoleName = data.RoleName;

                        var tokenResult = GenerateToken(admin);

                        return new Tuple<string, bool, AdminUserDetail, JwtTokenDetails>(msg, isSuccess, admin, tokenResult);
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "Something went wrong!";
                return new Tuple<string, bool, AdminUserDetail, JwtTokenDetails>(msg, isSuccess, null, null);
            }
        }

        public async Task<Tuple<string, bool>> UpdateAsync(AdminUser entity)
        {
            string msg = "";
            bool isSuccess = false;
            try
            {
                var query = "SP_OD_INSERT_UPDATE_ADMIN_USER";
                var parameters = new DynamicParameters();
                parameters.Add("Id", entity.Id, DbType.Int32);
                parameters.Add("Uuid", entity.Uuid, DbType.String);
                parameters.Add("FirstName", entity.FirstName, DbType.String);
                parameters.Add("LastName", entity.LastName, DbType.String);
                parameters.Add("Phone", entity.Phone, DbType.String);
                parameters.Add("Email", entity.Email, DbType.String);
                parameters.Add("SecondaryEmail", entity.SecondaryEmail, DbType.String);
                parameters.Add("Password", entity.Password, DbType.String);
                parameters.Add("RoleId", entity.RoleId, DbType.Int32);
                parameters.Add("IsActive", entity.IsActive, DbType.Boolean);
                parameters.Add("CreatedOn", null, DbType.DateTime);
                parameters.Add("CreatedBy", null, DbType.String);
                parameters.Add("UpdatedOn", entity.UpdatedOn, DbType.DateTime);
                parameters.Add("UpdatedBy", null, DbType.String);
                parameters.Add("IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                parameters.Add("MSG", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);

                using (var connection = CreateConnection())
                {
                    var result = (await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure));
                    isSuccess = parameters.Get<bool>("IsSuccess");
                    msg = parameters.Get<string>("MSG");
                }

                return new Tuple<string, bool>(msg, isSuccess);
            }
            catch (Exception ex)
            {
                msg = "Something went wrong!";
                isSuccess = false;
                return new Tuple<string, bool>(msg, isSuccess);

            }

        }

        private JwtTokenDetails GenerateToken(AdminUserDetail user)
        {
            var role = user.RoleName;
            JwtTokenDetails response = new JwtTokenDetails();

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                  new Claim(JwtRegisteredClaimNames.Aud, _jwtSettings.Audience),
            new Claim(JwtRegisteredClaimNames.Iss, _jwtSettings.Issuer),
                new Claim(ClaimTypes.Role , role)
            };


            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            response.IsAuthenticated = true;
            response.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return response;
        }
    }
}
