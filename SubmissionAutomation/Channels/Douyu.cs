using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SubmissionAutomation.Extensions;
using SubmissionAutomation.Helpers;
using SubmissionAutomation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SubmissionAutomation.Channels
{
    /// <summary>
    /// 斗鱼
    /// </summary>
    public class Douyu : Channel
    {

        public override string Url { get; } = "https://v.douyu.com/member/mycreate/center#/uploadVideo"; //网址
        public override string Name { get; } = "斗鱼";
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
        public Douyu(ChannelInitParam initParam) : base(initParam)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool Operate()
        {
            return base.Operate(new Func<bool>[] { Login }, null);
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
        /// 登录
        /// </summary>
        /// <returns></returns>
        public bool Login()
        {
            var tag = Wait.Until(Driver, wb => wb.FindElementByTagAndText("div", "安全登录", true), 3000, 500, false);
            if (tag != null)
            {
                SoundHelper.Remind();
                Wait.Until(Driver, x => x.FindElement(By.LinkText("上传视频")), 600000, 500).Click();
            }

            return true;
        }

        /// <summary>
        /// 上传视频
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal override bool UploadVideo(string path)
        {
            //查找上传按钮
            IWebElement uploadButton = wait.Until(wb => wb.FindElement(
                By.CssSelector("#Content > div > div.HomePage-container > div > div.UploadArea > div.UploadArea-DragArea > div.UploadArea-bigUploadBtn")
                ));
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
            IWebElement coverElement = wait.Until(wb => wb.FindElements(
                By.TagName("input")
                ).FirstOrDefault(x=>x.GetAttribute("accept").Contains(".jpg"))); //获取图片上传控件
            coverElement.SendKeys(path); //设置上传值

            Thread.Sleep(500);

            //点击确定
            IWebElement okBtn = wait.Until(wb => wb.FindElementByTagAndText("button", "确定", true));
            okBtn.Click();

            wait.Until(ExpectedConditions.AlertIsPresent());
            //关闭弹窗 https://stackoverflow.com/questions/41758813/selenium-close-a-window-with-a-confirmation-alert-c-sharp
            var alert = Driver.SwitchTo().Alert();
            alert.Accept(); // or alert.Dismiss()

            return true;
        }

        /// <summary>
        /// 写标题
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        internal override bool WriteTitle(string title)
        {
            IWebElement titleElement = wait.Until(wb => 
                wb.FindElementByTagAndAttribute("input", "placeholder", "请输入标题")
            );
            titleElement.Clear();
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
            wait.Until(wb => wb.FindElement(
                By.ClassName("detail-textarea")
                ))
                .SendKeys(introduction);

            return true;
        }

        /// <summary>
        /// 设置标签
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        internal override bool SetTags(string[] tags)
        {
            IWebElement tagElement = wait.Until(wb => wb.FindElementByTagAndAttribute("input", "placeholder", "标签", true)); //标签

            IEnumerable<string> _tags = tags.Take(maxTagCount);
            foreach (string tag in _tags)
            {
                tagElement.SendKeys(tag + Keys.Enter);
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
            //分类按钮
            IWebElement classifyButton = wait.Until(wb => wb.FindElement(
                By.ClassName("cate-info")
                ));
            classifyButton.Click();

            //知识分类
            IWebElement zhishiButton = wait.Until(wb => wb.FindElementByClassAndText("cate-item-first", name, true)); 
            zhishiButton.Click();

            ////输入搜索文本
            //IWebElement searchButton = wait.Until(wb => wb.FindElement(
            //    By.CssSelector("#Content > div > div.HomePage-container > div > div.SingleUpload > div.EditVideo > div.EditVideo-videoCate > div.cate-selected > div > div.CateSelect > div.cate-search > div.search-wrap > div > input")
            //    )); 
            //searchButton.Clear();
            //searchButton.SendKeys(name);

            //Thread.Sleep(100); //等待搜索结果

            //IWebElement resultItem = wait.Until(wb => wb.FindElement(
            //    By.CssSelector("#Content > div > div.HomePage-container > div > div.SingleUpload > div.EditVideo > div.EditVideo-videoCate > div.cate-selected > div > div.CateSelect > div.cate-search > div.cate-scrollbar-second > div:nth-child(1) > div > p")
            //    ));

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
