using brainstorm.Base;
using brainstorm.Exceptions;
using BrainStorm.Processor.SP2000.Instructions;
using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processors.SP2000.Processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainStorm.Processors.SP2000.Instructions.Memory
{
    class LbInstruction : TwoOperandInstruction
    {
        private string destRegister;
        private int ramSource;

        public LbInstruction(string instruction, string destRegister, int ramSource) : base(instruction, destRegister, ramSource.ToString())
        {
            this.destRegister = destRegister;
            this.ramSource = ramSource;
        }

        /// <summary>
        /// This excecuted the instruction and changes the CPU register values
        /// </summary>
        /// <param name="processor"></param>
        public override void execute(SP2000Core core)
        {
            SP2000Registers registers = (SP2000Registers)core.Registers;
            try
            {
                Register register = registers.FetchRegister(destRegister);
                SP2000DataMemory memory = SP2000DataMemory.Instance;
                byte data = memory.LoadByte(ramSource);
                //Should store the byte
                register.SetLowRight8(data);

            }
            catch (RegisterException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.increamentPC(core);
                core.Cycles += Cycles;
            }
        }
    }
}
