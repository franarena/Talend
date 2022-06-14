// See https://aka.ms/new-console-template for more information

var manager = Migration.Console.MigrationManager.Create(args);

Console.WriteLine($"\nFile to process --> {manager.FileName}");
manager.Execute();

Console.WriteLine($"{manager.Subscriptions.Count()} subscription(s) found in {System.IO.Path.GetFileName(manager.FileName)}");
Console.WriteLine("File json saved successfully");

Console.WriteLine("\nProgram ending");