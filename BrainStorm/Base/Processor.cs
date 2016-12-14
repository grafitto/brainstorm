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

        public void tick(Instruction instruction)
        {
            this.cycles += instruction.Cycles;
        }
    }
}
