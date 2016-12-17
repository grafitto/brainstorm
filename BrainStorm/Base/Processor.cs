using BrainStorm.Memory;

namespace BrainStorm.Base
{
    abstract class Processor
    {
        public int cycles;
        private InstructionMemory program;

        public InstructionMemory Program
        {
            get { return program; }
            set { this.program = Program; }
        }

        private DataMemory memory;

        public DataMemory Memory
        {
            get { return memory; }
            set { this.memory = Memory; }
        }
        private RegisterSet registers;

        public RegisterSet Registers
        {
            get { return registers; }
            set { this.registers = Registers; }
        }

        public Processor(InstructionMemory program, DataMemory memory, RegisterSet registers)
        {
            this.program = program;
            this.memory = memory;
            this.registers = registers;
        }

        /// <summary>
        /// The tick method keeps track of the total cycles since the program started
        /// </summary>
        /// <param name="instruction"></param>

        public virtual void tick(Instruction instruction) { }
    }
}
