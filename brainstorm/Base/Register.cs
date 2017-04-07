using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainStorm.Base
{
    public class Register
    {
        private string[] names;
        public string[] Names
        {
            get { return names;  }
            set { names = Names; }
        }
        private int value;
        public int Value
        {
            get { return value; }
            set { this.value = Value; }
        }
        public string text;
        public string Text
        {
            get { return text; }
            set { text = Text; }
        }

        public Register() { }
        public Register(string[] names, int value, string text)
        {
            this.names = names;
            this.value = value;
            this.text = text;
        }
        public string[] GetNames()
        {
            return Names;
        }
        public int GetValue()
        {
            return value;
        }
        public void SetValue(int value)
        {
            this.value = value;
        }

        /// <summary>
        /// Gets the Lower 16 bits of the register value in Int15
        /// </summary>
        /// <returns></returns>
        public Int16 GetLow16()
        {
            //First, move the bits 16 steps to the left
            //That drops the all 16 high order bits
            //Second, move the bits back to the initial position
            //That makes sure the the 16 high order bits are turned to 0's
            return (Int16)((value << 16) >> 16);
        }

        /// <summary>
        /// Gets the Higher 16bits of the register value in Int16
        /// </summary>
        /// <returns></returns>
        public Int16 GetHigh16()
        {
            //First, an AND operation is done on the value and 0xFFFF0000
            //That preserves the higher order 16 bits
            //Second, a 16 step shift to the right
            //That drops the 16 low order bits
            return (Int16)((value & 0xFFFF0000) >> 16);
        }

        /// <summary>
        /// Gets the 8 low order bits of the register value
        /// </summary>
        /// <returns></returns>
        public byte GetLowRight8()
        {
            //First get the 16 low order bits
            Int16 temp = GetLow16();

            //Second, move the bits 16 steps to the left
            //That drops the all 8 high order bits
            //Third, move the bits back to the initial position
            //That makes sure the the 8 high order bits are turned to 0's
            return (byte)((temp << 8) >> 8);
        }

        /// <summary>
        /// Gets the next 8 low order bits of the register value
        /// </summary>
        /// <returns></returns>
        public byte GetLowLeft8()
        {
            //First, get the 16 low order bits
            Int16 temp = GetLow16();

            //Second, an AND operation is done on the value and 0xFF00
            //That preserves the higher order 16 bits
            //Third, a 8 step shift to the right
            //That drops the 8 low order bits
            return (byte)((temp & 0xFF00) >> 8);
        }

        /// <summary>
        /// Gets the rightmost 8 bits of the 16 high order bits
        /// </summary>
        /// <returns></returns>
        public byte GetHighRight8()
        {
            //First get the 16 low order bits
            Int16 temp = GetHigh16();

            //Second, move the bits 16 steps to the left
            //That drops the all 8 high order bits
            //Third, move the bits back to the initial position
            //That makes sure the the 8 high order bits are turned to 0's
            return (byte)((temp << 8) >> 8);
        }

        /// <summary>
        /// Gets the high order 8 bits
        /// </summary>
        /// <returns></returns>
        public byte GetHighLeft8()
        {
            //First, get the 16 low order bits
            Int16 temp = GetHigh16();

            //Second, an AND operation is done on the value and 0xFF00
            //That preserves the higher order 16 bits
            //Third, a 8 step shift to the right
            //That drops the 8 low order bits
            return (byte)((temp & 0xFF00) >> 8);
        }

        /// <summary>
        /// Sets the 16 low order bits and stores the value
        /// </summary>
        /// <param name="value"></param>
        public void SetLow16(Int16 value)
        {
            //First, 
            //an OR operation joins the 16 low order bits in register value
            this.value = ((int)(this.value & 0xFFFF0000) | (int)value); 
        }

        /// <summary>
        /// Sets the 16 high order bits and stores the value
        /// </summary>
        /// <param name="value"></param>
        public void SetHigh16(Int16 value)
        {
            //First, move the 16 low order bits to high order level
            int temp = (int)(value << 16);
            //Second, an OR operation joins the 16 high order bits in register value
            this.value = (this.value & 0x0000FFFF | temp);
        }

        /// <summary>
        /// Sets the rightmost 8 bits of the 16 low order bits, and stores the value
        /// </summary>
        /// <param name="value"></param>
        public void SetLowRight8(byte value)
        {
            this.value = ((int)(this.value & 0xFFFFFF00) | value);
        }

        /// <summary>
        /// Sets the leftmost 8 bit of the 16 low order bits and stores the value
        /// </summary>
        /// <param name="value"></param>
        public void SetLowLeft8(byte value)
        {
            Int16 temp = (Int16)(value << 8);
            this.value = (int)((this.value & 0xFFFF00FF) | temp);
        }

        /// <summary>
        /// Sets the rightmost 8 bits of the 16 high order bits
        /// </summary>
        /// <param name="value"></param>
        public void SetHighRight8(byte value)
        {
            int temp = (int)(value << 16);
            this.value = (int)(this.value & 0xFF00FFFF | temp);
        }

        /// <summary>
        /// Sets the leftmost 8 bits of the 16 high order bits and stores the value
        /// </summary>
        /// <param name="value"></param>
        public void SetHighLeft8(byte value)
        {
            int temp = (int)(value << 24);
            this.value = (this.value & 0x00FFFFFF | temp);
        }

        /// <summary>
        /// Converts to string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return text;
        }

        public static int operator + (Register self, Register other)
        {
            return self.Value + other.Value;
        }

        public static int operator -(Register self, Register other)
        {
            return self.Value - other.Value;
        }
        public static int operator *(Register self, Register other)
        {
            return self.Value * other.Value;
        }
        public static int operator &(Register self, Register other)
        {
            return self.Value & self.Value;
        }
        public static int operator |(Register self, Register other)
        {
            return self.Value | other.Value;
        }
        public static int operator ^(Register self, Register other)
        {
            return self.Value ^ other.Value;
        }
    }
}
