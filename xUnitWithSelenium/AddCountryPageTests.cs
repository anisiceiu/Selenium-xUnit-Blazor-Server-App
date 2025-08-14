using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace xUnitWithSelenium
{
    public class AddCountryPageTests
    {

        [Fact]
        public void AddCountry_ShouldShowSuccessMessage()
        {
            using var driver = new ChromeDriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("https://localhost:7154/");

            // Click the "Add Country" menu link
            wait.Until(d => d.FindElement(By.CssSelector("body > div.page > div > div.nav-scrollable > nav > div:nth-child(4) > a"))).Click();// Change port as per your app

            // Wait for the form to load
            wait.Until(d=>d.FindElement(By.CssSelector("body > div.page > main > article > form > button")));

            // Fill in Continent
            var continentInput = driver.FindElement(By.CssSelector("body > div.page > main > article > form > div:nth-child(2) > input"));
            continentInput.Clear();
            continentInput.SendKeys("Asia");

            // Fill in Country Name
            var countryInput = driver.FindElement(By.CssSelector("body > div.page > main > article > form > div:nth-child(3) > input"));
            countryInput.Clear();
            countryInput.SendKeys("Bangladesh");

            // Submit the form
            var submitButton = driver.FindElement(By.CssSelector("button[type='submit']"));
            submitButton.Click();

            // Wait for the success message
            var successMessage = wait.Until(
                d=> d.FindElement(By.XPath("//p[contains(text(),'Country Added Successfully!')]"))
            );

            Assert.Equal("Country Added Successfully!", successMessage.Text);
        }
    }
}