using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo.Pages
{
   public class InventoryPage
    {
        private IPage _page;
        public InventoryPage(IPage page)
        {
            _page = page;
        }
        private const string PRICE_ITEM_LOCATOR_PARTIAL = "//div[contains(@class,'inventory_item_name')][contains(text(),'{0}')]/ancestor::div[@class='inventory_item']//div[@class='inventory_item_price']";
        private const string BTN_ADD_TO_CART_BACKPACK = "#add-to-cart-sauce-labs-backpack";
        private const string BTN_CART_LOCATOR = "#shopping_cart_container > a";
        private const string BTN_CHECKOUT_LOCATOR = "#checkout";
        private const string BTN_CONTINUE_LOCATOR = "#continue";
        private const string BTN_FINISH_LOCATOR = "#finish";
        private const string BTN_ADD_TO_CART_BIKE_LIGHT = "#add-to-cart-sauce-labs-bike-light";
        private const string CART_COUNT_LOCATOR = "#shopping_cart_container > a > span";
        private const string FIRST_NAME_LOCATOR = "#first-name";
        private const string LAST_NAME_LOCATOR = "#last-name";
        private const string ZIP_LOCATOR = "#postal-code";
        private const string ITEMS_TOTAL_LOCATR = "//div[@class='summary_subtotal_label']";
        private const string THANK_YOU_LOCATOR = "//h2[@class='complete-header']";



        public async Task AddBackpackToTheCart()
        {
                await _page.ClickAsync(BTN_ADD_TO_CART_BACKPACK);
        }
        public async Task AddBikeLightToTheCart()
        {
            await _page.ClickAsync(BTN_ADD_TO_CART_BIKE_LIGHT);
        }
        public async Task ClickOnCart()
        {
            await _page.ClickAsync(BTN_CART_LOCATOR);
        }
        public async Task ClickOnCheckout()
        {
            await _page.ClickAsync(BTN_CHECKOUT_LOCATOR);
        }
        public async Task ClickOnContinue()
        {
            await _page.ClickAsync(BTN_CONTINUE_LOCATOR);
        }
        public async Task ClickOnFinish()
        {
            await _page.ClickAsync(BTN_FINISH_LOCATOR);
        }
        public async Task<string> GetItemPrice(string itemName)
        {
            return await _page.TextContentAsync(string.Format(PRICE_ITEM_LOCATOR_PARTIAL, itemName));
        }
        public async Task<int> GetCartItemsCount()
        {
            var cartCount = await _page.TextContentAsync(CART_COUNT_LOCATOR);
            return int.Parse(cartCount);
        }

        public async Task FillFirstName(string firstName)
        {
            await _page.FillAsync(FIRST_NAME_LOCATOR, firstName);
        }
        public async Task FillLastName(string lastName)
        {
            await _page.FillAsync(LAST_NAME_LOCATOR, lastName);
        }
        public async Task FillZipCode(string zip)
        {
            await _page.FillAsync(ZIP_LOCATOR, zip);
        }
        public async Task<string> GetItemsTotal()
        {
            return await _page.TextContentAsync(ITEMS_TOTAL_LOCATR);
        }
        public async Task<string> GetThankYouMessage()
        {
            return await _page.TextContentAsync(THANK_YOU_LOCATOR);
        }
            
    }
}
