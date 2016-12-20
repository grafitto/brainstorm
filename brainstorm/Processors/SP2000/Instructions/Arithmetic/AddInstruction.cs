using System;
using brainstorm.Exceptions;
using BrainStorm.Processors.SP2000.Processor;
using BrainStorm.Processors.SP2000.Memory;

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
        public override void execute(SP2000Processor processor)
        {
            SP2000Registers registers = (SP2000Registers)processor.Registers;
            int result = 0;
            try {
                int first = registers.FetchRegister(firstOperand);
                int second = registers.FetchRegister(secondOperand);

                result = first + second;

            }catch(RegisterException e) {
                Console.WriteLine(e.Message);
            }finally {
                registers.StoreToName(destination, result);
            } 
        }
    }
}
