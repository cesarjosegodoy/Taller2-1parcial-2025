
namespace MiWebAPI.TvProgram
{
    ///<summary>
    ///Representa a un clase  Programa con su Id, Title , Genero, DiaDeLaSemana, StartTime y DurationMinutes
    ///</summary>
    public class Programa
    {

        private int id;
        private string? title; // Agrego el ? para  aceptar valores nulos para que no me de la Advertencia 
        private string? genero;
        private Estado diaDeLaSemana;
        private int startTime;
        private int durationMinutes;

    /*
        ///<summary> Constructor vacío (necesario para deserialización JSON). </summary>
       // public Programa() { }

        ///<summary> Contructor de la Clase Programa con parametros </summary>
        public Programa(int id, string? title, string? genero, Estado diaDeLaSemana, int startTime, int durationMinutes)
        {
            this.id = id;
            this.title = title;
            this.genero = genero;
            this.diaDeLaSemana = diaDeLaSemana;
            this.startTime = startTime;
            this.durationMinutes = durationMinutes;
        }
*/
        ///<summary> Identificador unico de los Programas </summary>
        public int Id { get; set; }
        ///<summary> Titulo del Programa </summary>
        public string? Title { get => title; set => title = value; }
        ///<summary> Genrero del Programa </summary>
        public string? Genero { get => genero; set => genero = value; }
        ///<summary> Dia de la semana que se emite el Programa </summary>
        public Estado DiaDeLaSemana { get => diaDeLaSemana; set => diaDeLaSemana = value; }

        ///<summary> Horario del Programa </summary>
        public int StartTime { get => startTime; set => startTime = value; }
        ///<summary> Duracion en Minutos del Programa </summary>
        public int DurationMinutes { get => durationMinutes; set => durationMinutes = value; }


    }
}


///<summary>
///Estado del pedido.
///</summary>
public enum Estado
{

    /// <summary>Estado 0 </summary>
    Lunes,
    /// <summary>Estado 1 </summary>
    Martes,
    /// <summary>Estado 2 </summary>
    Miercoles,
    /// <summary>Estado 3 </summary>
    Jueves,
    /// <summary>Estado 4 </summary>
    Viernes,
    /// <summary>Estado 5 </summary>
    Sabado,
    /// <summary>Estado 6 </summary>
    Domingo
    
}