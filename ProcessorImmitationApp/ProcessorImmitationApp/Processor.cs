using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorImmitationApp
{
    internal class Processor
    {
        private int[] reg = new int[4];  // 4 регистров общего назначения
        private int pc = 0;  // Счетчик команд (Program Counter)

        private const uint LOAD = 0;     // 000
        private const uint STORE = 1;    // 001
        private const uint ADD = 2;      // 010
        private const uint JUMP_IF = 3;  // 011
        private const uint JUMP = 4;     // 100
        private const uint HALT = 5;     // 101
        private const uint LOAD_SIZE = 6;// 110
        private const uint INC = 7;// 111

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
                    reg[op1] = memory.DataMemory[(int)reg[2]];  // Загрузка данных из памяти в регистр
                    Console.WriteLine($"Загрузка в {reg[op1]} значение {memory.DataMemory[(int)reg[2]]}");
                    break;
                case STORE:
                    Console.WriteLine($"Значение из R{op2} ({reg[op2]}) записано в последний элемент памяти.");
                    memory.DataMemory[memory.DataMemory[0]+1] = reg[op2];  // Сохранение данных из регистра в память
                    break;
                case ADD:
                    Console.WriteLine($"Сложение R{op2}({reg[op2]}) + R{op1}({reg[op1]}).");
                    reg[op1] = reg[op1] + reg[op2];  // Сложение данных двух регистров
                    break;
                case HALT:
                    Console.WriteLine("Остановка программы");
                    return false;
                case JUMP:
                    Console.WriteLine($"Переход");
                    pc = (int)op1-1;  // Переход к указанному адресу
                    break;
                case JUMP_IF:
                    Console.WriteLine($"Проверка условия совпадения {reg[2]} и {reg[3]}");
                    
                        // Условный прыжок: если R2 == R3, перейти к завершению
                        if (reg[2] == reg[3])
                        {
                            pc = (int)instruction.Operand1-1;  // Переход к метке завершения
                            return true;  // Не увеличивать PC, т.к. прыжок
                        }
                    
              
                    break;
                case INC:
                    if (op1 < reg.Length)
                    {
                        reg[op1] += 1;  // Увеличение значения регистра на 1
                        Console.WriteLine($"Увеличен регистр R{op1} до {reg[op1]}");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: Индекс выходит за пределы при INC.");
                    }
                    break;
                case LOAD_SIZE:
                    if (op1 < reg.Length)
                    {
                        reg[op1] = memory.DataMemory[0]+1;  // Автоматически загружаем размер массива в регистр
                        Console.WriteLine($"Загружен размер массива: {reg[op1]} в R{op1}");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: Индекс выходит за пределы регистров при LOAD_SIZE.");
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

