using System.Collections.Generic;

namespace Betfair.Roulette
{
    class Program
    {
        private static List<double> MaxValues;
        private static List<int> MaxSequences;

        static void Main(string[] args)
        {
            MaxValues = new List<double>();
            MaxSequences = new List<int>();

            var balances = new List<double>
            {
                Test(20, 4),
                Test(20, 5),
                Test(20, 6),

                Test(100, 4),
                Test(100, 5),
                Test(100, 6),

                Test(1000, 4),
                Test(1000, 5),
                Test(1000, 6),
            };
        }

        private static double Test(double startBalance, int betAfterXNumbers)
        {
            var play = new Play(startBalance, 32, 0.50);
            var wallet = play.Run(Color.Black, betAfterXNumbers);

            MaxValues.Add(Play.MaxValue);
            MaxSequences.Add(Play.MaxSequence);

            return wallet.Balance;
        }
    }
}
