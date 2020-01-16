using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace Betfair.Selenium
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver(@"C:\Temp"); //driver chrome

            try
            {
                driver.Url = "https://casino.betfair.com/c/live-roulette";

                IWebElement username = driver.FindElement(By.Id("ssc-liu"));
                username.SendKeys(""); //username

                IWebElement password = driver.FindElement(By.Id("ssc-lipw"));
                password.SendKeys(""); //password

                IWebElement logIn = driver.FindElement(By.Id("ssc-lis"));
                logIn.Click();

                IWebElement casino = driver.FindElement(By.CssSelector(".grid-2 > .tile-container:nth-child(1) .tile > .button"));
                casino.Click();

                driver.SwitchTo().Window(driver.WindowHandles[1]);

                Thread.Sleep(TimeSpan.FromSeconds(15));

                IWebElement casinoOk = driver.FindElement(By.CssSelector(".modal-footer-btn"));
                casinoOk.Click();

                IWebElement casinoBoard = driver.FindElement(By.CssSelector(".lobby-tables__item:nth-child(3) .lobby-table__container"));
                casinoBoard.Click();

                Thread.Sleep(TimeSpan.FromSeconds(7));

                var play = new Play(driver);
                play.Run();

                //IWebElement balanceElement = driver.FindElement(By.CssSelector(".balance__value .fit-container__content"));
                //var balanceText = balanceElement.Text.Split(" ")[1];

                //double balance = double.Parse(balanceText, CultureInfo.GetCultureInfo("en-GB"));
                //var walltet = new Balance(balance, 32);

                //IWebElement redElement = driver.FindElement(By.CssSelector(".rol-interaction-layer__cell_side-red"));
                //redElement.Click();

                //Thread.Sleep(TimeSpan.FromSeconds(1));

                //IWebElement undoElement = driver.FindElement(By.CssSelector(".action-button_undo > .action-button__icon"));
                //undoElement.Click();

                //var numbers = new List<int>();

                //IWebElement lastNumberElement = driver.FindElement(By.CssSelector(".roulette-history-line-item:nth-child(1) .regular-item-value"));
                //var number = lastNumberElement.Text;
                //numbers.Add(int.Parse(number));

                //while (number == lastNumberElement.Text)
                //{
                //    Thread.Sleep(TimeSpan.FromSeconds(1));
                //    lastNumberElement = driver.FindElement(By.CssSelector(".roulette-history-line-item:nth-child(1) .regular-item-value"));
                //}

                //number = lastNumberElement.Text;
                //numbers.Add(int.Parse(number));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                driver.Close();
            }
        }
    }
}
