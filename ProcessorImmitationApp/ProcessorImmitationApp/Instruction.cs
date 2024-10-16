using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorImmitationApp
{
    internal class Instruction
    {
        public uint CmdType { get; }
        public uint Operand1 { get; }
        public uint Operand2 { get; }

        public Instruction(uint binaryInstruction)
        {
            // Извлечение полей команды 
            CmdType = (binaryInstruction >> 8) & 0x3;     // Извлечение битов 9-8
            Operand1 = (binaryInstruction >> 4) & 0xF;    // Извлечение битов 7-4
            Operand2 = binaryInstruction & 0xF;           // Извлечение битов 3-0
        }

        public override string ToString()
        {
            return $"CmdType: {Convert.ToString(CmdType, 2).PadLeft(2, '0')}, Operand1: {Convert.ToString(Operand1, 2).PadLeft(4, '0')}, Operand2: {Convert.ToString(Operand2, 2).PadLeft(4, '0')}";
        }
    }
} 
