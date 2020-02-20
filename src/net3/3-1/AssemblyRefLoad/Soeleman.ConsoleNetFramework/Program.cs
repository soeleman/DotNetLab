using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Soeleman.ConsoleNetFramework
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var exluceAssemblies = new[] { "netstandard", "System", "Soeleman.Program" };

            var targetType = typeof(ISay);

            AppDomain.CurrentDomain
                .GetAssemblies()
                .Select(s => new { s.GetName().Name, Assembly = s })
                .OrderBy(ks => ks.Name)
                .Where(p => !exluceAssemblies.Any(p.Name.Contains))
                .Select(s => s.Name)
                //.SelectMany(s => s.Assembly.GetExportedTypes())
                //.SelectMany(s => s.Assembly.DefinedTypes)
                //.Select(s => s.FullName)
                .ToList()
                .ForEach(Console.WriteLine);

            AppDomain.CurrentDomain
                .GetAssemblies()
                .OrderBy(ks => ks.GetName().Name)
                .Select((s, i) => new
                {
                    No = i + 1,
                    Asm = s.GetName().Name,
                    Typ = string.Join(", ", s.DefinedTypes.Where(p => p.ImplementedInterfaces.Contains(targetType)).Select(t => t.FullName).ToArray())
                })
                .ToList()
                .ForEach(a =>
                {
                    Console.ForegroundColor = string.IsNullOrEmpty(a.Typ)
                        ? ConsoleColor.Green
                        : ConsoleColor.Cyan;
                    Console.WriteLine($"{a.No.ToString().PadLeft(2, '0')}. {a.Asm} {(string.IsNullOrEmpty(a.Typ) ? string.Empty : "-")} {a.Typ}");
                });

            Console.ForegroundColor = ConsoleColor.Red;

            ISay say = new SayHello();
            Console.WriteLine(say.SayIt());

            //say = new SayTikTok();
            //Console.WriteLine(say.SayIt());
            
            Console.ResetColor();
            Console.ReadLine();
        }
    }
}
