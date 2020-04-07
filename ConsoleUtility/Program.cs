﻿using System;
using System.Collections.Generic;
using IPSCRulesLibrary.ObjectClasses;
using IPSCRulesLibrary.Services;

namespace ConsoleUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Loading Rules Reader Service...");
            var rulesReader = new RulesReader();
            Console.WriteLine("Rules Reader Service Initialized!");
            Console.WriteLine();
            Console.WriteLine("Please add source files to root of this utility file.");
            Console.WriteLine("Press Enter to Continue...");
            Console.ReadLine();

            var rootPath = AppDomain.CurrentDomain.BaseDirectory;
            string[] filenames = {"ActionAir.txt", "Handgun.txt", "Shotgun.txt", "Rifle.txt", "Mini-Rifle.txt", "PCC.txt"};

            Console.WriteLine();
            Console.WriteLine("Converting text files to OO Rulebooks");

            var converts = new Dictionary<string, RulesReader.ConversionResult>();

            foreach (var filename in filenames)
            {
                converts.Add(filename, rulesReader.ConvertFromTxtFile($"{rootPath}/{filename}"));
            }

            Console.WriteLine("Conversions complete! Rules now parsed!");
            Console.WriteLine($"WARNINGS:");
            Console.WriteLine($"Rules 3.2.1 and 4.1.1.2 have bullet points which do not translate and need manual entry.");
            Console.WriteLine($"Rules with names that include parantheses will end up in description");
            Console.WriteLine();
            Console.WriteLine("Creating Rules Book from parsings!");

            var disciplines = new List<Discipline>();

            foreach (var convert in converts)
            {
                disciplines.Add(rulesReader.CreateRuleChapters(convert.Value, convert.Key));
            }

            Console.WriteLine("Rules book created. Printing...");
            Console.WriteLine();
            Console.WriteLine("Loading File Writer Service...");
            var fileWriter = new FileWriter();
            Console.WriteLine("File Writer Service Initialized!");
            Console.WriteLine();
            Console.WriteLine("Creating Website Structure...");
            fileWriter.CreateWebsiteFilesDirectory(disciplines);
            Console.WriteLine();
            Console.WriteLine("Website Created!");
        }
    }
}
