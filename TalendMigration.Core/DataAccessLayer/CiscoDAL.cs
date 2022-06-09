using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Globalization;
using TalendMigration.Core.Models;

namespace TalendMigration.Core.DataAccessLayer;
public class CiscoDAL : IMigrationDAL
{
    private char separator;
    public string OutputFolder { get; private set; } = string.Empty;
    public IEnumerable<T> RecuperaInvoices<T>(string migrationFile, char colSeparator = ',') where T : Invoice
    {
            var invoices = new List<T>();
            //var i = 0;
            try
            {
                separator = colSeparator;
                //Non serve la connessione, ma capire dove si trova il file
                //GetConnection();
                var fileSourceName = migrationFile;//Path.Combine(OutputFolder, Path.GetFileName(migrationFile));
                using (var reader =  File.OpenText(fileSourceName))
                {
                    var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
                    {
                        HasHeaderRecord = true,
                        Delimiter = separator.ToString()
                    };
                    using (var csv = new CsvReader(reader, csvConfig))
                    {
                        var headerRow = csv.Context.Reader.HeaderRecord;
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
                //trace.Trace_Exception(ex);
                throw;
            }
            finally
            {
                //trace.Trace__END();
            }
            return invoices;        
    }
}