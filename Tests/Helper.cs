using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Tests
{
    public static class Helper
    {
        public static string GetResourceFileText(string namespaceAndFileName)
        {
            try
            {
                using var stream = typeof(Helper).GetTypeInfo().Assembly.GetManifestResourceStream(namespaceAndFileName);
                using var reader = new StreamReader(stream, Encoding.UTF8);
                return reader.ReadToEnd();
            }
            catch (Exception)
            {
                throw new Exception($"Failed to read Embedded Resource {namespaceAndFileName}");
            }
        }
    }
}
