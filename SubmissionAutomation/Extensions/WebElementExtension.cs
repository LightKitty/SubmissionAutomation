using OpenQA.Selenium;
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
