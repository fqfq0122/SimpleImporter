using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Importer
{
    public class FileReader
    {
        private const int INPUT_LENGTH = 6;
        private readonly string[] outputDataHeader = { "id", "name", "address", "city", "state", "zip" };

        private string[] header;
        private List<string[]> data = new List<string[]>();


        private int[] getValidInput(string[] aInput)
        {
            List<int> input = new List<int>();

            foreach(string i in aInput)
            {
                int currentInput;
                if (Int32.TryParse(i, out currentInput))
                {
                    input.Add(currentInput);
                }
                else
                {
                    return null;
                }
            }

            for (int i = 1; i <= 6; i++)
            {
                if (!input.Contains(i))
                {
                    return null;
                }
            }

            return input.ToArray();
        }

        private int[] getRawDataOrder()
        {
            string standerHeaderOrder = " | ";
            for (int i = 0; i < outputDataHeader.Length; i++)
            {
                standerHeaderOrder += outputDataHeader[i] + " | ";
            }

            Console.WriteLine("Stander data header order: " + standerHeaderOrder);
            Console.WriteLine("Please enter matching column index for raw data order by stander data header (split by ','):");

            int[] order = null;
            do
            {
                string[] input = Console.ReadLine().Split(',');
                if (input != null)
                {
                    if (input.Length == INPUT_LENGTH)
                    {
                        order = getValidInput(input);
                    }
                }

                if (order == null)
                {
                    Console.WriteLine("Your input is not valid, please try again!");
                }
            } while (order == null);

            return order;
        }

        private string getStanderOutput(string[] aString)
        {
            StringBuilder standerOutput = new StringBuilder();

            for (int i = 0; i < aString.Length; i++)
            {
                if (i < aString.Length - 1)
                {
                    standerOutput.Append(aString[i] + ",");
                }
                else
                {
                    standerOutput.Append(aString[i]);
                }
            }

            return standerOutput.ToString();
        }

        private string[] processingFile()
        {
            if (header == null || data == null) return null;

            int[] rawDataOrder = getRawDataOrder();
            List<string> standerData = new List<string>();
            standerData.Add(getStanderOutput(outputDataHeader));

            foreach(string[] d in data)
            {
                string[] currentLine = new string[INPUT_LENGTH];

                for (int i = 0; i < currentLine.Length; i++)
                {
                    currentLine[i] = d[rawDataOrder[i] - 1];
                }

                standerData.Add(getStanderOutput(currentLine));
            }

            return standerData.ToArray();
        }

        private bool isValidFile(string[] aRawFile)
        {
            if (aRawFile.Length <= 1) return false;

            header = aRawFile[0].Split(',');
            if (header.Length != INPUT_LENGTH) return false;

            string[] currentLine;
            for (int i = 1; i < aRawFile.Length; i++)
            {
                currentLine = aRawFile[i].Split(',');
                if (currentLine == null)
                {
                    return false;
                }
                else
                {
                    if (currentLine.Length != INPUT_LENGTH)
                    {
                        return false;
                    }
                    else
                    {
                        data.Add(currentLine);
                    }
                }              
            }

            return true;
        }

        public string[] ReadInFile(string aPath)
        {
            try
            {
                string[] lines = File.ReadAllLines(aPath);

                if (isValidFile(lines))
                {
                    return processingFile();
                }

                return null;
            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe.ToString());
                return null;
            }
        }
    }
}
