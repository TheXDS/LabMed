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

namespace ContabServer.Models
{
    /// <summary>
    /// Modelo de una tabla que contiene la definición de una cuenta
    /// específica.
    /// </summary>
    public class Cuenta : Identificable
    {
        /// <summary>
        /// Valor en caché almacenado con el balance actual de la cuenta.
        /// </summary>
        public decimal CachedValue { get; set; }

        /// <summary>
        /// Valor opcional que permite agrupar al elemento.
        /// </summary>
        /// public List<CuentaGroup> MemberOf { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Cuenta"/>.
        /// </summary>
        public Cuenta()
        {
            // MemberOf = new List<CuentaGroup>();
        }
    }
}