using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Noskito.Enum;

namespace Noskito.Database.Entity
{
    [Table("characters")]
    public class DbCharacter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; init; }

        [Required] public long AccountId { get; init; }

        [Required] public string Name { get; init; }

        [Required] public int Level { get; init; }

        [Required] public int JobLevel { get; init; }

        [Required] public int HeroLevel { get; init; }

        [Required] public byte Slot { get; init; }

        [Required] public Job Job { get; init; }

        [Required] public HairColor HairColor { get; init; }

        [Required] public HairStyle HairStyle { get; init; }

        [Required] public Gender Gender { get; init; }

        [Required] public int X { get; init; }

        [Required] public int Y { get; init; }

        [Required] public int MapId { get; init; }

        public virtual DbAccount Account { get; set; }
    }
}