using System.IO;
using System.Linq;

namespace Vega.Contract.Models
{
    public class PhotoSettings
    {
        public int MaxBytes { get; set; }
        public string[] AcceptedFileTypes { get; set; }

        public bool IsSupported(string fileName) {
            return AcceptedFileTypes.Any(type => Path.GetExtension(fileName).ToLower().Equals(type));
        }
    }
}