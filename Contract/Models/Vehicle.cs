using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vega.Contract.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        public int Id { get; set; }
        // use this property istead of loading whole model 
        public int ModelId { get; set; }
        // naviagation property
        public Model Model { get; set; }

        public bool IsRegistered { get; set; }

        [Required]
        [MaxLength(255)]
        public string ContactName { get; set; }

        [MaxLength(255)]
        public string ContactEmail { get; set; }

        [Required]
        [MaxLength(255)]
        public string ContactPhone { get; set; }

        public DateTime LastUpdate { get; set; }

        public ICollection<VehicleFeature> Features { get; set; }

        public ICollection<Photo> Photos { get; set; }

        // always initialize collection properties
        public Vehicle()
        {
            Features = new Collection<VehicleFeature>();
            Photos = new Collection<Photo>();
        }
    }
}
