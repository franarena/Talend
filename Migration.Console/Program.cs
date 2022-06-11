// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
var fileName = args[0];
var dal = new CiscoDAL();
//var currentDir = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //Directory.GetCurrentDirectory();
var migration = new CiscoMigration(dal) { ColumnSeparator = ',', MigrationFile = fileName };

Console.WriteLine($"{fileName}");

var subs = migration.GetSubscriptions();

if (subs.Any())
    migration.SaveSubscriptions(subs);

Console.WriteLine($"Program ending! {subs.Count()} subscription(s)");