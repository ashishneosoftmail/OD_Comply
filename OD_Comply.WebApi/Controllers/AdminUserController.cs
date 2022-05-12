using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public async Task<IActionResult> Login(string email, string password)
        {
            var hashedPassword = GenerateHash.EncryptString(password);
            
            var data = await unitOfWork.AdminUsers.Login(email, hashedPassword);
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

        [HttpPost("AddAdminUser")]
        [Authorize(Roles= "Admin")]
        public async Task<IActionResult> AddAdminUser(AdminUser adminUser)
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

        [HttpPost("EditAdminUser")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateAdminUser(AdminUser adminUser)
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

        [HttpPost("DeleteAdminUser")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteAdminUser(int id)
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

        [HttpGet("GetAllAdminUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAdminUsers()
        {
            var data = await unitOfWork.AdminUsers.GetAllAdminUsersAsync();
            string msg = data.Item1;
            bool isSuccess = data.Item2;
            var AdminList = data.Item3;

            if (isSuccess)
            {
                return Ok(new
                {
                    Msg = msg,
                    IsSuccess = isSuccess,
                    Admin_Users = AdminList
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

        [HttpGet("GetByIdAdminUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAdminUserById(int id)
        {
            var data = await unitOfWork.AdminUsers.GetAdminUserByIdAsync(id);
            string msg = data.Item1;
            bool isSuccess = data.Item2;
            var Admin = data.Item3;

            if (isSuccess)
            {
                return Ok(new
                {
                    Msg = msg,
                    IsSuccess = isSuccess,
                    Admin_User = Admin
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
