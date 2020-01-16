namespace Betfair.Roulette
{
    public class Bet
    {
        public double Value { get; private set; }
        public Color Color { get; private set; }

        public void SetBet(double value, Color color)
        {
            Value = value;
            Color = color;
        }
    }
}
