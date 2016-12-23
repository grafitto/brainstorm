using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processors.SP2000.Processor;
using System;
using brainstorm.Processors.SP2000.Instructions.Arithmetic;

namespace BrainStorm
{

    class Test
    {
        public static void Main(string[] args)
        {
           
            SP200InstructionMemory program = new SP200InstructionMemory();
            SP2000DataMemory memory = new SP2000DataMemory();
            MoveInstruction add = new MoveInstruction("mov $v0, $v1", "$v0", "$v1");
            program.push(add, 0);

            SP2000Processor chip = new SP2000Processor(program, memory);

            chip.tick();
            //registers.ShowRegisters();

            /*  int value = 1088;

             value = value << 16;
             Convert(value);
             int res = (int)(value & 0xFFFF0000);

             Convert(res); */

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
