using Microsoft.IdentityModel.Tokens;
using PermisosMVC.Models.DB;
using WebApiYerbas.Models.ModelsUsuario;
using WebApiYerbas.Models.ModelsYerba;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApiYerbas.Services
{
    public class UsuarioServices: IUsuarioServices
    {
        #region DATA BASE INJECTION
        private readonly MiSistema02Context _context;

        public UsuarioServices(MiSistema02Context context)
        {
            _context = context;
        }
        #endregion

        #region GET 
        public IEnumerable<Usuario> Get() => _context.Usuarios.ToList();

        #endregion

        #region GET BY ID
        public Usuario? GetById(int id) => _context.Usuarios.FirstOrDefault(x => x.Id == id);
        #endregion

        #region ADD
        public bool Add(Usuario oUsuario)
        {
            bool result = false;

            try
            {
                var usuario = new Usuario()
                {
                    Nombre = oUsuario.Nombre,
                    Email = oUsuario.Email,
                    Password = oUsuario.Password,
                    IdRol = oUsuario.IdRol
                };

                _context.Add(usuario);
                _context.SaveChanges();

                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
        #endregion

        #region UPDATE
        public bool Update(int id, Usuario oUsuario)
        {
            bool result = false;

            try
            {
                var oAux = from x in _context.Usuarios 
                           where x.Id == id
                           select x;

                if (oAux.IsNullOrEmpty())
                {
                    throw new Exception();
                }
                else
                {
                    foreach (var x in oAux)
                    {
                        x.Nombre = oUsuario.Nombre;
                    }

                    _context.SaveChanges();

                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
        #endregion

        #region DELETE
        public bool Delete(int id)
        {
            bool result = false;

            try
            {
                var oAux = _context.Usuarios.FirstOrDefault(x => x.Id == id);

                if (oAux == null)
                {
                    throw new Exception();
                }
                else
                {
                    _context.Remove(oAux);

                    _context.SaveChanges();

                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
        #endregion
    }
}
