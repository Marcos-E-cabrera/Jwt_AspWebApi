using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PermisosMVC.Models.DB;
using System.Security.Claims;
using WebApiYerbas.Models;
using WebApiYerbas.Models.ModelsYerba;
using WebApiYerbas.Services;

namespace WebApiYerbas.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioServices _usuarioServices;

        public UsuarioController(IUsuarioServices usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_usuarioServices.Get());
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var oUsuario = _usuarioServices.GetById(id);
            if (oUsuario == null)
                return NotFound();

            return Ok(oUsuario);
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, Usuario usuario)
        {
            return Ok(_usuarioServices.Update(id, usuario));
        }


        [HttpPost]
        public IActionResult Post(Usuario usuario)
        {
            return Ok(_usuarioServices.Add(usuario));
        }


        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var resultToken = Jwt.ValidarToken(identity);

            if (!resultToken.success)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Error de autenticación",
                    result = ""
                });
            }

            var oUsuario = resultToken.results as Usuario;

            if (oUsuario == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "El resultado de autenticación no contiene un objeto Usuario válido",
                    result = ""
                });
            }

            if (oUsuario.IdRol != 1)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "No tienes permiso para eliminar el Cliente",
                    result = ""
                });
            }

            return Ok(_usuarioServices.Delete(id));
        }
    }
}
