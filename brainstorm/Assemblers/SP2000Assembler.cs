using BrainStorm.Exceptions;
using BrainStorm.Processors.SP2000.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BrainStorm.Base
{
    class SP2000Assembler : Assembler
    {
        private string filename = String.Empty;
        private FileStream stream;
        private int dataAddress = 0;
        private bool isDataSection = false;
        private bool isTextSection = false;
        private Dictionary<string, int> dataLabels = new Dictionary<string, int>();
        private Dictionary<string, int> labels = new Dictionary<string, int>();
        private Dictionary<string, int> variables = new Dictionary<string, int>();
        private List<string> codeLines = new List<string>();
        private SP2000InstructionMemory memory = new SP2000InstructionMemory();

        public SP2000Assembler(string filename)
        {
            this.filename = filename;
            this.stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            //Seperate code and text sections
            ReadLines();
        }
        public SP2000Assembler(FileStream filestream)
        {
            this.stream = filestream;
            //Seperate code and text sections
            ReadLines();
        }
        private void ReadLines()
        {
            StreamReader reader = new StreamReader(this.stream);

            do
            {
                string line = reader.ReadLine();
                line = line.Trim(new char[] { ' ' });
                //Check for comments or empty lines
                if (line.StartsWith("#") || line.StartsWith(";") || line.Equals(string.Empty)) continue;
                //Strip off comments within code lines
                string uncommentedLine = Regex.Replace(line, @"(#.*)|(;.*)", "");
                codeLines.Add(uncommentedLine);

            } while (reader.Peek() != -1);
        }
        public SP2000InstructionMemory Assemble()
        {
            int lineNumber = 0;
            foreach(string line in codeLines)
            {
                string[] lineItems = line.Split(new char[] { ' ', ',' });
                lineNumber += 1;

                //First check if we are in the data section
                if (isDataSection)
                {
                    if (Regex.IsMatch(lineItems[0], ".text"))
                    {
                        if (lineItems.Length > 2)
                            throw new AssemblerException("Unknown items: Line " + lineNumber);
                        this.isDataSection = false;
                        this.isTextSection = true;
                        continue;
                    }
                    this.ParseDataLine(lineItems, lineNumber);
                    //go to the next line
                    continue;
                }
                if (isTextSection)
                {
                    //Process text section

                    //Go to the next line
                    continue;
                }
                //If we are not in the data section then start from the beginning till we find the data section
                
                if (Regex.IsMatch(lineItems[0], ".data"))
                {
                    if(lineItems.Length == 2)
                    {
                        try
                        {
                            this.dataAddress = Convert.ToInt32(lineItems[1]);
                        }catch(FormatException e)
                        {
                            throw new AssemblerException("Second value must be a word: line " + lineNumber, e);
                        }
                        
                    }
                    if (lineItems.Length > 2)
                        throw new AssemblerException("Unknown items: Line " + lineNumber);
                    this.isDataSection = true;
                    this.isTextSection = false;
                    continue;
                }
                else if (Regex.IsMatch(lineItems[0], ".eqv"))
                {
                    this.PopulateVariables(lineItems, lineNumber);
                    continue;
                }
                else
                {
                    throw new AssemblerException("Unknown directive: line " + lineNumber + " .. " + lineItems[1]  );
                }
            }
            return memory;
        }
        /// <summary>
        /// Populates the variables dictionary
        /// </summary>
        /// <param name="lineItems"></param>
        private void PopulateVariables(string[] lineItems, int lineNumber)
        {
            if (lineItems.Length != 3) throw new AssemblerException("Not enough values: line " + lineNumber);
            try
            {
                int value = Convert.ToInt32(lineItems[2]);
                variables.Add(lineItems[1], value);
            }
            catch (FormatException e)
            {
                throw new AssemblerException("Item 3 on line " + lineNumber + " is not a value.", e);
            }
        }

        private void ParseDataLine(string[] lineItems, int lineNumber)
        {
            if(Regex.IsMatch(lineItems[0], @"([a-zA-Z]+: *)|(\..+)"))
            {
                SP2000DataMemory dataMemory = SP2000DataMemory.Instance;
                switch(lineItems[1].TrimEnd(new char[] { ' ' }))
                {
                    case ".asciiz":
                        var temp = lineItems.ToList();
                        temp.RemoveRange(0, 2);
                        string[] final = temp.ToArray();
                        string value = String.Join(" ", final);
                        dataMemory.StoreStringBytes(value, dataAddress);
                        this.dataLabels.Add(lineItems[0], dataAddress);
                        int length = Encoding.ASCII.GetByteCount(value);
                        dataAddress += length;
                        break;
                    case ".ascii":
                        var temp2 = lineItems.ToList();
                        temp2.RemoveRange(0, 2);
                        string[] final2 = temp2.ToArray();
                        string value2 = String.Join(" ", final2);
                        dataMemory.StoreStringBytes(value2, dataAddress);
                        dataLabels.Add(lineItems[0], dataAddress);
                        int length2 = Encoding.ASCII.GetByteCount(value2);
                        dataAddress += length2;
                        break;
                    case ".word":
                        string[] values = lineItems[2].Trim(new char[] { ' ' }).Split(new char[] { ',' });
                        Array.ForEach<string>(values, item => item = item.Trim(new char[] { ' ' }) );
                        int[] words = new int[values.Length];
                        for(int i = 0; i < values.Length; i++)
                        {
                            words[i] = Convert.ToInt32(values[i]);
                        }
                        dataMemory.StoreWords(words, dataAddress);
                        break;
                    case ".byte":
                        break;
                    case ".double":
                        break;
                    case ".float":
                        break;
                    case ".text":
                        break;
                    case ".half":
                        break;
                    default:
                        break;
                }
            }
            else
            {
                throw new AssemblerException("Unknown directive: line " + lineNumber);
            }
        }










        public void SeperateSections()
        {
            StreamReader reader = new StreamReader(this.stream);
            int codeAddress = 0;
            string line = reader.ReadLine();
            //Read the next line to ignore the .code directive
            line = reader.ReadLine();
            while (line.TrimStart(new char[] { ' ' }) != ".text")
            {
                line = line.Trim(new char[] { ' ' });
                //Check for comments or empty lines
                if (line.StartsWith("#") || line.StartsWith(";") || line.Equals(string.Empty)) continue;
                //Strip off comments within code lines
                string uncommentedLine = Regex.Replace(line, @"(#.*)|(;.*)", "");
                //dataSection.Add(codeAddress, uncommentedLine);
                line = reader.ReadLine();
                codeAddress += 1;
            }
            //set text address to 0
            int textAddress = 0;
            //This reads the next line ignoring the .text directive
            line = reader.ReadLine();
            do
            {
                //Remove trailling spaces
                line = line.Trim(new char[] { ' ' });
                //Check for comments or empty lines
                if (line.StartsWith("#") || line.StartsWith(";") || line.Equals(string.Empty)) continue;
                //Strip off comments within code lines
                string uncommentedLine = Regex.Replace(line, @"(#.*)|(;.*)", "");
                //Insert in the text section dictionary;
                //textSection.Add(textAddress, uncommentedLine);
                //read the next line
                line = reader.ReadLine();
                textAddress += 1;
            } while (reader.Peek() != -1); //Until the end of the file
            //Close the reader
            reader.Close();
        }
        /// <summary>
        /// Reads lines of the file specified
        /// </summary>
        public void ParseDataSection()
        {
           //These also reassigns variables to text section
        }
        public void ParseTextSection()
        {

        }
    }
}
