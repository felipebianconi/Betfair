using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Linq;

namespace Betfair.Selenium
{
    public class Play
    {
        private readonly IWebDriver webDriver;
        private Dictionary<int, Color> numbersColors;

        public Play(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            numbersColors = GetNumbersColors();
        }

        private Dictionary<int, Color> GetNumbersColors()
        {
            numbersColors = new Dictionary<int, Color>();

            var blackNumbers = new int[] { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };
            AddNumbersColors(blackNumbers, Color.Black, ref numbersColors);

            var greenNumbers = new int[] { 0 };
            AddNumbersColors(greenNumbers, Color.Green, ref numbersColors);

            var redNumbers = new int[] { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
            AddNumbersColors(redNumbers, Color.Red, ref numbersColors);

            return numbersColors;
        }

        private void AddNumbersColors(int[] numbers, Color color, ref Dictionary<int, Color> numbersColors)
        {
            foreach (var number in numbers)
                numbersColors.Add(number, color);
        }

        public void Run()
        {
            var win = true;
            var clicksDouble = 0;
            var currentBalance = GetBalance(); //100
            var balanceBeforeBet = currentBalance; //100

            try
            {


                while (currentBalance >= 70 || currentBalance <= 150)
                {
                    if (win)
                        clicksDouble = 0;
                    else
                        clicksDouble++;

                    var isPossibleToBet = false;
                    while (!isPossibleToBet)
                    {
                        isPossibleToBet = IsPossibleToBet(Message.PlaceYourBets);
                    }

                    var lastNumber = GetLastNumber();
                    while (lastNumber == 0)
                    {
                        lastNumber = GetLastNumber();
                    }

                    Color lastNumberColor = GetLastNumberColor(lastNumber);

                    IWebElement betElement = GetNextBet(lastNumberColor);
                    betElement.Click();

                    IWebElement doubleButton = GetDoubleButton();
                    for (int i = 0; i < clicksDouble; i++)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        doubleButton.Click();
                    }

                    var noMoreBets = false;
                    while (!noMoreBets)
                    {
                        noMoreBets = IsPossibleToBet(Message.NoMoreBets);
                    }

                    isPossibleToBet = false;
                    while (!isPossibleToBet)
                    {
                        isPossibleToBet = IsPossibleToBet(Message.PlaceYourBets);
                    }

                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    currentBalance = GetBalance();
                    win = currentBalance > balanceBeforeBet;
                    balanceBeforeBet = currentBalance;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private IWebElement GetDoubleButton()
        {
            return webDriver.FindElement(By.CssSelector(".action-button_double > .action-button__icon"));
        }

        private IWebElement GetNextBet(Color lastNumberColor)
        {
            if (lastNumberColor == Color.Black)
                return webDriver.FindElement(By.CssSelector(".rol-interaction-layer__cell_side-black"));

            return webDriver.FindElement(By.CssSelector(".rol-interaction-layer__cell_side-red"));
        }

        private Color GetLastNumberColor(int lastNumber)
        {
            var color = numbersColors.FirstOrDefault(p => p.Key == lastNumber).Value;

            return color;
        }

        private int GetLastNumber()
        {
            IWebElement lastNumberElement = webDriver.FindElement(By.CssSelector(".roulette-history-line-item:nth-child(1) .regular-item-value"));
            var lastNumber = int.Parse(lastNumberElement.Text);

            return lastNumber;
        }

        private bool IsPossibleToBet(string message)
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));
            var messageElement = webDriver.FindElement(By.CssSelector(".dealer-message-text"));

            return messageElement.Text.ToUpper() == message;
        }

        private double GetBalance()
        {
            var balanceElement = webDriver.FindElement(By.CssSelector(".balance__value .fit-container__content"));
            var balanceText = balanceElement.Text.Split(" ")[1];

            var balance = double.Parse(balanceText, CultureInfo.GetCultureInfo("en-GB"));

            return balance;
        }
    }
}
