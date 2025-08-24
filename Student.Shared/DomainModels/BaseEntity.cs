using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Student.Shared.DomainModels
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }

        [MaxLength(50)]
        public string CreatedBy { get; set; }


        public DateTime CreatedOn { get; set; }


        [MaxLength(50)]
        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public void UpdateModified(string actor)
        {
            ModifiedBy = actor;
            ModifiedOn = DateTime.UtcNow;
        }
    }
}
