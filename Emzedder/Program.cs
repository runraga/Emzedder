// See https://aka.ms/new-console-template for more information
using Emzedder.Datafile;
using ThermoFisher.CommonCore.Data.Interfaces;

string invalidFilePath = @"C:\Users\runra\source\repos\Emzedder\Emzedder\TestRawDatafiles\invalid.raw";

string validFilePath = @"C:\Users\runra\source\repos\Emzedder\Emzedder\TestRawDatafiles\1.raw";

string invalidRawFile = @"C:\Users\runra\source\repos\Emzedder\Emzedder\TestRawDatafiles\NotAValidRawFile.raw";

var df = new ThermoDatafile(validFilePath);
var chrom = df.GetUnfilteredChromatogram();
Console.WriteLine("finished");
