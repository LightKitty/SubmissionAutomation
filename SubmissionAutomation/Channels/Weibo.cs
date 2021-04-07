using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
    /// 百度（百家号）
    /// </summary>
    public class Weibo : Channel
    {

        private const string url = "https://weibo.com/leibizhenqi/home"; //网址
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
        public Weibo(string videoPath, string coverPath, string[] tags, string title, string introduction, string classifyName, string originalName) 
            : base(url, videoPath, coverPath, tags, title, introduction, classifyName, originalName, operateInterval)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool Operate()
        {
            return base.Operate(null, new Func<bool>[] { CompleteBtnClick });
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
            //发布区域
            IWebElement publishertop = wait.Until(wb => wb.FindElement(
                By.Id("v6_pl_content_publishertop")
                ));

            IWebElement videoInput = publishertop.FindElement(
                By.Name("video")
                );

            videoInput.SendKeys(path); //设置上传值

            Thread.Sleep(100); //等待系统弹窗

            //if (!Helpers.OpenFileDialog.SelectFileAndOpen(path)) return false;

            //Thread.Sleep(1000); //等待

            return true;
        }

        /// <summary>
        /// 写标题
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        internal override bool WriteTitle(string title)
        {
            //发布区域
            IWebElement publishertop = wait.Until(wb => wb.FindElement(
                By.Id("v6_pl_content_publishertop")
                ));

            var inputs = publishertop.FindElements(
                By.TagName("input")
                );

            //遍历input
            foreach(var input in inputs)
            {
                string action_type = input.GetAttribute("action-type");
                if(action_type== "inputTitle")
                { //找到标题input
                    input.Clear();
                    input.SendKeys(title);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 写简介
        /// </summary>
        /// <param name="introduction"></param>
        /// <returns></returns>
        internal override bool WriteIntroduction(string introduction)
        {
            //简介区域
            IWebElement textarea = wait.Until(wb => wb.FindElement(
                By.CssSelector("#v6_pl_content_publishertop > div > div.input > textarea")
                ));
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
            //简介区域
            IWebElement textarea = wait.Until(wb => wb.FindElement(
                By.CssSelector("#v6_pl_content_publishertop > div > div.input > textarea")
                ));

            textarea.SendKeys("#微博公开课#");

            IEnumerable<string> _tags = tags.Take(maxTagCount - 1);
            foreach (string tag in _tags)
            {
                textarea.SendKeys($"#{tag}#");
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
        /// 设置封面
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal override bool SetCover(string path)
        {
            //发布区域
            IWebElement publishertop = wait.Until(wb => wb.FindElement(
                By.Id("v6_pl_content_publishertop")
                ));

            var upbtn_v3_img = Wait.Until(publishertop, x => x.FindElement(
                 By.ClassName("upbtn_v3_img")
                 ));

            upbtn_v3_img.Click();

            var upbtn = publishertop.FindElement(
                By.ClassName("upbtn")
                );

            upbtn.Click();

            Thread.Sleep(1000); //等待上传
            if (!Helpers.OpenFileDialog.SelectFileAndOpen(path)) return false;
            Thread.Sleep(1000); //等待

            var aTags = publishertop.FindElements(
                By.TagName("a")
                );

            //遍历
            foreach (var aTag in aTags)
            {
                string action_type = aTag.GetAttribute("action-type");
                if (action_type == "setUploadCover")
                { //找到确定按钮
                    aTag.Click();
                    break;
                }
            }

            return true;
        }

        /// <summary>
        /// 原创声明
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        internal override bool OriginalStatement(string typeName)
        {
            //if (string.IsNullOrEmpty(typeName)) return true;

            //var input = wait.Until(wb => wb.FindElement(
            //    By.Id("spe1")
            //    ));
            //input.Click();

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CompleteBtnClick()
        {
            //发布区域
            IWebElement publishertop = wait.Until(wb => wb.FindElement(
                By.Id("v6_pl_content_publishertop")
                ));

            var aTags = Wait.Until(publishertop, x => x.FindElements(
                By.TagName("a")
                ));

            //遍历
            foreach (var aTag in aTags)
            {
                string node_type = aTag.GetAttribute("node-type");
                if (node_type == "completeBtn")
                { //找到按钮
                    aTag.Click();

                    return true;
                }
            }

            return false;
        }
    }
}
