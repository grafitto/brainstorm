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

        private int cores;
        public int Cores
        {
            get { return cores; }
            set { this.cores = Cores; }
        }

        public Processor(InstructionMemory program, int cores = 1)
        {
            this.program = program;
            this.cores = cores;
        }

        /// <summary>
        /// The tick method keeps track of the total cycles since the program started
        /// </summary>
        /// <param name="instruction"></param>

        public virtual void Tick() { }
    }
}
