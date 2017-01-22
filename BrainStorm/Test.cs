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

namespace BrainStorm
{
    class Test
    {
        public static void Main(string[] args)
        {
            
                SP2000InstructionMemory program = new SP2000InstructionMemory();
                SP2000DataMemory memory = new SP2000DataMemory();
                //JalInstruction li = new JalInstruction("jal 4", 40);
                LaInstruction li = new LaInstruction("li $t0, 4", "$t0", 4);
            LaInstruction li2 = new LaInstruction("li $t1, 5", "$t1", 5);
            LaInstruction li3 = new LaInstruction("li $t2, 6", "$t2", 6);
            LaInstruction li4 = new LaInstruction("li $t3, 7", "$t3", 7);
            LaInstruction li5 = new LaInstruction("li $t0, 8", "$t4", 8);
            //BeqInstruction lw = new BeqInstruction("blt $v1, $v0, 0", "$v1", "$v0", 20);

            program.Push(li, 0);
            program.Push(li2, 1);
            program.Push(li3, 2);
            program.Push(li4, 3);
            program.Push(li5, 4);
            //program.Push(j, 1);

            //SP2000Simulator mips = new SP2000Simulator(0, program, 5);
            // mips.Run();

            // SP2000DataMemory mem = SP2000DataMemory.Instance;
            // mem.ShowMemory();

            /*  int value = 1088;

             value = value << 16;
             Convert(value);
             int res = (int)(value & 0xFFFF0000);

             Convert(res); */
            string file = @"C:\Users\Nderitu\Desktop\emulation\mips.asm";
            SP2000Assembler assembler = new SP2000Assembler(file);
            assembler.Assemble();

            SP2000DataMemory.Instance.ShowMemory();

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
