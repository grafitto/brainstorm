using BrainStorm.Base;
using BrainStorm.Memory;
using BrainStorm.Processor.SP2000.Instructions;

namespace BrainStorm.Processors.SP2000.Instructions
{
    abstract class ThreeOpInstruction : SP2000Instruction
    {
        public ThreeOpInstruction(string instruction, string firstOperand, string secondOperand, string thirdOperand) : base(instruction)
        {

        }
    }
}
