using PermisosMVC.Models.DB;
using WebApiYerbas.Models.ModelsYerba;

namespace WebApiYerbas.Services
{
    public interface IUsuarioServices
    {
        public IEnumerable<Usuario> Get();

        public Usuario? GetById(int id);

        public bool Add(Usuario oUser);

        public bool Update(int id, Usuario oUser);

        public bool Delete(int id);
    }
}
