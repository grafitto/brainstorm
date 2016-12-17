using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrainStorm.Base;
using brainstorm.Exceptions;
using BrainStorm.Processors.SP2000.Processor;
using BrainStorm.Processors.SP2000.Memory;

namespace BrainStorm.Processors.SP2000.Instructions
{
    class AddInstruction : ArithmeticInstruction
    {
        private string firstOperand;
        private string secondOperand;

        public AddInstruction(string instruction, string destination, string firstOperand, string secondOperand) : base(instruction, destination, firstOperand, secondOperand)
        {
            this.firstOperand = firstOperand;
            this.secondOperand = secondOperand;
        }
        public override void execute(SP2000Processor processor)
        {
            SP2000Registers registers = (SP2000Registers)processor.Registers;
            int result = 0;
            try {
                int? first = registers.FetchRegister(firstOperand);
                int? second = registers.FetchRegister(secondOperand);
                if(first != null || second != null)
                {
                    result = (int)first + (int)second;
                    Console.WriteLine(result);
                }else
                {
                    Console.WriteLine("Öne of them is null");
                }

            }catch(RegisterException e) {
                Console.WriteLine(e.Message);
            }finally {
                registers.StoreToName(destination, result);
            }
            
           
        }
    }
}
