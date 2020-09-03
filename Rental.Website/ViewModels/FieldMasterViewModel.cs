using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Rental.Website.ViewModels
{
    public class FieldMasterViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [MaxLength(5)]
        public string GroupCode { get; set; }

        [Required]
        [MaxLength(30)]
        [Remote(action: "IsMasterNameInUse", controller: "internal")]
        public string Name { get; set; }

        [Required]
        public string Status { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

    }
}
