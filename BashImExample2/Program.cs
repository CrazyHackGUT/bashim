using System;
using BashIm;

namespace BashImExample2
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Введите номер цитаты, чтобы загрузить её: ");
                var quoteIdStr = Console.ReadLine();
                int quoteId;
                if (!int.TryParse(quoteIdStr, out quoteId))
                {
                    Console.WriteLine("Не удаётся распознать номер.");
                    continue;
                }

                var quote = Quote.GetQuoteById(quoteId);
                if (quote == null)
                {
                    Console.WriteLine("Не удалось найти цитату.");
                    continue;
                }

                Console.WriteLine($"#{quote.Id}");
                Console.WriteLine($"Рейтинг {quote.Rating}");
                Console.WriteLine($"Создана {quote.CreatedAt}");
                Console.WriteLine(" ");
                Console.WriteLine(quote.Text);
            }
        }
    }
}