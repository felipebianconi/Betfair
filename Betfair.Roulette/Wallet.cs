namespace Betfair.Roulette
{
    public class Wallet
    {
        public double Balance { get; private set; }
        public double Limit { get; private set; }

        public Wallet(double balance, double limit)
        {
            Balance = balance;
            Limit = limit;
        }

        public void Update(double value)
        {
            Balance += value;
        }

        public bool ExceededLimit(double value)
        {
            return value > Limit;
        }

        public bool HasBalanceToBet(double value)
        {
            return Balance >= value;
        }
    }
}
