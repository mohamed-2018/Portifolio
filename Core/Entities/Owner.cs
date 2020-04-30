using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Owner : EntityBase
    {
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string FullName { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Profil { get; set; }
        [Required]
        public string Avatar { get; set; }  // this is the path of avatar picture you selecteed
       
        public Address Address { get; set; }
    }
}
