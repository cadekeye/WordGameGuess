
using ApiServices;
using ApiServices.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;

using System.Threading.Tasks;
using System.Linq;

namespace WordGameApp
{
    class Program
    {
        private static IWordApiService _wordApiService;
        
        public Program()
        {
        }
        private static string RandomDisplay = Helper.HelperService.GetRandomLetters(12);

        private static int Count = 0;

        static void Main(string[] args)
        {

            var services = new ServiceCollection();

            ConfigureServices(services);

            _wordApiService = services.BuildServiceProvider()
                .GetService<IWordApiService>();


            bool isGuessValid = false;
            var projectedTime = DateTime.Now.AddSeconds(90);

            Timer timer = new Timer(TimerCallBack, null, 0, 90000);
            

            while(DateTime.Now < projectedTime && !isGuessValid)
            {
                StartGuessGame(out isGuessValid);
            }

            Console.WriteLine("End of Game");
            
            Console.ReadKey();
        }

       private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IWordApiService, WordApiService>();
        }

        public static bool StartGuessGame(out bool isGuessValid)
        {

            Console.WriteLine("         *******************************************************************");
            Console.WriteLine("         **********Please use below letters to form a valid word ***********");
            Console.WriteLine("         *******************************************************************");
            Console.WriteLine($"        ***********      {RandomDisplay}   *******************");
            Console.WriteLine("         *******************************************************************");

            Console.WriteLine("");
            Console.WriteLine("");
            Console.Write("         Enter Valid word using the above letters: ");

            var guessWord = Console.ReadLine();



            if(EntryNotValid(RandomDisplay, guessWord))
            {
                Console.Clear();
                Console.WriteLine($"The word guess was not from the displayed letters {RandomDisplay}");

                Console.WriteLine("");
                Console.WriteLine("");

                StartGuessGame(out isGuessValid);
            }

            if (string.IsNullOrEmpty(guessWord))
            {
                isGuessValid = false;
                return isGuessValid;
            }

            var isValid = IsGuessWordValid(guessWord).GetAwaiter().GetResult();

            isGuessValid = isValid;


            if (!isValid)
            {
                Console.Clear();
                Console.WriteLine($"Guess word {guessWord} not a valid word. Please Try again");

                Console.WriteLine("");
                Console.WriteLine("");

                StartGuessGame(out isGuessValid);

            }
            else
            {
                Console.Clear();
                Console.WriteLine($"You Win!.");
                isValid = true;
                isGuessValid = isValid;
            }

            return isValid;
        }

       
        private static bool EntryNotValid(string randomDisplay, string guessWord)
        {
          return guessWord.ToLower().Any(c => !randomDisplay.ToLower().Contains(c));
        }

        private static async Task<bool> IsGuessWordValid(string guessWord)
        {
            var response = await _wordApiService.GetValidWord(guessWord);

            return response.Valid;
        }

        private static void TimerCallBack(object sender)
        {
            if (Count > 0)
            {
                Console.Clear();
                Console.WriteLine("out of the allocated time!!!");
                Console.WriteLine("Game Over!!!");
            }

            Count++;
        }
    }

   
}
