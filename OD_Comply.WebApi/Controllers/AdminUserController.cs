using Microsoft.AspNetCore.Mvc;
using OD_Comply.Application.Interfaces;
using OD_Comply.Core.Entities;
using OD_Comply.WebApi.Helper;

namespace OD_Comply.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public AdminUserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var data = await unitOfWork.AdminUsers.Login(email, password);
            string msg = data.Item1;
            bool isSuccess = data.Item2;
            AdminUserDetail detail = data.Item3;
            JwtTokenDetails tokenDetails = data.Item4;


            if (isSuccess)
            {
                return Ok(new
                {
                    Msg = msg,
                    IsSuccess = isSuccess,
                    AdminUserDetail = detail,
                    JwtTokenDetails = tokenDetails
                });
            }
            else
            {
                return Ok(new
                {
                    Msg = msg,
                    IsSuccess = isSuccess,
                    AdminUserDetail = detail,
                    JwtTokenDetails = ""
                });
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Create(AdminUser adminUser)
        {
            var hashedPassword = GenerateHash.EncryptString(adminUser.Password);
            adminUser.Password = hashedPassword;
            var data = await unitOfWork.AdminUsers.AddAsync(adminUser);
            string msg = data.Item1;
            bool isSuccess = data.Item2;
            if (isSuccess)
            {
                return Ok(new
                {
                    Msg = msg,
                    IsSuccess = isSuccess                  
                });
            }
            else
            {
                return Ok(new
                {
                    Msg = msg,
                    IsSuccess = isSuccess                  
                });
            }

        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Update(AdminUser adminUser)
        {
            var hashedPassword = GenerateHash.EncryptString(adminUser.Password);
            adminUser.Password =hashedPassword;
            var data = await unitOfWork.AdminUsers.UpdateAsync(adminUser);
            string msg = data.Item1;
            bool isSuccess = data.Item2;
            if (isSuccess)
            {
                return Ok(new
                {
                    Msg = msg,
                    IsSuccess = isSuccess
                });
            }
            else
            {
                return Ok(new
                {
                    Msg = msg,
                    IsSuccess = isSuccess
                });
            }

        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.AdminUsers.DeleteAsync(id);
            string msg = data.Item1;
            bool isSuccess = data.Item2;
            if (isSuccess)
            {
                return Ok(new
                {
                    Msg = msg,
                    IsSuccess = isSuccess
                });
            }
            else
            {
                return Ok(new
                {
                    Msg = msg,
                    IsSuccess = isSuccess
                });
            }

        }
    }
}
