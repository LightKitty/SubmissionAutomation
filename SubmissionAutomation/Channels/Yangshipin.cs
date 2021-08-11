using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SubmissionAutomation.Extensions;
using SubmissionAutomation.Helpers;
using SubmissionAutomation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static OpenQA.Selenium.RelativeBy;

namespace SubmissionAutomation.Channels
{
    /// <summary>
    /// 央视频
    /// </summary>
    public class Yangshipin : Channel
    {

        public override string Url { get; } = "https://mp.yangshipin.cn/publish/uploadVideo"; //网址
        public override string Name { get; } = "央视频";
        private const int maxTagCount = 10; //最大标签个数
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
        public Yangshipin(ChannelInitParam initParam) : base(initParam)
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
            DateTime startTime = DateTime.Now;
            while(true)
            {
                try
                {
                    //查找上传按钮
                    var btns = wait.Until(x => x.FindElements(
                         By.ClassName("ant-btn")
                         ));

                    var btn = btns.FindElementByText("上传视频");
                    btn.Click();

                    Thread.Sleep(1000);

                    if (!OpenFileDialog.SelectFileAndOpen(path)) return false;

                    Thread.Sleep(1000);

                    return true;
                }
                catch
                {
                    if ((DateTime.Now - startTime).TotalMilliseconds > 10000)
                        throw;

                    Thread.Sleep(500);
                }
            }
        }

        /// <summary>
        /// 设置封面
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal override bool SetCover(string path)
        {
            IWebElement coverElement = wait.Until(wb => wb.FindElement(
                By.CssSelector("#Content > div > div.HomePage-container > div > div.SingleUpload > div.EditVideo > div.EditVideo-videoCover.use-video-auto-cover > div.cover-content > div > form > input[type=file]")
                )); //获取图片上传控件
            coverElement.SendKeys(path); //设置上传值
            Thread.Sleep(500);
            wait.Until(wb => wb.FindElement(
                By.CssSelector("body > div:nth-child(15) > div.shark-Modal-wrapper.shark-Modal-wrapper--center.VideoDialog > div > div > div > div.shark-Modal-body > div > div.upload-cover-mod-footer > button:nth-child(2)")
                )).Click(); //点击确定

            //var inputs = wait.Until(wb => wb.FindElements(
            //    By.TagName("input")
            //    ));

            //IWebElement inputTitle = inputs.First(x => x.GetAttribute("placeholder").Contains("标题"));

            return true;
        }

        /// <summary>
        /// 写标题
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        internal override bool WriteTitle(string title)
        {
            IWebElement titleInput = wait.Until(wb => wb.FindElement(
                By.Id("title")
                ));
            titleInput.Clear();
            titleInput.SendKeys(title);

            return true;
        }

        /// <summary>
        /// 写描述
        /// </summary>
        /// <param name="introduction"></param>
        /// <returns></returns>
        internal override bool WriteIntroduction(string introduction)
        {
            //简介
            var textareas = wait.Until(wb => wb.FindElements(
                By.TagName("textarea")
                ));

            IWebElement textarea = Wait.Until(textareas, x => x.FindElementByAttribute("placeholder", "请输入视频简介"));
            textarea.Clear();
            textarea.SendKeys(introduction);

            return true;
        }

        /// <summary>
        /// 设置标签
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        internal override bool SetTags(string[] tags)
        {
            var inputs = wait.Until(wb => wb.FindElements(
                By.TagName("label")
                ));

            IWebElement tempElement = Wait.Until(inputs, x => x.FindElementByAttribute("for", "tags"));

            IWebElement tagInput = Driver.FindElement(
                WithTagName("input")
                .RightOf(tempElement)
                );

            IEnumerable<string> _tags = tags.Take(maxTagCount);
            foreach (string tag in _tags)
            {
                tagInput.SendKeys(tag + Keys.Enter);
                Thread.Sleep(200);
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
                string[] classes = name.Split(' ');
                if (classes.Length == 2)
                {
                    IWebElement span = wait.Until(wb => wb.FindElement(
                        By.ClassName("ant-cascader-picker-label")
                        ));
                    span.Click();
                    Thread.Sleep(100);
                    var items = wait.Until(wb => wb.FindElements(
                    By.ClassName("ant-cascader-menu-item")
                    ));
                    var item = items.FindElementByText(classes[0]);
                    item.Click();
                    Thread.Sleep(100);
                    items = wait.Until(wb => wb.FindElements(
                    By.ClassName("ant-cascader-menu-item")
                    ));
                    item = items.FindElementByTextStart(classes[1]);
                    item.Click();
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 声明原创
        /// </summary>
        /// <returns></returns>
        internal override bool OriginalStatement(string typeName)
        {
            return true;
        }
    }
}
