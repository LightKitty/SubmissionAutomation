using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmissionAutomation.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ISearchContextExtension
    {
        /// <summary>
        /// 依据标签名和属性值查询
        /// </summary>
        /// <param name="context"></param>
        /// <param name="tagName"></param>
        /// <param name="attributeName"></param>
        /// <param name="attributeValue"></param>
        /// <param name="attributeFuzzySearch">属性值是否模糊查询</param>
        /// <returns></returns>
        public static IWebElement FindElementByTagAndAttribute(this ISearchContext context, string tagName, string attributeName, string attributeValue, bool attributeFuzzySearch = false)
        {
            return context.FindElements(
                    By.TagName(tagName)
                )
                .FirstOrDefault(x =>
                    attributeFuzzySearch
                        ? x.GetAttribute(attributeName).Contains(attributeValue)
                        : x.GetAttribute(attributeName) == attributeValue
                );
        }

        /// <summary>
        /// 依据标签名和文本查询
        /// </summary>
        /// <param name="context"></param>
        /// <param name="tagName"></param>
        /// <param name="text"></param>
        /// <param name="textFuzzySearch">文本是否模糊查询</param>
        /// <returns></returns>
        public static IWebElement FindElementByTagAndText(this ISearchContext context, string tagName, string text, bool textFuzzySearch = false)
        {
            return context.FindElements(
                    By.TagName(tagName)
                )
                .FirstOrDefault(x =>
                    textFuzzySearch
                        ? x.Text.Contains(text)
                        : x.Text == text
                );
        }
    }
}
