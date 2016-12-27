using brainstorm.Exceptions;
using BrainStorm.Base;
using System;
using System.Collections.Generic;
using System.Collections;

namespace BrainStorm.Memory
{
    class InstructionMemory : Base.Memory, IEnumerable<Dictionary<int,Instruction>>
    {
        private Dictionary<int, Instruction> memory;
        public InstructionMemory()
        {
            memory = new Dictionary<int, Instruction>();
        }

        /// <summary>
        /// Stores an instruction to the provided address
        /// </summary>
        /// <param name="instruction">Instruction to be saved</param>
        /// <param name="address">Address to save the instruction</param>
        public void Push(Instruction instruction, int address)
        {
            memory.Add(address, instruction);
        }

        /// <summary>
        /// Retrieves an instruction from the specified address
        /// </summary>
        /// <param name="address">Address for instruction retrieval</param>
        /// <returns name="Instruction">The instruction returned</returns>
        /// <exception cref="MemoryException">When the address specified is not found, MemoryException is throw</exception>
        public Instruction Pop(int address)
        {
            if(memory.ContainsKey(address)){
                return memory[address];
            }else{
                throw new MemoryException("Instruction not found in memory: " + Convert.ToString(address, 2) + " -> " + address);
            }
        }

        /****************************************************************************************/
        /// <summary>
        /// These are just to allow enumeration of the class object
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Instruction>.Enumerator GetEnumerator()
        {
            return memory.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<Dictionary<int, Instruction>> IEnumerable<Dictionary<int, Instruction>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
