using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vera.Models;
using Vera.Infrastructure;
using System.Data;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Vera.Controllers {
    public class HomeController : Controller {

        private readonly VeraContext db;
        public HomeController(VeraContext context) {
            db = context;
        }
        //-------------------------------------             
        //-------------------------------------
        [Authorize]
        //[Authorize(Roles = "admin, user")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Index() {

            //if(HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            /*var id = HttpContext.Session.GetString("idUser");

            if(id == null && HttpContext.User.Identity.IsAuthenticated){

                HttpContext.Session.Clear();
                id = HttpContext.Session.GetString("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            }*/

            /*
             * для тестов
             */
            ViewData["Message"] = "Hello world!";
            var result = View();
            result.ViewName = "Index";


            return result;
            //return NotFound();
        }
        //-------------------------------------

        [HttpGet]
        public IActionResult AddFormsDelCancel(){
            
            return PartialView();
        }
        //-------------------------------------

        public IActionResult MeasuringAdd() {

            return PartialView();
        }
        //-------------------------------------

        public IActionResult TableAddUpdate(){

            return PartialView();
        }
        //-------------------------------------

        [HttpPost]
        public async Task <IActionResult> AddSave(BloodPressure bloodpressure){
            

            var hostname = HttpContext.Request.Host.Host;
            var url = hostname == "localhost" ? "/home/index" : "/vera/home/index";

                var id = HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");              

                if(id != null){
                            var time = DateTime.Now;
                            db.BloodPressures.Add(new BloodPressure{
                            datetimebloodpressure = time,
                            sys = bloodpressure.sys,
                            dia = bloodpressure.dia,
                            pulse = bloodpressure.pulse,
                            comment = bloodpressure.comment,
                            UserId = id.Value
                        });
                        await db.SaveChangesAsync();            
            
                    return Redirect(url);
                }
                
                return NotFound();
        }
        //-------------------------------------

        [HttpPost]
        public async Task<IActionResult> Edit(BloodPressure bloodpressure){

            var hostname = HttpContext.Request.Host.Host;
            var url = hostname == "localhost" ? "/home/index" : "/vera/home/index";

            var id = HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if(id != null){

                    bloodpressure.UserId = id.Value;

                    db.BloodPressures.Update(bloodpressure);
                    await db.SaveChangesAsync();

                return Redirect(url);
            }
            
            return NotFound();
        }
        //-------------------------------------

        [HttpPost]
        public async Task<IActionResult> DeleteData(int? id){

            if(id != null){

                var hostname = HttpContext.Request.Host.Host;
                var url = hostname == "localhost" ? "/home/index" : "/vera/home/index";
                //BloodPressure model = (BloodPressure)db.BloodPressures.Where(m => m.id == id);
                BloodPressure? model = db.BloodPressures.FirstOrDefault(p => p.id == id);

                if(model != null) db.BloodPressures.Remove(model);
                await db.SaveChangesAsync();

                return Redirect(url);
            }

            return NotFound();
        }        
        //-------------------------------------
    }
}
