using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeeSharp.Domain.Models;

public class Entity
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime DateCreated { get; set; } = DateTime.Today;

    public DateTime DateUpdated { get; set; } = DateTime.Today;
}

