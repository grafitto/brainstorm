using BrainStorm.Processor.SP2000.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brainstorm.Processors.SP2000.Instructions
{
    class TwoOperandInstruction : SP2000Instruction
    {
        protected string firstOperand;
        protected string secondOperand;
        public TwoOperandInstruction(string instruction, string firstOperand, string secondOperand) : base(instruction){
            this.firstOperand = firstOperand;
            this.secondOperand = secondOperand;
        }
    }
}
