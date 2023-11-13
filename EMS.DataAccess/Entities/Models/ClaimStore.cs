using System.Security.Claims;

namespace EMS.DataAccess.Entities.Models
{
    public class ClaimStore
    {
        public static List<Claim> AllClaims { get; set; } = new List<Claim>()
        {
            new Claim("Create Role", "Create Role"),
            new Claim("Delete Role", "Delete Role"),
            new Claim("Edit Role", "Edit Role"),
        };
    }
}
