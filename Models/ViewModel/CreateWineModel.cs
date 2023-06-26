using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgustinDonalisioProyectoPNT1.Models.ViewModel
{
    [NotMapped]
    public class CreateWineModel
    {
        public int IdCellar { get; set; }

        [Required]
        public string Name { get; set; }

        public int IdWine { get; set; }
        public string Brand { get; set; }
        public String Type { get; set; }
        public int Year { get; set; }
        public int WineQuantity { get; set; }

        public bool IsWine { get; set; } = false;
    }
}
