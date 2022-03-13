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
    /// 哔哩哔哩
    /// </summary>
    public class Bilibili : Channel
    {

        public override string Url { get; } = "https://member.bilibili.com/platform/upload/video/frame"; //网址
        public override string Name { get; } = "哔哩哔哩";
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
        /// <param name="classifyName"></param>
        /// <param name="originalName"></param>
        /// <param name="handelException"></param>
        public Bilibili(ChannelInitParam initParam) : base(initParam)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool Operate()
        {
            return base.Operate(new Func<bool>[] { WaitIframeLoaded }, new Func<bool>[] { Dongtai });
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
        /// 等待上传iframe加载
        /// </summary>
        /// <returns></returns>
        internal bool WaitIframeLoaded()
        {
            wait.Until(wb => wb.FindElements(By.TagName("iframe")).Count() > 0);
            Driver.SwitchTo().Frame(1);

            Thread.Sleep(500); // 等待系统组装

            return true;
        }

        /// <summary>
        /// 上传视频
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal override bool UploadVideo(string path)
        {
            IWebElement uploadButton = wait.Until(wb => wb.FindElement(By.ClassName("upload-btn"))); //查找上传按钮
            uploadButton.Click(); //点击上传按钮

            Thread.Sleep(1000); //等待系统弹窗

            if (!Helpers.OpenFileDialog.SelectFileAndOpen(path)) return false;

            Thread.Sleep(1000); //等待

            return true;
        }

        /// <summary>
        /// 设置封面
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal override bool SetCover(string path)
        {
            //获取图片上传控件
            IWebElement coverElement = wait.Until(wb => wb.FindElements(
                By.TagName("input")
                ).First(x=>x.GetAttribute("accept").Contains("image/jpeg"))); 
            coverElement.SendKeys(path); //设置上传值

            Thread.Sleep(500);

            //点击确定
            var ele = wait.Until(wb => wb.FindElementByTagAndText("span", "确认"));
            ele.Click();

            Thread.Sleep(500);

            return true;
        }

        /// <summary>
        /// 写标题
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        internal override bool WriteTitle(string title)
        {
            //IWebElement titleElement = wait.Until(wb => wb.FindElement(
            //    By.CssSelector("#app > div.upload-v2-container > div.upload-v2-step2-container > div.file-content-v2-container > div.normal-v2-container > div.content-title-v2-container > div.content-title-v2-input-wrp > div > div > input")
            //    ));
            //Thread.Sleep(100);
            //titleElement.Clear();
            //titleElement.SendKeys(title);

            Thread.Sleep(100);

            var inputs = wait.Until(wb => wb.FindElements(
                By.TagName("input")
                ));

            IWebElement titleElement = inputs.FirstOrDefault(x => x.GetAttribute("placeholder").Contains("标题"));

            Thread.Sleep(100);
            //titleElement.Clear();
            titleElement.Click();
            titleElement.ClearValue();
            Thread.Sleep(100);
            titleElement.SendKeys(title);

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
            var ele = wait.Until(wb => wb.FindElementByClassAndAttribute("ql-editor", "data-placeholder", "填写更全面的相关信息", true));
            ele.Click();
            ele.SendKeys(introduction);

            return true;
        }

        /// <summary>
        /// 设置标签
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        internal override bool SetTags(string[] tags)
        {
            IWebElement tagElement = wait.Until(wb => wb.FindElements(
                By.TagName("input")
                ).FirstOrDefault(x=>x.GetAttribute("placeholder") == "按回车键Enter创建标签")); //标签

            IEnumerable<string> _tags = tags.Take(maxTagCount);
            foreach (string tag in _tags)
            {
                tagElement.SendKeys(tag+ Keys.Enter);
                Thread.Sleep(1000);
            }

            return true;
        }

        /// <summary>
        /// 设置分类
        /// </summary>
        /// <returns></returns>
        internal override bool SetClassify(string name)
        {
            return true;
        }

        /// <summary>
        /// 声明原创
        /// </summary>
        /// <returns></returns>
        internal override bool OriginalStatement(string typeName)
        {
            if(typeName != true.ToString())
            {
                //判断是否点击更多选项
                IWebElement moreSetting = Wait.Until(Driver, x => x.FindElementByClassAndText("title", "更多设置", true));
                IWebElement flag = Wait.Until(moreSetting, x => x.GetFollowingSibling(), y => y.Count > 0)?.FirstOrDefault();
                if (!string.IsNullOrEmpty(flag.GetAttribute("style")))
                { //需点击更多选项
                    moreSetting.GetChildern().First().Click();
                }


                IWebElement webElement = wait.Until(wb => wb.FindElementByTagAndText("span", "未经作者授权 禁止转载"));

                //IWebElement tagInput = Driver.FindElement(
                //    WithTagName("i")
                //    .LeftOf(webElement)
                //    );

                webElement.Click();
            }

            return true;
        }

        internal bool Dongtai()
        {
            IWebElement webElement = wait.Until(wb => wb.FindElements(
                By.ClassName("ql-editor")
                ).FirstOrDefault(x => x.GetAttribute("data-placeholder").Contains("有趣的动态描述")));

            webElement.SendKeys(Title);

            return true;
        }
    }
}
