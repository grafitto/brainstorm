using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainStorm.Base
{
    
    abstract class Memory
    {
        private readonly static object StoreWordPadlock = new object();
        private readonly static object StoreBytePadlock = new object();

        protected byte[] memory;

        /// <summary>
        /// Initializes the size to be 1024 bytes = 1 Kilobyte
        /// </summary>
        /// <param name="size"></param>
        public Memory(int size = 1024)
        {
            this.memory = new byte[size];
        }

        /// <summary>
        /// Loads a Word from the specified address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public int LoadWord(int address)
        {
            //Wait if there is any thread storing to memory
            lock(StoreWordPadlock)
            {
                return BitConverter.ToInt32(this.memory, address);
            }
        }

        /// <summary>
        /// Stores a word to the specified address
        /// </summary>
        /// <param name="data"></param>
        /// <param name="address"></param>
        public void StoreWord(int data, int address)
        {
            lock (StoreWordPadlock)
            {
                byte[] temp = GetBytesFromWord(data);

                for (int i = 0; i < temp.Length; i++)
                {
                    this.memory[address] = temp[i];
                    address = address + 1;
                }
            }
        }

        /// <summary>
        /// Gets a byte stored from an address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public byte LoadByte(int address)
        {
            lock (StoreBytePadlock)
            {
                return this.memory[address];
            }
        }

        /// <summary>
        /// Stores a byte to an address
        /// </summary>
        /// <param name="data"></param>
        /// <param name="address"></param>
        public void StoreByte(byte data, int address)
        {
            lock (StoreBytePadlock)
            {
                this.memory[address] = data;
            }
        }

        /// <summary>
        /// Gets bytes from a Word
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private byte[] GetBytesFromWord(int word)
        {
            return BitConverter.GetBytes(word);
        }
    }
}
