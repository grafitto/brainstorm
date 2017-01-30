using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processors.SP2000.Processor;
using System;
using BrainStorm.Processors.SP2000.Instructions.Arithmetic;
using BrainStorm.Processors.SP2000.Instructions.Memory;
using BrainStorm.Processors.SP2000.Instructions;
using BrainStorm.Base;
using BrainStorm.Processors.SP2000.Instructions.Branch;
using BrainStorm.Processors.SP2000.Instructions.Jump;
using BrainStorm.Simulators;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace BrainStorm
{
    class Test
    {
        public static void Main(string[] args)
        {
            string file = @"C:\Users\Nderitu\Desktop\emulation\mips.asm";
            SP2000Assembler assembler = new SP2000Assembler(file);
            SP2000InstructionMemory mem = assembler.Assemble();
            foreach(KeyValuePair<int, Instruction> ins in mem)
            {
                Console.WriteLine(ins.Key + " => " + ins.Value);
            }
            //SP2000Simulator mips = new SP2000Simulator(0, mem, 3);
            //mips.Run();
            //SP2000DataMemory.Instance.ShowMemory();

        }
        public static void Convert(int num)
        {
            for(int i = 0; i < 32; i++)
            {
                if((num & (0x80000000 >> i)) > 0)
                {
                    Console.Write("1");
                }else
                {
                    Console.Write("0");
                }
            }
            Console.WriteLine("");
        }
    }
   
}
