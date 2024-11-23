using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorImmitationApp
{
    internal class Assembler
    {
        private Dictionary<string, int> labelTable = new Dictionary<string, int>(); // Таблица меток
        private List<Tuple<string, int, int>> unresolvedLabels = new List<Tuple<string, int, int>>(); // Неразрешённые метки


        public List<uint> Assemble(string[] instructions)
        {
            List<uint> machineCode = new List<uint>();
            int commandIndex = 0;

            // Первый проход: анализ меток и команд
            foreach (string line in instructions)
            {
                string trimmedLine = line.Trim();
                if (string.IsNullOrWhiteSpace(trimmedLine)) continue;

                // Лексический анализ: распознавание меток
                if (trimmedLine.EndsWith(":"))
                {
                    string label = trimmedLine.TrimEnd(':');
                    if (labelTable.ContainsKey(label))
                        throw new InvalidOperationException($"Метка {label} уже определена.");
                    labelTable[label] = commandIndex;
                    continue;
                }

                // Синтаксический анализ команды
                string[] parts = trimmedLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string cmdType = parts[0].ToUpper();

                if (!IsValidCommand(cmdType))
                    throw new InvalidOperationException($"Неизвестная команда: {cmdType}");

                uint operand1 = parts.Length > 1 ? ParseOperand(parts[1], commandIndex, 0) : 0;
                uint operand2 = parts.Length > 2 ? ParseOperand(parts[2], commandIndex, 1) : 0;
                Console.WriteLine($"Генерируем  команду из cmdType={cmdType}, operand1={operand1},operand2={operand2}, commandIndex={commandIndex}");
                uint binaryInstruction = GenerateInstruction(cmdType, operand1, operand2);
                machineCode.Add(binaryInstruction);
                commandIndex++;
            }

            // Второй проход: разрешение меток
            ResolveLabels(machineCode);

            return machineCode;
        }

        private bool IsValidCommand(string cmdType)
        {
            bool isValid = false;
            switch (cmdType)
            {
                case "LOAD":
                    isValid = true;
                    break;
                case "STORE":
                    isValid = true;
                    break;
                case "ADD":
                    isValid = true;
                    break;
                case "JUMP_IF":
                    isValid = true;
                    break;
                case "JUMP":
                    isValid = true;
                    break;
                case "HALT":
                    isValid = true;
                    break;
                case "LOAD_SIZE":
                    isValid = true;
                    break;
                case "INC":
                    isValid = true;
                    break;
                
            };
            return isValid;
        }

        private uint GenerateInstruction(string cmdType, uint operand1, uint operand2)
        {
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
            };
            Console.WriteLine($"Добавлена команда {cmdType} в виде {Convert.ToString(binaryInstruction, 2).PadLeft(11, '0')}");
            return binaryInstruction;
        }

        private uint ParseOperand(string operand, int commandIndex, int operandIndex)
        {
            // Если операнд является числом
            if (uint.TryParse(operand, out uint value))
            {
                return value;
            }

            // Если операнд является меткой
            if (labelTable.ContainsKey(operand))
            {
                return (uint)labelTable[operand];
            }

            // Если метка ещё не определена, добавляем в нерешённые метки
            unresolvedLabels.Add(Tuple.Create(operand, commandIndex, operandIndex));
            return 0; // Временно
        }

        private void ResolveLabels(List<uint> machineCode)
        {
            foreach (var unresolved in unresolvedLabels)
            {
                string label = unresolved.Item1;
                int commandIndex = unresolved.Item2;
                int operandIndex = unresolved.Item3;

                if (!labelTable.ContainsKey(label))
                    throw new InvalidOperationException($"Неразрешённая метка: {label}");

                uint resolvedAddress = (uint)labelTable[label];

                // Обновляем нужный операнд
                uint instruction = machineCode[commandIndex];
                if (operandIndex == 0) // Для JUMP
                    machineCode[commandIndex] = (instruction & 0xFF0) | resolvedAddress;
                else if (operandIndex == 1) // Для JUMP_IF
                    machineCode[commandIndex] = (instruction & 0xF0F) | (resolvedAddress << 4);
            }
        }
    }
}

