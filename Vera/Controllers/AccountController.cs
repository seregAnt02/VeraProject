using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vera.Models;
using Vera.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Aspose.Drawing.Imaging;
using Microsoft.Extensions.Logging;
using System.Web;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
//using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Vera.Controllers
{
    //[Route("[controller]")]

    /*Мне удалось удалить предупреждения CA1416, добавив следующий декоратор в начало содержащего класса:*/
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public class AccountController : Controller
    {        

        //private readonly VeraContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly ILogger<AccountController> _logger;
        //-------------------------------------
        //public AccountController() { }
        //-------------------------------------
        public AccountController(UserManager<ApplicationUser> manager, SignInManager<ApplicationUser> signInManager,
        ILogger<AccountController> logger){
            
            _userManager = manager;
            _signInManager = signInManager;
            _logger = logger;
            //logger.LogInformation("asd", this);
        }
        //-------------------------------------  
        public IActionResult  Test(){

            return View();
        }                                              
        //-------------------------------------
        public ActionResult PrivacyPolicy() {

            return PartialView();
        }
        //-------------------------------------

        [HttpPost]
        //[AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model) {

            var hostname = HttpContext.Request.Host.Host;//$(window.location).attr('hostname');
            var url = hostname == "localhost" ? "/account/startpage" : "/vera/account/startpage";
            
            // _logger.LogInformation("host: " + hostname);
            // _logger.LogInformation("url_asv: " + url);

            if (ModelState.IsValid) {

                try {

                    ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);

                    _logger.LogInformation("User_id:" + user.Id);
                    
                    if (user != null) {
                        // проверяем, подтвержден ли email
                        if (!await _userManager.IsEmailConfirmedAsync(user)) {
                                //ModelState.AddModelError(string.Empty, "Вы не подтвердили свой email");
                            return Redirect(url);
                        }

                    }                                           
                    var result = user != null ? await _signInManager.PasswordSignInAsync(user.UserName, model.Password, true, false) : null;

                    var captcha = HttpContext.Session.GetString("code");

                    if (result != null && result.Succeeded && captcha == model.Captcha) {
                                                
                        // передача токена в заголовке                        
                        //HttpContext.Request.Headers.Add("Authorization", "Bearer " + CreateJWTTokenJsonResult(user));

                        return Redirect(hostname == "localhost" ? "/home/index" : "/vera/home/index");
                    }
                    else {
                        ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                    }

                }
                catch (Exception ex) {

                    ModelState.AddModelError("", ex.Message);
                }

            }

            return Redirect(url);
        }    
        //-------------------------------------
        
        //"Bearer": аутентификация на основе jwt-токенов. Хранится в константе JwtBearerDefaults.AuthenticationScheme
        // private string CreateJWTTokenJsonResult(ApplicationUser user) {

        //     ClaimsIdentity? claims = GetIdentity(user);
        //     // создаем JWT-токен
        //     var jwt = new JwtSecurityToken(
        //             //issuer: AuthOptions.ISSUER,
        //             //audience: AuthOptions.AUDIENCE,
        //             claims: claims?.Claims
        //             //expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
        //             //signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey, SecurityAlgorithms.HmacSha256)
        //             );
        //     var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);            

        //     return encodedJwt;
        // }
        //-------------------------------------    
