using WebApiYerbas.Models.ModelsYerba;

namespace WebApiYerbas.Services
{
    public interface IYerbaServices 
    {

        public IEnumerable<Yerba> Get();

        public Yerba? GetById(int id);

        public bool Add(Yerba oYerba);

        public bool Update(int id, Yerba oYerba);

        public bool Delete(int id);

    }
}
