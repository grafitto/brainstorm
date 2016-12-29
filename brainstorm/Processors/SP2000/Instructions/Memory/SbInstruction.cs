using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrainStorm.Processors.SP2000.Processor;
using BrainStorm.Processors.SP2000.Memory;
using brainstorm.Exceptions;
using brainstorm.Base;

namespace brainstorm.Processors.SP2000.Instructions.Memory
{
    class SbInstruction : TwoOperandInstruction
    {
        private string regSource;
        private int ramDest;

        public SbInstruction(string instruction, string regSource, int ramDest) : base(instruction, regSource, ramDest.ToString())
        {
            this.regSource = regSource;
            this.ramDest = ramDest;
        }

        public override void execute(SP2000Core core)
        {
            SP2000Registers registers = (SP2000Registers)core.Registers;
            SP2000DataMemory memory = SP2000DataMemory.Instance;
            try
            {
                Register source = registers.FetchRegister(this.regSource);
                memory.StoreByte(source.GetLowRight8(), ramDest);

            }
            catch (RegisterException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                increamentPC(core);
            }
        }
    }
}
