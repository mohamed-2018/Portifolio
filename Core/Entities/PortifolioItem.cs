using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class PortifolioItem : EntityBase
    {
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Projectname { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
