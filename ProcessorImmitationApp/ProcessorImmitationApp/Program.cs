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
            Memory memory = new Memory(20);

            // Инициализация данных в памяти 
            int[] initialData = { 15, 1, 2, 3, 4, 5, 6, 7, 8, 9,10,10,10, 10, 10, 10 };  
            
            memory.LoadData(initialData);

            List<uint> program = new List<uint>
            {
                0b000_0001_0000u,  // LOAD R0, dmem[1]    (Загрузить первый элемент в R0)
                0b111_0010_0000u,  // INC R2              (Инициализировать счётчик позиций R2 на 1)
                0b110_0011_0000u,  // LOAD_SIZE R3        (Загрузить размер массива в R3)
    
                // Метка начала цикла
                0b000_0001_0010u,  // LOAD R1, dmem[R2]   (Загрузить текущий элемент массива в R1)
                0b010_0000_0001u,  // ADD R0, R1          (Добавить элемент к сумме)
                0b111_0010_0000u,  // INC R2              (Увеличить счётчик позиций R2 на 1)
    
                // Проверка завершения цикла
                0b011_0010_0111u,  // JUMP_IF R2 == R3    (Если R2 == R3, переход к завершению)
                0b100_0010_0000u,  // JUMP START          (Переход к началу цикла для следующего элемента)
    
                // Метка завершения программы
                0b001_1000_0000u,  // STORE R0, dmem[9]   (Сохранить сумму в dmem[9])
                0b101_0000_0000u   // HALT                (Завершить выполнение программы)
            };
            memory.LoadProgram(program);

            // Создание процессора и выполнение программы
            Processor processor = new Processor(memory);
            processor.ExecuteProgram();
            Console.ReadLine();
        }
    }
}
