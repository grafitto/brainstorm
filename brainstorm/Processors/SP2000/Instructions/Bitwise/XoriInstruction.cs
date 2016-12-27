using brainstorm.Base;
using brainstorm.Exceptions;
using BrainStorm.Processors.SP2000.Instructions;
using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processors.SP2000.Processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brainstorm.Processors.SP2000.Instructions.Bitwise
{
    class XoriInstruction : ArithmeticInstruction
    {
        private int immidiate;
        public XoriInstruction(string instruction, string destination, string firstOperand, int secondOperand) : base(instruction, destination, firstOperand, secondOperand.ToString())
        {
            immidiate = secondOperand;
        }

        /// <summary>
        /// This excecuted the instruction and changes the CPU register values
        /// </summary>
        /// <param name="processor"></param>
        public override void execute(SP2000Core core)
        {
            SP2000Registers registers = (SP2000Registers)core.Registers;
            int result = 0;
            try
            {
                Register first = registers.FetchRegister(firstOperand);
  
                result = first.Value ^ immidiate;
            }
            catch (RegisterException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                registers.StoreToName(destination, result);
                this.increamentPC(core);
            }
        }
    }
}
