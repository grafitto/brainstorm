using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainStorm
{
    abstract class Instruction
    {
        private int cycles;
        public int Cycles
        {
            get{ return cycles; }
            set{ Cycles = cycles; }
        }
        /**
         * This is where the instruction executes itself using the visitor technique
         **/
        abstract public void execute(Processor processor);
    }
}
