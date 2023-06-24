using System.ComponentModel.DataAnnotations;

namespace AgustinDonalisioProyectoPNT1.Models
{
    public class Cellar
    {

        [Key]
        public int Id { get; set; }


        public string Name { get; set; }


        public string IdUser { get; set; }


        public string? Description { get; set; }

    }
}

