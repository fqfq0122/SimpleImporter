using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Importer
{
    class Program
    {
        static void Main(string[] args)
        {
            FileReader reader = new FileReader();
            string[] output = reader.ReadInFile("test.csv");

            if (output != null)
            {
                FileExporter export = new FileExporter();
                export.ExportFile("stander.csv", output);

                Console.WriteLine("Done! (Press any key to exit.)");
            }
            else
            {
                Console.WriteLine("Can not import file, please check file and try again! (Press any key to exit.)");
            }

            Console.ReadKey();
        }
    }
}
