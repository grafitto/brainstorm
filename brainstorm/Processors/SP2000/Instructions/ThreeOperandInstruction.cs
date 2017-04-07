using BrainStorm.Base;
using BrainStorm.Memory;
using BrainStorm.Processor.SP2000.Instructions;

namespace BrainStorm.Processors.SP2000.Instructions
{
    abstract class ThreeOperandInstruction : SP2000Instruction
    {
        public ThreeOperandInstruction(string instruction, string firstOperand, string secondOperand, string thirdOperand) : base(instruction){}
    }
}
