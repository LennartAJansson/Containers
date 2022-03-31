// See https://aka.ms/new-console-template for more information
using Common;

Console.WriteLine("This is my CronJob!");
ApplicationInfo appInfo = new ApplicationInfo(typeof(Program));
Console.WriteLine($"{appInfo.SemanticVersion} ({appInfo.Description})");