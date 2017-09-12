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

namespace CoreContable.Entities
{
    /// <summary>
    /// Representa un movimiento o cambio a una cuenta dentro de una partida.
    /// </summary>
    public class Movimiento
    {
        /// <summary>
        /// Campo llave primario de este elemento.
        /// </summary>
        public long MovimientoID { get; set; }
        /// <summary>
        /// Cuenta que ha sido afectada por este movimiento.
        /// </summary>
        public Cuenta RefCuenta { get; set; }
        /// <summary>
        /// Valor por el cual la cuenta ha sido afectada.
        /// </summary>
        public decimal Value { get; set; }
    }
}
