using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AgustinDonalisioProyectoPNT1.Models
{
    public class Cellar
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [AllowNull]
        public string? IdUser { get; set; }

        [AllowNull]
        public string? Description { get; set; }

        [AllowNull]
        [Display(Name = "Wine Quantity")]
        public int? WineQuantity { get; set; } = 0;
    }
}

