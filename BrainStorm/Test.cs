using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processors.SP2000.Processor;
using System;
using brainstorm.Processors.SP2000.Instructions.Arithmetic;
using BrainStorm.Processors.SP2000.Instructions.Memory;
using brainstorm.Processors.SP2000.Instructions;
using brainstorm.Base;
using brainstorm.Processors.SP2000.Instructions.Branch;
using BrainStorm.Processors.SP2000.Instructions.Jump;

namespace BrainStorm
{

    class Test
    {
        public static void Main(string[] args)
        {
            
                SP200InstructionMemory program = new SP200InstructionMemory();
                SP2000DataMemory memory = new SP2000DataMemory();
                //JalInstruction li = new JalInstruction("jal 4", 40);
                LaInstruction lia = new LaInstruction("li $v0, 3", "$v0", 4);
                //JrInstruction j = new JrInstruction("jr $ra", "$v0");
                //BeqInstruction lw = new BeqInstruction("blt $v1, $v0, 0", "$v1", "$v0", 20);

                program.Push(lia, 0);
                //program.Push(li, 1);
                //program.Push(j, 1);

                SP2000Processor chip = new SP2000Processor(program);

                //chip.Tick();
                //chip.Tick();
                chip.Tick();
                chip.ShowRegisters();


            SP2000DataMemory mem = SP2000DataMemory.Instance;
            mem.ShowMemory();

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
