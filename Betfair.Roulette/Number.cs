using System.Collections.Generic;

namespace Betfair.Roulette
{
    public class Number
    {
        public static List<int> Numbers
        {
            get
            {
                var numbers = new List<int>();

                for (int i = 0; i <= 36; i++)
                    numbers.Add(i);

                return numbers;
            }
        }
    }
}
