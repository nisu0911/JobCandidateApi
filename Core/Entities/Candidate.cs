using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Candidate
    {
        [Key]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string TimeInterval { get; set; }
        public string Linkedin { get; set; }
        public string Github { get; set; }
        [Required]
        public string Comment { get; set; }
    }
}
