using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace DataParser
{
    class Program
    {
        private static readonly string inputPath = @"..\..\..\..\Input.txt";
        private static readonly string outputPath = @"..\..\..\..\Output.txt";
        private static readonly Regex regex = new(@"CH([\d]+):.{7}:(?=(.*?)"")", RegexOptions.Compiled);

        static void Main()
        {
            StreamReader sr = new(inputPath);
            StreamWriter sw = new(outputPath);
            List<string> listToSort = new();
            Stopwatch stopwatch = new();
            string line;

            sw.WriteLine("####################"); //Line to be overwritten
            stopwatch.Start();
            while ((line = sr.ReadLine()) != null)
            {
                if (regex.IsMatch(line))
                {
                    foreach (Match match in regex.Matches(line))
                    {
                        listToSort.Add(string.Concat(match.Groups[2].Value, match.Groups[1].Value));
                    }
                }
            }

            listToSort.Sort();

            int listCount = listToSort.Count - 1;
            for (int a = listCount; a >= 0; a--)
            {
                sw.WriteLine(listToSort[a]);
            }

            stopwatch.Stop();

            sw.Flush();
            sw.BaseStream.Seek(0, SeekOrigin.Begin);
            sw.WriteLine($"Time elapsed: {stopwatch.ElapsedMilliseconds}ms");

            sw.Close();
            sr.Close();
        }
    }
}
