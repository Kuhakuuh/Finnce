

namespace Finnce_Api.core.Entities;

public class EntityCore
{
    [Column("Id")]
    public Guid Id { get; set; }
    [StringLength(25)]
    public string Name { get; set; }

    [Required]
    [ForeignKey(nameof(User))]
    public string IdUser { get; set; }
    public User User { get; set; }
}
