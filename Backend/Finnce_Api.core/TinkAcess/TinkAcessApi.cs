
namespace Finnce_Api.core.TinkAcess
{
    public class TinkAcessApi
    {
        [Column("IdTinkAcessApi")]
        public Guid Id { get; set; }
        [StringLength(50)]
        public string Token_type { get; set; }

        [StringLength(200)]
        public string Access_token { get; set; }
        public int Expires_in { get; set; }





    }
}
