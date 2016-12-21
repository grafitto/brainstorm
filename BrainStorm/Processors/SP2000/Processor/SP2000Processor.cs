using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processor.SP2000.Instructions;

namespace BrainStorm.Processors.SP2000.Processor
{
    class SP2000Processor : Base.Processor
    {
        private SP2000Core[] cores;
        private int context = 0;
        public SP2000Processor(SP200InstructionMemory program, SP2000DataMemory memory, SP2000Registers registers, int cores = 1) : base(program, memory, cores)
        {
            this.cores = new SP2000Core[cores];
            for(int i = 0; i < this.cores.Length; i++)
            {
                this.cores[i] = new SP2000Core(registers);
            }
        }
        public void tick(SP2000Instruction instruction)
        {
            base.tick(instruction);
            instruction.execute(cores[context]);
        }
    }
}
