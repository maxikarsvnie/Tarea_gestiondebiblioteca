using System;
using System.Collections.Generic;
using System.Linq;
using BibliotecaApp;

namespace Biblioteca
{
    class Program
    {
        static void Main(string[] args)
        {

            //Parte B: Operaciones -----------------------------
            Console.WriteLine("=== Paso 1: Cargar Registros de Material ===");
            
            List<Material> registros = new List<Material>
            {
                new Libro("M001", "El Aleph", "Libro", 2000, "Borges, J.L", 274),
                new Libro("M002", "Rayuela", "Libro", 1963, "Cortázar, J.", 600),
                new Revista("M003", "National Geo", "Revista", 2023, "NatGeo Ed.", 45),
                new Libro("M004", "Cien años", "Libro", 1967, "García Márquez, G.", 432),
                new Revista("M005", "Time Magazine", "Revista", 2024, "Time Inc.", 12),
                new Libro("M006", "Ficcones", "Libro", 1944, "Borges, J.L", 186)
            };
            foreach (var material in registros)
            {
                Console.WriteLine(material.ObtenerFicha());
            }

            Console.WriteLine("\n=== Paso 2: Cargar registros de prestamos ===");

            List<Prestamo> registro2 = new List<Prestamo>
            {
                new Prestamo("P001", "M001", "Ana López", "Lectura recreativa", 0, new DateTime(2026, 4, 1)),
                new Prestamo("P002", "M001", "Luis Paz", "Investigación", 150, new DateTime(2026, 4, 15)),
                new Prestamo("P003", "M002", "Ana López", "Tarea universitaria", 0, new DateTime(2026, 4, 10)),
                new Prestamo("P004", "M003", "Mario Ruiz", "Información general", 0, new DateTime(2026, 3, 20)),
                new Prestamo("P005", "M004", "Luis Paz", "Lectura recreativa", 200, new DateTime(2026, 4, 5)),
                new Prestamo("P006", "M005", "Ana López", "Investigación", 0, new DateTime(2026, 4, 22)),
                new Prestamo("P007", "M006", "Mario Ruiz", "Tarea universitaria", 0, new DateTime(2026, 4, 25)),
                new Prestamo("P008", "M003", "Luis Paz", "Revisión periódica", 100, new DateTime(2026, 2, 18))
            };
            foreach (var prestamo in registro2)
            {
                Console.WriteLine(prestamo.IdPrestamo + " | " + prestamo.IdMaterial + " | " + prestamo.NombreSocio + " | " + prestamo.Motivo + " | " + prestamo.multa + " | " + prestamo.Fecha.ToShortDateString());
            }

            Console.WriteLine("\n=== Paso 3: Agregar Nuevo Registro ===");
            Libro agregarRegistro = new Libro("M007", "Don Quijote", "Libro", 1605, "Cervantes, M.", 863);
            registros.Add(agregarRegistro);
            
            Console.WriteLine("Don quijote agregado exitosamente.");
                        
            Console.WriteLine(agregarRegistro.ObtenerFicha());
            
            Console.WriteLine("\n=== Paso 4: Eliminar un registro ===");

            // Buscar y eliminar "National Geo"
            Material? registroAEliminar = registros.FirstOrDefault(r => r.Nombre == "National Geo");
            if (registroAEliminar != null)
            {
                registros.Remove(registroAEliminar);
                Console.WriteLine("National Geo eliminado del sistema.");
            }
            else
            {
                Console.WriteLine("No se encontró ningún registro con el nombre National Geo.");
            }

            // Intentar eliminar "Ciencia Hoy"
            Material? registroInexistente = registros.FirstOrDefault(r => r.Nombre == "Ciencia Hoy");
            if (registroInexistente != null)
            {
                registros.Remove(registroInexistente);
                Console.WriteLine("Ciencia Hoy eliminado del sistema.");
            }
            else
            {
                Console.WriteLine("No se encontró ningún registro con el nombre Ciencia Hoy.");
            }

            Console.WriteLine("\n=== Paso 5: Recorrido polimórfico ===");

            // POLIMORFISMO: la lista es de tipo Material, pero en tiempo de ejecución
            // .NET invoca el ObtenerFicha() real de cada objeto (Libro o Revista).

            foreach (Material material in registros)
            {
                Console.WriteLine(material.ObtenerFicha());

                if (material is Libro libro)
                {
                    libro.AccionPropia();
                }
            }
            Console.WriteLine("\n=== Paso 6: Usar IPrestable ===");

            // Buscar el material con nombre "El Aleph"
            Material? materialAleph = registros.FirstOrDefault(r => r.Nombre == "El Aleph");

            if (materialAleph is IPrestable prestable)
            {
                // Ejecutar Prestar
                prestable.Prestar("prueba");
                Console.WriteLine("✔ Prestar aplicado a El Aleph.");

                // Ejecutar Devolver
                prestable.Devolver();
                Console.WriteLine("✔ Devolver ejecutado para El Aleph.");

                // Ejecutar ObtenerEstado
                string estado = prestable.ObtenerEstado();
                Console.WriteLine("Estado de El Aleph: " + estado);
            }
            else
            {
                Console.WriteLine("No se encontró el registro El Aleph o no implementa IPrestable.");
            }

            //Parte C: Consultas --------------------------------
            Console.WriteLine("\nConsulta 1: Materiales ordenados de mayor a menor año ===");

            var ordenadosDesc = registros.OrderByDescending(m => m.Anio);

            foreach (var material in ordenadosDesc)
            {
                Console.WriteLine($"{material.Nombre} — {material.Anio}");
            }
            Console.WriteLine("\nConsulta 2: Préstamos de Ana López en abril 2026 ===");

            var prestamosAnaAbril2026 = registro2

                .Where(p => p.NombreSocio == "Ana López"
                        && p.Fecha.Month == 4
                        && p.Fecha.Year == 2026);

            foreach (var prestamo in prestamosAnaAbril2026)
            {
                Console.WriteLine($"{prestamo.Motivo} | ID: {prestamo.IdPrestamo} | Importe: ${prestamo.multa}");
            }
            Console.WriteLine("\nConsulta 3: Total de multas por material ===");

            var multasPorMaterial = registro2
                .GroupBy(p => p.IdMaterial)
                .Select(g => new
                {
                    IdMaterial = g.Key,
                    TotalMulta = g.Sum(p => p.multa),
                    Nombre = registros.FirstOrDefault(m => m.ObtenerFicha().Contains(g.Key))?.Nombre
                })
                .OrderByDescending(x => x.TotalMulta);

            foreach (var item in multasPorMaterial)
            {
                Console.WriteLine($"{item.Nombre}: ${item.TotalMulta}");
            }
            Console.WriteLine("\n=== Parte C - Consulta 4: Estadísticas generales ===");

            // Total de materiales registrados
            int totalMateriales = registros.Count;

            // Cantidad de libros
            int cantidadLibros = registros.OfType<Libro>().Count();

            // Cantidad de revistas
            int cantidadRevistas = registros.OfType<Revista>().Count();

            // Multa promedio
            decimal multaPromedio = registro2.Average(p => p.multa);

            // Préstamo con mayor multa
            var mayorMulta = registro2.OrderByDescending(p => p.multa).FirstOrDefault();

            Console.WriteLine($"Total de materiales registrados: {totalMateriales}");
            Console.WriteLine($"Cantidad de libros: {cantidadLibros}");
            Console.WriteLine($"Cantidad de revistas: {cantidadRevistas}");
            Console.WriteLine($"Multa promedio: {multaPromedio}");
            Console.WriteLine($"Préstamo con mayor multa: {mayorMulta?.multa}");

            //Problema 2------------------------------------------------
            //Parte A--------------------------------------------------
            Console.WriteLine("\n=== Problema 2 - Tarea 1: Historial de El Aleph ===");

            // Lista auxiliar para acumular préstamos de El Aleph
            List<Prestamo> historialAleph = new List<Prestamo>();

            // Recorrido con for
            for (int x = 0; x < registro2.Count; x++)
            {
                if (registro2[x].IdMaterial == "M001") // El Aleph
                {
                    historialAleph.Add(registro2[x]);
                }
            }

            // Recorrido con foreach para imprimir
            decimal totalAleph = 0;
            foreach (var prestamo in historialAleph)
            {
                Console.WriteLine($"{prestamo.IdPrestamo} | {prestamo.Fecha:dd/MM/yyyy} | {prestamo.Motivo} | Responsable: {prestamo.NombreSocio} | ${prestamo.multa}");
                totalAleph += prestamo.multa;
            }

            // Total acumulado
            Console.WriteLine($"Total acumulado de El Aleph: ${totalAleph}");
            
            Console.WriteLine("\n=== Problema 2 - Tarea 2: Tabla de costos base ===");

            int i = 0;
            while (i < registros.Count)
            {
                var material = registros[i];
                decimal costoBase = material.CalcularCostoBase();

                Console.WriteLine($"{material.Nombre} ({material.Categoria}) → Costo base: ${costoBase}");

                i++;
            }
            Console.WriteLine("\n=== Problema 2 - Tarea 3: Reporte acumulado por responsable ===");
            Console.WriteLine("=== REPORTE POR RESPONSABLE ===");

            string[] responsables = { "Ana López", "Luis Paz", "Mario Ruiz" };

            int index = 0;
            decimal totalGeneral = 0;

            do
            {
                string responsable = responsables[index];//{"Ana López", "Luis Paz", "Mario Ruiz"};
                int cantidad = 0;
                decimal suma = 0;

                for (int z = 0; z < registro2.Count; z++)
                {
                    if (registro2[z].NombreSocio == responsable)
                    {
                        cantidad++;
                        suma += registro2[z].multa;
                    }
                }

                Console.WriteLine($"{responsable} → {cantidad} registros | Total: ${suma}");
                totalGeneral += suma;

                index++;
            } while (index < responsables.Length);

            Console.WriteLine("─────────────────────────────");
            Console.WriteLine($"TOTAL GENERAL: ${totalGeneral}");
            
            //Parte B-------------------------------------------------

            Console.WriteLine("\n=== Parte B - Ejercicio 1: BuscarMaterialPorId ===");

            var enc = Utilidades.BuscarMaterialPorId(registros, "M004");
            Console.WriteLine(enc != null ? enc.ObtenerFicha() : "M004 no encontrado.");

            var noEnc = Utilidades.BuscarMaterialPorId(registros, "M999");
            Console.WriteLine(noEnc != null ? noEnc.ObtenerFicha() : "M999 no encontrado.");

          Console.WriteLine("\n=== Parte B - Ejercicio 2: Costos totales por material ===");

            decimal[] costosPorMaterial = new decimal[registros.Count];

            // bucles anidados: materiales × préstamos
            for (int a = 0; a < registros.Count; a++)
            {
                for (int j = 0; j < registro2.Count; j++)
                {
                    // extraemos el Id del material desde la ficha
                    string idMaterial = registros[a].ObtenerFicha().Split('|')[0].Trim();

                    if (idMaterial == registro2[j].IdMaterial)
                    {
                        // costo de UNA consulta: base unitario + multa
                        decimal costoConsulta = registros[a].CalcularCostoBase() + registro2[j].multa;
                        costosPorMaterial[a] += costoConsulta;
                    }
                }
            }

            // mostrar totales por material
            for (int n = 0; n < registros.Count; n++)
            {
                Console.WriteLine($"{registros[n].Nombre}: ${costosPorMaterial[n]}");
            }

            // mayor gasto
            decimal mayor = decimal.MinValue;
            string nombreMayor = "";
            for (int b = 0; b < registros.Count; b++)
            {
                if (costosPorMaterial[b] > mayor)
                {
                    mayor = costosPorMaterial[b];
                    nombreMayor = registros[b].Nombre;
                }
            }
            Console.WriteLine($"Mayor gasto: {nombreMayor} — ${mayor}");

            // menor gasto (solo con consultas > 0)
            decimal menor = decimal.MaxValue;
            string nombreMenor = "";
            int contadorConConsultas = 0;
            decimal sumaConsultas = 0;

            for (int c = 0; c < registros.Count; c++)
            {
                if (costosPorMaterial[c] > 0)
                {
                    contadorConConsultas++;
                    sumaConsultas += costosPorMaterial[c];

                    if (costosPorMaterial[c] < menor)
                    {
                        menor = costosPorMaterial[c];
                        nombreMenor = registros[c].Nombre;
                    }
                }
            }

            Console.WriteLine($"Menor gasto: {nombreMenor} — ${menor}");

            // promedio
            decimal promedio = sumaConsultas / contadorConConsultas;
            Console.WriteLine($"Promedio: ${promedio}");

            Console.WriteLine("\n=== Parte B - Ejercicio 3: Matriz materiales × responsables ===");

            decimal[,] matriz = new decimal[registros.Count, 3];
            string[] NombreSocio = { "Ana López", "Luis Paz", "Mario Ruiz" };

            // triple for
            for (int d = 0; d < registros.Count; d++)
            {
                for (int j = 0; j < registro2.Count; j++)
                {
                    // obtenemos el Id del material desde la ficha
                    string idMaterial = registros[d].ObtenerFicha().Split('|')[0].Trim();

                    if (idMaterial == registro2[j].IdMaterial)
                    {
                        for (int k = 0; k < NombreSocio.Length; k++)
                        {
                            if (registro2[j].NombreSocio == NombreSocio[k])
                            {
                                decimal costoConsulta = registros[d].CalcularCostoBase() + registro2[j].multa;
                                matriz[d, k] += costoConsulta;
                            }
                        }
                    }
                }
            }

            // imprimir tabla
            Console.WriteLine("Material\tAna López\tLuis Paz\tMario Ruiz");
            for (int e = 0; e < registros.Count; e++)
            {
                Console.Write($"{registros[e].Nombre}\t");
                for (int k = 0; k < responsables.Length; k++)
                {
                    Console.Write($"{matriz[e, k]}\t");
                }
                Console.WriteLine();
            }

            // totales por responsable
            decimal[] totales = new decimal[responsables.Length];
            int[] prestamos = new int[responsables.Length];

            for (int k = 0; k < responsables.Length; k++)
            {
                for (int y = 0; y < registros.Count; y++)
                {
                    if (matriz[y, k] > 0)
                    {
                        prestamos[k]++; // contamos préstamos
                        totales[k] += matriz[y, k];
                    }
                }
                Console.WriteLine($"{responsables[k]} {prestamos[k]} préstamos ${totales[k]}");
            }

            // responsable con mayor recaudación
            decimal max = decimal.MinValue;
            string responsableMax = "";
            for (int k = 0; k < responsables.Length; k++)
            {
                if (totales[k] > max)
                {
                    max = totales[k];
                    responsableMax = responsables[k];
                }
            }
            Console.WriteLine($"Responsable con mayor recaudación: {responsableMax}");

        }
    }
}