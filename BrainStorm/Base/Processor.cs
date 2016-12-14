using BrainStorm.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainStorm
{
    abstract class Processor
    {
        private int cycles;
        private InstructionMemory program;
        private DataMemory memory;

        /// <summary>
        /// The tick method keeps track of the total cycles since the program started
        /// </summary>
        /// <param name="instruction"></param>
        
        public void tick(Instruction instruction)
        {
            this.cycles += instruction.Cycles;
        }
    }
}
