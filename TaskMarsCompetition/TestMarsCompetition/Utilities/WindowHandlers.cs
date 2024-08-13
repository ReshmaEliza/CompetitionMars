using OpenQA.Selenium;
using TestMarsCompetition.Utilities;

namespace TestMarsCompetition.Utilities
{
    class WindowHandlers: WebdriverManager
    {

        
      
        public static void ScrollToView(IWebElement value)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", value);
        }
    }
}
