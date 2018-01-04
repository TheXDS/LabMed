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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContabServer.Models
{
    /// <summary>
    /// Componente escencial del libro diario. Describe una operación contable.
    /// </summary>
    public class Partida
    {
        /// <summary>
        /// Campo llave primario de este elemento.
        /// </summary>
        [Key] public long PartidaID { get; set; }
        /// <summary>
        /// Momento en el que se ha efectuado una operación contable.
        /// </summary>
        public DateTime TimeStamp { get; set; }
        /// <summary>
        /// Descripción de la partida.
        /// </summary>
        public string Synopsys { get; set; }
        /// <summary>
        /// Lista de movimientos ocurridos en esta partida.
        /// </summary>
        public List<Movimiento> Movimientos { get; set; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Partida"/>.
        /// </summary>
        public Partida()
        {
            Movimientos = new List<Movimiento>();
        }
        /// <summary>
        /// Determina si esta partida es válida (está cuadrada).
        /// </summary>
        [NotMapped]
        public bool IsValid
        {
            get
            {
                decimal result = 0;
                foreach (var j in Movimientos)
                    result += j.Value;
                return (result == 0);
            }
        }
    }
}
