using BrainStorm.Processors.SP2000.Memory;
using System;
using BrainStorm.Base;
using BrainStorm.Simulators;
using BrainStorm.Assemblers;
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
           
            SP2000Simulator mips = new SP2000Simulator(0, mem, mem.Size());
            foreach(Register reg in mips.GetRegisters())
            {
                Console.WriteLine(reg.GetNames()[0] + " => " + reg.GetValue());
            }
            //SP2000DataMemory.Instance.ShowMemory();
        
        }

        public static void Convert(int num)
        {
            for(int i = 0; i < 8; i++)
            {
                if((num & (0x80 >> i)) > 0)
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
