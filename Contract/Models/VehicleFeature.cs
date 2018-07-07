using System.ComponentModel.DataAnnotations.Schema;

namespace Vega.Contract.Models
{
    [Table("VehicleFeatures")]
    public class VehicleFeature
    {
        // combination of this two properties should precisely identify VehicleFeature 
        public int VehicleId { get; set; }
        public int FeatureId { get; set; }

        public Vehicle Vehicle { get; set; }
        public Feature Feature { get; set; }
    }
}