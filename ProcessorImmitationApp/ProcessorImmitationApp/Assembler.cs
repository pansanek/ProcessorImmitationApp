using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorImmitationApp
{
    internal class Assembler
    {
        public List<uint> Assemble(string[] instructions)
        {
            List<uint> machineCode = new List<uint>();

            foreach (string line in instructions)
            {
                string[] parts = line.Split(' ');
                string cmdType = parts[0].ToUpper();
                uint operand1 = uint.Parse(parts[1]);
                uint operand2 = parts.Length > 2 ? uint.Parse(parts[2]) : 0;

                uint binaryInstruction;
                switch (cmdType)
                {
                    case "LOAD":
                        binaryInstruction = (0u << 8) | (operand1 << 4) | operand2;
                        break;
                    case "STORE":
                        binaryInstruction = (1u << 8) | (operand1 << 4) | operand2;
                        break;
                    case "ADD":
                        binaryInstruction = (2u << 8) | (operand1 << 4) | operand2;
                        break;
                    case "JUMP_IF":
                        binaryInstruction = (3u << 8) | (operand1 << 4) | operand2;
                        break;
                    case "JUMP":
                        binaryInstruction = (4u << 8) | (operand1 << 4);
                        break;
                    case "HALT":
                        binaryInstruction = (5u << 8);
                        break;
                    case "LOAD_SIZE":
                        binaryInstruction = (6u << 8) | (operand1 << 4);
                        break;
                    case "INC":
                        binaryInstruction = (7u << 8) | (operand1 << 4);
                        break;
                    default:
                        throw new InvalidOperationException($"Unknown command: {cmdType}");
                }

                Console.WriteLine($"Добавлена команда {line} в виде {Convert.ToString(binaryInstruction, 2).PadLeft(11, '0')}");
                machineCode.Add(binaryInstruction);
            }
            Console.WriteLine();
            return machineCode;
        }
    }
}
