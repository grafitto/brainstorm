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
        private int cores;
        public int Cores
        {
            get { return cores; }
            set { this.cores = Cores; }
        }

        public Processor(InstructionMemory program, DataMemory memory, int cores = 1)
        {
            this.program = program;
            this.memory = memory;
            this.cores = cores;
        }

        /// <summary>
        /// The tick method keeps track of the total cycles since the program started
        /// </summary>
        /// <param name="instruction"></param>

        public virtual void Tick() { }
    }
}
