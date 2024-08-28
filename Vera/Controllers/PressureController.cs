using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vera.Infrastructure;
using Vera.Models;
using System.ComponentModel.DataAnnotations;

namespace Vera.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PressureController : ControllerBase
    {
        VeraContext db;
        public PressureController(VeraContext context) {
            db = context;
        }
        //------------------------------------------
        //------------------------------------------
        [HttpGet("{login}")]
        public string GetLoginMessage(){

            return HttpContext.User.Identity != null && HttpContext.User.Identity.Name != null ? HttpContext.User.Identity.Name : "";
        }
        //------------------------------------------                
        [HttpGet]
        public IActionResult Get() {
            
            try{

                var id = HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

                    if(id != null){

                    var model = db.BloodPressures.Where(l => l.UserId == id.Value);
                    
                    return new JsonResult(model);
                }   
            }
            catch(Exception ex){
                
                ModelState.AddModelError("", ex.Message);
            }                                             

            return NotFound();
        }        
        //------------------------------------------
        
        //[Route("api/pressure/{date1}/{date2}")]
        [HttpGet("{date1}/{date2}")]
        public IActionResult GetDate(string date1, string date2){

            DateTime date_old = DateTime.Parse(date1);
            DateTime date_new = DateTime.Parse(date2);

            var id = HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if(id != null){
                
                var model = db.BloodPressures.Where(l => date_old <= l.datetimebloodpressure && l.datetimebloodpressure <= date_new &&
                                l.UserId == id.Value);

                return new JsonResult(model);
            }   

            return NotFound();         
        }
        //------------------------------------------
        [HttpPut("{id}")]
        public IActionResult PutUpdata(int id){

            var idUser = HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if(idUser != null){

                var model = db.BloodPressures.Where(m => m.id == id && m.UserId == idUser.Value);
                    
                return new JsonResult(model);
            }
            
            return NotFound();
        }                
        //------------------------------------------
        //------------------------------------------
    }
}