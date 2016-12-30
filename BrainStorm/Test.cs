using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processors.SP2000.Processor;
using System;
using BrainStorm.Processors.SP2000.Instructions.Bitwise;

namespace BrainStorm
{

    class Test
    {
        public static void Main(string[] args)
        {
           SP2000Registers registers = new SP2000Registers();

           registers.StoreToName("$s2", 4);
           registers.StoreToName("$s0", 60);
           registers.StoreToName("$s1", 2);
           //registers.ShowRegisters();

           
            SP200InstructionMemory program = new SP200InstructionMemory();
            SP2000DataMemory memory = new SP2000DataMemory();

            SP2000Processor chip = new SP2000Processor(program, memory, registers);

            SrlInstruction add = new SrlInstruction("srl $s2, $s0, 2", "$s2", "$s0", "2");

            chip.tick(add);
            registers.ShowRegisters();

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
