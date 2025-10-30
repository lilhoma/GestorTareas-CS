namespace GestorTareas
{
    public class Tarea
    {
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public bool Completada { get; set; }

        public Tarea(string nombre, DateTime fecha)
        {
            Nombre = nombre;
            Fecha = fecha;
            Completada = false;
        }

        public override string ToString()
        {
            string estado = Completada ? "[X]" : "[ ]";
            return $"{estado} - {Nombre} - {Fecha.ToShortDateString()}";
        }
    }
}