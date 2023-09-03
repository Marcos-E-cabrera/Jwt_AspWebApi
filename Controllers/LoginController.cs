    using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PermisosMVC.Models.DB;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiYerbas.Models;

namespace WebApiYerbas.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class LoginController : Controller
    {
        private IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        [Route("login")]
        public ActionResult Login( [FromBody] object optData)
        {
            try
            {
                var userString = JsonConvert.DeserializeObject<Usuario>(optData.ToString());

                string? user = userString.Email;
                string? password = userString.Password;

                using (Models.ModelsUsuario.MiSistema02Context db = new Models.ModelsUsuario.MiSistema02Context())
                {
                    var oUser = (from u in db.Usuarios
                                 where u.Email == user.Trim() && u.Password == password.Trim()
                                 select u).FirstOrDefault();

                    if (oUser == null)
                    {
                        return Json(new
                        {
                            success = false,
                            message = "Usuario o Password Incorrecto",
                            result = ""
                        });
                    }

                    var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", oUser.Id.ToString()),     
                        new Claim("User", oUser.Nombre),
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                    var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken
                        (
                            jwt.Issuer,
                            jwt.Audience,
                            claims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: singIn
                        ); 

                    return Json(new
                    {
                        success = true,
                        message = "Inicio de sesión exitoso",
                        result = new JwtSecurityTokenHandler().WriteToken(token)
                    });

                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message,
                    result = ""
                });
            }
        }
    }
}
