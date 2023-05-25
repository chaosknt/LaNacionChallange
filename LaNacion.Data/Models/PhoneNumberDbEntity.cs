using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaNacion.Data.Models
{
    public class PhoneNumberDbEntity : IDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PhoneNumberId { get; set; }

        [Required]
        public Guid ContactId { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        [StringLength(20)]
        public string Number { get; set; }

        [ForeignKey("ContactId")]
        public virtual ContactDbEntity Contact { get; set; }
    }
}
