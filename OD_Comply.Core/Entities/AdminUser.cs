using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OD_Comply.Core.Entities
{
    public class AdminUser : BaseEntity
    {
        public int Id { get; set; }
        public string Uuid { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SecondaryEmail { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }


    }

    public class AdminUserDetail
    {
        public int Id { get; set; }
        public string Uuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SecondaryEmail { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
     
       // public string RefreshToken { get; set; }
       // public DateTime? RefreshTokenExpiration { get; set; }

    }
    public class JwtTokenDetails
    {
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
    }
}
