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

namespace ContabServer.Controllers
{
    public static class ControllerConfig
    {
        public const string DefaultRoute = "api/[controller]";

    }

    /// <summary>
    /// Clase abstracta que implementa algunas funciones escenciales de un
    /// controlador de API web.
    /// </summary>
    //[Authorize]
    [Route(ControllerConfig.DefaultRoute)]
    public abstract class APIController : Controller
    {
        #region Extras
        /// <summary>
        /// Controla la solicitud GET a la dirección <c>api/&lt;controlador&gt;/</c>.
        /// </summary>
        /// <returns>
        /// Esta función siempre devuelve un <see cref="ForbidResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Get()
        {
            return new StatusCodeResult(403);
        }
        #endregion
    }
}
