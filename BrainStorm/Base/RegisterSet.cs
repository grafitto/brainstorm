using brainstorm.Base;
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
        private Register[] registers;

        /// <summary>
        /// Initialization if registers and indices
        /// </summary>
        /// <param name="size"></param>
        public RegisterSet(int size)
        {
            this.registers = new Register[size];
        }

        /// <summary>
        /// This looks for an index with the provided name, throws KeyNotFoundEsception if the key is not found
        /// </summary>
        /// <param name="name"></param>
        /// <returns>int</returns>
        private Register Lookup(string name)
        {
            //Console.WriteLine("Looking up");
            Register final = null;
            foreach(Register register in registers) {
                                                            //Console.WriteLine("  Looking for name: " + name + " on register: " + register.Names[0]);
                foreach(string regName in register.Names) {
                                                            //Console.WriteLine("     Comparing '" + name + "' with '" + regName + "'");
                    if(regName.Equals(name))
                    {
                                                            //Console.WriteLine("      Found!!!");
                        final = register;
                    }
                    if (final != null) break;
                }
                if (final != null) break;
            }
            if(final != null)
            {
                return final;
            } else {
                throw new RegisterException("Name lookup for " + name + " in Registers was not successfull :-(");
            }
            
              
        }

        /// <summary>
        /// This fetches a register value using an index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Nullable int</returns>
        
        public Register FetchRegister(int index)
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
        
        public Register FetchRegister(string name) {

                return this.Lookup(name);
                
        }

        /// <summary>
        /// This names a register using desirable names, many names can be assigned to a single register
        /// </summary>
        /// <param name="names"></param>
        /// <param name="index"></param>
        
        public void MakeRegister(string[] names, int index, string text = "Register")
        {

            Register register = new Register(names, 0, text);
            this.registers[index] = register;
            
        }

        /// <summary>
        /// Makes registers with names provided and the corresponding indices of the array
        /// </summary>
        /// <param name="names"></param>
        
        public void MakeRegisters(string[] names, string text = "Register") {

            for(int i = 0; i < names.Length; i++)
            {
                this.MakeRegister(new string[] { names[i] }, i, text);
            }
        }

        public void MakeRegisters(string[] names, int[] indices, string text = "Register")
        {
            if(names.Length == indices.Length)
            {
                for(int i = 0; i < names.Length; i++)
                {
                    this.MakeRegister(new string[] { names[i] }, indices[i], text);
                }
            }
        }

        /// <summary>
        /// This stores a value to a register using the register name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        
        public void StoreToName(string name, int value)
        {
            Register register = this.Lookup(name);
            register.SetValue(value);
        }

        /// <summary>
        /// This displays the values of a register, this is a testing method and should be removed in future
        /// </summary>
        public void ShowRegisters()
        {
            Console.WriteLine("Registers");
            foreach (Register register in registers)
            {
                if (register != null) {
                    Console.Write(register.Value);
                    Console.Write(" ");
                    Console.Write(register.Names[0]);
                    Console.Write(" ");
                    Console.Write(register.Text);
                }
                Console.WriteLine("");
            }
       
        }
    }
}
