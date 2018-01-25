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

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using static ContabServer.Logic.Commands;

namespace ContabServer.Models
{
    /// <summary>
    /// Representa el modelo de la base de datos de contabilidad.
    /// </summary>
    public class ContabContext : DbContext
    {        

//#if DEBUG_NPSQL
//            // Conector de PostgreSQL

//            const string Usr = "root";
//            const string Pwd = "TrustN@12543156";
//            const string Host = "thexds-srv1";
//            const ushort Port = 5432;
//            const string Database = "LabMedConta";
//            optionsBuilder.UseNpgsql($"User ID={Usr};Password={Pwd};Host={Host};Port={Port};Database={Database};Pooling=true;");
//#endif
//#if DEBUG_LOCAL
//            // FIXME: Por alguna razón, SQL LocalDB no permite iniciar sesión.
//            // Conector de SQL Server LocalDB

//            const string Server = @"(localdb)\mssqllocaldb";
//            const string Database = "LabMedConta";
//            optionsBuilder.UseSqlServer($"Server={Server};Database={Database};Trusted_Connection=True;Integrated security=True;");
//#endif
//#if RELEASE_TEST
//            // TODO: configurar el servidor alternativo de prueba
//            throw new System.NotImplementedException();
//#endif
//#if RELEASE
//            // TODO: configurar el servidor de producción
//            throw new System.NotImplementedException();

//            const string Server = @"(localdb)\mssqllocaldb";
//            const string Database = "LabMedConta";
//            optionsBuilder.UseSqlServer($"Server={Server};Database={Database};Trusted_Connection=True;Integrated security=True;");
//#endif


        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="ContabContext"/>.
        /// </summary>
        public ContabContext()
        {
            if (Database.CreateIfNotExists() || Categorias.Count() < 3) Task.WaitAll(InitialSetupAsync(this));
            Activo = Categorias.Find(1L);
            Pasivo = Categorias.Find(2L);
            Capital = Categorias.Find(3L);
        }

        #region Campos auxiliares
        /// <summary>
        /// Obtiene una referencia a la categoría de cuentas del Activo.
        /// </summary>
        [NotMapped] public Categoria Activo { get; }
        /// <summary>
        /// Obtiene una referencia a la categoría de cuentas del Pasivo.
        /// </summary>
        [NotMapped] public Categoria Pasivo { get; }
        /// <summary>
        /// Obtiene una referencia a la categoría de cuentas del Capital.
        /// </summary>
        [NotMapped] public Categoria Capital { get; }
        #endregion

        #region Tablas de datos
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
        #endregion

    }
}