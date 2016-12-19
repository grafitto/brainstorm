using BrainStorm.Base;
using BrainStorm.Processors.SP2000.Processor;

namespace BrainStorm.Processor.SP2000.Instructions
{
    abstract class SP2000Instruction : Instruction
    {
        private int cycles = 1;
        public SP2000Instruction(string instruction) : base(instruction) { }

        /// <summary>
        /// This is where the instruction executes itself using the visitor partern
        /// </summary>
        /// <param name="processor"></param>

        virtual public void execute(SP2000Processor processor)
        {

        }
    }
}
