using System;
using BrainStorm.Processors.SP2000.Processor;
using BrainStorm.Processors.SP2000.Memory;
using brainstorm.Base;
using brainstorm.Exceptions;

namespace brainstorm.Processors.SP2000.Instructions.Jump
{
    class JrInstruction : OneOperandInstruction
    {
        public JrInstruction(string instruction, string firstOperand) : base(instruction, firstOperand)
        {
        }

        public override void execute(SP2000Core core)
        {
            SP2000Registers registers = (SP2000Registers)core.Registers;

            try
            {
                Register addressRegister = registers.FetchRegister(this.firstOperand);
                int address = addressRegister.GetValue();
                Register pc = registers.FetchRegister("PC");
                pc.SetValue(address);

            }
            catch(RegisterException e)
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
