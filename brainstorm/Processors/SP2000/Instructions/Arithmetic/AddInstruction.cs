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
        public override void execute(SP2000Core processor)
        {
            SP2000Registers registers = (SP2000Registers)processor.Registers;
            int result = 0;
            try {
                Register first = registers.FetchRegister(firstOperand);
                Register second = registers.FetchRegister(secondOperand);

                result = first.Value + second.Value;

            }catch(RegisterException e) {
                Console.WriteLine(e.Message);
            }finally {
                registers.StoreToName(destination, result);
            } 
        }
    }
}
