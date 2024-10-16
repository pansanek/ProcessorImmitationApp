using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorImmitationApp
{
    internal class Processor
    {
        private int[] reg = new int[8];  // 8 регистров общего назначения
        private int pc = 0;  // Счетчик команд (Program Counter)

        private const uint LOAD = 0;     // 000
        private const uint STORE = 1;    // 001
        private const uint ADD = 2;      // 010
        private const uint JUMP_IF = 3;  // 011
        private const uint JUMP = 4;     // 100
        private const uint HALT = 5;     // 101

        private Memory memory;

        public Processor(Memory mem)
        {
            memory = mem;
        }

        public void ExecuteProgram()
        {
            bool running = true;
            while (running)
            {
                Instruction instruction = memory.FetchInstruction(pc);  // Извлечение команды
                running = DecodeAndExecute(instruction);  // Декодирование и выполнение команды
                pc++;  // Переход к следующей команде
                Console.WriteLine();
            }
        }

        private bool DecodeAndExecute(Instruction instruction)
        {
            uint cmdType = instruction.CmdType;
            uint op1 = instruction.Operand1;
            uint op2 = instruction.Operand2;
            
            switch (cmdType)
            {
                case LOAD:
                    reg[op1] = memory.DataMemory[op2];  // Загрузка данных из памяти в регистр
                    break;
                case STORE:
                    memory.DataMemory[op1] = reg[op2];  // Сохранение данных из регистра в память
                    break;
                case ADD:
                    reg[op1] = reg[op1] + reg[op2];  // Сложение данных двух регистров
                    break;
                case HALT:
                    Console.WriteLine("Остановка программы");
                    return false;
                case JUMP:
                    pc = (int)op1;  // Переход к указанному адресу
                    break;
                case JUMP_IF:
                    // Условный прыжок: если op1 == op2, перейти на адрес в op1
                    if (reg[op1] == reg[op2])
                    {
                        pc = (int)op1;
                        return true;  // Обновлённый PC
                    }
                    break;
                default:
                    Console.WriteLine($"Неизвестная команда: {cmdType}");
                    break;
            }

            // Вывод текущего состояния процессора
            Console.WriteLine($"PC: {pc}");
            Console.WriteLine("Регистры: " + string.Join(", ", reg));
            Console.WriteLine("Память данных: " + string.Join(", ", memory.DataMemory));

            return true;
        }
    }
}

