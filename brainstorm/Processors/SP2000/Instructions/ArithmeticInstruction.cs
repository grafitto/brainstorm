

namespace BrainStorm.Processors.SP2000.Instructions
{
    abstract class ArithmeticInstruction : ThreeOpInstruction
    {
        protected string destination;
        protected string firstOperand;
        protected string secondOperand;
        public ArithmeticInstruction(string instruction, string destination, string firstOperand, string secondOperand) : base(instruction, destination, firstOperand, secondOperand)
        {
            this.destination = destination;
            this.firstOperand = firstOperand;
            this.secondOperand = secondOperand;
        }
    }
}
