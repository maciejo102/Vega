using System.ComponentModel.DataAnnotations;

namespace Vega.Contract.Models
{
    public class Photo
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public int VehicleId { get; set; }
    }
}