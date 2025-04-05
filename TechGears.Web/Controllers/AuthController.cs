using Microsoft.AspNetCore.Mvc;
using TechGears.Web.Service.IService;
using TechGears.Web.Models.AuthModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using static TechGears.Web.Utility.SD;
using TechGears.Web.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace TechGears.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IAssignRole _assignRole;
        private readonly ITokenProvider _tokenProvider;

        public AuthController(IAuthService authService, IAssignRole assignRole, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _assignRole = assignRole;
            _tokenProvider = tokenProvider;
        }

        public IActionResult Login() => View();
        

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            ResponseDTO? response = await _authService.LoginAsync(request);

            if (response != null && response.IsSuccess)
            {
                string? comingResult = JsonConvert.SerializeObject(response.Result);

                LoginResponse? loginResponse = JsonConvert.DeserializeObject<LoginResponse>(comingResult);

                
                if (loginResponse!= null)
                {
                    await SignInUser(loginResponse);
                    _tokenProvider.SetToken(loginResponse.Token);
                }

                return RedirectToAction("Index", "Home");   
            }
            else
            {
                ModelState.AddModelError("CustomError", response.Message);
                return View(request);
            }
        }

        public IActionResult Register()
        {
            
            List<SelectListItem> listItem =
            [
                new SelectListItem() { Text = RoleAdmin, Value = RoleAdmin },
                new SelectListItem() { Text = RoleSeles, Value = RoleSeles }
            ];

            ViewBag.RoleList = listItem;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest obj)
        {
            ResponseDTO? response = await _authService.RegisterAsync(obj);

            if (response != null && response.IsSuccess)
            {
                LoginRequest loginRequest = new () { Username = obj.Email, Password = obj.Password };
                response = await _assignRole.AssignRole(obj.Role, loginRequest);

                if(response != null && response.IsSuccess)
                {
                    TempData["success"] = "Registration successfully complated";
                    return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                TempData["error"] = "Registration failed";
            }
            
            return View(obj);
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            
            return RedirectToAction("Index", "Home");
        }


        private async Task SignInUser(LoginResponse loginResponse)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(loginResponse.Token);

            var idientity = new ClaimsIdentity( CookieAuthenticationDefaults.AuthenticationScheme);
            
            idientity.AddClaim(
                new Claim("Email", jwt.Claims.FirstOrDefault(u => u.Type == "Email").Value)
            );

            idientity.AddClaim(
                new Claim("Sun", jwt.Claims.FirstOrDefault(u => u.Type == "Sub").Value)
            );
            
            idientity.AddClaim(
                new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == "Email").Value)
            );

            idientity.AddClaim(
                new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == "role").Value)
            );


            var principal = new ClaimsPrincipal(idientity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);
        }

    }
}