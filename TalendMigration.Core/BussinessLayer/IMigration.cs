namespace TalendMigration.Core.BussinessLayer;

public interface IMigration
{
        IEnumerable<DTO.DTOSubscription> GetSubscriptions<T>(string migrationFile) where T : Models.Invoice;
        IEnumerable<DTO.DTOSubscription> GetSubscriptions();
}

