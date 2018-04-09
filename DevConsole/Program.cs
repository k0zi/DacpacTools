using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Build.Evaluation;
namespace DevConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var globals = new Dictionary<string, string>()
            {
                ["Configuration"] = "Release"
            };
            var project = new Project("Database.sqlproj", globals, "4.0");
            foreach (var item in project.Items.Where(i => i.ItemType == "ArtifactReference"))
            {
                Console.WriteLine(item.EvaluatedInclude.ToString());
            }
            var dacname = "sql123.dacpac";
            var dacPath = $@"..\.dacpac\{dacname}";
            var metadata = new KeyValuePair<string, string>[] {
                new KeyValuePair<string, string>("HintPath", dacPath),
                new KeyValuePair<string, string>("SuppressMissingDependenciesErrors", "False")};
            project.AddItem("ArtifactReference", dacPath, metadata);
            var name = project.Properties.FirstOrDefault(p => p.Name == "SqlTargetFile");
            var version = project.Properties.FirstOrDefault(p => p.Name == "DacVersion");
            var path = project.Properties.Where(p => p.EvaluatedValue.Contains("Release")).ToList();
            //foreach (var o in project.Properties.OrderBy(p => p.Name))
            //{
            //    Console.WriteLine($"{o.Name} === {o.EvaluatedValue}");
            //}
            project.Save();
            Console.ReadKey();
        }
    }
}
