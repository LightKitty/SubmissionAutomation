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
    public static class WebElement
    {
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
    }
}
