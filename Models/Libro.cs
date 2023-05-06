using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaCRUD.Models
{
    [Table("libros")]
    public class Libro
    {
        [Key]
        public int IdLibro { get; set; }

        public string NombreLibro { get; set; }

        public string NombreAutor { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int NumeroPaginas { get; set; }

        public string Idioma { get; set; }

        public int CantidadDisponible { get; set; }

    }
}
