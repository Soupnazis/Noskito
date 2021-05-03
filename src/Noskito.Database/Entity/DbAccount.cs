using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Noskito.Database.Entity
{
    [Table("account")]
    public class DbAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}