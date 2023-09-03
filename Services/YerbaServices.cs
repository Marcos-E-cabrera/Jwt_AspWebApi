using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.PortableExecutable;
using WebApiYerbas.Models.ModelsYerba;

namespace WebApiYerbas.Services
{
    public class YerbaServices : IYerbaServices
    {
        #region DATA BASE INJECTION
        private readonly YerbasApiRestContext _context;

        public YerbaServices(YerbasApiRestContext context) 
        {
            _context = context;
        }


        #endregion

        #region GET 
        public IEnumerable<Yerba> Get() =>  _context.Yerbas.ToList();

        #endregion

        #region GET BY ID
        public Yerba? GetById(int id) => _context.Yerbas.FirstOrDefault(x  => x.Id == id);
        #endregion

        #region ADD
        public bool Add(Yerba oYerba)
        {
            bool result = false;

            try
            {
                var yerba = new Yerba()
                {
                    Nombre = oYerba.Nombre,
                    Cantidad = oYerba.Cantidad
                };

                _context.Add(yerba);
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
        public bool Update(int id, Yerba oYerba)
        {
            bool result = false;

            try
            {
                var oAux = from x in _context.Yerbas
                             where x.Id == id
                             select x;

                if ( oAux.IsNullOrEmpty() ) 
                {
                    throw new Exception();
                }
                else
                {
                    foreach (  var x in oAux)
                    {
                        x.Nombre = oYerba.Nombre;
                        x.Cantidad = oYerba.Cantidad;
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
                var oAux = _context.Yerbas.FirstOrDefault(x => x.Id == id);

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
