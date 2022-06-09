using Newtonsoft.Json;
using TalendMigration.Core.Models;
namespace TalendMigration.Core.DTO;
public class DTOCreditCard
{
    [JsonProperty("cc_four_digits")]
    public string? CCFourDigits { get; set; }
    [JsonProperty("cc_type")]
    public string? CCType { get; set; }
    [JsonProperty("payment_token")]
    public string? PaymentToken { get; set; }
    [JsonProperty("valid_to")]
    public string? ValidTo { get; set; }

    public static explicit operator DTOCreditCard(Invoice invoice)
    {
        return new DTOCreditCard
        {
            CCFourDigits = invoice.Credit_Card_Last_4_Digit ?? string.Empty,
            CCType = invoice.CreditCard_Type ?? string.Empty,
            PaymentToken = invoice.Payment_Token ?? string.Empty,
            ValidTo = invoice.CC_Valid_To ?? string.Empty
        };
    }    
}