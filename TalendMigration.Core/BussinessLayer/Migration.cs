using TalendMigration.Core.DTO;
using TalendMigration.Core.Models;

namespace TalendMigration.Core.BussinessLayer;
public abstract class Migration : IMigration
{
    public abstract IEnumerable<DTOSubscription> GetSubscriptions();
    public abstract IEnumerable<DTOSubscription> GetSubscriptions<T>(string migrationFile) where T : Invoice;
}