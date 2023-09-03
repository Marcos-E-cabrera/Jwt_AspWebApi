using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApiYerbas.Models.ModelsYerba;
using WebApiYerbas.Services;

namespace WebApiYerbas.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class YerbaController : Controller
    {
        private readonly IYerbaServices _yerbaServices;

        public YerbaController(IYerbaServices yerbaServices)
        {
            _yerbaServices = yerbaServices;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_yerbaServices.Get());
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var oYerba = _yerbaServices.GetById(id);
            if (oYerba == null)
                return NotFound();

            return Ok(oYerba);
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, Yerba yerba)
        {
            return Ok(_yerbaServices.Update(id, yerba));
        }


        [HttpPost]
        public IActionResult Post(Yerba yerba)
        {
            return Ok(_yerbaServices.Add(yerba));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_yerbaServices.Delete(id));
        }


    }
}
    