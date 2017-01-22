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
        public int StoreWord(int data, int address)
        {
            lock (StoreWordPadlock)
            {
                byte[] temp = GetBytesFromWord(data);

                for (int i = 0; i < temp.Length; i++)
                {
                    this.memory[address] = temp[i];
                    address = address + 1;
                }
                return temp.Length;
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
        public int StoreByte(byte data, int address)
        {
            lock (StoreBytePadlock)
            {
                this.memory[address] = data;
                return 1;
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

        /// <summary>
        /// Stores string bytes in memory
        /// </summary>
        /// <param name="input"></param>
        /// <param name="address"></param>
        public int StoreStringBytes(string input, int address)
        {
          
            byte[] bytes = Encoding.ASCII.GetBytes(input);
            foreach(byte b in bytes)
            {
                this.StoreByte(b, address++);
            }
            return bytes.Length;
        }

        /// <summary>
        /// Reads a string from memory
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public string LoadString(int address)
        {
            List<byte> bytes = new List<byte>();
            do
            {
                bytes.Add(this.LoadByte(address++));
            } while (this.memory[address] != '\n');
            return BitConverter.ToString(bytes.ToArray());
        }

        public int StoreWords(int[] data, int address)
        {
            int length = 0;
            foreach(int item in data)
            {
                int written = this.StoreWord(item, address);
                address += written;
                length += written;
            }
            return length;
        }
    }
}
