using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processor.SP2000.Instructions;
using BrainStorm.Processors.SP2000.Processor;
using BrainStorm.Processors.SP2000.Instructions;

namespace BrainStorm
{

    class Test
    {
        public static void Main(string[] args)
        {
            SP2000Registers registers = new SP2000Registers(6);
            registers.MakeRegister(new string[] { "$t0" }, 0);
            registers.MakeRegister(new string[] { "$t1" }, 1);
            registers.MakeRegister(new string[] { "$t2" }, 2);

            registers.StoreToName("$t0", 2);
            registers.StoreToName("$t1", 0);
            registers.StoreToName("$t2", 12);

            SP200InstructionMemory program = new SP200InstructionMemory();
            SP2000DataMemory memory = new SP2000DataMemory();

            SP2000Processor chip = new SP2000Processor(program, memory, registers);

            AddInstruction inst = new AddInstruction("add $t1, $t0, $t2", "$t1", "$t0", "$t2");

            chip.tick(inst);

            registers.ShowRegisters();
        }
    }
}
