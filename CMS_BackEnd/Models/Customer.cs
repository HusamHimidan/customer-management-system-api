using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CMS_BackEnd.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [StringLength(255)]
        [Index(IsUnique = true)]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}