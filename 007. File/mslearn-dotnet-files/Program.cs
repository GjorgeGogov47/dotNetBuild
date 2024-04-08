using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;


var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");
var salesFiles = FindFiles(storesDirectory);

var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);

var salesTotal = CalculateSalesTotal(salesFiles);
File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");

/*
//Good to know
Console.WriteLine(Directory.GetCurrentDirectory());
Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
//For Mac/Linux
Console.WriteLine($"stores{Path.DirectorySeparatorChar}201");
//Use combine instead
Console.WriteLine(Path.Combine("stores","201"));

string fileName = $"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales{Path.DirectorySeparatorChar}sales.json";
FileInfo info = new FileInfo(fileName);
Console.WriteLine($"Full Name: {info.FullName}{Environment.NewLine}Directory: {info.Directory}{Environment.NewLine}Extension: {info.Extension}{Environment.NewLine}Create Date: {info.CreationTime}");
*/

IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();
    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);
    foreach (var file in foundFiles)
    {
        var extension = Path.GetExtension(file);
        if(extension == ".json")
        {
            salesFiles.Add(file);
        }
    }
    return salesFiles;
}

double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;
    
    foreach(var file in salesFiles)
    {
        string salesJson = File.ReadAllText(file);
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);
        salesTotal += data?.Total ?? 0;
    }

    return salesTotal;
}

record SalesData (double Total);

/* Fancy
class SalesTotal
{
    public double Total { get; set; }
}
*/