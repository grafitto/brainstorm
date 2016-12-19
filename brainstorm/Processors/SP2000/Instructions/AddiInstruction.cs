using brainstorm.Exceptions;
using BrainStorm.Processors.SP2000.Instructions;
using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processors.SP2000.Processor;
using System;

namespace brainstorm.Processors.SP2000.Instructions
{
    class AddiInstruction : ArithmeticInstruction
    {
        private string firstOperand;
        private int secondOperand;

        public AddiInstruction(string instruction, string destination, string firstOperand, string secondOperand) : base(instruction, destination, firstOperand, secondOperand)
        {
            this.firstOperand = firstOperand;
            try
            {
                this.secondOperand = int.Parse(secondOperand);

            }catch(FormatException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// This excecuted the instruction and changes the CPU register values
        /// </summary>
        /// <param name="processor"></param>
        public override void execute(SP2000Processor processor)
        {
            SP2000Registers registers = (SP2000Registers)processor.Registers;
            int result = 0;
            try
            {
                int first = registers.FetchRegister(firstOperand);
                result = first + secondOperand;
            }
            catch (RegisterException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                registers.StoreToName(destination, result);
            }


        }
    }
}
