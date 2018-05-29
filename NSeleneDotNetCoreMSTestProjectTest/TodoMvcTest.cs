using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSelene;
using static NSelene.Selene;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace NSeleneDotNetCoreMSTestProjectTest
{

    [TestClass]
    public class TodoMvcTest
    {
        [TestInitialize]
        public void initDriver()
        {
            SetWebDriver(new ChromeDriver());
            Configuration.Timeout = 6;
        }

        [TestCleanup]
        public void disposeDriver()
        {
            GetWebDriver().Quit();
        }

        [TestMethod]
        public void FiltersTasks()
        {
            Open("https://todomvc4tasj.herokuapp.com/");

            WaitTo(Have.JSReturnedTrue(
                "return " +
                "$._data($('#new-todo').get(0), 'events').hasOwnProperty('keyup')&& " +
                "$._data($('#toggle-all').get(0), 'events').hasOwnProperty('change') && " +
                "$._data($('#clear-completed').get(0), 'events').hasOwnProperty('click')"));

            S("#new-todo").SetValue("a").PressEnter();
            S("#new-todo").SetValue("b").PressEnter();
            S("#new-todo").SetValue("c").PressEnter();
            SS("#todo-list>li").Should(Have.ExactTexts("a", "b", "c"));

            SS("#todo-list>li").FindBy(Have.ExactText("b")).Find(".toggle").Click();

            S(By.LinkText("Active")).Click();
            SS("#todo-list>li").FilterBy(Be.Visible).Should(Have.ExactTexts("a", "c"));

            S(By.LinkText("Completed")).Click();
            SS("#todo-list>li").FilterBy(Be.Visible).Should(Have.ExactTexts("b"));
        }
    }
}
