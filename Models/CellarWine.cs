using System.ComponentModel.DataAnnotations;

namespace AgustinDonalisioProyectoPNT1.Models
{
    public class CellarWine
    {
        public int Id { get; set; }
        public string IdUser { get; set; }
        public String IdWine { get; set; }
        public int Quantity { get; set; }
    }
}
