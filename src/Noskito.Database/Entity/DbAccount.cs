using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Noskito.Database.Entity
{
    public class DbAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; init; }

        [Required] public string Username { get; init; }

        [Required] public string Password { get; init; }

        public virtual ICollection<DbCharacter> Characters { get; set; }
    }
}