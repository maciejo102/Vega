using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vega.Models
{
    [Table("Models")]
    public class Model
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        // inversed property, navigation property 
        public Make Make { get; set; }

        // w celu łatwiejszego przeszukiwania listy
        public int MakeId { get; set; }
    }
}