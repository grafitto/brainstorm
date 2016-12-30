using BrainStorm.Base;
using BrainStorm.Exceptions;
using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processors.SP2000.Processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainStorm.Processors.SP2000.Instructions.Arithmetic
{
    class MultInstruction : TwoOperandInstruction
    {
        public MultInstruction(string instruction, string firstOperand, string secondOperand) : base(instruction, firstOperand, secondOperand) { }

        /// <summary>
        /// This excecuted the instruction and changes the CPU register values
        /// </summary>
        /// <param name="processor"></param>
        public override void execute(SP2000Core core)
        {
            SP2000Registers registers = (SP2000Registers)core.Registers;
            long result = 0;
            try
            {
                Register first = registers.FetchRegister(firstOperand);
                Register second = registers.FetchRegister(secondOperand);

                result = first.GetValue() * second.GetValue();
            }
            catch (RegisterException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                registers.SetHILO64(result);
                this.increamentPC(core);
                core.Cycles += Cycles;
            }
        }
    }
}
