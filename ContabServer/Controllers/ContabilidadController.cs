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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContabServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ContabServer.Logic;

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
        public async Task<IEnumerable<Cuenta>> GetCuentasOfGroup(long id)
        {
            var retVal = new List<Cuenta>();
            await Task.Run(() =>
            {
                foreach (var j in db.N2N_Cuenta_CuentaGroup)
                {
                    if (j.CuentaGroupID.ID == id) retVal.Add(j.CuentaID);
                }
            });
            return retVal;
        }
        #endregion






        [HttpPost("[action]")]
        public async Task<IActionResult> AddCategoria(
            [FromForm]string name,
            [FromForm]long parent,
            [FromForm]int prefix
            )
        {
            try
            {
                await Commands.AddCategoria(db, name, db.Categorias.Find(parent), prefix);
                return new OkResult();
            }
            catch
            {
                return new BadRequestResult();
            }
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
