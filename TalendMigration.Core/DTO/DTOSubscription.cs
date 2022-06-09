using Newtonsoft.Json;
using System.Collections.Generic;

namespace TalendMigration.Core.DTO;
public class DTOSubscription
{
        public string? RecordID { get; set; }
        public string? PONumber { get; set; }
        public string? Reseller_BCN { get; set; }
        public string? Subscription_Name { get; set; }
        public string? Subscription_Start_Date { get; set; }
        public string? End_Customer_SSEUID { get; set; }
        public string? Subscription_Period_Length { get; set; }
        public string? Subscription_Period_Unit { get; set; }
        public string? Billing_Period_Length { get; set; }
        public string? Billing_Period_Unit { get; set; }
        [JsonProperty("planId", NullValueHandling = NullValueHandling.Ignore)]
        public string? PlanId { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public string? Billing_Model { get; set; }
        public bool AutoRenewal { get; set; }
        public string? Subscription_Expiration_Date { get; set; }
        public string? Last_Billing_Date { get; set; }
        public string? Next_Billing_Date { get; set; }
        public string? migration_date { get; set; }
        public string? Vendor_Name { get; set; }
        public IEnumerable<DTONameValue>? parameters { get; set; }
        public DTOSubscriptionAttributes? attributes { get; set; }
        public IEnumerable<DTOProduct>? products { get; set; }
}