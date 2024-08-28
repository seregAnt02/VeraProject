using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Vera.Infrastructure
{    
    public class ApplicationUser : IdentityUser
    {     
        /*public ApplicationUser()
        {
        } */  
        //Класс пользователей:
        //[Key]
        public override string? Id { get; set; } 
        public int Year { get; set; }
        public override string? Email { get; set;}
        //public override bool EmailConfirmed { get; set;}
        //public override string PasswordHash { get; set;}
        //public override string SecurityStamp { get; set;}
        //public override string PhoneNumber { get; set;}
        //public override byte PhoneNumberConfirmed { get; set;}
        //public override byte TwoFactorEnabled { get; set;}
        //public DateTime LockoutEndDateUtc { get; set;}
        //public override byte LockoutEnabled { get; set;}
        //public override int AccessFailedCount { get; set;}
        //public override string UserName { get; set;}
        //public override string ConcurrencyStamp { get; set;}
        //public string Email { get; set;}
        public override string? UserName { get; set;}
        /*public async Task<ClaimsIdentity> GenerateUserIdentityAsync
                                 (UserManager<ApplicationUser> manager)
         {
             var userIdentity = await manager.CreateIdentityAsync(this,
                                     DefaultAuthenticationTypes.ApplicationCookie);
             return userIdentity;
         }*/

    }
}