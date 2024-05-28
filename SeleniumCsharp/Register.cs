using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;
using System;
using SeleniumCsharp;
using OpenQA.Selenium.Remote;
using NUnit.Framework.Legacy;
using System.Xml.Linq;

public class Register
{
    public IWebDriver driver = new ChromeDriver();
    string email;

    private string firstName = "Neha" + Utility.GenerateTime();
    private string lastName = "Katte" + Utility.GenerateTime();
    private string newPassword = Utility.GenerateRandomEmail(4);


    [SetUp]
    public void Setup()

    {
        //go to site
        driver.Navigate().GoToUrl("https://login.onefile.co.uk/login");
        driver.Manage().Window.Maximize();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement button = wait.Until(ExpectedConditions
            .ElementExists(By.XPath("//app-landing/div[2]/button/span[2]")));
        button.Click();
    }

    [Test]
    public void verifyRegister()

    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        try
        {
            // Wait for the Register page to be visible and then enter the start entering details
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@id='fistName']"))).SendKeys(firstName);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@id='lastName']"))).SendKeys(lastName);


            IWebElement email1 = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@id='email']")));
            email1.SendKeys(Utility.GenerateRandomEmail(10));

            driver.FindElement(By.XPath("//input[@id='newPassword']")).SendKeys(newPassword);
            driver.FindElement(By.XPath("//input[@id='confirmPassword']")).SendKeys(newPassword);

            Thread.Sleep(1000);
            IWebElement checkbox = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='rapid-checkbox__checkbox']")));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", checkbox);
            Actions actions = new Actions(driver);
            actions.Click(checkbox).Perform();
            Thread.Sleep(1000);

            IWebElement register = driver.FindElement(By.XPath("//button[text()='Register']"));
            actions.Click(register).Perform();

            Thread.Sleep(1000);

            IWebElement Logged = driver.FindElement(By.XPath("//h4[text()='Thank you for registering!']"));
            Assert.Pass(Logged.Text);

        }
        catch (NoSuchElementException e)
        {
            Console.WriteLine("No such element: " + e.Message);
        }
        catch (ElementNotInteractableException e)
        {
            Console.WriteLine("Element not interactable: " + e.Message);
        }

    }

    [TearDown]
    public void Teardown()
    {
        driver.Quit();
    }
}
