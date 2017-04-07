using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainStorm.Processors.SP2000.Instructions.Memory
{
    class LaInstruction : LiInstruction
    {
        public LaInstruction(string instruction, string firstOperand, int secondOperand) : base(instruction, firstOperand, secondOperand)
        {
        }
    }
}
