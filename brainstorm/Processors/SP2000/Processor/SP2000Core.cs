using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrainStorm.Base;
using BrainStorm.Memory;
using brainstorm.Base;
using BrainStorm.Processor.SP2000.Instructions;
using BrainStorm.Processors.SP2000.Memory;

namespace BrainStorm.Processors.SP2000.Processor
{
    class SP2000Core : Core
    {
        private RegisterSet registers;
        public new RegisterSet Registers
        {
            get { return registers; }
        }
        public SP2000Core(InstructionMemory program) : base(program)
        {
            registers = new SP2000Registers();
        }

        /// <summary>
        /// Executes an instruction found in the specified address if any, else fetches the from PC
        /// </summary>
        /// <param name="address"></param>
        public override void execute(int? address = null)
        {
            if(address != null)
            {
                SP2000Instruction current = this.getInstruction((int)address);
                current.execute(this);
            }
            else
            {
                Register register = registers.FetchRegister("PC");
                SP2000Instruction current = this.getInstruction(register.Value);
                current.execute(this);

            }
            //Registers.ShowRegisters();
        }

        /// <summary>
        /// Gets an instruction in an address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private SP2000Instruction getInstruction(int address)
        {
            return (SP2000Instruction)Program.pop((int)address);
        }
    }
}
