using System;
using brainstorm.Exceptions;
using BrainStorm.Processors.SP2000.Processor;
using BrainStorm.Processors.SP2000.Memory;
using brainstorm.Base;

namespace BrainStorm.Processors.SP2000.Instructions
{
    class AddInstruction : ArithmeticInstruction
    {

        public AddInstruction(string instruction, string destination, string firstOperand, string secondOperand) : base(instruction, destination, firstOperand, secondOperand)
        {
        }

        /// <summary>
        /// This excecuted the instruction and changes the CPU register values
        /// </summary>
        /// <param name="processor"></param>
        public override void execute(SP2000Core core)
        {
            SP2000Registers registers = (SP2000Registers)core.Registers;
            int result = 0;
            try {
                Register first = registers.FetchRegister(firstOperand);
                Register second = registers.FetchRegister(secondOperand);

                result = first.GetValue() + second.GetValue();

            }catch(RegisterException e) {
                Console.WriteLine(e.Message);
            }finally {
                registers.StoreToName(destination, result);
                this.increamentPC(core);
                core.Cycles += Cycles;
            } 
        }
    }
}
