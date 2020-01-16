using System;
using System.Collections.Generic;
using System.Linq;

namespace Betfair.Roulette
{
    public class Play
    {
        private Wallet Wallet;
        private Board Board;
        private readonly double InitialValue;
        private List<int> drawnNumbers;
        public static double MaxValue;
        public static int MaxSequence;

        public Play(double walletBalance, double wallterLimit, double initialValue)
        {
            Wallet = new Wallet(walletBalance, wallterLimit);
            Board = new Board();
            InitialValue = initialValue;
            drawnNumbers = new List<int>();
        }

        public Wallet Run(Color initialColor, int betAfterXNumbers)
        {
            MaxValue = 0;
            MaxSequence = 0;
            var betHistory = new List<double>();
            Color colorToBet = Color.Green;
            var play = false;
            var clicks = 1;
            var bet = new Bet();
            bet.SetBet(InitialValue, initialColor);

            var drawnNumber = new Random();
            var maxSequence = 0;

            for (int i = 0; i < 10000; i++)
            {
                betHistory.Add(bet.Value);
                var number = drawnNumber.Next(0, 37);
                drawnNumbers.Add(number);

                if (play)
                {
                    play = false;
                    if (bet.Value > MaxValue)
                        MaxValue = bet.Value;

                    var result = VerifiyNumberWithBet(number, bet);
                    UpdateWallet(result, bet);
                    if (result)
                    {
                        maxSequence = 0;
                        clicks = 1;
                        bet.SetBet(InitialValue, bet.Color);
                        continue;
                    }

                    clicks = 1;
                }
                else
                {
                    List<int> lastNumbers = drawnNumbers.TakeLast(betAfterXNumbers).ToList();

                    if (lastNumbers.Count() < betAfterXNumbers)
                        continue;

                    var colors = LastColorsAreTheSame(lastNumbers);
                    if (colors.Count() > 1)
                        continue;

                    colorToBet = colors.FirstOrDefault() == Color.Black ? Color.Red : Color.Black;
                }

                maxSequence++;

                if (maxSequence > MaxSequence)
                    MaxSequence = maxSequence;

                play = true;
                bet.SetBet(bet.Value * clicks, colorToBet);

                //var result = VerifiyNumberWithBet(number, bet);
                //UpdateWallet(result, bet);

                //if (!result)
                //{
                //    var nextColor = new Random();
                //    var next = nextColor.Next(1, 2);
                //    bet.SetBet(bet.Value * 2, next == 1 ? Color.Red : Color.Black);
                //    if (Wallet.ExceededLimit(bet.Value))
                //        bet.SetBet(InitialValue, bet.Color);
                //}
                //else
                //    bet.SetBet(InitialValue, bet.Color);

                //if (!Wallet.HasBalanceToBet(bet.Value))
                //    break;
            }

            return Wallet;
        }

        private List<Color> LastColorsAreTheSame(List<int> lastNumbers)
        {
            var colors = new List<Color>();
            foreach (var number in lastNumbers)
                colors.Add(Board.Numbers.FirstOrDefault(p => p.Key == number).Value);

            var colorsDistinct = colors.Distinct().ToList();

            return colorsDistinct;
        }

        private void UpdateWallet(bool result, Bet bet)
        {
            if (result)
                Wallet.Update(bet.Value);
            else
                Wallet.Update(bet.Value * -1);
        }

        private bool VerifiyNumberWithBet(int number, Bet bet)
        {
            var numberColor = Board.Numbers.FirstOrDefault(p => p.Key == number).Value;

            return bet.Color == numberColor;
        }
    }
}
