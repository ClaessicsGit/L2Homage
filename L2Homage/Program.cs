using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;
using System.IO;
using System.Windows;

namespace L2Homage
{
    class Program
    {
        [STAThreadAttribute]
        public static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            App.Main();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var exception = e.ExceptionObject is Exception ? ((Exception)e.ExceptionObject).Message + ((Exception)e.ExceptionObject).StackTrace : string.Empty;
                MessageBox.Show("Uh Oh, something went wrong:\r\n\r\n" + exception,
                                "Error", MessageBoxButton.OK);
            }
            finally
            {
                Environment.Exit(0);
            }
        }

        private static Assembly OnResolveAssembly(object sender, ResolveEventArgs args)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            AssemblyName assemblyName = new AssemblyName(args.Name);

            var path = assemblyName.Name + ".dll";
            if (assemblyName.CultureInfo.Equals(CultureInfo.InvariantCulture) == false) path = String.Format(@"{0}\{1}", assemblyName.CultureInfo, path);

            using (Stream stream = executingAssembly.GetManifestResourceStream(path))
            {
                if (stream == null) return null;

                var assemblyRawBytes = new byte[stream.Length];
                stream.Read(assemblyRawBytes, 0, assemblyRawBytes.Length);
                return Assembly.Load(assemblyRawBytes);
            }
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var dllName = new AssemblyName(args.Name).Name + ".dll";
            var execAsm = Assembly.GetExecutingAssembly();
            var resourceName = execAsm.GetManifestResourceNames().FirstOrDefault(s => s.EndsWith(dllName));
            if (resourceName == null) return null;
            using (var stream = execAsm.GetManifestResourceStream(resourceName))
            {
                var assbebmlyBytes = new byte[stream.Length];
                stream.Read(assbebmlyBytes, 0, assbebmlyBytes.Length);
                return Assembly.Load(assbebmlyBytes);
            }

        }
    }
}
