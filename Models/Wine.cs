using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AgustinDonalisioProyectoPNT1.Models
{
    public class Wine
    {
        [Key]
        public int Id { get; set; }


        public string? IdUser { get; set; }


        public string? Brand { get; set; }


        public string Name { get; set; }

        [DisplayName("Variety")]
        public string? Type { get; set; }


        public DateTime CreatedAt { get; set; }


        public DateTime UpdatedAt { get; set; }


        public decimal? Price { get; set; }

        [DisplayName("Consumption date")]
        [DataType(DataType.Date)]
        public DateTime? ConsumptionDate { get; set; }


        public string? Notes { get; set; }

        
    }
}
