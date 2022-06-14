using CsvHelper;
using CsvHelper.Configuration;
using TalendMigration.Core.Models;
using System.Globalization;

namespace TalendMigration.Core.DataAccessLayer;
public class SonicWallDAL : IMigrationDAL
{
    //private char separator;
    public string OutputFolder { get; private set; } = string.Empty;

    public IEnumerable<T> RecuperaInvoices<T>(string migrationFile, char colSeparator = ',') where T : Invoice
    {
        //trace.Trace__BEGIN();
        var invoices = new List<T>();
        //var i = 0;
        try
        {
            //OutputFolder = Path.GetDirectoryName(migrationFile);
            var fileSourceName = migrationFile;
            using (var reader =  File.OpenText(fileSourceName))
            {
                    var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
                    {
                        HasHeaderRecord = true,
                        Delimiter = colSeparator.ToString()
                    };
                    using (var csv = new CsvReader(reader, csvConfig))
                    {
                        csv.Read();
                        csv.ReadHeader();
                        var headers = csv.Parser.RawRecord ?? string.Empty;
                        var headerRow = headers.Split(',');
                        while (csv.Read())
                        {
                            var invoice = Activator.CreateInstance<T>();
                            invoice.ReadFromCsvReader(csv, headerRow);
                            invoices.Add(invoice);
                            //if (i % 10000 == 0)
                            //    trace.Trace__INFO($"{i} records retrieved");                            
                        }
                    }

            }
        }
        catch (Exception)
        {
            //trace.Trace__DEBUG($"Errore in riga #{i}");
            //trace.Trace__DEBUG(ex.ToString());
            throw;
        }
        finally
        {
            //trace.Trace__END();
        }
        return invoices;
    }
}