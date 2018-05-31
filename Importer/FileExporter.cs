using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Importer
{
    public class FileExporter
    {
        public void ExportFile(string aPath, string[] aLines)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(aPath))
            {
                foreach (string line in aLines)
                {
                    file.WriteLine(line);
                }
            }
        }
    }
}
