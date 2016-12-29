using brainstorm.Base;
using brainstorm.Exceptions;
using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processors.SP2000.Processor;
using System;

namespace brainstorm.Processors.SP2000.Instructions.Memory
{
    class MfloInstruction : OneOperandInstruction
    {
        public MfloInstruction(string instruction, string firstOperand) : base(instruction, firstOperand) { }

        public override void execute(SP2000Core core)
        {
            SP2000Registers registers = (SP2000Registers)core.Registers;

            try
            {
                Register destination = registers.FetchRegister(firstOperand);
                Register lo = registers.FetchRegister("LO");

                destination.SetValue(lo.GetValue());
            }
            catch (RegisterException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                //Pass
                this.increamentPC(core);
                core.Cycles += Cycles;
            }
        }
    }
}
