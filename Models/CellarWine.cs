using System.ComponentModel.DataAnnotations;

namespace AgustinDonalisioProyectoPNT1.Models
{
    public class CellarWine
    {
        [Key]
        public int Id { get; set; }
        public string IdUser { get; set; }
        public int IdWine { get; set; }
        public int Quantity { get; set; }
    }
}
