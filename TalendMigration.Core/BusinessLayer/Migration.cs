using TalendMigration.Core.DataAccessLayer;
using TalendMigration.Core.DTO;
using TalendMigration.Core.Models;

namespace TalendMigration.Core.BusinessLayer;
public abstract class Migration : IMigration
{
    protected readonly IMigrationDAL? dal;
    public abstract IEnumerable<DTOSubscription> GetSubscriptions();
    public abstract IEnumerable<DTOSubscription> GetSubscriptions<T>(string migrationFile) where T : Invoice;

    public Migration(IMigrationDAL _dal)
    {
        dal = _dal;
    }
}