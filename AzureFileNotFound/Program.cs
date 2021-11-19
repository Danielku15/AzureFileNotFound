using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace AzureFileNotFound
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DumpSystemInformation();
            ProgramInternal.Run(args);
        }

        private static void DumpSystemInformation()
        {
            var principal = System.Security.Principal.WindowsIdentity.GetCurrent();
            Console.WriteLine("Running under: " + principal.Name);
            if (Directory.Exists(Environment.CurrentDirectory))
            {
                Console.WriteLine("Directory exists: " + Environment.CurrentDirectory);
                try
                {
                    var collection = Directory.GetAccessControl(Environment.CurrentDirectory)
                        .GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));
                    foreach (FileSystemAccessRule rule in collection)
                    {
                        Console.WriteLine("Rule: ");
                        Console.WriteLine(" Ref: " + rule.IdentityReference?.Value);
                        Console.WriteLine(" Inherted: " + rule.IsInherited);
                        Console.WriteLine(" Rights: " + rule.FileSystemRights);
                    }

                    foreach (var file in Directory.EnumerateFiles(Environment.CurrentDirectory))
                    {
                        Console.WriteLine(file);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to check permissions: " + e);
                }
            }
            else
            {
                Console.WriteLine("Directory does not exist: " + Environment.CurrentDirectory);
            }


            var dll = Path.Combine(Environment.CurrentDirectory, "Microsoft.AspNetCore.Hosting.Abstractions.dll");
            if (File.Exists(dll))
            {
                Console.WriteLine("File does exist: " + dll);
                try
                {
                    var collection = File.GetAccessControl(dll)
                        .GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));
                    foreach (FileSystemAccessRule rule in collection)
                    {
                        Console.WriteLine("Rule: ");
                        Console.WriteLine(" Ref: " + rule.IdentityReference?.Value);
                        Console.WriteLine(" Inherted: " + rule.IsInherited);
                        Console.WriteLine(" Rights: " + rule.FileSystemRights);
                    }

                    var info = new FileInfo(dll);
                    Console.WriteLine("Readonly: " + info.IsReadOnly);
                    Console.WriteLine("Length: " + info.Length);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to check file permissions: " + e);
                }
                
                try
                {
                    var bytes = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory,
                        "Microsoft.AspNetCore.Hosting.Abstractions.dll"));
                    Console.WriteLine("Bytes loaded: " + bytes.Length);
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("Directory " + Environment.CurrentDirectory);
                    Console.WriteLine("Assembly " + typeof(Program).Assembly.FullName);
                    Console.WriteLine("Assembly Location " + typeof(Program).Assembly.Location);
                    Console.WriteLine("Failed to load file: " + e.FileName);
                    Console.WriteLine("FusionLog: " + e.FusionLog);
                    Console.WriteLine("Failed to start " + e);
                    throw;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Directory " + Environment.CurrentDirectory);
                    Console.WriteLine("Assembly " + typeof(Program).Assembly.FullName);
                    Console.WriteLine("Assembly Location " + typeof(Program).Assembly.Location);
                    Console.WriteLine("Failed to start " + e);
                    throw;
                }
            }
            else
            {
                Console.WriteLine("File does not exist: " + dll);
            }

            Console.WriteLine("Runtime: " + Environment.Version + ", " + RuntimeInformation.FrameworkDescription);
            Console.WriteLine("Bits: " + IntPtr.Size);
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;
            AppDomain.CurrentDomain.AssemblyLoad += AssemblyLoad;
        }

        private static void AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            Console.WriteLine("Assembly Load: '" + args.LoadedAssembly?.FullName + "' from '" +
                              args.LoadedAssembly?.Location + "'");
        }

        private static Assembly AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Console.WriteLine("Assembly Resolve: '" + args.Name + "' by '" + args.RequestingAssembly?.FullName + "'");
            return null;
        }
    }

    internal class ProgramInternal
    {
        public static void Run(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            host.Run();
        }
        
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseStartup<Startup>();
        }
    }
}
