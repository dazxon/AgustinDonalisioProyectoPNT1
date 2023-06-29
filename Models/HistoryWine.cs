using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgustinDonalisioProyectoPNT1.Models
{
    public class HistoryWine
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Guardado en")]
        public string CellarName { get; set; }

        [DisplayName("Nombre del Vino")]
        public string WineName { get; set; }

        [DisplayName("Marca del Vino")]
        public string WineBrand { get; set; }

        [DisplayName("Variedad")]
        public string WineType { get; set; }

        [DisplayName("Fecha del Vino")]
        public int WineYear { get; set; }

        [DisplayName("Fecha de Consumo")]
        public DateTime Consumed { get; set; }

        public string UserId { get; set; }

    }
}
