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

        public override string ToString()
        {
            return this.instruction;
        }
    }
}
