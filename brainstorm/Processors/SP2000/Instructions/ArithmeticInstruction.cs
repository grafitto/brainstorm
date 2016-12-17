using BrainStorm.Processor.SP2000.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainStorm.Processors.SP2000.Instructions
{
    abstract class ArithmeticInstruction : ThreeOpInstruction
    {
        protected string destination;
        public ArithmeticInstruction(string instruction, string destination, string firstOperand, string secondOperand) : base(instruction, destination, firstOperand, secondOperand)
        {
            this.destination = destination;
        }
    }
}
