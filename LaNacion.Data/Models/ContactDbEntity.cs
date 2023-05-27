using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaNacion.Data.Models
{
    public class ContactDbEntity : IDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ContactId { get; set; }

        [Required]
        [StringLength(48)]
        public string Name { get; set; }

        [Required]
        [StringLength(48)]
        public string Company { get; set; }

        [Required]
        [StringLength(40)]
        public string Email { get; set; }

        [Required]
        [StringLength(120)]
        public string AddressLine { get; set; }

        [Required]
        [StringLength(48)]
        public string City { get; set; }

        [Required]
        [StringLength(48)]
        public string State { get; set; }

        [StringLength(150)]
        public string ProfileImage { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [NotMapped]
        public virtual ICollection<PhoneNumberDbEntity> PhoneNumbers { get; set; } = new List<PhoneNumberDbEntity>();
    }
}
