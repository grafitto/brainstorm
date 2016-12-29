using BrainStorm.Base;
using BrainStorm.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace brainstorm.Base
{
    abstract class Core
    {
        private SP2000RegisterSet registers;
        public SP2000RegisterSet Registers
        {
            get { return this.registers; }
            set { this.registers = Registers; }
        }
        private InstructionMemory program;
        public InstructionMemory Program
        {
            get { return program; }
            set { program = Program; }
        }
        public Core(InstructionMemory program)
        {
            this.registers = registers;
            this.program = program;
        }
        public abstract void execute();
    }
}
