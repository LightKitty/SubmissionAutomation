using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SubmissionAutomation.Channels;
using SubmissionAutomation.Consts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmissionAutomation.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebElementExtension
    {
        /// <summary>
        /// 清除value
        /// </summary>
        /// <param name="webElement"></param>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static IWebElement ClearValue(this IWebElement webElement)
        {
            return SetValue(webElement, "");
        }

        /// <summary>
        /// 设置value
        /// </summary>
        /// <param name="webElement"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IWebElement SetValue(this IWebElement webElement, string value)
        {
            string js = $"arguments[0].value = '{value}'";
            Context.Driver.ExecuteScript(js, webElement);
            return webElement;
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="webElement"></param>
        /// <param name="driver"></param>
        /// <param name="attributeName"></param>
        /// <param name="attributeValue"></param>
        /// <returns></returns>
        public static IWebElement SetAttribute(this IWebElement webElement, ChromeDriver driver, string attributeName, string attributeValue)
        {
            string js = $"arguments[0].setAttribute('{attributeName}', '{attributeValue}');";
            driver.ExecuteScript(js, webElement);
            return webElement;
        }

        /// <summary>
        /// 滚动至元素可见
        /// </summary>
        /// <param name="webElement"></param>
        /// <param name="driver"></param>
        public static IWebElement ScrollIntoView(this IWebElement webElement, ChromeDriver driver)
        {
            driver.ExecuteScript("arguments[0].scrollIntoView();", webElement);
            return webElement;
        }

        /// <summary>
        /// 获取父节点
        /// </summary>
        /// <param name="webElement"></param>
        /// <returns></returns>
        public static IWebElement GetParent(this IWebElement webElement)
        {
            return webElement.FindElement(By.XPath("./.."));
        }

        /// <summary>
        /// 获取所有子节点
        /// </summary>
        /// <param name="webElement"></param>
        /// <returns></returns>
        public static ReadOnlyCollection<IWebElement> GetChildern(this IWebElement webElement)
        {
            return webElement.FindElements(By.XPath("./*"));
        }

        /// <summary>
        /// 获取前面的兄弟节点
        /// </summary>
        /// <param name="webElement"></param>
        /// <returns></returns>
        public static ReadOnlyCollection<IWebElement> GetPrecedingSibling(this IWebElement webElement)
        {
            return webElement.FindElements(By.XPath("./preceding-sibling::*"));
        }

        /// <summary>
        /// 获取后面的兄弟节点
        /// </summary>
        /// <param name="webElement"></param>
        /// <returns></returns>
        public static ReadOnlyCollection<IWebElement> GetFollowingSibling(this IWebElement webElement)
        {
            return webElement.FindElements(By.XPath("./following-sibling::*"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="attributeName"></param>
        /// <param name="attributeValue"></param>
        /// <returns></returns>
        public static IWebElement FindElementByAttribute(this ReadOnlyCollection<IWebElement> elements, string attributeName, string attributeValue)
        {
            //遍历
            foreach (var element in elements)
            {
                string attribute = element.GetAttribute(attributeName);
                if (attribute == attributeValue)
                {
                    return element;
                }
            }

            throw new NoSuchElementException($"No such element with attribute name '{attributeName}' and value '{attributeValue}'");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public static IWebElement FindElementByTagName(this ReadOnlyCollection<IWebElement> elements, string tagName)
        {
            //遍历
            foreach (var element in elements)
            {
                if (element.TagName == tagName)
                {
                    return element;
                }
            }

            throw new NoSuchElementException($"No such element with attribute name '{tagName}'");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public static List<IWebElement> FindElementsByTagName(this ReadOnlyCollection<IWebElement> elements, string tagName)
        {
            var result = new List<IWebElement>();
            //遍历
            foreach (var element in elements)
            {
                if (element.TagName == tagName)
                {
                    result.Add(element);
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IWebElement FindElementByText(this ReadOnlyCollection<IWebElement> elements, string text)
        {
            //遍历
            foreach (var element in elements)
            {
                if (element.Text == text)
                {
                    return element;
                }
            }

            throw new NoSuchElementException($"No such element with text '{text}'");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IWebElement FindElementByTextStart(this ReadOnlyCollection<IWebElement> elements, string text)
        {
            //遍历
            foreach (var element in elements)
            {
                if (element.Text.StartsWith(text))
                {
                    return element;
                }
            }

            throw new NoSuchElementException($"No such element with text '{text}'");
        }
    }
}
