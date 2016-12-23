using brainstorm.Base;
using BrainStorm.Base;
using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processors.SP2000.Processor;
using System;

namespace BrainStorm.Processor.SP2000.Instructions
{
    abstract class SP2000Instruction : Instruction
    {
        private int cycles = 1;
        public SP2000Instruction(string instruction) : base(instruction) { }

        /// <summary>
        /// This is where the instruction executes itself using the visitor partern
        /// </summary>
        /// <param name="processor"></param>

        virtual public void execute(SP2000Core processor)
        {

        }
        protected void increamentPC(SP2000Core core, int? address = null )
        {

            SP2000Registers registers = (SP2000Registers)core.Registers;
            Register pc = registers.FetchRegister("PC");
            if(address == null)
            {
                pc.SetValue(pc.Value + 1);
            }
            else
            {
                pc.SetValue((int)address);
            }
        }
    }
}
