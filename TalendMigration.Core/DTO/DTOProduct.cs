using Newtonsoft.Json;
using System;
namespace TalendMigration.Core.DTO;
public class DTOProduct
{
    [JsonProperty("Product_MPN")]
    public string? ProductMPN { get; set; }
    [JsonProperty("Product_Name")]
    public string? ProductName { get; set; }
    [JsonProperty("Quantity")]
    public string? Quantity { get; set; }
    [JsonProperty("Currency")]
    public string? Currency { get; set; }
    [JsonProperty("Price")]
    public string? Price { get; set; }
    [JsonProperty("Price_after_credits", NullValueHandling = NullValueHandling.Ignore)]
    public string? PriceAfterCredits { get; set; }
    [JsonProperty("Cost")]
    public string? Cost { get; set; }
    [JsonProperty("Cost_after_credits", NullValueHandling = NullValueHandling.Ignore)]
    public string? CostAfterCredits { get; set; }
    [JsonProperty("Credits_applicable_at_renewal", NullValueHandling = NullValueHandling.Ignore)]
    public string? CreditsApplicableAtRenewal { get; set; }

    public static explicit operator DTOProduct(Models.Invoice invoice)
    {
        var objProduct = new DTOProduct
        {
            ProductMPN = invoice.Product_MPN,
            ProductName = invoice.ProductName,
            Quantity = $"{invoice.Quantity}",
            Currency = invoice.Currency,
            Price = $"{invoice.Price:0.0000}",
            Cost = $"{invoice.Cost:0.0000}",
            PriceAfterCredits = null,
            CostAfterCredits = null,
            CreditsApplicableAtRenewal = null
        };
        //var invoiceCisco = (Models.InvoiceCisco)invoice;
        Models.InvoiceCisco? invoiceCisco = null;
        if (invoice.GetType() == typeof (Models.InvoiceCisco))
            invoiceCisco = (Models.InvoiceCisco)invoice;

        if (invoiceCisco != null)
        {
            objProduct.PriceAfterCredits = Decimal.TryParse(invoiceCisco.PriceAfterCredits, out decimal d)
                ? d.ToString("0.0000")
                : "0.0000";
            objProduct.CostAfterCredits = Decimal.TryParse(invoiceCisco.CostAfterCredits, out d)
                ? d.ToString("0.0000")
                : "0.0000";
            objProduct.CreditsApplicableAtRenewal = invoiceCisco.CreditsApplicableAtRenewal;
        }

        return objProduct;
    }
}