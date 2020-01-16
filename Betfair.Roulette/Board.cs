using System.Collections.Generic;

namespace Betfair.Roulette
{
    public class Board
    {
        public Dictionary<int, Color> Numbers { get; private set; }

        public Board()
        {
            Numbers = GetNumbers();
        }

        private Dictionary<int, Color> GetNumbers()
        {
            var blackNumbers = new int[] { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };
            var greenNumbers = new int[] { 0 };
            var redNumbers = new int[] { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };

            var result = new Dictionary<int, Color>();
            foreach (var number in blackNumbers)
                result.Add(number, Color.Black);

            foreach (var number in greenNumbers)
                result.Add(number, Color.Green);

            foreach (var number in redNumbers)
                result.Add(number, Color.Red);

            return result;
        }
    }
}
