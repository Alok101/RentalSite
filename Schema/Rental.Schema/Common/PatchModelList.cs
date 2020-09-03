using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Rental.Schema.Common
{
    public class PatchModelList
    {
        public PatchModelList()
        {
            Update = new List<PatchModel>();
        }
        public List<PatchModel> Update { get; set; }
    }
}
