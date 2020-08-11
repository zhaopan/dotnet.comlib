/*
 * Copyright (c) 2020 ZP
 * Revision: 0.0.0.1
 * CLR: 4.0.30319.42000
 * Date 8/11/2020 9:40:16 AM
 * Name AssemblyUtils
 * Create on device ZPX.ZPX
 * Author Create By ZHAOPAN
 * Describe something
 *
 */

using System.IO;
using System.Reflection;

namespace Comlib.Reflection
{
    /// <summary>
    /// Assembly related reflection utils.
    /// </summary>
    public class AssemblyUtils
    {
        /// <summary>
        /// Get the internal template content from the commonlibrary assembly.
        /// </summary>
        /// <param name="assemblyFolderPath">"CommonLibrary.Notifications.Templates."</param>
        /// <param name="fileName">"welcome.html"</param>
        /// <returns></returns>
        public static string GetInternalFileContent(string assemblyFolderPath, string fileName)
        {
            var current = Assembly.GetExecutingAssembly();

            var stream = current.GetManifestResourceStream(assemblyFolderPath + fileName);
            if (stream == null)
            {
                return string.Empty;
            }
            var reader = new StreamReader(stream);
            var content = reader.ReadToEnd();
            return content;
        }
    }
}