using System;
using System.IO;
using System.Xml.Xsl;

namespace EdoConnectorMock
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Эмулятор коннектора ЭДО ===");

            string inputXmlPath = "input.xml";
            string xsltPath = "transform.xslt";
            string outputXmlPath = "output.xml";

            if (!File.Exists(inputXmlPath) || !File.Exists(xsltPath))
            {
                Console.WriteLine("Ошибка: Не найдены входные файлы XML или XSLT.");
                Console.ReadLine();
                return;
            }

            try
            {
                // Загружаем XSLT-трансформацию
                XslCompiledTransform transform = new XslCompiledTransform();
                transform.Load(xsltPath);

                Console.WriteLine("Трансформация загружена успешно.");
                Console.WriteLine("Чтение данных ERP клиента...");

                // Выполняем преобразование
                transform.Transform(inputXmlPath, outputXmlPath);

                Console.WriteLine("Успех! Данные преобразованы в формат ГИС ЭПД.");
                Console.WriteLine($"Файл сохранен как: {outputXmlPath}");

                // Читаем и выводим результат в консоль для наглядности
                string result = File.ReadAllText(outputXmlPath);
                Console.WriteLine("\n--- Результат преобразования ---\n");
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nПроизошла ошибка при обработке: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}