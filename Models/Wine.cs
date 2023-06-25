using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AgustinDonalisioProyectoPNT1.Models
{
    public class Wine
    {
        [Key]
        public int Id { get; set; }

        [AllowNull]
        public string? Brand { get; set; }

        [AllowNull]
        public string Name { get; set; }

        [DisplayName("Variety")]
        public string? Type { get; set; }

        [AllowNull]
        public int Year { get; set; }
    }
}
