namespace Finnce_Api.Models.PatrimonyModelDto
{
    public class PatrimonyModel
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Type { get; set; }

        public DateTime PurchaseDate { get; set; }
        public string IdUser { get; set; }
        public Guid Id { get; set; }

    }
}
