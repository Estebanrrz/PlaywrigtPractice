using Microsoft.Playwright;

namespace SauceDemo.Pages
{
    public class LoginPage
    {
        private IPage _page;
        public LoginPage(IPage page)
        {
            _page = page;
        }
        private const string USER_NAME_LOCATOR = "#user-name";
        private const string PASSWORD_LOCATOR = "#password";
        private const string BTN_LOGIN_LOCATOR = "#login-button";

        public async Task Login(string userName, string password)
        {
            await _page.FillAsync(USER_NAME_LOCATOR, userName);
            await _page.FillAsync(PASSWORD_LOCATOR, password);
            await _page.ClickAsync(BTN_LOGIN_LOCATOR);
        }

    }
}
