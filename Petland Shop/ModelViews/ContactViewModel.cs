using System.ComponentModel.DataAnnotations;

namespace Petland_Shop.ModelViews
{
    public class ContactViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Note { get; set; }
    }
}
