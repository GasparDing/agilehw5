using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeDataAndDoc
{
    class Program
    {
        public static void mergeTheFiles(string lineOfData, string lineOfTemp, StreamReader inputFile, StreamReader inputFile2, StreamWriter outputFile)
        {
            inputFile.ReadLine();   //Escape the first line of data (Name ID Years).
            while ((lineOfData = inputFile.ReadLine()) != null)    // while data is not EOF do the following actions.
            {
                int sizeOfDataLine = System.Text.ASCIIEncoding.ASCII.GetByteCount(lineOfData); // get the size of line of data.
                Console.WriteLine(lineOfData);
                int i = 0, j = 0;

                while ((lineOfTemp = inputFile2.ReadLine()) != null)
                {  // read the line from template file.
                    j = 0;

                    int sizeOfTempLine = System.Text.ASCIIEncoding.ASCII.GetByteCount(lineOfTemp); // get the size of line of template.

                    // Check the read line.
                    // if the first letter isn't $, it will write.
                    // Otherwise, it will write the variable
                    while (lineOfTemp[j] != '$' && j < sizeOfTempLine) {
                        outputFile.Write(lineOfTemp[j]);
                        Console.Write(lineOfTemp[j]);
                        j++;
                        if (j >= sizeOfTempLine)
                            break;
                    }

                    bool meetRightCro;
                    while (j < sizeOfTempLine) {
                        // If the word is $, then write the variable
                        while (lineOfData[i] != '\t' && lineOfTemp[j] == '$') {
                            outputFile.Write(lineOfData[i]);
                            i++;
                            if (i >= sizeOfDataLine) {
                                i--;
                                break;
                            }
                        }
                        if (lineOfData[i] == '\t') i++;    // If we meet tab, escape it.

                        while (j < sizeOfTempLine) {
                            if (lineOfTemp[j] == '}' ? meetRightCro = true : meetRightCro = false) ;
                            if (!meetRightCro) j++;          // If temp is not '}' yet, escape the word till it is '}'.
                            else {
                                j++;
                                while (lineOfTemp[j] != '$' && j < sizeOfTempLine) {
                                    outputFile.Write(lineOfTemp[j]);
                                    j++;
                                    if (j >= sizeOfTempLine) {
                                        j = 0;
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        if (j == 0) break;
                    }
                    outputFile.WriteLine();
                    Console.WriteLine();
                }
                inputFile2.DiscardBufferedData();
                inputFile2.BaseStream.Seek(0, SeekOrigin.Begin);
                inputFile2.BaseStream.Position = 0;
            }
        }
        
        static void Main(string[] args) {
            //A program that can put the data into the from and become a result file.
            string inputFileName = "data.txt";
            string inputFileName2 = "template.txt";
            string outputFileName = "result.txt";
            if (args.Length == 3) {
                for (int i = 0; i < 3;i++ )
                    switch (args[i]) {
                        case "data.txt":
                            inputFileName = args[i];
                            break;
                        case "template.txt":
                            inputFileName2 = args[i];
                            break;
                        case "result.txt":
                            outputFileName = args[i];
                            break;
                    }
            }
            using (StreamReader inputFile = new StreamReader(inputFileName))    //data.txt
            using (StreamReader inputFile2 = new StreamReader(inputFileName2))  //template.txt
            using (StreamWriter outputFile = new StreamWriter(outputFileName)) {   //result.txt
                string lineOfData = " "; //read from data.
                string lineOfTemp = " "; // read from template.
                mergeTheFiles(lineOfData, lineOfTemp, inputFile, inputFile2, outputFile);
            }
        }
    }
}

 