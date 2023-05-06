using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaCRUD.Models
{
    [Table("reservas")]
    public class Reservacion
    {
        [Key]
        public int IdReservacion { get; set; }

        public string NombrePersona { get; set; }

        public DateTime FechaReserva { get; set; }

        public int IdLibro { get; set; }

        [ForeignKey("IdLibro")]
        public Libro Libro { get; set; }

        public int DiasAReservar { get; set; }

    }
}
