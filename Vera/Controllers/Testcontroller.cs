using Microsoft.AspNetCore.Mvc;
using Vera.Models;
using Vera.Infrastructure;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using System.DirectoryServices.AccountManagement; 

namespace Vera.Controllers
{
    // [ApiController]
    // [Route("api/[controller]")]
    public class Testcontroller : Controller//ControllerBase
    {
        private readonly VeraContext db;
        private NewUser newUser;
        public Testcontroller(VeraContext context) {
            db = context;
            newUser = new NewUser();
        }

        //-------------------------------------
        //-------------------------------------
         public void Test(){
            
            newUser.Domain();
            
         }

         public IActionResult Testdb(){

            List<ApplicationUser> user = db.Users.ToList();
            //ViewBag.People = user;
            return View(user);
         }
    }
}