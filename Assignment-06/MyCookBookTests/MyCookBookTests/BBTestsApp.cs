using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Xunit;
using System.Threading;

public class BBTestsApp
{
    [Fact]
    public void TestHomePageTitle()
    {
        // Arrange
        IWebDriver driver = new ChromeDriver();
        driver.Navigate().GoToUrl("http://localhost:5282");

        // Wait for the page to fully load
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

        // Check if specific elements are rendered using their full XPath
        try
        {
            IWebElement Header = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/header")));
            IWebElement TopBar = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/header/nav")));
            IWebElement MainSuggestions = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/main/div[2]")));
            IWebElement DietaryOptions = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/main/div[3]")));
            IWebElement RecipeList = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/main/div[4]")));
            IWebElement Footer = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/footer")));

            // Assert that the elements are not null (indicating they were found and visible)
            Assert.NotNull(Header);
            Assert.NotNull(TopBar);
            Assert.NotNull(MainSuggestions);
            Assert.NotNull(DietaryOptions);
            Assert.NotNull(RecipeList);
            Assert.NotNull(Footer);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Fail("One or more elements were not rendered within the timeout period.");
        }

        driver.Navigate().GoToUrl("http://localhost:5282/recipes");

        // Wait for the new page to fully load
        wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

        // Check if specific elements are rendered using their full XPath
        try
        {
            IWebElement Header = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/header")));
            IWebElement TopBar = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/header/nav")));
            IWebElement DietaryOptions = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/main/div[1]")));
            IWebElement RecipeList = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/main/div[2]/div")));
            IWebElement Footer = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/footer")));

            // Assert that the elements are not null (indicating they were found and visible)
            Assert.NotNull(Header);
            Assert.NotNull(TopBar);
            Assert.NotNull(DietaryOptions);
            Assert.NotNull(RecipeList);
            Assert.NotNull(Footer);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Fail("One or more elements were not rendered within the timeout period.");
        }

        driver.Navigate().GoToUrl("http://localhost:5282/about");

        // Wait for the new page to fully load
        wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

        try {
            IWebElement Header = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/header")));
            IWebElement TopBar = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/header/nav")));
            IWebElement AboutContent = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/main/div")));
            IWebElement Footer = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/footer")));

            // Assert that the elements are not null (indicating they were found and visible)
            Assert.NotNull(Header);
            Assert.NotNull(TopBar);
            Assert.NotNull(AboutContent);
            Assert.NotNull(Footer);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Fail("One or more elements were not rendered within the timeout period.");
        }

        // Cleanup
        driver.Quit();
    }
}