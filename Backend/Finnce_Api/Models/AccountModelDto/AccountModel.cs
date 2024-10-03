namespace Finnce_Api.Models.AccountModelDto;

public class AccountModel
{
    public string Name { get; set; }
    public decimal? Amount { get; set; }
    public string TypeAccount { get; set; }
    public string Description { get; set; }
    public bool StatusBlockedTransation { get; set; }


}


