using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitWithSelenium
{
    public class CountryListPageTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public CountryListPageTests()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
        }

        [Fact]
        public void CountryList_EditCountry_UpdatesSuccessfully()
        {
            // Navigate to Country List page
            _driver.Navigate().GoToUrl("https://localhost:7154/country-list");

            // Wait for the table to appear
            _wait.Until(d => d.FindElement(By.CssSelector("table.table")));

            // Click the Edit button on the first row (always query fresh)
            _wait.Until(d =>
            {
                try
                {
                    var firstRow = d.FindElements(By.CssSelector("table.table tbody tr"))[0];
                    var editButton = firstRow.FindElement(By.CssSelector("button"));
                    editButton.Click();
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return false; // retry
                }
                catch (IndexOutOfRangeException)
                {
                    return false; // retry if rows not loaded yet
                }
            });

            // Wait for the edit form to appear
            var continentInput = _wait.Until(d => d.FindElement(By.CssSelector("body > div.page > main > article > form > div:nth-child(1) > input")));
            var countryNameInput = _driver.FindElement(By.CssSelector("body > div.page > main > article > form > div:nth-child(2) > input"));
            var saveButton = _driver.FindElement(By.CssSelector("body > div.page > main > article > form > button:nth-child(3)"));

            // Modify values
            continentInput.Clear();
            continentInput.SendKeys("UpdatedContinent");

            countryNameInput.Clear();
            countryNameInput.SendKeys("UpdatedCountry");

            // Submit the form
            saveButton.Click();

            // Wait for table to reload and verify updated values
            var updatedRow = _wait.Until(d =>
            {
                var rows = d.FindElements(By.CssSelector("table.table tbody tr"));
                if (rows.Count == 0) return null;

                var first = rows[0];
                var cells = first.FindElements(By.CssSelector("td"));
                if (cells.Count >= 2 &&
                    cells[0].Text == "UpdatedContinent" &&
                    cells[1].Text == "UpdatedCountry")
                {
                    return first;
                }

                return null; // keep waiting
            });

            Assert.NotNull(updatedRow);
        }

        [Fact]
        public void CountryList_DeleteCountry_Successfully()
        {
            // Navigate to Country List page
            _driver.Navigate().GoToUrl("https://localhost:7154/country-list");

            // Wait for table to be displayed
            _wait.Until(d => d.FindElement(By.CssSelector("table.table")));

            // Get initial row count dynamically
            var initialRows = _wait.Until(d =>
            {
                var rows = d.FindElements(By.CssSelector("table.table tbody tr"));
                return rows.Count > 0 ? rows : null;
            });

            Assert.True(initialRows.Count > 0, "No countries found in the table.");

            // Click Delete on the first row (re-query each time)
            _wait.Until(d =>
            {
                try
                {
                    var firstRow = d.FindElements(By.CssSelector("table.table tbody tr"))[0];
                    var deleteButton = firstRow.FindElements(By.CssSelector("button"))[1];
                    deleteButton.Click();
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return false; // retry
                }
                catch (IndexOutOfRangeException)
                {
                    return false; // retry if no rows yet
                }
            });

            // Wait until the table updates (row count decreases)
            _wait.Until(d =>
            {
                var rowsAfterDelete = d.FindElements(By.CssSelector("table.table tbody tr"));
                return rowsAfterDelete.Count == initialRows.Count - 1;
            });

            // Verify final row count
            var finalRows = _driver.FindElements(By.CssSelector("table.table tbody tr"));
            Assert.Equal(initialRows.Count - 1, finalRows.Count);
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}
