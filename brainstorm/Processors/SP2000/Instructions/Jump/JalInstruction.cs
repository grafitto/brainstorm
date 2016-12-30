using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrainStorm.Processors.SP2000.Processor;
using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Exceptions;
using BrainStorm.Base;

namespace BrainStorm.Processors.SP2000.Instructions.Jump
{
    class JalInstruction : OneOperandInstruction
    {
        private int address;
        public JalInstruction(string instruction, int address) : base(instruction, address.ToString())
        {
            this.address = address;
        }

        public override void execute(SP2000Core core)
        {
            SP2000Registers registers = (SP2000Registers)core.Registers;

            try
            {
                Register pc = registers.FetchRegister("PC");
                int initialAddress = pc.GetValue();
                //Store initial address to return address register
                Register ra = registers.FetchRegister("$ra");
                ra.SetValue(initialAddress);
                //Now we store the address to PC
                pc.SetValue(address);

            }catch(RegisterException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                core.Cycles += Cycles;
            }
        }
    }
}
