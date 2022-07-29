using System.Globalization;
using TalendMigration.Core.DataAccessLayer;
using TalendMigration.Core.DTO;
using TalendMigration.Core.Models;

namespace TalendMigration.Core.BusinessLayer;
public class SonicWallMigration : Migration
{
    public Char ColumnSeparator { get; set; }

    public SonicWallMigration(IMigrationDAL dal) : base(dal)
    {
        
    }

    public override IEnumerable<DTOSubscription> GetSubscriptions<T>(string migrationFile)
    {
        MigrationFile = migrationFile;
        return GetSubscriptions();
    }

    public override IEnumerable<DTOSubscription> GetSubscriptions()
    {
        //trace.Trace__BEGIN(migrationFile);
        var subscriptions = new List<DTOSubscription>();

        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", false);
            
            var invoices = dal.RecuperaInvoices<Invoice>(MigrationFile, ColumnSeparator);

            var inv_nodup = invoices
                .Where(x => x.Bill_To.HasValue)
                .GroupBy(x => new { x.ResellerBCN, x.VendorSubscriptionID })
                .Select(g => g.FirstOrDefault());

            var products = invoices
                .Where(x => x.Bill_To.HasValue)
                .GroupBy(x => new { ResellerBCN = x.ResellerBCN?.ToString(), x.VendorSubscriptionID })
                .Select(g => new { g.Key.ResellerBCN, g.Key.VendorSubscriptionID, Products = g.Select(x => (DTOProduct)x).ToList() });

            foreach (var invoice in inv_nodup)
                subscriptions.Add((DTOSubscription)invoice);
            foreach (var p in products)
            {
                var invoice = subscriptions
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
        return subscriptions;
    }
}