using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SubmissionAutomation.Extensions;
using SubmissionAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SubmissionAutomation.Channels
{
    /// <summary>
    /// 快手
    /// </summary>
    public class Kuaishou : Channel
    {

        private const string url = "https://cp.kuaishou.com/article/publish/video"; //网址
        private const int maxTagCount = 3; //最大标签个数
        private const int operateInterval = 100; //默认操作间隔

        private WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10)); //等待器

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="videoPath"></param>
        /// <param name="coverPath"></param>
        /// <param name="tags"></param>
        /// <param name="title"></param>
        /// <param name="introduction"></param>
        /// <param name="classifyName"></param>
        /// <param name="originalName"></param>
        public Kuaishou(string videoPath, string coverPath, string[] tags, string title, string introduction, string classifyName, string originalName) 
            : base(url, videoPath, coverPath, tags, title, introduction, classifyName, originalName, operateInterval)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool Operate()
        {
            return base.Operate(null, null);
        }

        /// <summary>
        /// 跳转到网址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        internal override bool GoTo(string url)
        {
            Driver.SwitchTo().NewWindow(WindowType.Tab);
            Driver.Navigate().GoToUrl(url);

            return true;
        }

        /// <summary>
        /// 上传视频
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal override bool UploadVideo(string path)
        {
            IWebElement videoInput = wait.Until(x => x.FindElement(
                //By.ClassName("el-upload-dragger")
                By.Name("file")
                ));

            videoInput.SendKeys(path); //设置上传值

            return true;
        }

        /// <summary>
        /// 写标题
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        internal override bool WriteTitle(string title)
        {
            return true;
        }

        /// <summary>
        /// 写简介
        /// </summary>
        /// <param name="introduction"></param>
        /// <returns></returns>
        internal override bool WriteIntroduction(string introduction)
        {
            IWebElement published_description = wait.Until(wb => wb.FindElement(
                By.ClassName("published-description")
                ));

            published_description.SendKeys(introduction);

            return true;
        }

        /// <summary>
        /// 设置标签
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        internal override bool SetTags(string[] tags)
        {
            if(tags?.Count()>0)
            {
                IWebElement published_description = wait.Until(wb => wb.FindElement(
                 By.ClassName("published-description")
                 ));

                var _tag = tags.Take(maxTagCount);
                foreach(string tag in _tag)
                {
                    published_description.SendKeys("#" + tag);
                }
            }

            return true;
        }

        /// <summary>
        /// 设置分类
        /// </summary>
        /// <returns></returns>
        internal override bool SetClassify(string name)
        {
            if(!string.IsNullOrEmpty(name))
            {
                string[] names = name.Split(' ');
                if (names.Length >= 2)
                {
                    IWebElement el_input__inner = wait.Until(x => x.FindElement(
                    By.ClassName("el-input__inner")
                    ));

                    el_input__inner.Click();

                    Thread.Sleep(100);

                    var selectItems = wait.Until(x => x.FindElements(
                        By.ClassName("el-select-dropdown__item")
                        ));

                    var item1 = selectItems.FindElementBText(names[0]);
                    item1.Click();

                    Thread.Sleep(100);

                    var inputs = wait.Until(x => x.FindElements(
                    By.ClassName("el-input__inner")
                    ));

                    inputs[1].Click();
                    Thread.Sleep(100);

                    selectItems = wait.Until(x => x.FindElements(
                        By.ClassName("el-select-dropdown__item")
                        ));

                    var item2 = selectItems.FindElementBText(names[1]);
                    item2.Click();
                }
            }

            return true;
        }

        /// <summary>
        /// 设置封面
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal override bool SetCover(string path)
        {
            return true;
        }

        /// <summary>
        /// 原创声明
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        internal override bool OriginalStatement(string typeName)
        {
            return true;
        }
    }
}
