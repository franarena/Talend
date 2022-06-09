using System.Runtime.Serialization;

namespace TalendMigration.Core.Models;
[DataContract]
public class InvoiceCisco : Invoice
{
        [DataMember(Name = "Billing_Model")]
        public string? BillingModel { get; set; }
        [DataMember(Name = "PlanId")]
        public string? PlanId { get; set; }
        [DataMember(Name = "Subscription_Expiration_Date")]
        public string? SubscriptionExpirationDate { get; set; }
        [DataMember(Name = "deal_id")]
        public string? DealId { get; set; }
        [DataMember(Name = "delayshipdate")]
        public string? DelayShipDate { get; set; }
        [DataMember(Name = "cisco_web_order_id")]
        public string? CiscoWebOrderId { get; set; }
        [DataMember(Name = "vendor_portal_submission")]
        public string? VendorPortalSubmission { get; set; }
        [DataMember(Name = "Price_after_credits")]
        public string? PriceAfterCredits { get; set; }
        [DataMember(Name = "Cost_after_credits")]
        public string? CostAfterCredits { get; set; }
        [DataMember(Name = "Credits_applicable_at_renewal")]
        public string? CreditsApplicableAtRenewal { get; set; }    
}