/*С помощью параметра claims: identity.Claims в токен добавляются набор объектов Claim,
 которые содержат информацию о логине и роли пользователя.*/
        // private ClaimsIdentity? GetIdentity(ApplicationUser user)
        // {            
        //     if (user != null)
        //     {
        //         var claims = new List<Claim>
        //         {
        //             new Claim(ClaimsIdentity.DefaultNameClaimType, "user"),
        //             new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin")
        //         };                                

        //         return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,ClaimsIdentity.DefaultRoleClaimType);                                    
        //     }
 
        //     // если пользователя не найдено
        //     return null;
        // }    
        //-------------------------------------

         public IActionResult ValidPage(){

            return View();
        } 
        //-------------------------------------

        public IActionResult StartPage(){

            
            return View();
        }                
        //-------------------------------------

        [HttpGet]
        //[AllowAnonymous]
        public IActionResult Login(){
            
            //HttpContext.Authentication.SignInAsync()
            return PartialView();
        }    
        //-------------------------------------

        [HttpGet]
        public async Task <IActionResult> Logout(){
            
            await _signInManager.SignOutAsync();
            //await HttpContext.SignOutAsync();

            var hostname = HttpContext.Request.Host.Host;//$(window.location).attr('hostname');
            var url = hostname == "localhost" ? "/account/startpage" : "/vera/account/startpage";

            return Redirect(url);
        }

        //-------------------------------------

        [HttpGet]
        public IActionResult Register(){
            
            return PartialView();
        }

        //-------------------------------------

        [HttpPost]
        public async Task <IActionResult> Register(RegisterModel model){

             var hostname = HttpContext.Request.Host.Host;//$(window.location).attr('hostname');
            var url_startpage = hostname == "localhost" ? "/account/startpage" : "/vera/account/startpage";

            if (ModelState.IsValid){
                
                try{
                
                var user = new ApplicationUser(){
                    
                    Email = model.Email,
                    UserName = model.Email, // !!! временное решение
                    Year = model.Year
                };
                // добавляем пользователя
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);         

                _logger.LogInformation("Identity_result:" + result.Succeeded);       

                var captcha = HttpContext.Session.GetString("code");

                if (result.Succeeded && captcha == model.Captcha)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, true);

                    await GenerateToken(user);                                        
                    var url_localhost = hostname == "localhost" ? "/account/registrationsuccessful" : "/vera/account/registrationsuccessful";
                    return Redirect(url_localhost);                    
                }                
                //db.Users.Add(user);
                //await db.SaveChangesAsync();
                
                //await Authenticate(user);
                
                //await GetEmailCookies(user);   
                }
                catch(Exception ex){
                    _logger.LogInformation("Info ex" + ex.Message);
                }                         
            }

            ViewData["TextMessage"] = "Test";

            return Redirect(url_startpage);            
        }
        //-------------------------------------
        public async Task GenerateToken(ApplicationUser user) {

            //var hostname = HttpContext.Request.Host.Value;//$(window.location).attr('hostname');

            var code_user = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            // создаем ссылку для подтверждения
            var callbackUrl = Url.Action("confirmemail", "account", new { userId = user.Id, code = code_user },
                        HttpContext.Request.Scheme);

            //var url = hostname ? "/UserData/Parameter/" : "/duma/UserData/Parameter/";

            // отправка письма
            EmailService emailMessage = new();

            string body = "Для завершения регистрации перейдите по ссылке:<a href=\"" + callbackUrl + "\">завершить регистрацию</a>";

            _logger.LogInformation("Processing message body: {0}", body);

            _logger.LogInformation("Processing message host: {0}", callbackUrl);

            await emailMessage.SendEmailAsync("s_antonov02@rambler.ru", user.Email, body);//, "/account/confirmemail/?userEmail=" + user.Email + "&code=" + callbackUrl

            //return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");

            //аутентификация пользователя
            //await AuthenticateJWT(user);

            //return Redirect("/");

        }
        //-------------------------------------
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code) {

            var hostname = HttpContext.Request.Host.Host;
            var url = hostname == "localhost" ? "/home/index" : "/vera/home/index";

            if (userId == null || code == null) {
                return View("Error");
            }            
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if(result.Succeeded)
                return Redirect(url);
            else
                return View("Error");
        }        
        //-------------------------------------

        public ActionResult RegistrationSuccessful(){

            return View();
        }

        // private async Task Authenticate()
        // {
            
        //             // создаем один claim
        //         var claims = new List<Claim>
        //         {
        //             new Claim(ClaimsIdentity.DefaultNameClaimType, "user"),
        //             new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin"),
        //             new Claim("idUser", "5")
        //         };
        //         // создаем объект ClaimsIdentity
        //         ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
        //             ClaimsIdentity.DefaultRoleClaimType);
        //         // установка аутентификационных куки
        //         await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

                
        //         claims.Clear();
        // }
        //-------------------------------------

        public ActionResult Captcha() {

            string? code = new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString();

            HttpContext.Session.Clear();
            HttpContext.Session.SetString("code", code);

            CaptchaImage captcha = new(code, 110, 50);

            MemoryStream stream = new();

            captcha.Image.Save(stream, ImageFormat.Bmp);
            captcha.Dispose();
            stream.Dispose();
            
            // var image = System.IO.File.OpenRead("wwwroot/images/help.jpg"); 
            // return File(image, "image/jpeg");
            return new FileContentResult(stream.ToArray(), "image/jpeg");

        }
        //-------------------------------------    
        // [HttpGet]
        // public IActionResult Get()
        // {
        //     var image = System.IO.File.OpenRead("C:\\test\\random_image.jpeg");
        //     return File(image, "image/jpeg");
        // }    
        //-------------------------------------
        /*public Task SendResponseAsync(IDictionary<string, object> environment)
        {
            // определяем ответ
            string responseText = "Hello ASP.NET Core";
            // кодируем его в массив байтов
            byte[] responseBytes = Encoding.UTF8.GetBytes(responseText);
 
            // получаем поток ответа
            var responseStream = (Stream)environment["owin.ResponseBody"];
            // отправка ответа
            return responseStream.WriteAsync(responseBytes, 0, responseBytes.Length);
        } */
        //-------------------------------------
    }
}