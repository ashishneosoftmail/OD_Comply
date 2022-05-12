using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OD_Comply.Application.Interfaces;
using OD_Comply.Core.Entities;

namespace OD_Comply.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public RoleController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        
        [HttpGet("GetAllRole")]
        public async Task<IActionResult> GetAllRole()
        {
            var data = await unitOfWork.Roles.GetAllAsync();
            string msg = data.Item1;
            bool isSuccess = data.Item2;
            var Roles = data.Item3;

            if (isSuccess)
            {
                return Ok(new
                {
                    Msg = msg,
                    IsSuccess = isSuccess,
                    Roles = Roles
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
