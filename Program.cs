// TODO: Filtrar Tareas completadas
// TODO: Ordenar por fecha
// TODO: Hacer que si no se agrega un fecha, se ponga la actual

using System.Text.Json;

namespace GestorTareas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Tarea> tareas = CargarTareas();


            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("----- Gestor de Tareas -----");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("1. Agregar Tareas");
                Console.WriteLine("2. Mostrar Tareas");
                Console.WriteLine("3. Marcar Tarea Como Completada");
                Console.WriteLine("4. Editar o Eliminar");
                Console.WriteLine("5. Guardar y Salir");
                Console.ResetColor();
                Console.WriteLine("Selecciona una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Clear();
                        AgregarTarea(tareas);
                        break;
                    case "2":
                        Console.Clear();
                        MostrarTareas(tareas);
                        break;
                    case "3":
                        Console.Clear();
                        MarcarCompletada(tareas);
                        break;
                    case "4":
                        Console.Clear();
                        EditarEliminar(tareas);
                        break;
                    case "5":
                        Console.Clear();
                        GuardarTareas(tareas);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Seleccione un opción valida.");
                        Console.ResetColor();
                        break;
                }
            }


            static void AgregarTarea(List<Tarea> tareas)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Nombre de la tarea: ");
                Console.ResetColor();
                string nombre = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Fecha (dd/mm/yyyy): ");
                Console.ResetColor();

                DateTime fecha = DateTime.Parse(Console.ReadLine());

                tareas.Add(new Tarea(nombre, fecha));
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Tarea Agregada Correctamente.");
                Console.ResetColor();
            }
            static void MostrarTareas(List<Tarea> tareas)
            {
                if (tareas.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("No hay tareas.");
                    Console.ResetColor();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("----- Lista de Tareas -----");
                Console.ResetColor();
                for (int i = 0; i < tareas.Count; i++)
                {
                    if (tareas[i].Completada)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.WriteLine($"{i + 1}. {tareas[i]}");
                }
                Console.ResetColor();
            }
            static void MarcarCompletada(List<Tarea> tareas)
            {
                MostrarTareas(tareas);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Ingrese la tarea a marcar: ");
                Console.ResetColor();
                if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > tareas.Count)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opción inválida. Intenta de nuevo.");
                    Console.ResetColor();
                    return;
                }

                var tarea = tareas[index - 1];
                if (tarea.Completada)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n La tarea \"{tarea.Nombre}\" ya está completada.");
                    Console.Write("¿Deseas marcarla como pendiente? (s/n): ");
                    Console.ResetColor();
                    string respuesta = Console.ReadLine()?.Trim().ToLower() ?? "";
                    if (respuesta == "s" || respuesta == "si")
                    {
                        tarea.Completada = false;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"\n Tarea \"{tarea.Nombre}\" marcada como *pendiente*.");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("\nNo se realizaron cambios.");
                    }
                    Console.ResetColor();
                    return;
                }

                tarea.Completada = true;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Tarea Completada.");
                Console.ResetColor();
            }
            static void EditarEliminar(List<Tarea> tareas)
            {
                if (tareas.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("No hay tareas para modificar.");
                    Console.ResetColor();
                    return;
                }
                MostrarTareas(tareas);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Ingrese el numero de la tarea: ");
                Console.ResetColor();
                if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > tareas.Count)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Indice Invalido");
                    Console.ResetColor();
                    return;
                }

                var tarea = tareas[index - 1];

                Console.WriteLine($"\nTarea Seleccionada: {tarea}");
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("1. Editar");
                Console.WriteLine("2. Eliminar");
                Console.WriteLine("Opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("Nuevo Nombre (Dejar vacío para no cambiar)");
                        string nuevoNombre = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(nuevoNombre))
                            tarea.Nombre = nuevoNombre;
                        Console.WriteLine("Nueva Fecha (dd/mm/yyyy) - (Dejar vacío para no cambiar)");
                        string nuevaFecha = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(nuevaFecha) && DateTime.TryParse(nuevaFecha, out DateTime fecha))
                            tarea.Fecha = fecha;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Tarea Actualizada.");
                        Console.ResetColor();
                        break;

                    case "2":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        System.Console.WriteLine($"¿Desea eliminar la tarea? '{tarea.Nombre}' (s/n)");
                        string respuesta = Console.ReadLine()?.Trim().ToLower() ?? "";
                        if (respuesta == "s")
                        {
                            tareas.RemoveAt(index - 1);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Tarea Eliminada.");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine("Acción Cancelada.");
                        }
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Opción No Valida.");
                        Console.ResetColor();
                        break;
                }


            }
            static void GuardarTareas(List<Tarea> tareas)
            {
                string json = JsonSerializer.Serialize(tareas, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("tareas.json", json);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Guardando Tareas...");
                Console.ResetColor();
                Thread.Sleep(2000);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Tareas guardadas correctamente. Saliendo...");
                Console.ResetColor();

                Thread.Sleep(1500);
                Environment.Exit(0);

            }

            static List<Tarea> CargarTareas()
            {
                if (File.Exists("tareas.json"))
                {
                    string json = File.ReadAllText("tareas.json");
                    return JsonSerializer.Deserialize<List<Tarea>>(json);
                }
                return new List<Tarea>();
            }
        }
    }
}