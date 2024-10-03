
namespace Finnce_Api.core.Categories;

public class Category
{
    //this id is the id of database
    [Column("IdCategory")]
    public Guid Id { get; set; }
    [StringLength(20)]
    public string Name { get; set; }
    //This id is a internal id of tink
    public string? IdInternal { get; set; }
    [StringLength(20)]
    public string? Parent { get; set; }
    [StringLength(20)]
    public string? PrimaryName { get; set; }
    [StringLength(20)]
    public string? SearchTerms { get; set; }
    [StringLength(20)]
    public string? SecondaryName { get; set; }


    [Required]
    [ForeignKey(nameof(User))]
    public string IdUser { get; set; }
    public User User { get; set; }




}

