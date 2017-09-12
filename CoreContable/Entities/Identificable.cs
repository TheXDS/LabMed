/*
Copyright © 2017 César Andrés Morgan
Pendiente de licenciamiento
===============================================================================
Este archivo está pensado para uso interno exclusivamente por su autor y otro
personal autorizado. No debe ser distribuido en ningún producto comercial sin
haber antes pasado por un control de calidad adecuado, ni tampoco debe ser
considerado como código listo para producción. El autor se absuelve de toda
responsabilidad y daños causados por el uso indebido de este archivo o de
cualquier parte de su contenido.
*/

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreContable.Entities
{
    /// <summary>
    /// Clase abstracta que permite compartir la definición de algunos campos
    /// comunes entre tablas.
    /// </summary>
    public abstract class Identificable
    {
        [Key] public long ID { get; set; }
        /// <summary>
        /// Prefijo para generación de códigos de cuenta.
        /// </summary>
        public int Prefix { get; set; }
        /// <summary>
        /// Nombre para mostrar del elemento de la tabla.
        /// </summary>
        [Required] public string DisplayName { get; set; }
    }
}