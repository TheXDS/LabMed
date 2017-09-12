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

using System.Collections.Generic;

namespace CoreContable.Entities
{
    /// <summary>
    /// Modelo de una tabla que contiene información sobre las diferentes
    /// clases de cuentas.
    /// </summary>
    public class Categoria : Identificable
    {
        /// <summary>
        /// Enumera las cuentas contenidas en esta categoría.
        /// </summary>
        public List<Cuenta> Cuentas { get; set; }
        /// <summary>
        /// Enumera las subcategorías contenidas en esta categoría.
        /// </summary>
        public List<Categoria> SubCategorias { get; set; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Categoria"/>.
        /// </summary>
        public Categoria()
        {
            if (Cuentas == null) Cuentas = new List<Cuenta>();
            if (SubCategorias == null) SubCategorias = new List<Categoria>();
        }
    }
}