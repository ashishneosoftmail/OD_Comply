using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OD_Comply.Application.Interfaces;

namespace OD_Comply.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
   // [Authorize(Roles = "Content Writer")]
    public class RoleController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public RoleController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        //[Authorize]
        [HttpGet("GetAllRole")]
        public async Task<IActionResult> GetAllRole()
        {
            var data = await unitOfWork.Roles.GetAllAsync();
            return Ok(data);
        }
    }
}
