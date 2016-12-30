using BrainStorm.Base;
using BrainStorm.Exceptions;
using BrainStorm.Processors.SP2000.Instructions;
using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processors.SP2000.Processor;
using System;

namespace BrainStorm.Processors.SP2000.Instructions.Bitwise
{
    class SllInstruction : ArithmeticInstruction
    {
        private int immidiate;
        public SllInstruction(string instruction, string destination, string firstOperand, string secondOperand) : base(instruction, destination, firstOperand, secondOperand)
        {
            try
            {
                immidiate = int.Parse(secondOperand);
            }catch(FormatException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// This excecuted the instruction and changes the CPU register values
        /// </summary>
        /// <param name="processor"></param>
        public override void execute(SP2000Core processor)
        {
            SP2000Registers registers = (SP2000Registers)processor.Registers;
            int result = 0;
            try
            {
                Register first = registers.FetchRegister(firstOperand);
                result = first.Value << immidiate;
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
