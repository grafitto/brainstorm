using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processor.SP2000.Instructions;

namespace BrainStorm.Processors.SP2000.Processor
{
    class SP2000Processor : Base.Processor
    {
        public SP2000Processor(SP200InstructionMemory program, SP2000DataMemory memory, SP2000Registers registers) : base(program, memory, registers)
        {

        }
        public void tick(SP2000Instruction instruction)
        {
            base.tick(instruction);
            instruction.execute(this);
        }
    }
}
