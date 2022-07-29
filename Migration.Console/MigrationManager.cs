using TalendMigration.Core.DataAccessLayer;
using TalendMigration.Core.BusinessLayer;
using TalendMigration.Core.Models;
using TalendMigration.Core.DTO;

namespace Migration.Console;
internal sealed class MigrationManager
{
    internal bool IsCisco { get; private set; }
    internal bool IsSonicWall { get; private set; }
    internal string FileName { get; private set; } = default!;

    internal IEnumerable<DTOSubscription> Subscriptions { get; private set; } = default!;

    private IMigration migration = default!;
    private IMigrationDAL dal = default!;

    private MigrationManager()
    {
        
    }
    internal static MigrationManager Create(string[] args)
    {
        var obj = new MigrationManager();

        obj.IsSonicWall = args.Any(x => x == "-s");
        obj.IsCisco = args.Any(x => x == "-c");

        if (!obj.IsSonicWall && !obj.IsCisco)
            obj.IsCisco = true;

        if (args.Any(x => !x.StartsWith("-"))){
            obj.FileName = args.Where(x => !x.StartsWith("-")).FirstOrDefault() ?? string.Empty;
        }

        return obj;
    }

    internal bool Execute()
    {
        Subscriptions = new List<DTOSubscription>();
        if (IsCisco){
            dal = new CiscoDAL();
            migration = new CiscoMigration(dal) { ColumnSeparator = ','};
            Subscriptions = migration.GetSubscriptions<InvoiceCisco>(FileName);
        } else if (IsSonicWall){
            dal = new SonicWallDAL();
            migration = new SonicWallMigration(dal) { ColumnSeparator = ','};
            Subscriptions = migration.GetSubscriptions<Invoice>(FileName);
        }
        return migration.SaveSubscriptions(Subscriptions);
    }
}