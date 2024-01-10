using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using SauceDemo.Pages;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using static System.Net.WebRequestMethods;

namespace SauceDemo;

public class Tests : PageTest
{
    [SetUp]
    public async Task SetupAsync()
    {
        await Page.GotoAsync(url: "https://www.saucedemo.com/");
    }

    [Test]
    public async Task GivenUserExists_UserAddSomeElementsToTheCardAndCompleteTheOrder()
    {
        string item1Toadd = "Sauce Labs Backpack";
        string item2Toadd = "Sauce Labs Bike Light";
        string firstName = "John";
        string lastName = "Doe";
        string zipCode = "12345";

        LoginPage loginPage = new LoginPage(Page);
        await loginPage.Login("standard_user", "secret_sauce");

        await Expect(Page).ToHaveURLAsync(new Regex(".*inventory"));
        InventoryPage inventoryPage = new InventoryPage(Page);
        await inventoryPage.AddBackpackToTheCart();

        var itemPrice1 = await inventoryPage.GetItemPrice(item1Toadd);
        await inventoryPage.AddBikeLightToTheCart();

        var itemPrice2 = await inventoryPage.GetItemPrice(item2Toadd);

        var count = await inventoryPage.GetCartItemsCount();
       
        Assert.AreEqual(2, count);
        
        await inventoryPage.ClickOnCart();
        await Expect(Page).ToHaveURLAsync(new Regex(".*cart"));
      
        await inventoryPage.ClickOnCheckout();
         await Expect(Page).ToHaveURLAsync(new Regex(".*checkout-step-one"));

        await inventoryPage.FillFirstName(firstName);
        await inventoryPage.FillLastName(lastName);
        await inventoryPage.FillZipCode(zipCode);   
        await inventoryPage.ClickOnContinue();
      
        await Expect(Page).ToHaveURLAsync(new Regex(".*checkout-step-two"));
      
        var itemsTotal = await inventoryPage.GetItemsTotal();
        Assert.AreEqual(float.Parse(itemsTotal.Trim().Replace("Item total: $", "")), float.Parse(itemPrice1.Replace("$", "")) + float.Parse(itemPrice2.Replace("$", "")), "Item Total is not the expected");
      
        await inventoryPage.ClickOnFinish();
        await Expect(Page).ToHaveURLAsync(new Regex(".*checkout-complete"));

        var thankYouMessage = await inventoryPage.GetThankYouMessage();
       
        Assert.AreEqual("Thank you for your order!", thankYouMessage);
    }
}