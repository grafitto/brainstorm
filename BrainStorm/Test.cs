using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processor.SP2000.Instructions;
using BrainStorm.Processors.SP2000.Processor;
using BrainStorm.Processors.SP2000.Instructions;
using brainstorm.Processors.SP2000.Instructions;
using System;

namespace BrainStorm
{

    class Test
    {
        public static void Main(string[] args)
        {
            /* SP2000Registers registers = new SP2000Registers(6);
             registers.MakeRegister(new string[] { "$t0" }, 0);
             registers.MakeRegister(new string[] { "$t1" }, 1);
             registers.MakeRegister(new string[] { "$t2" }, 2);

             registers.StoreToName("$t0", 24);
             registers.StoreToName("$t1", 0);
             registers.StoreToName("$t2", 2);

             SP200InstructionMemory program = new SP200InstructionMemory();
             SP2000DataMemory memory = new SP2000DataMemory();

             SP2000Processor chip = new SP2000Processor(program, memory, registers);

             AddiInstruction inst = new AddiInstruction("sub $t1, $t0, 5", "$t1", "$t0", "5");

             chip.tick(inst);

             registers.ShowRegisters(); */

            int value = 1088;
            
            value = value << 16;
            Convert(value);
            int res = (int)(value & 0xFFFF0000);
          
            Convert(res);

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
