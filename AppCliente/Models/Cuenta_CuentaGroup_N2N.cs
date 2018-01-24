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
    /// Tabla de relación N a N entre <see cref="Cuenta"/> y
    /// <see cref="CuentaGroup"/>.
    /// </summary>
    public class Cuenta_CuentaGroup_N2N
    {
        public long ID { get; set; }
        /// <summary>
        /// <see cref="Cuenta"/> relacionada.
        /// </summary>
        public Cuenta CuentaID { get; set; }
        /// <summary>
        /// <see cref="CuentaGroup"/> relacionado.
        /// </summary>
        public CuentaGroup CuentaGroupID { get; set; }
    }
}