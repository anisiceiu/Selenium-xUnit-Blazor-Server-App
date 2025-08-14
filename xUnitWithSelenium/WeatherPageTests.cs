using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Xml.Linq;

namespace xUnitWithSelenium
{
    public class WeatherPageTests
    {

        [Fact]
        public void Weather_ShouldExistsElement()
        {
            using var driver = new ChromeDriver();
            WebDriverWait _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("https://localhost:7154/");
            try
            {

               
                // Click the "Weather" menu link
                var counterLink = _wait.Until(d => d.FindElement(By.CssSelector("nav div:nth-child(3) > a")));
                counterLink.Click();

                // Wait until Counter page heading appears and assert
                var tableRowOne = _wait.Until(d => d.FindElement(By.CssSelector("main > article > table > tbody > tr:nth-child(1)")));

                Assert.NotNull(tableRowOne);
                Assert.True(tableRowOne.Displayed, "The element exists but is not visible.");

            }
            catch (NoSuchElementException ex)
            {
                Assert.False(true, $"Element does not exist: {ex.Message}");
            }
            catch (WebDriverTimeoutException ex)
            {
                Assert.False(true, $"Element did not load in expected time: {ex.Message}");
            }
        }
    }
}