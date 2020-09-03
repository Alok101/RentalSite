using System.Runtime.Serialization;
namespace Rental.Schema.Common
{
   
    public class PatchModel
    {
        
        public string op { get; set; }
        public string path { get; set; }
        public string value { get; set; }
    }
}
