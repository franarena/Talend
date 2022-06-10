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

        public static explicit operator DTOSubscription(Models.Invoice invoice)
        {
            var subscription = new DTOSubscription()
            {
                RecordID = invoice.Record_ID,
                PONumber = invoice.PoNumber,
                Reseller_BCN = invoice.ResellerBCN,
                Subscription_Name = invoice.Subscription_Name,
                Subscription_Start_Date = invoice.Subscription_Start_Date.ToString("dd/MM/yyyy"),
                End_Customer_SSEUID = invoice.End_Customer_SSEUID,
                Subscription_Period_Length = invoice.Subscription_Period_Length.ToString(),
                Subscription_Period_Unit = invoice.Subscription_Period_Unit,
                Billing_Period_Length = invoice.Billing_Period_Length.ToString(),
                Billing_Period_Unit = invoice.Billing_Period_Unit,
                AutoRenewal = bool.TryParse(invoice.AutoRenewal, out bool flg) ? flg : (invoice.AutoRenewal == "1"),
                Subscription_Expiration_Date = invoice.Subscription_End_Date.ToString("dd/MM/yyyy"),
                Last_Billing_Date = invoice.Last_Billing_Date?.Trim(),
                Next_Billing_Date = invoice.Next_Billing_Date?.Trim(), 
                migration_date = invoice.Migration_Date.ToString("dd/MM/yyyy"),
                Vendor_Name = invoice.VendorName,
            };
            //var invoiceCisco = (Models.InvoiceCisco)invoice;
            Models.InvoiceCisco? invoiceCisco = null;
            if (invoice.GetType() == typeof (Models.InvoiceCisco))
                invoiceCisco = (Models.InvoiceCisco)invoice;
            var parameters = new List<DTONameValue>();
            if (invoiceCisco == null)
            {
                parameters.AddRange(new DTONameValue[]
                {
                    new DTONameValue(){ name = "spf_number", value = invoice.SPFNumber ?? string.Empty },
                    new DTONameValue(){ name = "previous_contract_number", value = invoice.Previous_Contract_Number ?? string.Empty },
                    new DTONameValue(){ name = "agreement_number_sp_prm_id", value = invoice.agreement_number_sp_prm_id ?? string.Empty},
                    new DTONameValue(){ name = "agg_prm_id", value = invoice.agg_prm_id ?? string.Empty },
                    new DTONameValue(){ name = "vendor_subscription_id", value = invoice.VendorSubscriptionID ?? string.Empty },
                    new DTONameValue(){ name = "provisioning_contact_name", value = invoice.provisioning_contact_name ?? string.Empty},
                    new DTONameValue(){ name = "provisioning_contact_phone", value = invoice.provisioning_contact_phone ?? string.Empty },
                    new DTONameValue(){ name = "provisioning_contact_email", value = invoice.provisioning_contact_email ?? string.Empty },
                });
            }
            else
            {
                subscription.Billing_Model = invoiceCisco.BillingModel;
                subscription.Subscription_Expiration_Date = invoiceCisco.SubscriptionExpirationDate;
                subscription.PlanId = invoiceCisco.PlanId ?? string.Empty;

                parameters.AddRange(new DTONameValue[]
                {
                    new DTONameValue(){ name = "deal_id", value = invoiceCisco.DealId },
                    new DTONameValue(){ name = "provisioning_contact_name", value = invoice.provisioning_contact_name ?? string.Empty},
                    new DTONameValue(){ name = "provisioning_contact_phone", value = invoice.provisioning_contact_phone ?? string.Empty },
                    new DTONameValue(){ name = "provisioning_contact_email", value = invoice.provisioning_contact_email ?? string.Empty },
                    new DTONameValue(){ name = "delayshipdate", value = invoiceCisco.DelayShipDate },
                    new DTONameValue(){ name = "cisco_web_order_id", value = invoiceCisco.CiscoWebOrderId },
                    new DTONameValue(){ name = "vendor_portal_submission", value = invoiceCisco.VendorPortalSubmission },
                    new DTONameValue(){ name = "vendor_subscription_id", value = invoice.VendorSubscriptionID ?? string.Empty }
                });
            }

            subscription.parameters = parameters;
            subscription.attributes = (DTOSubscriptionAttributes)invoice;
            subscription.products = new List<DTOProduct>() { (DTOProduct)invoice };
            return subscription;
        }        
}