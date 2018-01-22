/*
Copyright © 2017, 2018 César Andrés Morgan
Pendiente de licenciamiento
===============================================================================
Este archivo está pensado para uso interno exclusivamente por su autor y otro
personal autorizado. No debe ser distribuido en ningún producto comercial sin
haber antes pasado por un control de calidad adecuado, ni tampoco debe ser
considerado como código listo para producción. El autor se absuelve de toda
responsabilidad y daños causados por el uso indebido de este archivo o de
cualquier parte de su contenido.
*/

using ContabServer.Logic;
using ContabServer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContabServer.Controllers
{
    public class ContabilidadController : APIController
    {
        ContabContext db = new ContabContext();

        #region Consultas
        /// <summary>
        /// Obtiene una categoría.
        /// </summary>
        /// <param name="id">Id a buscar.</param>
        /// <returns>
        /// Una <see cref="Categoria"/> cuyo índice es igual a
        /// <paramref name="id"/>.
        /// </returns>
        [HttpGet("[action]/{id}")]
        public async Task<Categoria> GetCategoria(long id) => await db.Categorias.FindAsync(id);
        /// <summary>
        /// Obtiene una cuenta.
        /// </summary>
        /// <param name="id">Id a buscar.</param>
        /// <returns>
        /// Una <see cref="Cuenta"/> cuyo índice es igual a
        /// <paramref name="id"/>.
        /// </returns>
        [HttpGet("[action]/{id}")]
        public async Task<Cuenta> GetCuenta(long id) => await db.Cuentas.FindAsync(id);
        /// <summary>
        /// Obtiene una partida.
        /// </summary>
        /// <param name="id">Id a buscar.</param>
        /// <returns>
        /// Una <see cref="Partida"/> cuyo índice es igual a
        /// <paramref name="id"/>.
        /// </returns>
        [HttpGet("[action]/{id}")]
        public async Task<Partida> GetPartida(long id) => await db.LibroDiario.FindAsync(id);
        /// <summary>
        /// Obtiene una movimiento.
        /// </summary>
        /// <param name="id">Id a buscar.</param>
        /// <returns>
        /// Una <see cref="CuentaGroup"/> cuyo índice es igual a
        /// <paramref name="id"/>.
        /// </returns>
        [HttpGet("[action]/{id}")]
        public async Task<Movimiento> GetMovimiento(long id) => await db.Movimientos.FindAsync(id);
        /// <summary>
        /// Obtiene un grupo de cuentas.
        /// </summary>
        /// <param name="id">Id a buscar.</param>
        /// <returns>
        /// Una <see cref="CuentaGroup"/> cuyo índice es igual a
        /// <paramref name="id"/>.
        /// </returns>
        [HttpGet("[action]/{id}")]
        public async Task<CuentaGroup> GetCuentaGroup(long id) => await db.CuentaGroups.FindAsync(id);
        #endregion

        #region Consultas relacionales
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<Cuenta>> GetCuentasOfGroup(long id)
        {
            var retVal = new List<Cuenta>();
            await Task.Run(() =>
            {
                foreach (var j in db.N2N_Cuenta_CuentaGroup)
                    if (j.CuentaGroupID.ID == id) retVal.Add(j.CuentaID);
            });
            return retVal;
        }
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<CuentaGroup>> GetGroupsOfCuenta(long id)
        {
            var retVal = new List<CuentaGroup>();
            await Task.Run(() =>
            {
                foreach (var j in db.N2N_Cuenta_CuentaGroup)
                    if (j.CuentaID.ID == id) retVal.Add(j.CuentaGroupID);
            });
            return retVal;
        }
        #endregion

        [HttpPost("[action]")]
        public async Task<IActionResult> AddCategoria([FromForm]string name, [FromForm]long parent, [FromForm]int prefix)
        {
            try
            {
                await db.AddCategoria(name, await db.Categorias.FindAsync(parent), prefix);
                return new OkResult();
            }
            catch { return new BadRequestResult(); }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddCuenta([FromForm]string name, [FromForm]long parent, [FromForm]int prefix, [FromForm]long[] memberOf)
        {
            try
            {
                var c = await db.AddCuenta(name, db.Categorias.Find(parent), prefix);
                List<CuentaGroup> lst = new List<CuentaGroup>();
                foreach (var j in memberOf) lst.Add(await db.CuentaGroups.FindAsync(j));
                if (lst.Any()) await db.GroupCuentas(new[] { c }, lst.ToArray());
                return new OkResult();
            }
            catch { return new BadRequestResult(); }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddCuentaGroup([FromForm]string name, [FromForm]long[] members)
        {
            try
            {
                var g = await db.AddCuentaGroup(name);
                List<Cuenta> lst = new List<Cuenta>();
                foreach (var j in members) lst.Add(await db.Cuentas.FindAsync(j));
                if (lst.Any()) await db.GroupCuentas(lst.ToArray(), new[] { g });
                return new OkResult();
            }
            catch { return new BadRequestResult(); }
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
