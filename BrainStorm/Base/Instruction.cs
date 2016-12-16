namespace BrainStorm.Base
{
    abstract class Instruction
    {
        private string instruction;
        private int cycles;
        public int Cycles
        {
            get{ return cycles; }
            set{ Cycles = cycles; }
        }
        /// <summary>
        /// This saves the instruction text for display purposes
        /// </summary>
        /// <param name="instruction"></param>
        public Instruction(string instruction)
        {
            this.instruction = instruction;

        }
        /// <summary>
        /// This is where the instruction executes itself using the visitor partern
        /// </summary>
        /// <param name="processor"></param>

        abstract public void execute(Processor processor);
    }
}
