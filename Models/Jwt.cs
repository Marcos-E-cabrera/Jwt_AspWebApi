using Microsoft.AspNetCore.Http.HttpResults;
using SQLitePCL;
using System.Security.Claims;
using WebApiYerbas.Models.ModelsUsuario;

namespace WebApiYerbas.Models
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }


        public static dynamic ValidarToken(ClaimsIdentity identity)
        {
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return new
                    {
                        success = false,
                        message = "Token invalido",
                        result = ""
                    };

              
                }

                var id = identity.Claims.FirstOrDefault(x => x.Type == "Id").Value;


                using ( MiSistema02Context db = new MiSistema02Context())
                {
                    var oUsuario = db.Usuarios.FirstOrDefault(x => x.Id == int.Parse(id));

                    return new
                    {
                        success = true,
                        message = "Exito",
                        results = oUsuario
                    };
                }
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = ex.Message,
                    result = ""
                };
            }
        }
    }
}
