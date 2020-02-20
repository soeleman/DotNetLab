using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Soeleman
{
    internal static class Program
    {
        public static void Main()
        {
            var exluceAssemblies = new[] {"netstandard", "System", "Soeleman.Program" };

            var targetType = typeof(ISay);

            Assembly
                .GetEntryAssembly()?
                .GetReferencedAssemblies()
                .Select(Assembly.Load)
                .Select(s => s.FullName)
                .ToList()
                .ForEach(Console.WriteLine);

            //Assembly
            //    .GetEntryAssembly()?
            //    .GetReferencedAssemblies()
            //    .Select(Assembly.Load)
            //    .SelectMany(s => s.DefinedTypes)
            //    .Where(p => targetType.IsAssignableFrom(p))
            //    .Select(s => s.FullName)
            //    .ToList()
            //    .ForEach(Console.WriteLine);

            //Console.ForegroundColor = ConsoleColor.DarkMagenta;

            //AppDomain.CurrentDomain
            //    .GetAssemblies()
            //    .SelectMany(s => s.DefinedTypes)
            //    .Select(s => s.FullName)
            //    .ToList()
            //    .ForEach(Console.WriteLine);

            //AppDomain.CurrentDomain
            //    .GetAssemblies()
            //    .OrderBy(ks => ks.GetName().Name)
            //    .Select(s => s.GetName().Name)
            //    .ToList()
            //    .ForEach(Console.WriteLine);

            Console.ForegroundColor = ConsoleColor.DarkRed;

            AppDomain.CurrentDomain
                .GetAssemblies()
                .Select(s => new { s.GetName().Name, Assembly = s})
                .OrderBy(ks => ks.Name)
                .Where(p => !exluceAssemblies.Any(a => p.Name.Contains(a, StringComparison.InvariantCultureIgnoreCase)))
                //.SelectMany(s => s.Assembly.GetExportedTypes())
                .SelectMany(s => s.Assembly.DefinedTypes)
                .Select(s => s.FullName)
                .ToList()
                .ForEach(Console.WriteLine);

            Console.ResetColor();

            AppDomain.CurrentDomain
                .GetAssemblies()
                .OrderBy(ks => ks.GetName().Name)
                .Select((s, i) => new
                {
                    No = i + 1,
                    Asm = s.GetName().Name,
                    Typ = string.Join(',', s.DefinedTypes.Where(p => p.ImplementedInterfaces.Contains(targetType)).Select(t => t.FullName))
                })
                .ToList()
                .ForEach(a =>
                {
                    Console.ForegroundColor = string.IsNullOrEmpty(a.Typ)
                        ? ConsoleColor.Green
                        : ConsoleColor.Cyan;
                    Console.WriteLine($"{a.No.ToString().PadLeft(2, '0')}. {a.Asm} {(string.IsNullOrEmpty(a.Typ) ? string.Empty : "-")} {a.Typ}");
                });

            AppDomain.CurrentDomain
                .GetAssemblies()
                .OrderBy(ks => ks.GetName().Name)
                .SelectMany(s => s.GetExportedTypes().Where(p => p.GetInterface(nameof(ISay)) != null))
                //.SelectMany(s => s.GetExportedTypes().Where(p=> p.GetInterfaces().Contains(targetType)))
                .Select((s, i) => new
                {
                    No = i + 1,
                    Asm = s.Name,
                    Typ = s.FullName
                })
                .ToList()
                .ForEach(a =>
                {
                    Console.ForegroundColor = string.IsNullOrEmpty(a.Typ)
                        ? ConsoleColor.DarkBlue
                        : ConsoleColor.Blue;
                    Console.WriteLine($"{a.No.ToString().PadLeft(2, '0')}. {a.Asm} {(string.IsNullOrEmpty(a.Typ) ? string.Empty : "-")} {a.Typ}");
                });

            AssemblyLoadContext
                .Default
                .Assemblies
                .OrderBy(ks => ks.GetName().Name)
                .Select((s, i) => new
                {
                    No  = i + 1,
                    Asm = s.GetName().Name,
                    Typ = string.Join(',', s.DefinedTypes.Where(p => p.ImplementedInterfaces.Contains(targetType)).Select(t => t.FullName))
                })
                .ToList()
                .ForEach(a =>
                {
                    Console.ForegroundColor = string.IsNullOrEmpty(a.Typ)
                        ? ConsoleColor.DarkYellow
                        : ConsoleColor.Yellow;
                    Console.WriteLine($"{a.No.ToString().PadLeft(2, '0')}. {a.Asm} {(string.IsNullOrEmpty(a.Typ) ? string.Empty : "-")} {a.Typ}");
                });

            Console.ForegroundColor = ConsoleColor.Gray;

            FromAssemblyDependencies(typeof(Program).Assembly)
                .Where(p => p.GetInterface(nameof(ISay)) != null)
                .ToList()
                .ForEach(Console.WriteLine);

            Console.ForegroundColor = ConsoleColor.Red;

            ISay say = new SayHello();
            Console.WriteLine(say.SayIt());

            //say = new SayTikTok();
            //Console.WriteLine(say.SayIt());

            //say = new SayNetCore();
            //Console.WriteLine(say.SayIt());

            Console.ResetColor();
            Console.ReadLine();
        }

        public static IEnumerable<Type> FromAssemblyDependencies(
            Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var assemblies = new List<Assembly> { assembly };

            try
            {
                foreach (var dependencyName in assembly.GetReferencedAssemblies())
                {
                    try
                    {
                        // Try to load the referenced assembly...
                        assemblies.Add(Assembly.Load(dependencyName));
                    }
                    catch
                    {
                        // Failed to load assembly. Skip it.
                    }
                }

                return assemblies.SelectMany(s => s.DefinedTypes).Select(s => s.AsType());
            }
            catch
            {
                return assemblies.SelectMany(s => s.DefinedTypes).Select(s => s.AsType());
            }
        }
    }
}