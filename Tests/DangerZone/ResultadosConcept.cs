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
/*
   ______
 (   (    )
(  (    )  )
 (________)
    |  |
  --+--+--
    |  |
   (    )
 (_(____)_)

==== PELIGRO: Archivo explosivo! ==== 
*/
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreContable.Entities;
using static CoreContable.Logic.Commands;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Tests.DangerZone
{
    partial class ExamenConcept
    {
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        sealed class QuestionAttribute : Attribute
        {
            public readonly string Value;
            public readonly ResultType Tipo;
            public QuestionAttribute(string displaytext, ResultType resultType)
            {
                Value = displaytext;
            }
        }
        enum ResultType
        {
            Text,
            Enum,
            Bool,
            Date,
            Range,
            Series
        }
        enum Genero
        {
            Femenino,
            Masculino
        }

        class Factura
        {
            /// <summary>
            /// Referencia al paciente de esta factura.
            /// </summary>
            public Paciente paciente;
            /// <summary>
            /// Lista ded examenes que el paciente se hará.
            /// </summary>
            public List<Examen> examenes = new List<Examen>();
            /// <summary>
            /// Respuestas brindadas por el paciente.
            /// </summary>
            public List<Condition> respuestas = new List<Condition>();
        }
        class Paciente
        {
            public string nombre;
            public DateTime nacimiento;
            [Question("Género", ResultType.Enum)]
            public Genero genero;
            /// <summary>
            /// peso del paciente, en Kg.
            /// </summary>
            [Question("Peso (en Kg)", ResultType.Range)]
            public float peso;
            /// <summary>
            /// Altura del paciente, en metros.
            /// </summary>
            [Question("Altura (en metros)", ResultType.Range)]
            public float altura;

            [Question("Edad", ResultType.Range)]
            public int Edad => (int)((DateTime.Now - nacimiento).TotalDays / 365.25);
            public IEnumerable<MorInfo> ToMorInfo()
            {
                foreach (var j in GetType().GetMembers())
                {
                    object[] attrs = j.GetCustomAttributes(typeof(QuestionAttribute), true);
                    if ((bool)attrs?.Any() && (j.MemberType & (System.Reflection.MemberTypes.Property | System.Reflection.MemberTypes.Field)) != 0)
                    {
                        QuestionAttribute q = attrs.First() as QuestionAttribute;
                        yield return new MorInfo
                        {
                            pregunta = q?.Value,
                            tipoRespuesta = q?.Tipo ?? default(ResultType)
                        };
                    }
                }
            }
        }
        class Examen
        {
            /// <summary>
            /// Referencia al tipo de examen
            /// </summary>
            public ExamenDef examenDef;
            /// <summary>
            /// Resultado obtenido del examen.
            /// </summary>
            public string actualResult;
            /// <summary>
            /// Precio al que se hizo este examen.
            /// </summary>
            public decimal storedPrecio;
            /// <summary>
            /// guarda todos los resultados de las pruebas
            /// </summary>
            public List<Resultado> resultados = new List<Resultado>();
        }
        class Resultado
        {
            public ResultadoDef resultadoDef;
            public string resultado;
        }
        class ExamenDef
        {
            /// <summary>
            /// Descripción del examen
            /// </summary>
            public string descripcion;
            /// <summary>
            /// Define los resultados que este examen arroja.
            /// </summary>
            public List<ResultadoDef> analisis = new List<ResultadoDef>();
            /// <summary>
            /// Lista de exámenes de los cuales depende este.
            /// </summary>
            public List<ExamenDef> depends = new List<ExamenDef>();
            /// <summary>
            /// Costo del examen.
            /// </summary>
            public decimal precio;
        }
        class Prueba
        {
            public string nombre;
            /// <summary>
            /// Lista de las muestras que son necesarias para este resultado.
            /// </summary>
            public List<Muestra> muestrasNecesarias = new List<Muestra>();
            public TimeSpan tardado;
        }
        class MorInfo
        {
            public string pregunta;
            public ResultType tipoRespuesta;
            /// <summary>
            /// Si es true, esta pregunta aparece en la hoja de trabajo.
            /// </summary>
            public bool forPrueba;
        }
        class ResultadoDef
        {
            public string descripcion;
            public string unit;
            public ResultType tipo;
            // Morinfo ayuda a interpretar el metadata.
            /// <summary>
            /// Preguntas adicionales que este resultado necesita.
            /// </summary>
            public List<MorInfo> morInfo = new List<MorInfo>();
            /// <summary>
            /// Lista de pruebas necesarias para obtener este resultado.
            /// </summary>
            public List<Prueba> pruebas = new List<Prueba>();
            /// <summary>
            /// Para rangos, lista los diferentes valores normales.
            /// </summary>
            public List<RangeVal> normales = new List<RangeVal>();
        }
        class RangeVal
        {
            public string Value;
            public List<Condition> conditions = new List<Condition>();
        }
        class Condition
        {
            public MorInfo question;
            /// <summary>
            /// Respuesta que hace válido a esta condición.
            /// </summary>
            public string response;
            // Necesita un switch con cada tipo de enum y un parsing para saber si el valor dado hace a esto válido.
        }
        /// <summary>
        /// define las muestras que este lab. acepta.
        /// </summary>
        class Muestra
        {
            public  string nombre;

        }
    }
}