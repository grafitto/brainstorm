using brainstorm.Exceptions;
using BrainStorm.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainStorm.Base
{
    abstract class RegisterSet
    {
        private Dictionary<String, int> indices;
        //nullable int
        private int?[] registers;

        /// <summary>
        /// Initialization if registers and indices
        /// </summary>
        /// <param name="size"></param>
        
        public RegisterSet(int size)
        {
            this.registers = new int?[size];
            this.indices = new Dictionary<string, int>();
        }
        /// <summary>
        /// This looks for an index with the provided name, throws KeyNotFoundEsception if the key is not found
        /// </summary>
        /// <param name="name"></param>
        /// <returns>int</returns>
        
        public int Lookup(String name)
        {
            if (this.indices.ContainsKey(name)) {

                return this.indices[name];
            } else {

                RegisterException up = new RegisterException("Name lookup for "+ name +" in Registers was not successfull :-(");
                throw up;
            }      
        }
        /// <summary>
        /// This fetches a register value using an index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Nullable int</returns>
        
        public int? FetchRegister(int index)
        {
            if (this.registers.Length > index) { 

                    return this.registers[index];
            } else {

                RegisterException up = new RegisterException("Index out of Bound");
                throw up;
            }
        }
        /// <summary>
        /// Returns a register referred to with the name provided
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Nullable int</returns>
        
        public int? FetchRegister(string name) {

                int index = this.Lookup(name);
                int? value = this.FetchRegister(index);
                return value;
        }
        /// <summary>
        /// This names a register using desirable names, many names can be assigned to a single register
        /// </summary>
        /// <param name="names"></param>
        /// <param name="index"></param>
        
        public void MakeRegister(string[] names, int index){

            foreach(var name in names) {

                this.indices.Add(name, index);
            }
        }
        /// <summary>
        /// Makes registers with names provided and the corresponding indices of the array
        /// </summary>
        /// <param name="names"></param>
        
        public void MakeRegisters(string[] names) {

            for(int i = 0; i < names.Length; i++)
            {
                this.MakeRegister(new string[] { names[i] }, i);
            }
        }
        /// <summary>
        /// This stores a value to a register using the register name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        
        public void StoreToName(string name, int value)
        {
            int index = this.Lookup(name);
            this.registers[index] = value;
        }
        public void ShowRegisters()
        {
            Console.WriteLine("Registers");
            foreach(var register in registers)
            {
                if (register != null)
                    Console.Write(register);
                else
                    Console.Write("NULL");
                Console.Write(",");
            }
            Console.WriteLine("");
            Console.WriteLine("Indices");
            foreach(var index in indices)
            {
                Console.WriteLine(index.Key + " > " + index.Value);
            }
        }
    }
}
