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
    ||||
  --++++--
    ||||
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
using static System.Diagnostics.Debug;

namespace Tests.DangerZone
{
    [TestClass]
    partial class ExamenConcept
    {
        [TestMethod]
        public void MyTestMethod()
        {
            Muestra orina = new Muestra { nombre = "Orina" };
            Muestra sangre = new Muestra { nombre = "Sangre" };
            Muestra heces = new Muestra { nombre = "Heces" };

            Prueba miados = new Prueba { nombre = "Miadología" };
            miados.muestrasNecesarias.Add(orina);
            Prueba mierda = new Prueba { nombre = "Mierdología" };
            mierda.muestrasNecesarias.Add(heces);
            Prueba vihTest = new Prueba { nombre = "Prueba de VIH" };
            vihTest.muestrasNecesarias.Add(sangre);
            Prueba glucosa = new Prueba { nombre = "Prueba de glucosa" };
            glucosa.muestrasNecesarias.Add(sangre);

            ResultadoDef sida = new ResultadoDef
            {
                descripcion = "Presencia de VIH",
                unit = null,
                tipo = ResultType.Bool,
                morInfo = null
            };
            sida.pruebas.Add(vihTest);

            ResultadoDef glucLevels = new ResultadoDef
            {
                descripcion = "Nivel de glucosa",
                unit = "mg/L",
                tipo = ResultType.Range
            };
            glucLevels.morInfo.Add(new MorInfo
            {
                pregunta = "Persona tanque",
                tipoRespuesta = ResultType.Bool,
                forPrueba = false
            });
            glucLevels.pruebas.Add(glucosa);
            RangeVal tanqueyes = new RangeVal
            {
                Value = "100 - 160"
            };
            tanqueyes.conditions.Add(new Condition
            {
                question = glucLevels.morInfo.First(),
                response = "true"
            });
            RangeVal tanqueno = new RangeVal
            {
                Value = "60 - 110"
            };
            tanqueno.conditions.Add(new Condition
            {
                question = glucLevels.morInfo.First(),
                response = "false"
            });
            glucLevels.normales.Add(tanqueyes);
            glucLevels.normales.Add(tanqueno);

            ExamenDef PruebaSida = new ExamenDef
            {
                descripcion = "Exaxmen de VIH",
                precio = 250
            };
            PruebaSida.analisis.Add(sida);

            ExamenDef TestGlucosa = new ExamenDef
            {
                descripcion = "Examen de niveles de glucosa",
                precio = 450
            };
            TestGlucosa.analisis.Add(glucLevels);

            ExamenDef EstaVivo = new ExamenDef
            {
                descripcion = "Salud General",
                precio = 100
            };
            EstaVivo.depends.Add(PruebaSida);
            EstaVivo.depends.Add(TestGlucosa);


            Paciente karla = new Paciente
            {
                nombre = "Karla Perez",
                nacimiento = DateTime.Parse("5/5/1965"),
                genero = Genero.Femenino,
                peso = 45.5f,
                altura = 1.66f
            };
            Factura kFueAlDr = new Factura
            {
                paciente = karla
            };
            Examen kexamen = new Examen
            {
                examenDef = EstaVivo,
                storedPrecio = EstaVivo.precio
            };
            kFueAlDr.examenes.Add(kexamen);

            Print($"Paciente: {kFueAlDr.paciente.nombre}");
            Print("Se hará los siguientes exámenes:");
            List<MorInfo> todoResp = new List<MorInfo>();
            todoResp.AddRange(kFueAlDr.paciente.ToMorInfo());
            foreach (var j in kFueAlDr.examenes)
            {
                Print($"{j.examenDef.descripcion}, el cual arroja:");
                foreach (var k in j.examenDef.analisis)
                {
                    Print($"{k.descripcion}, el cual realizará:");
                    foreach (var l in k.pruebas)
                    {
                        Print($"{l.nombre}, el cual requiere las muestras:");
                        foreach (var m in l.muestrasNecesarias)
                        {
                            Print(m.nombre);
                        }
                        Print("-");
                    }
                    if ((bool)k.morInfo?.Any())
                    {
                        todoResp.AddRange(k.morInfo);
                        Print("Se necesita obtener la siguiente información adicional:");
                        foreach (var l in k.morInfo)
                        {
                            Print($"Pregunta: {l.pregunta}, tipo: {l.tipoRespuesta.ToString()}");
                            foreach (var m in kFueAlDr.respuestas)
                            {
                                bool resp = false;
                                if (ReferenceEquals(m.question, l.pregunta))
                                {
                                    resp = true;
                                    Print($"En el caso de {kFueAlDr.paciente.nombre}, la respuesta es: {m.response}.");
                                    break;
                                }
                                if (!resp) Print($"Se deberá preguntar {l.pregunta}. Crear UI.");
                            }
                        }
                    }
                }
                Print("-");
            }
            Print("===== Ae fiew mouments lategggg... =====");
            Print("haciendo exámenes...");
            foreach (var j in kFueAlDr.examenes)
            {
                foreach (var k in j.examenDef.analisis)
                {
                    // Inventar resultados...
                    // En teoría, este switch es un formulario.
                    string r = null;
                    switch (k.tipo)
                    {
                        case ResultType.Bool:
                            r = "true";
                            break;
                        case ResultType.Date:
                            r = "8/8/2006";
                            break;
                        case ResultType.Range:
                            r = "46";
                            break;
                        case ResultType.Text:
                            r = "Se ha encontrado que blah, blah, blah...";
                            break;
                    }
                    j.resultados.Add(new Resultado
                    {
                        resultado = r,
                        resultadoDef = k
                    });
                }
            }
            Print("===== Ae fiew mouments lategggg... =====");
            Print("Resultados:");
            foreach (var j in kFueAlDr.examenes)
            {
                foreach (var k in j.resultados)
                {
                    switch (k.resultadoDef.tipo)
                    {
                        case ResultType.Bool:
                            Print($"{k.resultadoDef.descripcion}: {(k.resultado == "true" ? "Positivo" : "Negativo")}");
                            break;
                        case ResultType.Range:
                            string sb = string.Empty;
                            foreach (var l in k.resultadoDef.normales)
                            {
                                bool ev = true;
                                foreach (var m in l.conditions)
                                {
                                    foreach (var n in kFueAlDr.respuestas)
                                    {
                                        if (n.response != m.response)
                                        {
                                            ev = false;
                                            break;
                                        }
                                    }
                                }
                                if (ev)
                                {
                                    sb = l.Value;
                                    break;
                                }
                            }
                            Print($"{k.resultadoDef.descripcion}: {k.resultado} {k.resultadoDef.unit}, {sb}");
                            break;
                        default:
                            Print($"{k.resultadoDef.descripcion}: {k.resultado}");
                            break;
                    }
                    Print($"{k.resultadoDef.descripcion}:");
                }
            }
        }
    }
}