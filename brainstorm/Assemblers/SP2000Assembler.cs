using BrainStorm.Exceptions;
using BrainStorm.Base;
using BrainStorm.Processors.SP2000.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BrainStorm.Assemblers
{
    public class SP2000Assembler : Assembler
    {
        private string filename = String.Empty;
        private FileStream stream;
        private int dataAddress = 0;
        private int codeAddress = 0;
        private int instructionAddress = 0;
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
            foreach (string line in codeLines)
            {
                string[] lineItems = line.Split(new char[] { ' ', ',' });
                lineNumber += 1;

                //First check if we are in the data section
                if (isDataSection)
                {
                    if (Regex.IsMatch(lineItems[0], ".text"))
                    {
                        if (lineItems.Length > 2)
                            throw new AssemblerException("Unknown items: Line " + lineNumber + " => " + lineItems[3]);
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
                    //Check if a label is the only one in a line
                    if (Regex.IsMatch(line, @"[a-zA-Z]+: *"))
                    {
                        //Line is A Label
                        labels.Add(line, codeAddress);

                    }
                    if(Regex.IsMatch(line, @"syscall"))
                    {
                        string name = line;
                        var  characters = name.ToCharArray();
                        characters[0] = characters[0].ToString().ToUpper().ToCharArray()[0];
                        name = new string(characters);
                        string className = "BrainStorm.Processors.SP2000.Instructions." + name + "Instruction";
                        var instruction = Activator.CreateInstance(Type.GetType(className));
                        this.memory.Push((Instruction)instruction, instructionAddress++);
                    } 
                    if (Regex.IsMatch(line, @"[a-z]+ +\$[a-z]{1}[0-9]{1} *, +\$[a-z]{1}[0-9]{1} *, *. *")) //li    $v0, $a0, 4
                    {
                        //This detects assembly code

                        Regex expression = new Regex(@"(:?[a-z]+) +(:?\$[a-z]{1}[0-9]{1}) *, +(:?\$[a-z]{1}[0-9]{1}) *, *(:?.+) *");
                        Match group = expression.Match(line);

                        this.CreateThreeOperandInstruction(line, group.Groups[1].Value, group.Groups[2].Value, group.Groups[3].Value, group.Groups[4].Value);
                    }else if (Regex.IsMatch(line, @"[a-z]+ +\$[a-z]{1}[0-9]{1} *, *. *")) //li    $v0, 4
                    {
                        //This detects assembly code

                        Regex expression = new Regex(@"(:?[a-z]+) +(:?\$[a-z]{1}[0-9]{1}) *, *(:?.+) *");
                        Match group = expression.Match(line);
                        System.Console.WriteLine("Here");
                        this.CreateTwoOperandInstruction(line, group.Groups[1].Value, group.Groups[2].Value, group.Groups[3].Value);
                    }
                    else if (Regex.IsMatch(line, @"[a-z]+ +\$[a-z]{1}[0-9]{1} *")) //mflo   $v0
                    {
                        //This detects assembly code

                        Regex expression = new Regex(@"(:?[a-z]+) +(:?\$[a-z]{1}[0-9]{1}) *");
                        Match group = expression.Match(line);

                        this.CreateOneOperandInstruction(line, group.Groups[1].Value, group.Groups[2].Value);
                    }
                    /**
                    nderitukelvin19@gmail.com 
                    nderitukelvin@gmail.com
                    13s01acs010@anu.ac.ke
                    johndoe@students.jkuat.ac.ke

                    ============================================
                    ASCII character encoding
                    ============================================

                    ----------------------------------------------
                    |  NUMBERS   |  HEXADECIMAL  |  BINARY       | 
                    ----------------------------------------------
                    |  0        |  0x30          |  0b00110001   |
                    ----------------------------------------------
                    |  1        |  0x31          |  0b00110010   |
                    ----------------------------------------------
                    |  2        |  0x32          |  0b00110011   |
                    ----------------------------------------------

                    ----------------------------------------------
                    |  LETTER   |  HEXADECIMAL    |  BINARY      |
                    ----------------------------------------------
                    |  A        |   0x41          |  0b01000001  |
                    ----------------------------------------------
                    |  B        |   0x42          |  0b01000010  |
                    ----------------------------------------------
                    |  C        |   0x43          |  0b01000011  |
                    ----------------------------------------------

                    ----------------------------------------------
                    |  LETTER   |  HEXADECIMAL    |  BINARY      |
                    ----------------------------------------------
                    |  a        |   0x61          |  0b01100001  |
                    ----------------------------------------------
                    |  b        |   0x62          |  0b01100010  |
                    ----------------------------------------------
                    |  c        |   0x63          |  0b01100011  |
                    ----------------------------------------------
                    s = 0b01110011 
                    i = 0b01101001
                    m = 0b01101101
                    o = 0b01101111
                    n = 0b01101110
                      
                      0x73, 0x69, 0x6D, 0x6F, 0x6E
                      0111001101101001011011010110111101101110
                    */
                    //Go to the next line
                    continue;
                }
                //If we are not in the data section then start from the beginning till we find the data section

                if (Regex.IsMatch(lineItems[0], ".data"))
                {
                    if (lineItems.Length == 2)
                    {
                        try
                        {
                            this.dataAddress = Convert.ToInt32(lineItems[1]);
                        } catch (FormatException e)
                        {
                            throw new AssemblerException("Second value must be a word: line " + lineNumber, e);
                        }

                    }
                    if (lineItems.Length > 2)
                        throw new AssemblerException("Unknown items: Line " + lineNumber + " => " + lineItems[2]);
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
                    throw new AssemblerException("Unknown directive: line " + lineNumber + " => " + lineItems[0]);
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
            if (Regex.IsMatch(lineItems[0], @"([a-zA-Z]+: *)|(\..+)"))
            {
                SP2000DataMemory dataMemory = SP2000DataMemory.Instance;
                switch (lineItems[1].TrimEnd(new char[] { ' ' }))
                {
                    case ".asciiz":
                        var temp = lineItems.ToList();
                        temp.RemoveRange(0, 2);
                        string[] final = temp.ToArray();
                        string value = String.Join(" ", final);
                        value = value.Trim(new char[] { '"' });
                        dataMemory.StoreStringBytes(value, dataAddress);
                        string dataName = lineItems[0].TrimEnd(new char[] { ':' });
                        dataLabels.Add(dataName, dataAddress);
                        int length = Encoding.ASCII.GetByteCount(value);
                        dataAddress += length;
                        break;
                    case ".ascii":
                        var temp2 = lineItems.ToList();
                        temp2.RemoveRange(0, 2);
                        string[] final2 = temp2.ToArray();
                        string value2 = String.Join(" ", final2);
                        dataMemory.StoreStringBytes(value2, dataAddress);
                        string dataName2 = lineItems[0].TrimEnd(new char[] { ':' });
                        dataLabels.Add(dataName2, dataAddress);
                        int length2 = Encoding.ASCII.GetByteCount(value2);
                        dataAddress += length2;
                        break;
                    case ".word":
                        string[] values = lineItems[2].Trim(new char[] { ' ' }).Split(new char[] { ',' });
                        Array.ForEach<string>(values, item => item = item.Trim(new char[] { ' ' }));
                        int[] words = new int[values.Length];
                        for (int i = 0; i < values.Length; i++)
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
                throw new AssemblerException("Unknown directive: line " + lineNumber + " => " + lineItems.ToString());
            }
        }

        private void CreateOneOperandInstruction(string line, string name, string firstArg)
        {

            //Console.WriteLine(secondValue);
            var characters = name.ToCharArray();
            characters[0] = characters[0].ToString().ToUpper().ToCharArray()[0];
            name = new string(characters);
            string space = GetNameSpace(name);
            if (space != "")
            {
                name = name + "Instruction";
                var instruction = Activator.CreateInstance(Type.GetType(space + name, false), new object[] { line, firstArg });
                this.memory.Push((Instruction)instruction, instructionAddress++);
            }
            else
            {
                throw new AssemblerException("Unknown instruction <" + name + ">");
            }
        }
        private void CreateTwoOperandInstruction(string line, string name, string firstArg, string secondArg)
        {
            string secondValue = "";
            //replace data sections
            //Console.WriteLine(secondArg);
            if (variables.ContainsKey(secondArg))
                secondValue = variables[secondArg].ToString();
            else if (dataLabels.ContainsKey(secondArg))
                secondValue = dataLabels[secondArg].ToString();
            else
                secondValue = secondArg;

            //Console.WriteLine(secondValue);
            var  characters = name.ToCharArray();
            characters[0] = characters[0].ToString().ToUpper().ToCharArray()[0];
            name = new string(characters);
            string space = GetNameSpace(name);
            if(space != "")
            {
                name = name + "Instruction";
                var instruction = Activator.CreateInstance(Type.GetType(space + name, false), new object[] { line, firstArg, secondArg });
                this.memory.Push((Instruction)instruction, instructionAddress++);
            }else{
                throw new AssemblerException("Unknown instruction <" + name + ">");
            }
        }

        private void CreateThreeOperandInstruction(string line, string name, string firstArg, string secondArg, string thirdArg)
        {
            //Console.WriteLine(thirdArg);
            var characters = name.ToCharArray();
            characters[0] = characters[0].ToString().ToUpper().ToCharArray()[0];
            name = new string(characters);
            string space = GetNameSpace(name);
            if (space != "")
            {
                name = name + "Instruction";
                //Console.WriteLine("Third arg: " + thirdValue);
                var instruction = Activator.CreateInstance(Type.GetType(space + name, false), new object[] { line, firstArg, secondArg, thirdArg });
                this.memory.Push((Instruction)instruction, instructionAddress++);
                
            }else{
                throw new AssemblerException("Unknown instruction <" + name + ">");
            }
        }

        private string GetNameSpace(string name)
        {
            string space = "";
            switch (name)
            {
                case "La":
                case "Lb":
                case "Li":
                case "Lw":
                case "Mfhi":
                case "Mflo":
                case "Sb":
                case "Sw":
                    space = "BrainStorm.Processors.SP2000.Instructions.Memory.";
                    break;
                case "Ja":
                case "J":
                case "Jr":
                    space = "BrainStorm.Processors.SP2000.Instructions.Jump.";
                    break;
                case "Beq":
                case "Bge":
                case "Bgt":
                case "Ble":
                case "Blt":
                case "Bne":
                    space = "BrainStorm.Processors.SP2000.Instructions.Branch.";
                    break;
                case "And":
                case "Andi":
                case "Nor":
                case "Or":
                case "Ori":
                case "Sll":
                case "Srl":
                case "Xor":
                case "Xori":
                    space = "BrainStorm.Processors.SP2000.Instructions.Bitwise.";
                    break;
                case "Add":
                case "Addi":
                case "Addu":
                case "Div":
                case "Move":
                case "Mult":
                case "Sub":
                case "Subu":
                    space = "BrainStorm.Processors.SP2000.Instructions.Arithmetic.";
                    break;
            }
            return space;
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
 
    }
}
