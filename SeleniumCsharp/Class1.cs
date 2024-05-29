using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Legacy;
using OpenQA.Selenium.Interactions;

namespace SeleniumCsharp
{
    public class Class1
    {
        private IWebDriver driver;
        private string baseUrl = "https://login.onefile.co.uk/login"; // Replace with your URL

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(baseUrl);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement button = wait.Until(ExpectedConditions
                .ElementExists(By.XPath("//app-landing/div[2]/button/span[2]")));
            button.Click();
        }

        [Test]
        public void CreateAndResetPassword()
        {
            Thread.Sleep(1000);
            string newUsername = "testuser" + new Random().Next(1000, 9999)+"@gmail.com";
            string password = "TestPassword123!";
            string newPassword = "NewTestPassword456!";

            // Create a new keychain account
            createNewKeychainAccount(newUsername, password);

            // Reset the password
            resetPassword(newUsername, password, newPassword);

            // Verify the password reset (by logging in with the new password)
            Login(newUsername, newPassword);
           /* WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement IsLoggedIn = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("logout-button")));
            ClassicAssert.IsTrue(IsLoggedIn , "User should be logged in successfully with the new password.");*/
        }

        public void createNewKeychainAccount(string newUsername, string password)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@id='fistName']"))).SendKeys("Neha");
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@id='lastName']"))).SendKeys("Test");


            IWebElement email1 = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@id='email']")));
            email1.SendKeys(newUsername);

            driver.FindElement(By.XPath("//input[@id='newPassword']")).SendKeys(password);
            driver.FindElement(By.XPath("//input[@id='confirmPassword']")).SendKeys(password);

            Thread.Sleep(1000);
            IWebElement checkbox = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='rapid-checkbox__checkbox']")));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", checkbox);
            Actions actions = new Actions(driver);
            actions.Click(checkbox).Perform();
            Thread.Sleep(1000);

            IWebElement register = driver.FindElement(By.XPath("//button[text()='Register']"));
            actions.Click(register).Perform();

            IWebElement backToLogin = driver.FindElement(By.XPath("//button[@class='standalone-page__back rapid-button rapid-button--full rapid-button--md']"));
            actions.Click(backToLogin).Perform();

            // Navigate to the sign-up page
            /*wait.Until(ExpectedConditions.ElementIsVisible(By.Id("signup-button"))).Click();

            // Fill in the sign-up form
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='email']"))).SendKeys(username);
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='password']"))).SendKeys(password);
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("confirm-password"))).SendKeys(password);
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("create-account-button"))).Click();*/
        }

        private void resetPassword(string username, string oldPassword, string newPassword)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Now try to login with the old password
            Login(username, oldPassword);

            // Navigate to account settings
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//span[@class='rapid-icon rapid-icon--settings']")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//span[text()='Keychain Settings']")).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@id='currentPassword']"))).SendKeys(oldPassword);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@id='newPassword']"))).SendKeys(newPassword);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@id='confirmPassword']"))).SendKeys(newPassword);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//button[contains(@class,'rapid-button--secondary rapid-button--md')]) [2]"))).Click();
            Thread.Sleep(1000);
        }

        private void Login(string username, string password)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@id='email']"))).SendKeys(username);
            Thread.Sleep(2000);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@id='password']"))).SendKeys(password);
            IWebElement login = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@type='submit']")));
            Actions actions = new Actions(driver);
            actions.Click(login).Perform();
        }

        [TearDown]
        public void Teardown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                driver = null;
            }
        }
    }
}
