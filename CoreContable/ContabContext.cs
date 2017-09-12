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

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using static CoreContable.Logic.Commands;

namespace CoreContable.Entities
{
    /// <summary>
    /// Representa el modelo de la base de datos de contabilidad.
    /// </summary>
    public class ContabContext : DbContext
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="ContabContext"/>.
        /// </summary>
        public ContabContext() : base("LabMed.Conta")
        {
            if (!Database.Exists() || Categorias.Count() < 3) Task.WaitAll(InitialSetupAsync(this));
            Activo = Categorias.Find(1);
            Pasivo = Categorias.Find(2);
            Capital = Categorias.Find(3);
        }

        /// <summary>
        /// Obtiene una referencia a la categoría de cuentas del Activo.
        /// </summary>
        [NotMapped] public readonly Categoria Activo;
        /// <summary>
        /// Obtiene una referencia a la categoría de cuentas del Pasivo.
        /// </summary>
        [NotMapped] public readonly Categoria Pasivo;
        /// <summary>
        /// Obtiene una referencia a la categoría de cuentas del Capital.
        /// </summary>
        [NotMapped] public readonly Categoria Capital;

        /// <summary>
        /// Tabla que contiene las diferentes agrupaciones o clases de cuentas.
        /// </summary>
        public DbSet<Categoria> Categorias { get; set; }
        /// <summary>
        /// Tabla que contiene las cuentas específicas.
        /// </summary>
        public DbSet<Cuenta> Cuentas { get; set; }
        /// <summary>
        /// Tabla que contiene todas las partidas del libro diario.
        /// </summary>
        public DbSet<Partida> LibroDiario { get; set; }
        /// <summary>
        /// Tabla que contiene todos los movimientos.
        /// </summary>
        public DbSet<Movimiento> Movimientos { get; set; }
        /// <summary>
        /// Tabla que contiene las definiciones de grupos de cuentas.
        /// </summary>
        public DbSet<CuentaGroup> CuentaGroups { get; set; }
    }
}