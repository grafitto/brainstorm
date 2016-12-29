using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processors.SP2000.Processor;
using System;
using brainstorm.Processors.SP2000.Instructions.Arithmetic;
using brainstorm.Processors.SP2000.Instructions.Memory;
using brainstorm.Processors.SP2000.Instructions;
using brainstorm.Base;

namespace BrainStorm
{

    class Test
    {
        public static void Main(string[] args)
        {
            
                SP200InstructionMemory program = new SP200InstructionMemory();
                SP2000DataMemory memory = new SP2000DataMemory();
                LiInstruction li = new LiInstruction("li $v1, 4", "$v1", 4);
                SwInstruction sw = new SwInstruction("sw $v1, 4", "$v1", 12);
                SwInstruction lw = new SwInstruction("sw $v1, 2($v1)", "$v1", "-1($v1)");
                //DivInstruction mul = new DivInstruction("div $v1, $v0", "$v1", "$v0");
                //MfhiInstruction mfhi = new MfhiInstruction("mfhi $a0", "$a0");
                //MfloInstruction mflo = new MfloInstruction("mflo $a1", "$a1");
                //SwInstruction sw = new SwInstruction("sw $v1, 4", "$v1", 4);
                //SbInstruction sb = new SbInstruction("sw $v0, 0", "$v0", 0);
                program.Push(li, 0);
                program.Push(sw, 1);
                program.Push(lw, 2);
                //program.Push(mul, 2);
                // program.Push(mfhi, 3);
                //program.Push(mflo, 4);
                //program.Push(sw, 5);
                // program.Push(sb, 6);

                SP2000Processor chip = new SP2000Processor(program);

                chip.Tick();
                chip.Tick();
                chip.Tick();
                // chip.Tick();
                // chip.Tick();
                // chip.Tick();
                // chip.Tick();
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
