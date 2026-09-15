using System;
using System.IO;
using System.Xml.Xsl;
using System.Text.RegularExpressions;

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
                // --- БЛОК ВАЛИДАЦИИ REGEX ---
                string inputData = File.ReadAllText(inputXmlPath);

                // Ищем тег TaxID и захватываем то, что внутри него
                Match match = Regex.Match(inputData, @"<TaxID>(.*?)</TaxID>");
                if (match.Success)
                {
                    string inn = match.Groups[1].Value;
                    // Регулярное выражение: строго 10 или 12 цифр (стандарт ИНН в РФ)
                    if (Regex.IsMatch(inn, @"^(\d{10}|\d{12})$"))
                    {
                        Console.WriteLine($"[Regex] ИНН {inn} валиден.");
                    }
                    else
                    {
                        Console.WriteLine($"[Regex Ошибка] ИНН '{inn}' имеет неверный формат!");
                    }
                }
                // -----------------------------

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