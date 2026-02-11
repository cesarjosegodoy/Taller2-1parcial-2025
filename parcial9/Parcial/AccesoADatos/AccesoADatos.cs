using MiWebAPI.TvProgram;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MiWebAPI.AcDatosTV

{
    ///<summary>
    ///Representa el Acceso a Datos de la Canal TV
    ///</summary>
    public class AccesoADatosCanalTV
    {
        //private static readonly string FilePath = Path.Combine(AppContext.BaseDirectory, "Data", "data.json"); // ruta más fiable

        private const string FilePath = "Data/data.json";
        private static readonly object _lock = new();

/*
        private List<Programa> LeerDatos() // lee el archivo
        {
            if (!File.Exists(FilePath))
                return new List<Programa>();

            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Programa>>(json) ?? new List<Programa>(); // retorna el Listado de Estudiantes, si no encuentra nos devuelve una lista vacia con new

        }

        private void GuardarProgramas(List<Programa> programas)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(programas, options); // serealiza lo que llega
            File.WriteAllText(FilePath, json); // lo guarda como texto
        }

*/
        
             private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };
        static AccesoADatosCanalTV()
        {
            _jsonOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        }
        
        private List<Programa> LeerDatos()
         {
             try
             {
                 if (!File.Exists(FilePath))
                     return new List<Programa>();

                 var json = File.ReadAllText(FilePath);
                 return JsonSerializer.Deserialize<List<Programa>>(json, _jsonOptions) ?? new List<Programa>();
             }
             catch (Exception ex)
             {
                 // Loguear para debugging (puede ser Console, ILogger, etc.)
                 Console.WriteLine($"ERROR LeerDatos: {ex.Message}");
                 // Evitar que la API se caiga: devolver lista vacía o re-lanzar según tu política
                 return new List<Programa>();
             }
         }

         private void GuardarProgramas(List<Programa> programa)
         {
             try
             {
                 var dir = Path.GetDirectoryName(FilePath);
                 if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                     Directory.CreateDirectory(dir);

                 var json = JsonSerializer.Serialize(programa, _jsonOptions);
                 File.WriteAllText(FilePath, json);
             }
             catch (Exception ex)
             {
                 Console.WriteLine($"ERROR GuardarPedido: {ex.Message}");
                 throw; // o manejar según prefieras
             }
         }
 

        ///<summary> Lista la los programas </summary>
        public List<Programa> GetAll() // permite obtener todos los programas
        {   // esta parte se llama semaforos
            lock (_lock) // lock hace que solo ingrese una persona a la vez
            {
                return LeerDatos();
            }

        }


        ///<summary>
        ///Agrega un Pedido nuevo al listado
        ///</summary>
        ///<returns>200 OK - Pedido Agregado </returns>
        public Programa AddPrograma(Programa programa)
        {
            lock (_lock)
            {
                var programas = LeerDatos();
                programa.Id = programas.Any() ? programas.Max(e => e.Id) + 1 : 1; //pedido.Any() ? pregunto me devuelve un bool si existe me retorna un id y sino el 1 que sera el primer estudiante que creo

                // if (programa.Estadopedido == 0)
                //     programa.Estadopedido = Estado.Pendiente;

                programas.Add(programa);// agrego el programa a la grilla
                GuardarProgramas(programas);
                return programa;
            }

        }


        /// <summary>Lista un programa por su Id.</summary>
        public Programa? GetById(int id) // ? Este método puede devolver un Pedido o null.
        {
            lock (_lock)
            {

                return LeerDatos().FirstOrDefault(e => e.Id == id);
            }


        }

            /// <summary>Lista un programa por su Id.</summary>
        public Programa? GetByDay(Estado day) // ? Este método puede devolver un Pedido o null.
        {
            lock (_lock)
            {

                return LeerDatos().FirstOrDefault(e => e.DiaDeLaSemana == day);
            }


        }
        /// <summary>Elimina un programa.</summary>
        public bool Delete(int id)
        {
            lock (_lock)
            {
                var programas = LeerDatos();
                var programa = programas.FirstOrDefault(e => e.Id == id);
                if (programa is null) return false;

                programas.Remove(programa);
                GuardarProgramas(programas);
                return true;
            }
        }


        public bool UpdatePrograma(int id, Programa programa)
        {
            lock (_lock)
            {
                var programas = LeerDatos();
                var existente = programas.FirstOrDefault(p => p.Id == id);
                if (existente is null) return false;

                existente.Title = programa.Title;
                existente.Genero = programa.Genero;
                existente.DiaDeLaSemana = programa.DiaDeLaSemana;
                existente.StartTime = programa.StartTime;
                existente.DurationMinutes = programa.DurationMinutes;

                GuardarProgramas(programas);
                return true;
            }
        }


    }
}