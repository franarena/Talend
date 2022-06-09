namespace TalendMigration.Core.DataAccessLayer;
public interface IMigrationDAL
{
    string OutputFolder { get; }
    IEnumerable<T> RecuperaInvoices<T>(string migrationFile, char colSeparator = ',') where T : Models.Invoice;
}