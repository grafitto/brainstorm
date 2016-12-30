using BrainStorm.Processor.SP2000.Instructions;

namespace BrainStorm.Processors.SP2000.Instructions
{
    class OneOperandInstruction : SP2000Instruction
    {
        protected string firstOperand;

        public OneOperandInstruction(string instruction, string firstOperand) : base(instruction)
        {
            this.firstOperand = firstOperand;
        }
    }
}
