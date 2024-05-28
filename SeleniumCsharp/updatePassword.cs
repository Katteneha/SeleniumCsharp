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


public class updatePassword
{
    public IWebDriver driver = new ChromeDriver();
    private string username = "NehaTest112@gmail.com";
    private string originalPassword = "OriginalPassword123!";
    private string updatedPassword = "UpdatedPassword456!";

    [SetUp]
    public void Setup()
    {
        //go to site
         driver.Navigate().GoToUrl("https://login.onefile.co.uk/login");
        driver.Manage().Window.Maximize();

    }
    
    [Test]
    public void changePassword()
    {
        // Log in to the account
        verifyLoginUI(username, originalPassword);

        // Change the password
        ChangePassword(originalPassword, updatedPassword);
                   
    }

    [TearDown]
    public void Teardown()
    {
        Thread.Sleep(2000);
        // Now try to login with the updated password to revert it back to the original
        verifyLoginUI(username, updatedPassword);

        // Revert password changed
        ChangePassword(updatedPassword, originalPassword);

        

        if (driver != null)
        {
            //driver.Manage().Cookies.DeleteAllCookies();
            driver.Quit();
            driver.Dispose();
            driver = null;
        }



    }

    private void verifyLoginUI(string username, string password)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@id='email']"))).SendKeys(username);
        Thread.Sleep(2000);
        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@id='password']"))).SendKeys(password);
        IWebElement login = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@type='submit']")));
        Actions actions = new Actions(driver);
        actions.Click(login).Perform();
    }

    private void ChangePassword(string currentPassword, string newPassword)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        //go to Settings
        Thread.Sleep(2000);
        driver.FindElement(By.XPath("//span[@class='rapid-icon rapid-icon--settings']")).Click();
        Thread.Sleep(2000);
        driver.FindElement(By.XPath("//span[text()='Keychain Settings']")).Click();

        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@id='currentPassword']"))).SendKeys(currentPassword);
        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@id='newPassword']"))).SendKeys(newPassword);
        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@id='confirmPassword']"))).SendKeys(newPassword);
        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//button[contains(@class,'rapid-button--secondary rapid-button--md')]) [2]"))).Click();
        
    }

    
}
