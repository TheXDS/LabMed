using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContabServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContabServer.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    public class ContabilidadController : Controller
    {
        ContabContext db = new ContabContext();

        // GET api/contabilidad
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("[action]/{id}")]
        public Cuenta GetCuenta(long id)
        {
            return db.Cuentas.Find(id);
        }

        [HttpGet("[action]/{id}")]
        public Categoria GetCategoria(long id)
        {
            return db.Categorias.Find(id);
        }


        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see https://go.microsoft.com/fwlink/?LinkID=717803
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see https://go.microsoft.com/fwlink/?LinkID=717803
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see https://go.microsoft.com/fwlink/?LinkID=717803
        }
    }
}
