using FutureFridges.Business.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FutureFridges.Business.UserManagement
{
    public class FridgeUser : IdentityUser
    {
        [Required]
        public UserType UserType { get; set; }
    }
}
