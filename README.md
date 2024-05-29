# Test Automation Exercise - Selenium with Csharp

## How to test
1. Open Viusal Studio 2022
2. Open the zip file from Open project/solution
3. Add plugins that are missing
4. Save the project/solution
5. Navigate to Tests --> Select Run All Tests
   (Please refer Documentation.docx attached above, for screenshot)

## Setup
* IDE: Visual Studio 2022
* Browser: chrome  125.0.6422.113 version
* Programming Language : Csharp (C#)
* Packages Used : Nunit 4.1.0, Selenium.WebDriver 4.21.0, xunit.assert 2.8.1

## Test Cases
Test Case 1 (TC1):
Create a new keychain account.
Log in successfully with this keychain account.

Test Case 2 (TC2):
Create a new keychain account.
Reset the password of the created account.

## Implementation
The Hybrid-driven framework, which incorporates 90% of the Modular-driven framework principles, is designed to support methods such as 

Test Case 1: Register class

Methods used:
* Setup()-Launch the browser and navigate to the Login page
* verifyRegister()- fill first name,lastname, random email, random password, confirm password, select the checkbox for terms and conditions, and Register
* Teardown()- to completely close the browser

Test Case 2: ResetPassword class
 
Methods used:
* Setup()- Launch the browser, navigate to the Login page, and click on Create new keychain
* CreateAndResetPassword()- To verify login  and change the password
* createNewKeychainAccount()- To create and register the account
* Teardown()- to completely close the browser
* Login()- enter email and password and Login
* resetPassword()- to reset the password 

Utility class- used to create Random email ID and  generate Time to append for first and Last Names and email and passwords for uniqueness.


## Assumptions 

1. Exception are used to handle any runtime error. 


