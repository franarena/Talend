using CsvHelper;
using System.Runtime.Serialization;
using System.Reflection;
using TalendMigration.Core.Exceptions;

namespace TalendMigration.Core.Models;
[DataContract]
public class Invoice
{
        [DataMember(Name = "Record_ID")]
        public string? Record_ID { get; set; }
        [DataMember(Name = "ResellerBCN")]
        public string? ResellerBCN { get; set; }
        [DataMember(Name = "End_Customer_SSEUID")]
        public string? End_Customer_SSEUID { get; set; }
        [DataMember(Name = "PoNumber")]
        public string? PoNumber { get; set; }
        [DataMember(Name = "Subscription_Name")]
        public string? Subscription_Name { get; set; }
        [DataMember(Name = "Product_MPN")]
        public string? Product_MPN { get; set; }
        [DataMember(Name = "ProductName")]
        public string? ProductName { get; set; }
        [DataMember(Name = "VendorName")]
        public string? VendorName { get; set; }
        [DataMember(Name = "Billing_Period_Unit")]
        public string? Billing_Period_Unit { get; set; }
        [DataMember(Name = "Billing_Period_Length")]
        public Double Billing_Period_Length { get; set; }
        [DataMember(Name = "Billing_Period")]
        public string? Billing_Period { get; set; }
        [DataMember(Name = "Last_Billing_Date2")]
        public string? Last_Billing_Date2 { get; set; }
        [DataMember(Name = "Subscription_Period_Unit")]
        public string? Subscription_Period_Unit { get; set; }
        [DataMember(Name = "Subscription_Period_Length")]
        public Double Subscription_Period_Length { get; set; }
        [DataMember(Name = "Quantity")]
        public Int32 Quantity { get; set; }
        [DataMember(Name = "Price")]
        public Double Price { get; set; }
        [DataMember(Name = "Cost")]
        public Double Cost { get; set; }
        [DataMember(Name = "Currency")]
        public string? Currency { get; set; }
        [DataMember(Name = "Subscription_Start_Date")]
        public DateTime Subscription_Start_Date { get; set; }
        [DataMember(Name = "Subscription_End_Date")]
        public DateTime Subscription_End_Date { get; set; }
        [DataMember(Name = "Last_Billing_Date")]
        public string? Last_Billing_Date { get; set; }
        [DataMember(Name = "Next_Billing_Date")]
        public string? Next_Billing_Date { get; set; }
        [DataMember(Name = "AutoRenewal")]
        public string? AutoRenewal { get; set; }
        [DataMember(Name = "Migration_Date")]
        public DateTime Migration_Date { get; set; }
        [DataMember(Name = "Bill_To")]
        public int? Bill_To { get; set; }
        [DataMember(Name = "Credit_Card_Last_4_Digit")]
        public string? Credit_Card_Last_4_Digit { get; set; }
        [DataMember(Name = "CreditCard_Type")]
        public string? CreditCard_Type { get; set; }
        [DataMember(Name = "Payment_Token")]
        public string? Payment_Token { get; set; }
        [DataMember(Name = "CC_Valid_To")]
        public string? CC_Valid_To { get; set; }
        [DataMember(Name = "Payment_Method")]
        public string? Payment_Method { get; set; }
        [DataMember(Name = "Reseller_Contact_Email")]
        public string? Reseller_Contact_Email { get; set; }
        [DataMember(Name = "Reseller_Concat_Name")]
        public string? Reseller_Concat_Name { get; set; }
        [DataMember(Name = "Reseller_Concat_Phone")]
        public string? Reseller_Concat_Phone { get; set; }
        [DataMember(Name = "SPFNumber")]
        public string? SPFNumber { get; set; }
        [DataMember(Name = "Previous_Contract_Number")]
        public string? Previous_Contract_Number { get; set; }
        [DataMember(Name = "agreement_number_sp_prm_id")]
        public string? agreement_number_sp_prm_id { get; set; }
        [DataMember(Name = "agg_prm_id")]
        public string? agg_prm_id { get; set; }
        [DataMember(Name = "VendorSubscriptionID")]
        public string? VendorSubscriptionID { get; set; }
        [DataMember(Name = "provisioning_contact_name")]
        public string? provisioning_contact_name { get; set; }
        [DataMember(Name = "provisioning_contact_phone")]
        public string? provisioning_contact_phone { get; set; }
        [DataMember(Name = "provisioning_contact_email")]
        public string? provisioning_contact_email { get; set; }
        [DataMember(Name = "isresellercentric")]
        public Int32 isresellercentric { get; set; }
        [DataMember(Name = "Original_PO")]
        public string? Original_PO { get; set; }
        [DataMember(Name = "Source")]
        public string? Source { get; set; }
        [DataMember(Name = "Note")]
        public string? Note { get; set; }
        [DataMember(Name = "REFERENCE")]
        public string? REFERENCE { get; set; }

        public override string ToString()
        {
            return $"{VendorSubscriptionID}-{ResellerBCN}-{Product_MPN}";
        }
        
        public virtual void ReadFromCsvReader(CsvReader reader, string[] headerRow)
        {
            var tipo = this.GetType();
            var props = tipo.GetProperties();
            var columns = headerRow.ToList();
            //var members = tipo.GetMembers(BindingFlags.Public | BindingFlags.Instance)
            //                                .Where(x => x.MemberType == MemberTypes.Field || x.MemberType == MemberTypes.Property);
            //var columns = Enumerable.Range(0, dr.FieldCount).Select(dr.GetName).ToList();
            foreach (var p in props)
            {
                var attr = p.GetCustomAttributes<DataMemberAttribute>().SingleOrDefault();
                if (string.IsNullOrEmpty(attr?.Name ?? string.Empty))
                    continue;

                var nomeCampo = columns.Where(c => c.ToLower() == attr?.Name?.ToLower()).FirstOrDefault(); // c == p.Name).SingleOrDefault();
                if (!string.IsNullOrEmpty(nomeCampo))
                {
                    var i = (columns as List<string>).FindIndex(x => x == nomeCampo);
                    try
                    {
                        if (nomeCampo == (attr?.Name ?? "")) //!dr.IsDBNull(i))
                        {
                            string valore = reader.GetField(i) ?? string.Empty; // dr.GetString(i);
                            if (p.PropertyType == typeof(string))
                                p.SetValue(this, valore);
                            else if (p.PropertyType == typeof(int) && int.TryParse(valore, out int intero))
                                p.SetValue(this, intero);
                            else if (p.PropertyType == typeof(double) && double.TryParse(valore, out double numero))
                                p.SetValue(this, numero);
                            else if (p.PropertyType == typeof(DateTime) &&
                                        DateTime.TryParseExact(valore, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out DateTime data))
                                p.SetValue(this, data);
                            else if (int.TryParse(valore, out int numero2))
                                p.SetValue(this, numero2);
                        }
                    }
                    catch (Exception ex)
                    {
                        var strMessaggio = $"Errore nella conversione del {nomeCampo}[{i}] con valore {reader.GetField(i)}, destinazione {p.Name} ({p.PropertyType.Name})";
                        throw new MigrationException(strMessaggio, ex);
                    }
                }
            }            
        }         
}