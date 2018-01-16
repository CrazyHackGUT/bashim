using System;
using BashIm;

namespace BashImExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Цитаты с Bash.Im");
            Console.WriteLine("Секундочку... Загружаемся...");
            var Loader = new BashIm.RandomLoader();
            var Quotes = Loader.GetQuotes();

            var CurrentQuote = 0;
            var QuotesCount = Quotes.Count;

            foreach (var Quote in Quotes)
            {
                CurrentQuote++;
                
                Console.Clear();
                Console.WriteLine($"#{Quote.Id}");
                Console.WriteLine($"Рейтинг {Quote.Rating}");
                Console.WriteLine($"Создана {Quote.CreatedAt}");
                Console.WriteLine(" ");
                Console.WriteLine(Quote.Text);
                Console.WriteLine(" ");
                Console.WriteLine($"{CurrentQuote} из {QuotesCount}");
                Console.WriteLine("Чтобы продолжить просмотр цитат, нажмите Enter.");
                while (Console.ReadKey().Key != ConsoleKey.Enter)
                {}
            }
        }
    }
}