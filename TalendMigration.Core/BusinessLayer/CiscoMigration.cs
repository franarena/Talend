using System.Globalization;
using TalendMigration.Core.DataAccessLayer;
using TalendMigration.Core.DTO;
using TalendMigration.Core.Models;

namespace TalendMigration.Core.BusinessLayer;
public class CiscoMigration : Migration
{
    public Char ColumnSeparator { get; set; }

    public CiscoMigration(IMigrationDAL dal) : base(dal)
    {
    }
    public override IEnumerable<DTOSubscription> GetSubscriptions<T>(string migrationFile)
    {
        MigrationFile = migrationFile;
        return GetSubscriptions();
    }

    public override IEnumerable<DTOSubscription> GetSubscriptions()
    {
        //trace.Trace__BEGIN();
        var subscriptions2 = new List<DTOSubscription>();
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", false);

            var invoices = dal?.RecuperaInvoices<InvoiceCisco>(MigrationFile, ColumnSeparator);

/*
            if (invoices.All(x => x.Bill_To == null))
                trace.Trace__INFO("No BillTo information found, in all records!");
            else if (invoices.Any(x => x.Bill_To == null))
                trace.Trace__INFO("No BillTo information found, at least in one subscription");

            if (invoices.All(x => string.IsNullOrEmpty(x.ResellerBCN)))
                trace.Trace__INFO("No BCN information found, in all records!");
            else if (invoices.Any(x => string.IsNullOrEmpty(x.ResellerBCN)))
                trace.Trace__INFO("No BCN information found, at least in one subscription");
                */

            var inv_nodup = invoices?
                .Where(x => x.Bill_To.HasValue)
                .GroupBy(x => new { x.ResellerBCN, x.VendorSubscriptionID })
                .Select(g => g.FirstOrDefault())
                .ToList();

            var products = invoices?
                .Where(x => x.Bill_To.HasValue)
                .GroupBy(x => new { ResellerBCN = x.ResellerBCN?.ToString(), x.VendorSubscriptionID })
                .Select(g => new { g.Key.ResellerBCN, g.Key.VendorSubscriptionID, Products = g.Select(x => (DTOProduct)x).ToList() })
                .ToList();

            //NEW VERSION
            foreach (var invoice in inv_nodup)
                subscriptions2.Add((DTOSubscription)invoice);
            foreach (var p in products)
            {
                var invoice = subscriptions2
                    .Where(s => s.Reseller_BCN == p.ResellerBCN
                            && s.parameters.Any(x => x.name == "vendor_subscription_id" && x.value == p.VendorSubscriptionID))
                    .SingleOrDefault();
                if (invoice != null)
                {
                    //Rimozione duplicati
                    var invProducts = p.Products
                        .OrderBy(pr => pr.ProductMPN)
                        .ThenByDescending(pr => pr.Quantity)
                        .ToList()
                        .GroupBy(x => x.ProductMPN)
                        .Select(g => g.FirstOrDefault())
                        .ToList();
                    invoice.products = invProducts;
                }
            }
        }
        finally
        {
            //trace.Trace__END();
        }
        return subscriptions2;
    }    
}