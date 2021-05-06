using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Noskito.Database.Entity
{
    public class DbMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; init; }
        
        [Required]
        public string Name { get; init; }
        
        [Required]
        public byte[] Grid { get; init; }
    }
}