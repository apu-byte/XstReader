// Copyright (c) 2020, Dijji, and released under Ms-PL.  This can be found in the root of this distribution. 

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace XstReader
{
    static public class Extensions
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        static Regex removeInvalidChars = null;
        public static string ReplaceInvalidFileNameChars(this string value, string with = "")
        {
            if (removeInvalidChars == null)
                removeInvalidChars = new Regex(String.Format("[{0}]", Regex.Escape(new string(Path.GetInvalidFileNameChars()))),
                        RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.CultureInvariant);
            return removeInvalidChars.Replace(value, with);
        }



        private static readonly HashSet<string> ReservedWindowsNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "CON","PRN","AUX","NUL","COM1","COM2","COM3","COM4","COM5","COM6","COM7","COM8","COM9",
            "LPT1","LPT2","LPT3","LPT4","LPT5","LPT6","LPT7","LPT8","LPT9"
        };

        public static string SanitizeFileSystemName(this string value, string fallback = "unnamed")
        {
            var name = (value ?? string.Empty).ReplaceInvalidFileNameChars(" ").Trim();
            name = name.Replace("..", ".");
            name = name.Trim(' ', '.');
            if (string.IsNullOrWhiteSpace(name))
                name = fallback;
            if (ReservedWindowsNames.Contains(name))
                name = "_" + name;
            return name;
        }

        public static string SafeFileName(this string value, string fallback = "unnamed")
        {
            var leaf = Path.GetFileName(value ?? string.Empty);
            if (string.IsNullOrWhiteSpace(leaf))
                leaf = fallback;
            return leaf.SanitizeFileSystemName(fallback);
        }

        public static string SafeFolderSegment(this string value, string fallback = "folder")
            => (value ?? string.Empty).Replace('/', '_').Replace('\\', '_').SanitizeFileSystemName(fallback);
        public static void PopulateWith<T>(this ObservableCollection<T> collection, List<T> list)
        {
            collection.Clear();
            foreach (T value in list)
                collection.Add(value);
        }

        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> e, Func<T, IEnumerable<T>> f)
          => e.SelectMany(c => f(c).Flatten(f)).Concat(e);

    }
}
