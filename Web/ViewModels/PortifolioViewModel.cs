using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    // get object of data from their classes and view it in Controlles
    public class PortifolioViewModel
    {
        public Guid Id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Projectname { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public IFormFile File { get; set; }  // to add a photo you need to add it as formfile
        

    }
}
