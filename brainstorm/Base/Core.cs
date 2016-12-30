using BrainStorm.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainStorm.Base
{
    abstract class Core
    {
        private RegisterSet registers;
        public RegisterSet Registers
        {
            get { return this.registers; }
            set { this.registers = Registers; }
        }
        public Core(RegisterSet registers)
        {
            this.registers = registers;
        }
    }
}
