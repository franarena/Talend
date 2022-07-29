using Microsoft.Extensions.Logging.Console;
using TalendMigration.Core.BusinessLayer;
using TalendMigration.Core.DataAccessLayer;

// See https://aka.ms/new-console-template for more information

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) => {
        services.AddTransient<IMigrationDAL, CiscoDAL>();
        services.AddTransient<IMigrationDAL, SonicWallDAL>();
        services.AddSingleton<IMigration, CiscoMigration>();
        services.AddSingleton<IMigration, SonicWallMigration>();
        services.AddTransient<CiscoDAL>();
        services.AddTransient<SonicWallDAL>();
        services.AddSingleton<CiscoMigration>();
        services.AddSingleton<SonicWallMigration>();
    })
    .ConfigureLogging((hostContext, logger) => {
        logger.SetMinimumLevel(LogLevel.Debug);
        logger.AddSimpleConsole(options => options.IncludeScopes = true );
    })
    .Build();


var manager = Migration.Console.MigrationManager.Create(args);

Console.WriteLine($"\nFile to process --> {manager.FileName}");
manager.Execute();

Console.WriteLine($"{manager.Subscriptions.Count()} subscription(s) found in {System.IO.Path.GetFileName(manager.FileName)}");
Console.WriteLine("File json saved successfully");

Console.WriteLine("\nProgram ending");