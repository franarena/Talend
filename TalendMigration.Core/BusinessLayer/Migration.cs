using TalendMigration.Core.DataAccessLayer;
using TalendMigration.Core.DTO;
using TalendMigration.Core.Models;
using Newtonsoft.Json;

namespace TalendMigration.Core.BusinessLayer;
public abstract class Migration : IMigration
{
    protected readonly IMigrationDAL? dal;
    public string MigrationFile { get; set; } = string.Empty;
    public abstract IEnumerable<DTOSubscription> GetSubscriptions();
    public abstract IEnumerable<DTOSubscription> GetSubscriptions<T>(string migrationFile) where T : Invoice;

    public Migration(IMigrationDAL _dal)
    {
        dal = _dal;
    }

    public virtual bool SaveSubscriptions(IEnumerable<DTO.DTOSubscription> subscriptions)
    {
        var fileName = string.Empty;
        fileName = Path.Combine(Path.GetDirectoryName(MigrationFile) ?? string.Empty, $"{Path.GetFileNameWithoutExtension(MigrationFile)}.json");
        if (File.Exists(fileName))
            File.Delete(fileName);
        using (var file = File.CreateText(fileName))
        {
            //JsonSerializer serializer = new JsonSerializer();
            var serializer = JsonSerializer.Create(new JsonSerializerSettings() { Formatting = Formatting.Indented });
            // serialize JSON directly to a file
            serializer.Serialize(file, subscriptions);
        }
        return true;
    }
}