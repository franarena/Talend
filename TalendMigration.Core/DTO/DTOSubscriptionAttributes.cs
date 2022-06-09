using Newtonsoft.Json;

namespace TalendMigration.Core.DTO;
public class DTOSubscriptionAttributes
{
    [JsonProperty("bill_to")]
    public string? BillTo { get; set; }
    [JsonProperty("payment_method")]
    public string? PaymentMethod { get; set; }
    [JsonProperty("credit_card_details")]
    public DTOCreditCard? CreditCardDetails { get; set; }
    [JsonProperty("reseller_contact_email")]
    public string? ResellerContactEmail { get; set; }
    [JsonProperty("reseller_contact_name")]
    public string? ResellerContactName { get; set; }
    [JsonProperty("reseller_contact_phone")]
    public string? ResellerContactPhone { get; set; }
    [JsonProperty("isresellercentric")]
    public string? IsResellerCentric { get; set; }

    public static explicit operator DTOSubscriptionAttributes(Models.Invoice invoice)
    {
        var creditCard = (DTOCreditCard)invoice;
        //TODO
        var pm = int.TryParse(invoice.Payment_Method, out int i) ? $"{i:000}" : invoice.Payment_Method;
        return new DTOSubscriptionAttributes()
        {
            BillTo = $"{invoice.Bill_To:000}",
            CreditCardDetails = creditCard,
            PaymentMethod = pm,
            ResellerContactEmail = invoice.Reseller_Contact_Email,
            ResellerContactName = invoice.Reseller_Concat_Name,
            ResellerContactPhone = $"{invoice.Reseller_Concat_Phone}",
            IsResellerCentric = invoice.isresellercentric.ToString()
        };
    }
}