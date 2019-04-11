using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System;

namespace TeleprompterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = ReadFrom("sampleQuotes.txt");
            foreach (var line in lines)
            {


                Console.Write(line);
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var pause = Task.Delay(200);
                    // Synchronously waiting on a task is an
                    // anti-pattern. This will get fixed in later
                    // steps.
                    pause.Wait();
                }
            }
        }
        static IEnumerable<string> ReadFrom(string file)
        {
            string line;
            var lineLength = 0;
            using (var reader = File.OpenText(file))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    var words = line.Split(' ');
                    foreach (var word in words)
                    {
                        yield return word + " ";
                        lineLength += word.Length + 1;
                        if (lineLength > 70)
                        {
                            yield return Environment.NewLine;
                            lineLength = 0;
                        }
                    }
                    //yield return Environment.NewLine;
                }
            }
        }
    }

}
