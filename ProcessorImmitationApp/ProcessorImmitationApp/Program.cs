using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorImmitationApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            // Создание памяти
            Memory memory = new Memory(10);

            // Инициализация данных в памяти
            int[] initialData = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 };  // Последняя ячейка для суммы
            memory.LoadData(initialData);

            List<uint> program = new List<uint>
            {
                0b00_0000_0000,  // LOAD R0, dmem[0] -> Загрузить значение dmem[0] в R0
                0b00_0001_0001,  // LOAD R1, dmem[1] -> Загрузить значение dmem[1] в R1
                0b10_0000_0001,  // ADD R0, R1 -> Сложить R0 и R1, результат в R0

                0b00_0001_0010,  // LOAD R1, dmem[2] -> Загрузить значение dmem[2] в R1
                0b10_0000_0001,  // ADD R0, R1 -> Сложить R0 и R1, результат в R0

                0b00_0001_0011,  // LOAD R1, dmem[3] -> Загрузить значение dmem[3] в R1
                0b10_0000_0001,  // ADD R0, R1 -> Сложить R0 и R1, результат в R0

                0b00_0001_0100,  // LOAD R1, dmem[4] -> Загрузить значение dmem[4] в R1
                0b10_0000_0001,  // ADD R0, R1 -> Сложить R0 и R1, результат в R0

                0b00_0001_0101,  // LOAD R1, dmem[5] -> Загрузить значение dmem[5] в R1
                0b10_0000_0001,  // ADD R0, R1 -> Сложить R0 и R1, результат в R0

                0b00_0001_0110,  // LOAD R1, dmem[6] -> Загрузить значение dmem[6] в R1
                0b10_0000_0001,  // ADD R0, R1 -> Сложить R0 и R1, результат в R0

                0b00_0001_0111,  // LOAD R1, dmem[7] -> Загрузить значение dmem[7] в R1
                0b10_0000_0001,  // ADD R0, R1 -> Сложить R0 и R1, результат в R0

                0b00_0001_1000,  // LOAD R1, dmem[8] -> Загрузить значение dmem[8] в R1
                0b10_0000_0001,  // ADD R0, R1 -> Сложить R0 и R1, результат в R0

                0b01_1001_0000,  // STORE R0, dmem[9] -> Сохранить значение R0 (сумма) в dmem[9]
                0b11_0000_0000   // HALT -> Остановить выполнение программы
            };
            memory.LoadProgram(program);

            // Создание процессора и выполнение программы
            Processor processor = new Processor(memory);
            processor.ExecuteProgram();
            Console.ReadLine();
            // Ожидаемый результат:
            // Значение dmem[9] = 9 (сумма всех элементов массива dmem[0] - dmem[8])
        }
    }
}
