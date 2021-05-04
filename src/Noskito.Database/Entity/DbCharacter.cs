using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Noskito.Enum.Character;

namespace Noskito.Database.Entity
{
    [Table("characters")]
    public class DbCharacter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        
        [Required]
        public long AccountId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        [DefaultValue(1)]
        public byte Level { get; set; }
        
        [Required]
        public byte Slot { get; set; }
        
        [Required]
        [DefaultValue(Job.Adventurer)]
        public Job Job { get; set; }
        
        [Required]
        public HairColor HairColor { get; set; }
        
        [Required]
        public HairStyle HairStyle { get; set; }
        
        [Required]
        public Gender Gender { get; set; }
        
        public virtual DbAccount Account { get; set; }
    }
}