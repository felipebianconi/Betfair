namespace Betfair.Selenium
{
    public class Balance
    {
        public double Value { get; private set; }
        public double Limit { get; private set; }

        public Balance(double balance, double limit)
        {
            Value = balance;
            Limit = limit;
        }

        public void Update(double value)
        {
            Value += value;
        }

        public bool ExceededLimit(double value)
        {
            return value > Limit;
        }

        public bool HasBalanceToBet(double value)
        {
            return Value >= value;
        }
    }
}
