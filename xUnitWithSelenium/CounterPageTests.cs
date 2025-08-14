using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace xUnitWithSelenium
{
    public class CounterPageTests
    {

        [Fact]
        public void Counter_ShouldExistsElement()
        {
            using var driver = new ChromeDriver();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("https://localhost:7154/");

            try
            {
                // Click the "Counter" menu link safely
                wait.Until(d =>
                {
                    try
                    {
                        var el = d.FindElement(By.CssSelector("nav div:nth-child(2) > a"));
                        el.Click();
                        return true;
                    }
                    catch (StaleElementReferenceException) { return false; }
                    catch (NoSuchElementException) { return false; }
                });

                // Wait for heading to appear and assert
                wait.Until(d =>
                {
                    try
                    {
                        var heading = d.FindElement(By.CssSelector("main article h1"));
                        return heading.Text.Trim() == "Counter" ? heading : null;
                    }
                    catch (StaleElementReferenceException) { return null; }
                    catch (NoSuchElementException) { return null; }
                });

                // Click the "Click me" button safely
                wait.Until(d =>
                {
                    try
                    {
                        var button = d.FindElement(By.CssSelector("main article button"));
                        button.Click();
                        return true;
                    }
                    catch (StaleElementReferenceException) { return false; }
                    catch (NoSuchElementException) { return false; }
                });

                // Wait for counter text to update and assert
                var counterText = wait.Until(d =>
                {
                    try
                    {
                        var el = d.FindElement(By.CssSelector("main article p"));
                        return el.Text.Trim() == "Current count: 1" ? el : null;
                    }
                    catch (StaleElementReferenceException) { return null; }
                    catch (NoSuchElementException) { return null; }
                });

                Assert.Equal("Current count: 1", counterText.Text.Trim());
            }
            catch (Exception ex)
            {
                Assert.False(true, $"Test failed due to exception: {ex.Message}");
            }
        }


    }
}